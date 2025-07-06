USE [SID.Config]
GO
/****** Object:  StoredProcedure [dbo].[SyncAuthDb]    Script Date: 5/4/2020 12:45:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[SyncAuthDb](
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
		Id not in (select Id from ['+@dbName+'].Auth.Apps)
		AND (TenantId is null OR TenantId='+convert(varchar(15),@tenantId)+');';

	exec sp_executesql @sql;
	print 'Added Apps : '+convert(varchar(10),@@ROWCOUNT);

END;