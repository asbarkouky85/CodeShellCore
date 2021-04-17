using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeShellCore.Moldster.Migrations
{
    public partial class Procedures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"
CREATE PROCEDURE [dbo].[AddAuditingColumns]

AS
BEGIN
	declare @command nvarchar(max);
	declare @tables TABLE(ID int,TABLE_NAME varchar(60));
	declare @i int=0;
	declare @table varchar(60);

	insert into @tables (ID,TABLE_NAME)
	select 
		ROW_NUMBER() over (order by TABLE_NAME) ,
		TABLE_SCHEMA+'.'+ TABLE_NAME 
	from INFORMATION_SCHEMA.TABLES
	where 
		TABLE_NAME!='sysdiagrams' AND
		TABLE_SCHEMA+'.'+TABLE_NAME NOT IN (SELECT TABLE_SCHEMA+'.'+TABLE_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE COLUMN_NAME='CreatedOn');

	select @i=0;
	while(@i<(select count(*) from @tables))
	begin
		select @i=@i+1;
		select @table=TABLE_NAME from @tables where ID=@i;
		select @command='ALTER TABLE '+@table+' ADD CreatedOn Datetime null;'
		print @command;
		EXEC sp_executesql @command;
	end;
	delete from @tables

	insert into @tables (ID,TABLE_NAME)
	select 
		ROW_NUMBER() over (order by TABLE_NAME) ,
		TABLE_SCHEMA+'.'+ TABLE_NAME 
	from INFORMATION_SCHEMA.TABLES
	where 
		TABLE_NAME!='sysdiagrams' AND
		TABLE_SCHEMA+'.'+TABLE_NAME NOT IN (SELECT TABLE_SCHEMA+'.'+TABLE_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE COLUMN_NAME='CreatedBy');

	select @i=0;
	while(@i<(select count(*) from @tables))
	begin
		select @i=@i+1;
		select @table=TABLE_NAME from @tables where ID=@i;
		select @command='ALTER TABLE '+@table+' ADD CreatedBy bigint null;'
		print @command;
		EXEC sp_executesql @command;
	end;
	delete from @tables

	insert into @tables (ID,TABLE_NAME)
	select 
		ROW_NUMBER() over (order by TABLE_NAME) ,
		TABLE_SCHEMA+'.'+ TABLE_NAME 
	from INFORMATION_SCHEMA.TABLES
	where 
		TABLE_NAME!='sysdiagrams' AND
		TABLE_SCHEMA+'.'+TABLE_NAME NOT IN (SELECT TABLE_SCHEMA+'.'+TABLE_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE COLUMN_NAME='UpdatedOn');

	select @i=0;
	while(@i<(select count(*) from @tables))
	begin
		select @i=@i+1;
		select @table=TABLE_NAME from @tables where ID=@i;
		select @command='ALTER TABLE '+@table+' ADD UpdatedOn Datetime null;';
		print @command;
		EXEC sp_executesql @command;
	end;
	delete from @tables

	insert into @tables (ID,TABLE_NAME)
	select 
		ROW_NUMBER() over (order by TABLE_NAME) ,
		TABLE_SCHEMA+'.'+ TABLE_NAME 
	from INFORMATION_SCHEMA.TABLES
	where 
		TABLE_NAME!='sysdiagrams' AND
		TABLE_SCHEMA+'.'+TABLE_NAME NOT IN (SELECT TABLE_SCHEMA+'.'+TABLE_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE COLUMN_NAME='UpdatedBy');

	select @i=0;
	while(@i<(select count(*) from @tables))
	begin
		select @i=@i+1;
		select @table=TABLE_NAME from @tables where ID=@i;
		select @command='ALTER TABLE '+@table+' ADD UpdatedBy bigint null;';
		print @command;
		EXEC sp_executesql @command;
	end;
	delete from @tables

END;");
			migrationBuilder.Sql(@"
 CREATE PROCEDURE [dbo].[MergeDBs] (
	 @db1 nvarchar(100),
	 @db2 nvarchar(100),
	 @id varchar(15)='1',
	 @res varchar(max)=null OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @rowC int=0;
	DECLARE @q nvarchar(max);
	DECLARE @result TABLE(
		SourceDB varchar(150),
		TargetDB varchar(150),

		AddedDomains int,
		AddedResources int,
		AddedCollections int,
		
		AddedPages int,
		UpdatedPages int,
		
		AddedPageControls int,
		UpdatedPageControls int,

		AddedParameters int,
		UpdatedParameters int,

		AddedPageCategories int,
		UpdatedPageCategories int
	);

	INSERT @result 
		select
			@db1,@db2,
			0,0,0,0,0,0,0,0,0,0,0;

	SET @q=N'insert '+@db2+N'.dbo.Domains
		select * from '+@db1+N'.dbo.Domains
		where Id NOT IN ( select Id from '+@db2+N'.dbo.Domains )';

	EXEC sp_executesql @q; SET @rowC = @@ROWCOUNT; print CONCAT('Domains : ',@rowC);

	update @result set AddedDomains=@rowC;

	SET @q=N'insert '+@db2+N'.dbo.Resources
		select * from '+@db1+N'.dbo.Resources
		where Id NOT IN ( select Id from '+@db2+N'.dbo.Resources )';
	
	EXEC sp_executesql @q; SET @rowC = @@ROWCOUNT; print CONCAT('Resources : ',@rowC);

	update @result set AddedResources=@rowC;

	SET @q=N'insert '+@db2+N'.dbo.PageCategories
		select * from 
		'+@db1+N'.dbo.PageCategories
		WHERE Id NOT IN ( select Id from '+@db2+N'.dbo.PageCategories )';
	
	EXEC sp_executesql @q; SET @rowC = @@ROWCOUNT; print CONCAT('PageCategories : ',@rowC);

	update @result set AddedPageCategories=@rowC;

	SET @q=N'insert '+@db2+N'.dbo.PageCategoryParameters
		select * from 
		'+@db1+N'.dbo.PageCategoryParameters
		WHERE Id NOT IN ( select Id from '+@db2+N'.dbo.PageCategoryParameters )';
	
	EXEC sp_executesql @q; SET @rowC = @@ROWCOUNT; print CONCAT('PageCategoryParameters : ',@rowC);

	SET @q=N'insert '+@db2+N'.dbo.ResourceActions
		select * from '+@db1+N'.dbo.ResourceActions
		where Id NOT IN ( select Id from '+@db2+N'.dbo.ResourceActions )';
	
	EXEC sp_executesql @q; SET @rowC = @@ROWCOUNT; print CONCAT('ResourceActions : ',@rowC);

	SET @q=N'insert '+@db2+N'.dbo.ResourceCollections
		select * from '+@db1+N'.dbo.ResourceCollections
		where Id NOT IN ( select Id from '+@db2+N'.dbo.ResourceCollections)';
	
	EXEC sp_executesql @q; SET @rowC = @@ROWCOUNT; print CONCAT('ResourceCollections : ',@rowC);

	update @result set AddedCollections=@rowC;

	SET @q=N'insert '+@db2+N'.dbo.Controls
		select * from '+@db1+N'.dbo.Controls
		where Id NOT IN ( select Id from '+@db2+N'.dbo.Controls )';
	
	EXEC sp_executesql @q; SET @rowC = @@ROWCOUNT; print CONCAT('Controls : ',@rowC);

	SET @q=N'insert '+@db2+N'.dbo.Pages
		select * from '+@db1+N'.dbo.Pages
		where Id NOT IN ( select Id from '+@db2+N'.dbo.Pages) AND TenantId='+@id+'';
	
	EXEC sp_executesql @q; SET @rowC = @@ROWCOUNT; print CONCAT('Pages Created : ',@rowC);

	UPDATE @result set AddedPages=@rowC;

	SET @q=N'insert '+@db2+N'.dbo.PageControls
		select * from '+@db1+N'.dbo.PageControls C
		where 
			NOT EXISTS (
				select C2.Id 
					from '+@db2+N'.dbo.PageControls C2
				where C.ControlId=C2.ControlId AND C.PageId=C2.PageId 
			) AND C.PageId IN (select Id from Pages where TenantId='+@id+')';
	
	EXEC sp_executesql @q; SET @rowC = @@ROWCOUNT; print CONCAT('PageControls Created : ',@rowC);

	update @result set AddedPageControls=@rowC;

	SET @q=N'insert '+@db2+N'.dbo.PageParameters
		select * from '+@db1+N'.dbo.PageParameters C
		where 
			NOT EXISTS (
				select C2.Id 
					from '+@db2+N'.dbo.PageParameters C2
				where C.PageCategoryParameterId=C2.PageCategoryParameterId AND C.PageId=C2.PageId 
			) AND C.PageId IN (select Id from Pages where TenantId='+@id+')';
	
	EXEC sp_executesql @q; SET @rowC = @@ROWCOUNT; print CONCAT('PageParameters Created : ',@rowC);

	UPDATE @result set AddedParameters=@rowC;

	SET @q=N'
	UPDATE '+@db2+N'.dbo.PageCategories SET
		[Name] = tx.Name
		,[BaseComponent] = tx.BaseComponent
		,[ViewPath] = tx.[ViewPath]
		,[ResourceId] = tx.ResourceId
		,[CreatedOn] = tx.CreatedOn
		,[UpdatedOn] = tx.UpdatedOn
		,[CreatedBy] = tx.CreatedBy
		,[UpdatedBy] = tx.UpdatedBy
		,[Layout] = tx.Layout
		,[DomainId] = tx.DomainId
	from '+@db2+N'.dbo.PageCategories
		join '+@db1+N'.dbo.PageCategories tx on tx.Id=PageCategories.Id
	WHERE
		tx.UpdatedOn > '+@db2+N'.dbo.PageCategories.UpdatedOn';
	
	EXEC sp_executesql @q; SET @rowC = @@ROWCOUNT; print CONCAT('Pages PageCategories : ',@rowC);

	update @result set UpdatedPageCategories = @rowC;

	SET @q=N'
	UPDATE '+@db2+N'.dbo.Pages SET
		[Name] = tx.Name
		,[ViewPath] = tx.ViewPath
		,[Apps] = tx.Apps
		,[ViewParams] = tx.ViewParams
		,[ResourceId] = tx.ResourceId
		,[Layout] = tx.Layout
		,[PrivilegeType] = tx.PrivilegeType
		,[ResourceActionId] = tx.ResourceActionId
		,[SpecialPermission] = tx.SpecialPermission
		,[SourceCollectionId] = tx.SourceCollectionId
		,[DisplayName] = tx.DisplayName
		,[PageCategoryId] = tx.PageCategoryId
		,[RouteParameters] = tx.RouteParameters
		,[HasRoute] = tx.HasRoute
		,[CanEmbed] = tx.CanEmbed
		,[CreatedOn] = tx.CreatedOn
		,[UpdatedOn] = tx.UpdatedOn
		,[CreatedBy] = tx.CreatedBy
		,[UpdatedBy] = tx.UpdatedBy
		,[DefaultAccessibility] = tx.DefaultAccessibility
		,[DomainId] = tx.DomainId
		,[TenantId] = tx.TenantId
		,[IsHomePage] = tx.IsHomePage
		,[ParentId] = tx.ParentId
	FROM '+@db2+N'.dbo.Pages
		JOIN '+@db1+N'.dbo.Pages tx on tx.Id=Pages.Id
	WHERE
		tx.UpdatedOn > '+@db2+N'.dbo.Pages.UpdatedOn AND tx.TenantId='+@id+'';
	
	EXEC sp_executesql @q; SET @rowC = @@ROWCOUNT; print CONCAT('Pages Updates : ',@rowC);

	UPDATE @result set UpdatedPages=@rowC;

	SET @q=N'
	UPDATE '+@db2+N'.dbo.PageControls set 
		[Accessability] = tx.Accessability
		,[SourceCollectionId] = tx.SourceCollectionId
		,[CreatedOn] = tx.CreatedOn
		,[UpdatedOn] = tx.UpdatedOn
		,[CreatedBy] = tx.CreatedBy
		,[UpdatedBy] = tx.UpdatedBy
		,[Persistent] = tx.Persistent
	FROM '+@db2+N'.dbo.PageControls
		JOIN '+@db1+N'.dbo.PageControls tx on tx.PageId=PageControls.PageId AND tx.ControlId=PageControls.ControlId
	WHERE
		tx.UpdatedOn > '+@db2+N'.dbo.PageControls.UpdatedOn AND 
		tx.PageId IN (select Id from '+@db2+N'.dbo.Pages where TenantId='+@id+')';
	
	EXEC sp_executesql @q; SET @rowC = @@ROWCOUNT; print CONCAT('PageControls Updates : ',@rowC);

	UPDATE @result set UpdatedPageControls=@rowC;

		SET @q=N'
	UPDATE '+@db2+N'.dbo.PageParameters set 
		[LinkedPageId] = tx.LinkedPageId
      ,[ParameterValue] = tx.ParameterValue
      ,[CreatedOn] = tx.CreatedOn
      ,[CreatedBy] = tx.CreatedBy
      ,[UpdatedOn] = tx.UpdatedOn
      ,[UpdatedBy] = tx.UpdatedBy
      ,[UseDefault] = tx.UseDefault
	FROM '+@db2+N'.dbo.PageParameters
		JOIN '+@db1+N'.dbo.PageParameters tx on tx.PageId=PageParameters.PageId AND tx.PageCategoryParameterId=PageParameters.PageCategoryParameterId
	WHERE
		tx.UpdatedOn > '+@db2+N'.dbo.PageParameters.UpdatedOn AND 
		tx.PageId IN (select Id from '+@db2+N'.dbo.Pages where TenantId='+@id+')';
	
	EXEC sp_executesql @q; SET @rowC = @@ROWCOUNT; print CONCAT('PageControls Updates : ',@rowC);

	UPDATE @result set UpdatedParameters=@rowC;

		select @res=CONCAT(
			'{""SourceDB"":""',SourceDB,'""',
			',""TargetDB"":""', TargetDB, '""',
			',""AddedPages"":', AddedPages,
			',""UpdatedPages"":', UpdatedPages,
			',""AddedPageControls"":', AddedPageControls,
			',""UpdatedPageControls"":', UpdatedPageControls,
			'}')
		from @result;

			SELECT* FROM @result;
			END;
			");
			migrationBuilder.Sql(@"
CREATE PROCEDURE [dbo].[SyncAuthDb](
 @tenantId bigint,
 @dbName nvarchar(100)
)
AS
BEGIN
	SET NOCOUNT ON;


	declare @sql nvarchar(max)='';

	set @sql ='insert into ['+@dbName+'].Auth.Domains (Id,Name,CreatedOn,UpdatedOn)
	select Id,Name,CreatedOn,UpdatedOn
	from Domains
	where 
		ParentId is null AND 
		Id not in (select Id from ['+@dbName+'].Auth.Domains);';

	exec sp_executesql @sql;
	print 'Added Domains : '+convert(varchar(10),@@ROWCOUNT);

	set @sql ='insert into ['+@dbName+'].Auth.Resources(Id,Name,DomainId,CreatedOn,UpdatedOn)
	select Id,Name,DomainId,CreatedOn,UpdatedOn
	from dbo.Resources
	where Id not in (select Id from ['+@dbName+'].Auth.Resources);'

	exec sp_executesql @sql;
	print 'Added Resources : '+convert(varchar(10),@@ROWCOUNT);

	set @sql ='insert into ['+@dbName+'].Auth.ResourceActions(Id,Name,ResourceId,CreatedOn,UpdatedOn)
	select Id,Name,ResourceId,CreatedOn,UpdatedOn
	from dbo.ResourceActions
	where Id not in (select Id from ['+@dbName+'].Auth.ResourceActions);'

	exec sp_executesql @sql;
	print 'Added Actions : '+convert(varchar(10),@@ROWCOUNT);

	set @sql ='insert into ['+@dbName+'].Auth.Apps(Id,Name,CreatedOn,UpdatedOn)
	select Id,Name,CreatedOn,UpdatedOn
	from dbo.TenantApps
	where 
		Id not in (select Id from ['+@dbName+'].Auth.ResourceActions)
		AND (TenantId is null OR TenantId='+convert(varchar(15),@tenantId)+');';

	exec sp_executesql @sql;
	print 'Added Apps : '+convert(varchar(10),@@ROWCOUNT);

END;");
			migrationBuilder.Sql(@"CREATE PROCEDURE [dbo].[SyncTenants]
	-- Add the parameters for the stored procedure here
	 @sourceTenant bigint,
	@targetTenant bigint,
	@res varchar(max)=null OUTPUT
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
		AddedPageControls int,
		UpdatedPageControls int,
		NavigationPages int
	);

	INSERT @result 
		select
			(SELECT Code FROM Tenants WHERE Id=@sourceTenant), 
			(SELECT Code FROM Tenants WHERE Id=@targetTenant), 
			0,0,0,0,0;

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
		,[DisplayName]
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
		,[DisplayName]
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
					tarPages.ParentId=srcPages.Id AND
					tarPages.TenantId=@targetTenant 
			);

	UPDATE @result set AddedPageControls=@@ROWCOUNT;
	print 'Create controls';

-----------------------------------------------------------------------------------------------

	UPDATE PageControls
	SET 
		Accessability=Existing.Accessability,
		SourceCollectionId=Existing.SourceCollectionId
	FROM PageControls
		join Controls on Controls.Id=PageControls.ControlId
		join Pages on Pages.Id=PageControls.PageId
		join (
			SELECT 
				PageControls.PageId,
				Identifier,
				Accessability,
				PageControls.SourceCollectionId,
				PageControls.UpdatedOn
			FROM PageControls
				join Controls on Controls.Id=PageControls.ControlId
				join Pages on Pages.Id=PageControls.PageId
			WHERE
				TenantId=@sourceTenant
		) as Existing on 
			Pages.ParentId=Existing.PageId AND
			Controls.Identifier=Existing.Identifier AND
			(PageControls.Persistent != 1 )
	WHERE 
		Pages.TenantId=@targetTenant;

	UPDATE @result set UpdatedPageControls=@@ROWCOUNT;
	print 'Update controls';

-----------------------------------------------------------------------------------------------

	INSERT INTO NavigationPages (Id,PageId,DisplayOrder,NavigationGroupId,CreatedOn,CreatedBy,UpdatedOn,UpdatedBy)
	SELECT 
		dbo.GenerateId(GETDATE(),ROW_NUMBER() over (order by srcNavs.Id))
		,tarPages.[Id]
		,0
		,[NavigationGroupId]
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
	print 'Update controls';

	select @res=CONCAT(
		'{""SourceTenant"":""',SourceTenant,'""',
		',""TargetTenant"":""', TargetTenant, '""',
		',""AddedPages"":', AddedPages,
		',""UpdatedPages"":', UpdatedPages,
		',""AddedPageControls"":', AddedPageControls,
		',""UpdatedPageControls"":', UpdatedPageControls,
		',""NavigationPages"":', NavigationPages,
		'}')
	from @result;

			SELECT* FROM @result;
			END");

		}

		protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql("DROP PROCEDURE [dbo].[AddAuditingColumns]");
			migrationBuilder.Sql("DROP PROCEDURE [dbo].[MergeDBs]");
			migrationBuilder.Sql("DROP PROCEDURE [dbo].[SyncAuthDb]");
			migrationBuilder.Sql("DROP PROCEDURE [dbo].[SyncTenants]");
		}
    }
}
