using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeShellCore.FileServer.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AttachmentCategories",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    CreatedBy = table.Column<long>(nullable: true),
                    UpdatedBy = table.Column<long>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    ValidExtensions = table.Column<string>(unicode: false, nullable: true),
                    MaxSize = table.Column<int>(nullable: false),
                    FolderPath = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttachmentCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BinaryAttachments",
                columns: table => new
                {
                    Bytes = table.Column<byte[]>(nullable: true),
                    Id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BinaryAttachments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Attachments",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<long>(nullable: true),
                    UpdatedBy = table.Column<long>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    FileName = table.Column<string>(maxLength: 200, nullable: true),
                    FullPath = table.Column<string>(maxLength: 300, nullable: true),
                    AttachmentCategoryId = table.Column<long>(nullable: false),
                    BinaryAttachmentId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachments_AttachmentCategories",
                        column: x => x.AttachmentCategoryId,
                        principalTable: "AttachmentCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attachments_BinaryAttachments",
                        column: x => x.BinaryAttachmentId,
                        principalTable: "BinaryAttachments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_AttachmentCategoryId",
                table: "Attachments",
                column: "AttachmentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_BinaryAttachmentId",
                table: "Attachments",
                column: "BinaryAttachmentId",
                unique: true,
                filter: "[BinaryAttachmentId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attachments");

            migrationBuilder.DropTable(
                name: "AttachmentCategories");

            migrationBuilder.DropTable(
                name: "BinaryAttachments");
        }
    }
}
