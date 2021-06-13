using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Asga.Auth.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Auth");

            migrationBuilder.CreateTable(
                name: "Apps",
                schema: "Auth",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Domains",
                schema: "Auth",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Domains", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                schema: "Auth",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "ntext", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "Auth",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    TenantDomainId = table.Column<long>(type: "bigint", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsUserRole = table.Column<bool>(type: "bit", nullable: false),
                    TenantAppId = table.Column<long>(type: "bigint", nullable: true),
                    Identifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDefaultAppRole = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Roles__TenantApp__2B3F6F97",
                        column: x => x.TenantAppId,
                        principalSchema: "Auth",
                        principalTable: "Apps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Resources",
                schema: "Auth",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    DomainId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resources_Domains",
                        column: x => x.DomainId,
                        principalSchema: "Auth",
                        principalTable: "Domains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "Auth",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    LogonName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Password = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    PersonId = table.Column<long>(type: "bigint", nullable: true),
                    UserType = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Mobile = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    Photo = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Gender = table.Column<bool>(type: "bit", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    AppId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Apps",
                        column: x => x.AppId,
                        principalSchema: "Auth",
                        principalTable: "Apps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Tenants",
                        column: x => x.TenantId,
                        principalSchema: "Auth",
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ResourceActions",
                schema: "Auth",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    ResourceId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourceActions_Resources",
                        column: x => x.ResourceId,
                        principalSchema: "Auth",
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ResourceCollections",
                schema: "Auth",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ResourceId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceCollections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourceCollections_Resources",
                        column: x => x.ResourceId,
                        principalSchema: "Auth",
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserEntityLinks",
                schema: "Auth",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    EntityName = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: true),
                    EntityId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEntityLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserEntityLinks_Users",
                        column: x => x.UserId,
                        principalSchema: "Auth",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "Auth",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles",
                        column: x => x.RoleId,
                        principalSchema: "Auth",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users",
                        column: x => x.UserId,
                        principalSchema: "Auth",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleResourceActions",
                schema: "Auth",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    ResourceActionId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleResourceActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleResourceActions_ResourceActions",
                        column: x => x.ResourceActionId,
                        principalSchema: "Auth",
                        principalTable: "ResourceActions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoleResourceActions_Roles",
                        column: x => x.RoleId,
                        principalSchema: "Auth",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleResources",
                schema: "Auth",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    ResourceId = table.Column<long>(type: "bigint", nullable: false),
                    CanInsert = table.Column<bool>(type: "bit", nullable: false),
                    CanUpdate = table.Column<bool>(type: "bit", nullable: false),
                    CanDelete = table.Column<bool>(type: "bit", nullable: false),
                    CanViewDetails = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    CollectionId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleResources_ResourceCollections",
                        column: x => x.CollectionId,
                        principalSchema: "Auth",
                        principalTable: "ResourceCollections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoleResources_Resources",
                        column: x => x.ResourceId,
                        principalSchema: "Auth",
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoleResources_Roles",
                        column: x => x.RoleId,
                        principalSchema: "Auth",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResourceActions_ResourceId",
                schema: "Auth",
                table: "ResourceActions",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceCollections_ResourceId",
                schema: "Auth",
                table: "ResourceCollections",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_DomainId",
                schema: "Auth",
                table: "Resources",
                column: "DomainId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleResourceActions_ResourceActionId",
                schema: "Auth",
                table: "RoleResourceActions",
                column: "ResourceActionId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleResourceActions_RoleId",
                schema: "Auth",
                table: "RoleResourceActions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleResources_CollectionId",
                schema: "Auth",
                table: "RoleResources",
                column: "CollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleResources_ResourceId",
                schema: "Auth",
                table: "RoleResources",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleResources_RoleId",
                schema: "Auth",
                table: "RoleResources",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_TenantAppId",
                schema: "Auth",
                table: "Roles",
                column: "TenantAppId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEntityLinks_UserId",
                schema: "Auth",
                table: "UserEntityLinks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                schema: "Auth",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                schema: "Auth",
                table: "UserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AppId",
                schema: "Auth",
                table: "Users",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TenantId",
                schema: "Auth",
                table: "Users",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleResourceActions",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "RoleResources",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "UserEntityLinks",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "ResourceActions",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "ResourceCollections",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "Resources",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "Apps",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "Tenants",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "Domains",
                schema: "Auth");
        }
    }
}
