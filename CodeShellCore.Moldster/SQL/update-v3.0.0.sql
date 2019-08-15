alter table Resources drop constraint FK_Resources_Domains;
alter table Resources drop column DomainId;
alter table Resources add ServiceName varchar(50) null;
GO
alter table Pages drop constraint FK_Pages_TenantDomains;
alter table Pages add DomainId bigint null;
alter table Pages add TenantId bigint null;
GO

update Pages 
set
	DomainId=td.DomainId,
	TenantId=td.TenantId
from Pages ps
	join TenantDomains td on td.Id=ps.TenantDomainId;

drop table TenantDomains;

GO
alter table Pages drop column TenantDomainId;
alter table Pages alter column DomainId bigint not null;
alter table Pages alter column TenantId bigint not null;
GO;
alter table Domains add ParentId bigint null;
alter table Domains add Chain varchar(max) null;
GO

create trigger Domains_Chain_TR
    ON [dbo].[Domains]
    AFTER INSERT, UPDATE 
	
	AS
		SET NOCOUNT ON;
		DECLARE @i int=1;
        DECLARE @id BIGINT;
        DECLARE @chain NVARCHAR(max);
		DECLARE @name NVARCHAR(max);
		DECLARE @nameChain NVARCHAR(max);
		DECLARE @parentId bigint;

		while(@i<=(select count(*) from inserted))
		begin
			 SELECT 
				@id=Id,
				@parentId=ParentId,
				@name=Name
			FROM (
				select ROW_NUMBER() over (order by Id) i,*
				from inserted
			) TX
			WHERE i=@i;

			SET @i=@i+1;

			IF(@parentId IS NULL)
				SELECT 
					@chain='|'+CONVERT(NVARCHAR(25),@id)+'|'
			ELSE
				SELECT 
					@chain=Chain+CONVERT(NVARCHAR(25),@id)+'|',
					@nameChain=NameChain+'/'+@name
				FROM Domains WHERE Id=@parentId;

			UPDATE Domains 
			SET 
				Chain=@chain,
				NameChain=@nameChain
			WHERE Id=@id;
		end;

GO

CREATE FUNCTION [dbo].[GetPath]
(
	@EntityType varchar(50),
	@chain nvarchar(max)
	
)
RETURNS nvarchar(max)
AS
BEGIN
	declare @ColumnNameList nvarchar(max);

	if(@EntityType='Domain')
		SELECT  
			@ColumnNameList = COALESCE(@ColumnNameList,'')+(CASE WHEN (@ColumnNameList is NULL) THEN '' ELSE '/' END) +Name
		FROM Domains 
		WHERE @chain like '%|'+convert(varchar(15),Id)+'|%';

	return @ColumnNameList;
END

GO
