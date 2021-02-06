using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeShellCore.Moldster.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Identifier = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Secret = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Address = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Domains",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    Chain = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    NameChain = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Domains", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NavigationGroups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    Chain = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NavigationGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "ntext", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    ConnectionString = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    MainComponentBase = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    BaseStyle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Version = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    ParentId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    LogonName = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Validators",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Type = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    CalendarType = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    MinValue = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    MaxValue = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    MinLength = table.Column<int>(type: "int", nullable: true),
                    MaxLength = table.Column<int>(type: "int", nullable: true),
                    Pattern = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Validators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ServiceName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    DomainId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resources_Domains",
                        column: x => x.DomainId,
                        principalTable: "Domains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Apps",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    DashboardUrl = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenantApps_Tenants",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomTexts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Locale = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomTexts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomTexts_Tenants",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PageCategories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    BaseComponent = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: true),
                    ViewPath = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: true),
                    ResourceId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    Layout = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    DomainId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PageCategories_Domains",
                        column: x => x.DomainId,
                        principalTable: "Domains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PageCategories_Resources",
                        column: x => x.ResourceId,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ResourceActions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    ResourceId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourceActions_Resources",
                        column: x => x.ResourceId,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ResourceActions_Tenants",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ResourceCollections",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ResourceId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceCollections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourceCollections_Resources",
                        column: x => x.ResourceId,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Controls",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ControlType = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    DesignParameters = table.Column<string>(type: "ntext", nullable: true),
                    ParentControl = table.Column<long>(type: "bigint", nullable: true),
                    PageCategoryId = table.Column<long>(type: "bigint", nullable: true),
                    Identifier = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Controls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Controls_PageCategories",
                        column: x => x.PageCategoryId,
                        principalTable: "PageCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PageControls_PageControls",
                        column: x => x.ParentControl,
                        principalTable: "Controls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PageCategoryParameters",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    DefaultValue = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    PageCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageCategoryParameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PageCategoryParameters_PageCategories",
                        column: x => x.PageCategoryId,
                        principalTable: "PageCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ViewPath = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: true),
                    Apps = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    ViewParams = table.Column<string>(type: "ntext", nullable: true),
                    ResourceId = table.Column<long>(type: "bigint", nullable: true),
                    Layout = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    PrivilegeType = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ResourceActionId = table.Column<long>(type: "bigint", nullable: true),
                    SpecialPermission = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    SourceCollectionId = table.Column<long>(type: "bigint", nullable: true),
                    PageCategoryId = table.Column<long>(type: "bigint", nullable: true),
                    RouteParameters = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    HasRoute = table.Column<bool>(type: "bit", nullable: false),
                    CanEmbed = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    DefaultAccessibility = table.Column<int>(type: "int", nullable: false),
                    DomainId = table.Column<long>(type: "bigint", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false),
                    IsHomePage = table.Column<bool>(type: "bit", nullable: true),
                    ParentId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pages_DomainEntityCollections1",
                        column: x => x.SourceCollectionId,
                        principalTable: "ResourceCollections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pages_Domains",
                        column: x => x.DomainId,
                        principalTable: "Domains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pages_PageCategories",
                        column: x => x.PageCategoryId,
                        principalTable: "PageCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pages_ResourceActions",
                        column: x => x.ResourceActionId,
                        principalTable: "ResourceActions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pages_Resources",
                        column: x => x.ResourceId,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pages_Tenants",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ResourceCollectionConditions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    DomainEntityCollectionId = table.Column<long>(type: "bigint", nullable: true),
                    Property = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Value = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceCollectionConditions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntityCollectionConditions_DomainEntityCollections",
                        column: x => x.DomainEntityCollectionId,
                        principalTable: "ResourceCollections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ControlValidators",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ControlId = table.Column<long>(type: "bigint", nullable: false),
                    ValidatorId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControlValidators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ControlValidators_Controls",
                        column: x => x.ControlId,
                        principalTable: "Controls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ControlValidators_Validators",
                        column: x => x.ValidatorId,
                        principalTable: "Validators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomFields",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    Type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    PageId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomFields_Pages",
                        column: x => x.PageId,
                        principalTable: "Pages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NavigationPages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    PageId = table.Column<long>(type: "bigint", nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    NavigationGroupId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NavigationPages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NavigationPages_NavigationGroups",
                        column: x => x.NavigationGroupId,
                        principalTable: "NavigationGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NavigationPages_Pages",
                        column: x => x.PageId,
                        principalTable: "Pages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PageControls",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ControlId = table.Column<long>(type: "bigint", nullable: false),
                    PageId = table.Column<long>(type: "bigint", nullable: false),
                    Accessability = table.Column<byte>(type: "tinyint", nullable: false),
                    SourceCollectionId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    Persistent = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageControls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PageControls_Controls",
                        column: x => x.ControlId,
                        principalTable: "Controls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PageControls_DomainEntityCollections",
                        column: x => x.SourceCollectionId,
                        principalTable: "ResourceCollections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PageControls_Pages",
                        column: x => x.PageId,
                        principalTable: "Pages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PageParameters",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    PageCategoryParameterId = table.Column<long>(type: "bigint", nullable: false),
                    PageId = table.Column<long>(type: "bigint", nullable: false),
                    LinkedPageId = table.Column<long>(type: "bigint", nullable: true),
                    ParameterValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UseDefault = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageParameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PageParameters_PageCategoryParameters",
                        column: x => x.PageCategoryParameterId,
                        principalTable: "PageCategoryParameters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PageParameters_Pages",
                        column: x => x.PageId,
                        principalTable: "Pages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PageRoutes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    PageId = table.Column<long>(type: "bigint", nullable: false),
                    ListUrl = table.Column<long>(type: "bigint", nullable: true),
                    AddUrl = table.Column<long>(type: "bigint", nullable: true),
                    EditUrl = table.Column<long>(type: "bigint", nullable: true),
                    DetailsUrl = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageRoutes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PageRoutes_Pages",
                        column: x => x.PageId,
                        principalTable: "Pages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PageControlValidators",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    PageControlId = table.Column<long>(type: "bigint", nullable: false),
                    ValidatorId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageControlValidators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PageControlValidators_PageControls",
                        column: x => x.PageControlId,
                        principalTable: "PageControls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PageControlValidators_Validators",
                        column: x => x.ValidatorId,
                        principalTable: "Validators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Apps_TenantId",
                table: "Apps",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Controls_PageCategoryId",
                table: "Controls",
                column: "PageCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Controls_ParentControl",
                table: "Controls",
                column: "ParentControl");

            migrationBuilder.CreateIndex(
                name: "IX_ControlValidators_ControlId",
                table: "ControlValidators",
                column: "ControlId");

            migrationBuilder.CreateIndex(
                name: "IX_ControlValidators_ValidatorId",
                table: "ControlValidators",
                column: "ValidatorId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomFields_PageId",
                table: "CustomFields",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomTexts_TenantId",
                table: "CustomTexts",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_NavigationPages_NavigationGroupId",
                table: "NavigationPages",
                column: "NavigationGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_NavigationPages_PageId",
                table: "NavigationPages",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_PageCategories_DomainId",
                table: "PageCategories",
                column: "DomainId");

            migrationBuilder.CreateIndex(
                name: "IX_PageCategories_ResourceId",
                table: "PageCategories",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_PageCategoryParameters_PageCategoryId",
                table: "PageCategoryParameters",
                column: "PageCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PageControls_ControlId",
                table: "PageControls",
                column: "ControlId");

            migrationBuilder.CreateIndex(
                name: "IX_PageControls_PageId",
                table: "PageControls",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_PageControls_SourceCollectionId",
                table: "PageControls",
                column: "SourceCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_PageControlValidators_PageControlId",
                table: "PageControlValidators",
                column: "PageControlId");

            migrationBuilder.CreateIndex(
                name: "IX_PageControlValidators_ValidatorId",
                table: "PageControlValidators",
                column: "ValidatorId");

            migrationBuilder.CreateIndex(
                name: "IX_PageParameters_PageCategoryParameterId",
                table: "PageParameters",
                column: "PageCategoryParameterId");

            migrationBuilder.CreateIndex(
                name: "IX_PageParameters_PageId",
                table: "PageParameters",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_PageRoutes_PageId",
                table: "PageRoutes",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_DomainId",
                table: "Pages",
                column: "DomainId");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_PageCategoryId",
                table: "Pages",
                column: "PageCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_ResourceActionId",
                table: "Pages",
                column: "ResourceActionId");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_ResourceId",
                table: "Pages",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_SourceCollectionId",
                table: "Pages",
                column: "SourceCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_TenantId",
                table: "Pages",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceActions_ResourceId",
                table: "ResourceActions",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceActions_TenantId",
                table: "ResourceActions",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceCollectionConditions_DomainEntityCollectionId",
                table: "ResourceCollectionConditions",
                column: "DomainEntityCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceCollections_ResourceId",
                table: "ResourceCollections",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_DomainId",
                table: "Resources",
                column: "DomainId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Apps");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "ControlValidators");

            migrationBuilder.DropTable(
                name: "CustomFields");

            migrationBuilder.DropTable(
                name: "CustomTexts");

            migrationBuilder.DropTable(
                name: "NavigationPages");

            migrationBuilder.DropTable(
                name: "PageControlValidators");

            migrationBuilder.DropTable(
                name: "PageParameters");

            migrationBuilder.DropTable(
                name: "PageRoutes");

            migrationBuilder.DropTable(
                name: "ResourceCollectionConditions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "NavigationGroups");

            migrationBuilder.DropTable(
                name: "PageControls");

            migrationBuilder.DropTable(
                name: "Validators");

            migrationBuilder.DropTable(
                name: "PageCategoryParameters");

            migrationBuilder.DropTable(
                name: "Controls");

            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropTable(
                name: "ResourceCollections");

            migrationBuilder.DropTable(
                name: "PageCategories");

            migrationBuilder.DropTable(
                name: "ResourceActions");

            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropTable(
                name: "Tenants");

            migrationBuilder.DropTable(
                name: "Domains");
        }
    }
}
