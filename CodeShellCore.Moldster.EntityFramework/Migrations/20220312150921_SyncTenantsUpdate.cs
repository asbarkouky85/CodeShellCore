using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeShellCore.Moldster.Migrations
{
    public partial class SyncTenantsUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
            migrationBuilder.Sql(@"

-- =============================================
-- Author:		Asgatech
-- Version:		1.0.0
-- Update Date:	17/02/2022
-- =============================================
ALTER PROCEDURE [dbo].[SyncTenants]
	@sourceTenant bigint,
	@targetTenant bigint,
	@res varchar(max) = null OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;

	DECLARE @count int=0;
	DECLARE @message nvarchar(max);
	DECLARE @result TABLE(
		SourceTenant varchar(150),
		TargetTenant varchar(150),
		AddedPages int,
		UpdatedPages int,
		AddedParameters int,
		UpdatedParameters int,
		AddedRoutes int,
		UpdatedRoutes int,
		AddedPageControls int,
		UpdatedPageControls int,
		NavigationPages int
	);

	INSERT @result 
		select
			(SELECT Code FROM Tenants WHERE Id=@sourceTenant), 
			(SELECT Code FROM Tenants WHERE Id=@targetTenant), 
			0,0,0,0,0,0,0,0,0;

-----------------------------------------------------------------------------------------------
	begin transaction;
	UPDATE targetPage
	SET
		Apps=tx.Apps,
		ViewParams=tx.ViewParams,
		Layout=tx.Layout,
		DomainId=tx.DomainId,
		ViewPath=tx.ViewPath,
		PrivilegeType=tx.PrivilegeType,
		ResourceActionId=tx.ResourceActionId,
		SpecialPermission=tx.SpecialPermission,
		SourceCollectionId=tx.SourceCollectionId,
		RouteParameters=tx.RouteParameters,
		DefaultAccessibility=tx.DefaultAccessibility,
		PageCategoryId=tx.PageCategoryId,
		ResourceId=tx.ResourceId,
		CanEmbed=tx.CanEmbed,
		HasRoute=tx.HasRoute,
		Name=tx.Name
	FROM Pages targetPage
		join (
			SELECT *
			FROM Pages 
			WHERE TenantId=@sourceTenant
		) tx ON tx.Id=targetPage.ParentId
	WHERE 
		targetPage.TenantId=@targetTenant;

	UPDATE @result set UpdatedPages=@@ROWCOUNT;
	print 'Update pages';
	commit

-----------------------------------------------------------------------------------------------
	begin transaction;
	INSERT INTO dbo.Pages
	([Id]
		,[Name]
		,[ViewPath]
		,[ViewParams]
		,[ResourceId]
		,[Layout]
		,[PrivilegeType]
		,[ResourceActionId]
		,[SpecialPermission]
		,[SourceCollectionId]
		,[DefaultAccessibility]
		,[PageCategoryId]
		,[RouteParameters]
		,[TenantId]
		,[DomainId]
		,[Apps]
		,[HasRoute]
		,[CanEmbed]
		,[CreatedOn]
		,[UpdatedOn]
		,[CreatedBy]
		,[UpdatedBy]
		,ParentId)
	SELECT 
		dbo.GenerateId(GETDATE(),ROW_NUMBER() OVER (ORDER BY Ps.Id))
		,[Name]
		,[ViewPath]
		,[ViewParams]
		,[ResourceId]
		,[Layout]
		,[PrivilegeType]
		,[ResourceActionId]
		,[SpecialPermission]
		,[SourceCollectionId]
		,[DefaultAccessibility]
		,[PageCategoryId]
		,[RouteParameters]
		,@targetTenant
		,[DomainId]
		,[Apps]
		,[HasRoute]
		,[CanEmbed]
		,GETDATE()
		,GETDATE()
		,1
		,1
		,Ps.Id
	FROM 
		dbo.Pages Ps
	WHERE 
		TenantId=@sourceTenant AND 
		Ps.ViewPath NOT IN (
			SELECT Pages.ViewPath 
			FROM Pages
			WHERE TenantId=@targetTenant
		);

	UPDATE @result set AddedPages=@@ROWCOUNT;
	print 'Create pages';
	Commit;

-----------------------------------------------------------------------------------------------
	INSERT INTO [dbo].[PageControls]
			   ([Id]
			   ,[ControlId]
			   ,[PageId]
			   ,[Accessability]
			   ,[SourceCollectionId]
			   ,[CreatedOn]
			   ,[UPDATEdOn]
			   ,[CreatedBy]
			   ,[UPDATEdBy])
		 SELECT
			(dbo.GenerateId(GETDATE(),row_number() OVER ( ORDER BY PageControls.Id))) Id
			,ControlId
			,targetPages.Id
			,Accessability
			,PageControls.SourceCollectionId
			,GETDATE() CreatedOn
			,GETDATE() UPDATEdOn
			,1 CreatedBy
			,1 UPDATEdBy
		FROM PageControls
			join Pages srcPages on srcPages.Id=PageControls.PageId
			join Pages targetPages on 
				targetPages.ViewPath=srcPages.ViewPath AND
				targetPages.TenantId=@targetTenant
		WHERE 
			srcPages.TenantId=@sourceTenant AND
			NOT EXISTS (
				SELECT 1
				FROM PageControls tarControls
					join Pages tarPages on tarPages.Id=tarControls.PageId
				WHERE 
					tarControls.ControlId=PageControls.ControlId AND
					tarPages.ViewPath=srcPages.ViewPath AND
					tarPages.TenantId=@targetTenant 
			);

	UPDATE @result set AddedPageControls=@@ROWCOUNT;
	print 'Create controls';

-----------------------------------------------------------------------------------------------
	UPDATE PageControls
	SET 
		Accessability=srcControls.Accessability,
		SourceCollectionId=srcControls.SourceCollectionId
	FROM PageControls
		join Controls on Controls.Id=PageControls.ControlId
		join Pages on Pages.Id=PageControls.PageId
		join (
			SELECT 
				PageControls.PageId,
				Identifier,
				Accessability,
				PageControls.SourceCollectionId,
				PageControls.UpdatedOn,
				Pages.ViewPath
			FROM PageControls
				join Controls on Controls.Id=PageControls.ControlId
				join Pages on Pages.Id=PageControls.PageId
			WHERE
				TenantId=@sourceTenant
		) as srcControls on 
			Pages.ViewPath=srcControls.ViewPath AND
			Controls.Identifier=srcControls.Identifier AND
			(PageControls.Persistent != 1 )
	WHERE 
		Pages.TenantId=@targetTenant;

	UPDATE @result set UpdatedPageControls=@@ROWCOUNT;
	print 'Update controls';

-----------------------------------------------------------------------------------------------
	INSERT INTO [dbo].[PageParameters]
           ([Id]
           ,[PageCategoryParameterId]
           ,[PageId]
           ,[LinkedPageId]
           ,[ParameterValue]
           ,[CreatedOn]
           ,[CreatedBy]
           ,[UpdatedOn]
           ,[UpdatedBy]
           ,[UseDefault])
     SELECT 
		dbo.GenerateId(GETDATE(),ROW_NUMBER() over (order by srcParams.Id))
		,[PageCategoryParameterId]
		,[tarPages].[Id]
		,(CASE 
			WHEN srcParams.LinkedPageId IS NULL THEN NULL
			ELSE (
				SELECT top 1 Id 
				FROM Pages innerTarPages 
				WHERE 
					ViewPath=linkedPages.ViewPath 
					AND innerTarPages.TenantId=@targetTenant)
				
			END
			)
		,[ParameterValue]
		,GETDATE()
		,1
		,GETDATE()
		,1
		,srcParams.UseDefault
	FROM [dbo].[PageParameters] srcParams
		join [dbo].Pages srcPages on srcPages.Id=srcParams.PageId
		join [dbo].Pages tarPages on tarPages.ViewPath = srcPages.ViewPath AND tarPages.TenantId=@targetTenant
		left join Pages linkedPages on LinkedPageId=linkedPages.Id
	WHERE
		srcPages.TenantId=@sourceTenant AND
		not exists (
			SELECT 1
			FROM [dbo].PageParameters tarParams
			WHERE 
				tarParams.PageId=tarPages.Id AND
				tarParams.PageCategoryParameterId=srcParams.PageCategoryParameterId
		)

	UPDATE @result set AddedParameters=@@ROWCOUNT;
	print 'Create Page Parameters';

-----------------------------------------------------------------------------------------------
	UPDATE tarParams SET 
	--select	
		LinkedPageId=(
			CASE 
				WHEN tarParams.LinkedPageId IS NULL THEN NULL
				ELSE (
					SELECT top 1 Id 
					FROM Pages innerTarPages 
					WHERE 
						ViewPath=linkedPageViewPath 
						AND innerTarPages.TenantId=@targetTenant)
			END
		)
		,ParameterValue= tarParams.[ParameterValue]
		,UpdatedOn= GETDATE()
		,UseDefault= srcParams.UseDefault
	FROM [dbo].[PageParameters] tarParams
		join Pages tarPages on tarPages.Id=tarParams.PageId
		join PageCategoryParameters pcp on pcp.Id=tarParams.PageCategoryParameterId
		join (
			select 
				srcPP.*,
				srcP.ViewPath SrcViewPath,
				linkedPages.ViewPath as LinkedPageViewPath
			from PageParameters srcPP
			join Pages srcP on srcP.Id=srcPP.PageId
			left join Pages linkedPages on srcPP.LinkedPageId=linkedPages.Id
			where srcP.TenantId=@sourceTenant
		) srcParams on 
			srcParams.PageCategoryParameterId=tarParams.PageCategoryParameterId AND
			srcParams.SrcViewPath=tarPages.ViewPath
	WHERE 1=1
		AND tarPages.TenantId=@targetTenant
		AND (
			(pcp.Type=1 AND tarParams.ParameterValue is null AND srcParams.ParameterValue is not null) OR
			(pcp.Type!=1 AND (tarParams.LinkedPageId is null OR tarParams.LinkedPageId=0) AND (srcParams.LinkedPageId is not null AND srcParams.LinkedPageId!=0))
		);

	UPDATE @result set UpdatedParameters=@@ROWCOUNT;
	print 'Update Page Parameters';
-----------------------------------------------------------------------------------------------
	INSERT INTO [dbo].[PageRoutes]
		([Id]
		,[PageId]
		,[ListUrl]
		,[AddUrl]
		,[EditUrl]
		,[DetailsUrl]
		,[CreatedOn]
		,[CreatedBy]
		,[UpdatedOn]
		,[UpdatedBy])
	SELECT 
		dbo.GenerateId(GETDATE(),ROW_NUMBER() over (order by srcRoutes.Id))
		,[tarPages].[Id]
		,(SELECT top 1 Id FROM Pages innerTarPages 
			WHERE ViewPath=listPages.ViewPath AND innerTarPages.TenantId=@targetTenant)
		,(SELECT top 1 Id FROM Pages innerTarPages 
			WHERE ViewPath=addPages.ViewPath AND innerTarPages.TenantId=@targetTenant)
		,(SELECT top 1 Id FROM Pages innerTarPages 
			WHERE ViewPath=editPages.ViewPath AND innerTarPages.TenantId=@targetTenant)
		,(SELECT top 1 Id FROM Pages innerTarPages 
			WHERE ViewPath=detailsPages.ViewPath AND innerTarPages.TenantId=@targetTenant)
		,GETDATE()
		,1
		,GETDATE()
		,1
	FROM [dbo].[PageRoutes] srcRoutes
		join [dbo].Pages srcPages on srcPages.Id=srcRoutes.PageId
		join [dbo].Pages tarPages on tarPages.ViewPath = srcPages.ViewPath AND tarPages.TenantId=@targetTenant
		left join Pages listPages on ListUrl=listPages.Id
		left join Pages addPages on AddUrl=addPages.Id
		left join Pages editPages on EditUrl=editPages.Id
		left join Pages detailsPages on DetailsUrl=detailsPages.Id
	WHERE
		srcPages.TenantId = @sourceTenant AND
		not exists (
			SELECT 1
			FROM [dbo].[PageRoutes] tarRoutes
			WHERE 
				tarRoutes.PageId=tarPages.Id
		);

	UPDATE @result set AddedRoutes=@@ROWCOUNT;
	print 'Create Page Routes';

-----------------------------------------------------------------------------------------------
	UPDATE tarRoutes set
		ListUrl=(SELECT top 1 Id FROM Pages innerTarPages 
			WHERE ViewPath=listPages.ViewPath AND innerTarPages.TenantId=@targetTenant)
		,AddUrl=(SELECT top 1 Id FROM Pages innerTarPages 
			WHERE ViewPath=addPages.ViewPath AND innerTarPages.TenantId=@targetTenant)
		,EditUrl=(SELECT top 1 Id FROM Pages innerTarPages 
			WHERE ViewPath=editPages.ViewPath AND innerTarPages.TenantId=@targetTenant)
		,DetailsUrl=(SELECT top 1 Id FROM Pages innerTarPages 
			WHERE ViewPath=detailsPages.ViewPath AND innerTarPages.TenantId=@targetTenant)
		,UpdatedOn=GETDATE()
	FROM [dbo].[PageRoutes] tarRoutes
		join [dbo].Pages tarPages on tarPages.Id=tarRoutes.PageId
		join [dbo].PageRoutes srcRoutes on srcRoutes.PageId=tarPages.ParentId
		left join Pages listPages on srcRoutes.ListUrl=listPages.Id
		left join Pages addPages on srcRoutes.AddUrl=addPages.Id
		left join Pages editPages on srcRoutes.EditUrl=editPages.Id
		left join Pages detailsPages on srcRoutes.DetailsUrl=detailsPages.Id
	WHERE
		tarPages.TenantId=@targetTenant 

	UPDATE @result set UpdatedRoutes=@@ROWCOUNT;
	print 'Update Page Routes';

-----------------------------------------------------------------------------------------------
	INSERT INTO NavigationPages (Id,PageId,DisplayOrder,NavigationGroupId,CreatedOn,CreatedBy,UpdatedOn,UpdatedBy)
	SELECT 
		dbo.GenerateId(GETDATE(),ROW_NUMBER() over (order by srcNavs.Id))
		,tarPages.[Id]
		,srcNavs.DisplayOrder
		,srcNavs.NavigationGroupId
		,GETDATE()
		,1
		,GETDATE()
		,1
	FROM [dbo].[NavigationPages] srcNavs
		join [dbo].Pages srcPages on srcPages.Id = srcNavs.PageId
		join [dbo].Pages tarPages on tarPages.ViewPath = srcPages.ViewPath AND tarPages.TenantId=@targetTenant
	WHERE
		srcPages.TenantId = @sourceTenant AND
		not exists (
			SELECT 1
			FROM [dbo].[NavigationPages] tarNavs
			WHERE 
				tarNavs.PageId=tarPages.Id
		);

	UPDATE @result set NavigationPages=@@ROWCOUNT;
	print 'Update Navigation Pages';

-----------------------------------------------------------------------------------------------
	select @res=CONCAT(
		'{""SourceTenant"":""',SourceTenant,'""',
		',""TargetTenant"":""', TargetTenant, '""',
		',""AddedPages"":', AddedPages,
		',""UpdatedPages"":', UpdatedPages,
		',""AddedPageControls"":', AddedPageControls,
		',""UpdatedPageControls"":', UpdatedPageControls,
		',""NavigationPages"":', NavigationPages,
		',""AddedParameters"":', AddedParameters,
		',""UpdatedParameters"":', UpdatedParameters,
		'}')
	from @result;

			SELECT* FROM @result;
			END
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    LogonName = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }
    }
}
