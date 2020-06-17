CREATE PROCEDURE SyncAuthDb(
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
	from [Example.Config].dbo.Resources
	where Id not in (select Id from ['+@dbName+'].Auth.Resources);'

	exec sp_executesql @sql;
	print 'Added Resources : '+convert(varchar(10),@@ROWCOUNT);

	set @sql ='insert into ['+@dbName+'].Auth.ResourceActions(Id,Name,ResourceId,CreatedOn,UpdatedOn)
	select Id,Name,ResourceId,CreatedOn,UpdatedOn
	from [Example.Config].dbo.ResourceActions
	where Id not in (select Id from ['+@dbName+'].Auth.ResourceActions);'

	exec sp_executesql @sql;
	print 'Added Actions : '+convert(varchar(10),@@ROWCOUNT);

	set @sql ='insert into ['+@dbName+'].Auth.Apps(Id,Name,CreatedOn,UpdatedOn)
	select Id,Name,CreatedOn,UpdatedOn
	from [Example.Config].dbo.TenantApps
	where 
		Id not in (select Id from ['+@dbName+'].Auth.ResourceActions)
		AND (TenantId is null OR TenantId='+convert(varchar(15),@tenantId)+');';

	exec sp_executesql @sql;
	print 'Added Apps : '+convert(varchar(10),@@ROWCOUNT);

END;