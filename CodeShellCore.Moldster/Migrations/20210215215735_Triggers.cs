using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeShellCore.Moldster.Migrations
{
    public partial class Triggers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"CREATE trigger [dbo].[Domains_Chain_TR]
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
		end;");
		}

        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql("DROP TRIGGER [dbo].[Domains_Chain_TR]");
		}
    }
}
