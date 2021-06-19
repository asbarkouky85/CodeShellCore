using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Asga.Public.Migrations
{
    public partial class Public_Add_Localizables_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE FUNCTION [dbo].[GetLocalized]
(
	-- Add the parameters for the function here
	@EntityType  varchar(50),
	@EntityId bigint,
	@LocaleId int,
	@PropertyName varchar(50),
	@default nvarchar(max)
)
RETURNS nvarchar(max)
AS
BEGIN
	DECLARE @ret nvarchar(max)=NULL;

	select 
		TOP 1 @ret=Value
	from [dbo].[Localizables] 
	where 
		EntityId=@EntityId AND 
		LocaleId=@LocaleId AND 
		EntityType=@EntityType AND 
		ColumnName=@PropertyName;

	if @ret IS null
		return @default;
	
	return @ret;
END");
            migrationBuilder.CreateTable(
                name: "Localizables",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityId = table.Column<long>(type: "bigint", nullable: false),
                    EntityType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocaleId = table.Column<int>(type: "int", nullable: false),
                    ColumnName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localizables", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Drop function [dbo].[GetLocalized]");
            migrationBuilder.DropTable(
                name: "Localizables");
        }
    }
}
