using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Asga.Public.Migrations
{
    public partial class Public_Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Publ");

            migrationBuilder.CreateTable(
                name: "Attachments",
                schema: "Publ",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    EntityType = table.Column<string>(type: "varchar(70)", unicode: false, maxLength: 70, nullable: true),
                    FileType = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    EntityId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HomeSlides",
                schema: "Publ",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Text = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IsMobile = table.Column<bool>(type: "bit", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Image_sm = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeSlides", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MessageTypes",
                schema: "Publ",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BaseType = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PublicContents",
                schema: "Publ",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Code = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Locale = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicContents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                schema: "Publ",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Body = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    Phone = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    MessageTypeId = table.Column<long>(type: "bigint", nullable: false),
                    Response = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ResponseOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ReferenceId = table.Column<long>(type: "bigint", nullable: true),
                    ReferenceEntity = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_MessageTypes",
                        column: x => x.MessageTypeId,
                        principalSchema: "Publ",
                        principalTable: "MessageTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_MessageTypeId",
                schema: "Publ",
                table: "Messages",
                column: "MessageTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attachments",
                schema: "Publ");

            migrationBuilder.DropTable(
                name: "HomeSlides",
                schema: "Publ");

            migrationBuilder.DropTable(
                name: "Messages",
                schema: "Publ");

            migrationBuilder.DropTable(
                name: "PublicContents",
                schema: "Publ");

            migrationBuilder.DropTable(
                name: "MessageTypes",
                schema: "Publ");
        }
    }
}
