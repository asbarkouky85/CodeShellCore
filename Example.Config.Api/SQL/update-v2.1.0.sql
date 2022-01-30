alter table Resources alter column DomainId bigint null;
alter table Resources add ServiceName varchar(50) null;
alter table PageCategories add DomainId bigint null;
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
GO
alter table Domains add ParentId bigint null;
alter table Domains add Chain varchar(max) null;
alter table Domains add NameChain nvarchar(max) null;

GO

CREATE trigger [dbo].[Domains_Chain_TR]
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
					@chain='|'+CONVERT(NVARCHAR(25),@id)+'|',
					@nameChain='/'+@name+'/'
			ELSE
				SELECT 
					@chain=Chain+CONVERT(NVARCHAR(25),@id)+'|',
					@nameChain=NameChain+@name+'/'
				FROM Domains WHERE Id=@parentId;

			UPDATE Domains 
			SET 
				Chain=@chain,
				NameChain=@nameChain
			WHERE Id=@id;
		end;



GO

