using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeShellCore.Moldster.Migrations
{
    public partial class TenantUseParentUI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "UseParentUI",
                table: "Tenants",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UseParentUI",
                table: "Tenants");
        }
    }
}
