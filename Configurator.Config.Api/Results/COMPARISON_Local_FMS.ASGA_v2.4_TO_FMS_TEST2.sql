-- NO ADDED TABLES
 
-- ADDED COLUMNS :
ALTER TABLE [FMS_TEST2].Asts.AssetsConfig ADD [ResourceTypeCodeMinLength] int NOT NULL;
ALTER TABLE [FMS_TEST2].Asts.AssetsConfig ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.AssetsConfig ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.AssetsConfig ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.AssetsConfig ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.AssetsConfig ADD [PartTypeCodeMinLength] int NOT NULL;
ALTER TABLE [FMS_TEST2].Asts.Attachments ADD [Description] nvarchar(300) NULL;
ALTER TABLE [FMS_TEST2].Asts.Attachments ADD [EntityType] varchar(70) NULL;
ALTER TABLE [FMS_TEST2].Asts.Attachments ADD [FileType] varchar(50) NULL;
ALTER TABLE [FMS_TEST2].Asts.Attachments ADD [FilePath] nvarchar(300) NULL;
ALTER TABLE [FMS_TEST2].Asts.Attachments ADD [EntityId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Asts.Attachments ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.Attachments ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.Attachments ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.Attachments ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.Cities ADD [Name] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Asts.Cities ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.Cities ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.Cities ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.Cities ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.Contracts ADD [Code] varchar(50) NULL;
ALTER TABLE [FMS_TEST2].Asts.Contracts ADD [Name] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Asts.Contracts ADD [PartyId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Asts.Contracts ADD [EndDate] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.Contracts ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.Contracts ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.Contracts ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.Contracts ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.CustomFields ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.CustomFields ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.CustomFields ADD [EntityId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Asts.CustomFields ADD [EntityType] varchar(50) NULL;
ALTER TABLE [FMS_TEST2].Asts.CustomFields ADD [Name] varchar(150) NULL;
ALTER TABLE [FMS_TEST2].Asts.CustomFields ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.CustomFields ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.CustomFields ADD [Value] nvarchar(max) NULL;
ALTER TABLE [FMS_TEST2].Asts.FolderWorkOrderTypes ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.FolderWorkOrderTypes ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.FolderWorkOrderTypes ADD [ResourceTypeFolderId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Asts.FolderWorkOrderTypes ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.FolderWorkOrderTypes ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.FolderWorkOrderTypes ADD [WorkOrderTypeId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Asts.ItemFolders ADD [Name] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.ItemFolders ADD [Chain] text NULL;
ALTER TABLE [FMS_TEST2].Asts.ItemFolders ADD [NameChain] ntext NULL;
ALTER TABLE [FMS_TEST2].Asts.ItemFolders ADD [ParentId] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.ItemFolders ADD [PartyId] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.ItemFolders ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.ItemFolders ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.ItemFolders ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.ItemFolders ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.ItemItemFolders ADD [ItemId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Asts.ItemItemFolders ADD [ItemFolderId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Asts.ItemItemFolders ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.ItemItemFolders ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.ItemItemFolders ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.ItemItemFolders ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.Items ADD [Code] varchar(50) NULL;
ALTER TABLE [FMS_TEST2].Asts.Items ADD [Name] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Asts.Items ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.Items ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.Items ADD [StatusId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Asts.Items ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.Items ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.Items ADD [ResourceTypeId] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.Items ADD [LocationId] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.Items ADD [PartyId] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.Items ADD [SerialNo] varchar(50) NULL;
ALTER TABLE [FMS_TEST2].Asts.Items ADD [PurchaseDate] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.Items ADD [Note] nvarchar(100) NULL;
ALTER TABLE [FMS_TEST2].Asts.Items ADD [CodePattern] varchar(50) NULL;
ALTER TABLE [FMS_TEST2].Asts.Items ADD [WarrantyEndDate] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Asts.Items ADD [ManufacturerName] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Asts.Items ADD [SupplierId] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.Items ADD [WarrantyStartDate] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.Items ADD [ImportId] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.Items ADD [InstallationDate] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.Items ADD [SupplierName] nvarchar(200) NULL;
ALTER TABLE [FMS_TEST2].Asts.Items ADD [WarrantyStatus] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Asts.Items ADD [ContractId] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.ItemStates ADD [ChangeType] int NOT NULL;
ALTER TABLE [FMS_TEST2].Asts.ItemStates ADD [ReferenceId] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.ItemStates ADD [Message] nvarchar(max) NULL;
ALTER TABLE [FMS_TEST2].Asts.ItemStates ADD [ItemId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Asts.ItemStates ADD [UserName] nvarchar(250) NULL;
ALTER TABLE [FMS_TEST2].Asts.ItemStates ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.ItemStates ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.ItemStates ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.ItemStates ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.Localizables ADD [EntityId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Asts.Localizables ADD [EntityType] varchar(100) NOT NULL;
ALTER TABLE [FMS_TEST2].Asts.Localizables ADD [LocaleId] int NOT NULL;
ALTER TABLE [FMS_TEST2].Asts.Localizables ADD [ColumnName] varchar(70) NULL;
ALTER TABLE [FMS_TEST2].Asts.Localizables ADD [Value] nvarchar(max) NULL;
ALTER TABLE [FMS_TEST2].Asts.Localizables ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.Localizables ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.Localizables ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.Localizables ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.Locations ADD [Name] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Asts.Locations ADD [Code] varchar(50) NULL;
ALTER TABLE [FMS_TEST2].Asts.Locations ADD [Chain] varchar(max) NULL;
ALTER TABLE [FMS_TEST2].Asts.Locations ADD [NameChain] nvarchar(max) NULL;
ALTER TABLE [FMS_TEST2].Asts.Locations ADD [ParentId] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.Locations ADD [SiteId] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.Locations ADD [PartyId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Asts.Locations ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.Locations ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.Locations ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.Locations ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.Parties ADD [Name] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Asts.Parties ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.Parties ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.Parties ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.Parties ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.Parties ADD [Code] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.Parties ADD [IsDeleted] bit NULL;
ALTER TABLE [FMS_TEST2].Asts.PartTypes ADD [Name] nvarchar(150) NOT NULL;
ALTER TABLE [FMS_TEST2].Asts.PartTypes ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.PartTypes ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.PartTypes ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.PartTypes ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.PartTypes ADD [Code] varchar(50) NULL;
ALTER TABLE [FMS_TEST2].Asts.PartTypes ADD [BrandName] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Asts.PartTypes ADD [Description] nvarchar(max) NULL;
ALTER TABLE [FMS_TEST2].Asts.PartTypes ADD [ResourceComponentId] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.PartTypes ADD [CodePattern] varchar(50) NULL;
ALTER TABLE [FMS_TEST2].Asts.ResourceComponents ADD [Name] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Asts.ResourceComponents ADD [ResourceTypeFolderId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Asts.ResourceComponents ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.ResourceComponents ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.ResourceComponents ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.ResourceComponents ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.ResourceTypeFolders ADD [Name] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Asts.ResourceTypeFolders ADD [ParentId] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.ResourceTypeFolders ADD [Chain] varchar(max) NULL;
ALTER TABLE [FMS_TEST2].Asts.ResourceTypeFolders ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.ResourceTypeFolders ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.ResourceTypeFolders ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.ResourceTypeFolders ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.ResourceTypeFolders ADD [NameChain] nvarchar(max) NULL;
ALTER TABLE [FMS_TEST2].Asts.ResourceTypeFolders ADD [PartyId] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.ResourceTypePartTypes ADD [ResourceTypeId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Asts.ResourceTypePartTypes ADD [PartTypeId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Asts.ResourceTypePartTypes ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.ResourceTypePartTypes ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.ResourceTypePartTypes ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.ResourceTypePartTypes ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.ResourceTypeResourceTypeFolders ADD [FolderId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Asts.ResourceTypeResourceTypeFolders ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.ResourceTypeResourceTypeFolders ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.ResourceTypeResourceTypeFolders ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.ResourceTypeResourceTypeFolders ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.ResourceTypeResourceTypeFolders ADD [ResourceTypeId] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.ResourceTypes ADD [Code] varchar(50) NULL;
ALTER TABLE [FMS_TEST2].Asts.ResourceTypes ADD [Name] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Asts.ResourceTypes ADD [ProductionYear] int NULL;
ALTER TABLE [FMS_TEST2].Asts.ResourceTypes ADD [BrandName] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Asts.ResourceTypes ADD [Price] decimal(10,3) NULL;
ALTER TABLE [FMS_TEST2].Asts.ResourceTypes ADD [Description] nvarchar(max) NULL;
ALTER TABLE [FMS_TEST2].Asts.ResourceTypes ADD [PartNumber] varchar(50) NULL;
ALTER TABLE [FMS_TEST2].Asts.ResourceTypes ADD [ImagePath] nvarchar(250) NULL;
ALTER TABLE [FMS_TEST2].Asts.ResourceTypes ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.ResourceTypes ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.ResourceTypes ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.ResourceTypes ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.ResourceTypes ADD [CodePattern] varchar(100) NULL;
ALTER TABLE [FMS_TEST2].Asts.ResourceTypes ADD [IsSerialized] bit NOT NULL;
ALTER TABLE [FMS_TEST2].Asts.ResourceTypes ADD [CountryName] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Asts.Sites ADD [Name] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Asts.Sites ADD [Address] nvarchar(max) NULL;
ALTER TABLE [FMS_TEST2].Asts.Sites ADD [PartyId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Asts.Sites ADD [Longitude] decimal(18,7) NULL;
ALTER TABLE [FMS_TEST2].Asts.Sites ADD [Latitude] decimal(18,7) NULL;
ALTER TABLE [FMS_TEST2].Asts.Sites ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.Sites ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.Sites ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.Sites ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.Sites ADD [CityId] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.Sites ADD [IsDeleted] bit NULL;
ALTER TABLE [FMS_TEST2].Asts.Specifications ADD [Code] nvarchar(50) NULL;
ALTER TABLE [FMS_TEST2].Asts.Specifications ADD [Name] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Asts.Specifications ADD [ResourceTypeId] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.Specifications ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.Specifications ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.Specifications ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.Specifications ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.Statuses ADD [Name] nvarchar(50) NULL;
ALTER TABLE [FMS_TEST2].Asts.Statuses ADD [EntityType] varchar(70) NULL;
ALTER TABLE [FMS_TEST2].Asts.Statuses ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.Statuses ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.Statuses ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.Statuses ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.StockReservations ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.StockReservations ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.StockReservations ADD [DispatchId] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.StockReservations ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.StockReservations ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.StockReservations ADD [WorkOrderId] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.Stocks ADD [ResourceTypeId] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.Stocks ADD [PartTypeId] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.Stocks ADD [Amount] decimal(18,3) NOT NULL;
ALTER TABLE [FMS_TEST2].Asts.Stocks ADD [StatusId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Asts.Stocks ADD [WarehouseId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Asts.Stocks ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.Stocks ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.Stocks ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.Stocks ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.Stocks ADD [StockReservationId] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.Stocks ADD [StockTransactionItemId] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.Stocks ADD [TotalCost] decimal(18,3) NOT NULL;
ALTER TABLE [FMS_TEST2].Asts.Stocks ADD [UnitCost] decimal(18,3) NOT NULL;
ALTER TABLE [FMS_TEST2].Asts.StockTransactionItems ADD [PartTypeId] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.StockTransactionItems ADD [ResourceTypeId] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.StockTransactionItems ADD [Amount] decimal(18,3) NOT NULL;
ALTER TABLE [FMS_TEST2].Asts.StockTransactionItems ADD [PostTransactionLevel] decimal(18,3) NOT NULL;
ALTER TABLE [FMS_TEST2].Asts.StockTransactionItems ADD [StockTransactionId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Asts.StockTransactionItems ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.StockTransactionItems ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.StockTransactionItems ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.StockTransactionItems ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.StockTransactionItems ADD [TotalCost] decimal(18,3) NOT NULL;
ALTER TABLE [FMS_TEST2].Asts.StockTransactionItems ADD [UnitCost] decimal(18,3) NOT NULL;
ALTER TABLE [FMS_TEST2].Asts.StockTransactions ADD [StockTransactionTypeId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Asts.StockTransactions ADD [WarehouseId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Asts.StockTransactions ADD [RecieptId] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.StockTransactions ADD [PurchaseOrderId] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.StockTransactions ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.StockTransactions ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.StockTransactions ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.StockTransactions ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.StockTransactions ADD [WorkOrderId] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.StockTransactions ADD [SequenceNumber] varchar(50) NULL;
ALTER TABLE [FMS_TEST2].Asts.StockTransactions ADD [ActualTime] datetime NOT NULL;
ALTER TABLE [FMS_TEST2].Asts.StockTransactions ADD [Notes] nvarchar(max) NULL;
ALTER TABLE [FMS_TEST2].Asts.StockTransactions ADD [PurchaseOrderSequenceNumber] varchar(50) NULL;
ALTER TABLE [FMS_TEST2].Asts.StockTransactions ADD [AgentId] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.StockTransactions ADD [AgentName] nvarchar(200) NULL;
ALTER TABLE [FMS_TEST2].Asts.StockTransactionTypes ADD [Direction] bit NOT NULL;
ALTER TABLE [FMS_TEST2].Asts.StockTransactionTypes ADD [Name] varchar(150) NULL;
ALTER TABLE [FMS_TEST2].Asts.StockTransactionTypes ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.StockTransactionTypes ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.StockTransactionTypes ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.StockTransactionTypes ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.Warehouses ADD [Name] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Asts.Warehouses ADD [Address] nvarchar(max) NULL;
ALTER TABLE [FMS_TEST2].Asts.Warehouses ADD [PartyId] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.Warehouses ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.Warehouses ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Asts.Warehouses ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Asts.Warehouses ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Auth.DefaultRoles ADD [UserType] int NOT NULL;
ALTER TABLE [FMS_TEST2].Auth.DefaultRoles ADD [RoleId] bigint NULL;
ALTER TABLE [FMS_TEST2].Auth.DefaultRoles ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Auth.DefaultRoles ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Auth.DefaultRoles ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Auth.DefaultRoles ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Auth.Domains ADD [Name] varchar(50) NULL;
ALTER TABLE [FMS_TEST2].Auth.Domains ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Auth.Domains ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Auth.Domains ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Auth.Domains ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Auth.ResourceActions ADD [Name] varchar(150) NULL;
ALTER TABLE [FMS_TEST2].Auth.ResourceActions ADD [ResourceId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Auth.ResourceActions ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Auth.ResourceActions ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Auth.ResourceActions ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Auth.ResourceActions ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Auth.Resources ADD [Name] varchar(150) NULL;
ALTER TABLE [FMS_TEST2].Auth.Resources ADD [DomainId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Auth.Resources ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Auth.Resources ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Auth.Resources ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Auth.Resources ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Auth.RoleResourceActions ADD [RoleId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Auth.RoleResourceActions ADD [ResourceActionId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Auth.RoleResourceActions ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Auth.RoleResourceActions ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Auth.RoleResourceActions ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Auth.RoleResourceActions ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Auth.RoleResources ADD [RoleId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Auth.RoleResources ADD [ResourceId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Auth.RoleResources ADD [CanInsert] bit NOT NULL;
ALTER TABLE [FMS_TEST2].Auth.RoleResources ADD [CanUpdate] bit NOT NULL;
ALTER TABLE [FMS_TEST2].Auth.RoleResources ADD [CanDelete] bit NOT NULL;
ALTER TABLE [FMS_TEST2].Auth.RoleResources ADD [CanViewDetails] bit NOT NULL;
ALTER TABLE [FMS_TEST2].Auth.RoleResources ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Auth.RoleResources ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Auth.RoleResources ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Auth.RoleResources ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Auth.Roles ADD [Name] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Auth.Roles ADD [TenantDomainId] bigint NULL;
ALTER TABLE [FMS_TEST2].Auth.Roles ADD [Description] nvarchar(300) NULL;
ALTER TABLE [FMS_TEST2].Auth.Roles ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Auth.Roles ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Auth.Roles ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Auth.Roles ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Auth.Roles ADD [IsUserRole] bit NOT NULL;
ALTER TABLE [FMS_TEST2].Auth.Roles ADD [TenantAppId] bigint NULL;
ALTER TABLE [FMS_TEST2].Auth.TenantApps ADD [Name] varchar(150) NULL;
ALTER TABLE [FMS_TEST2].Auth.TenantApps ADD [DisplayName] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Auth.TenantApps ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Auth.TenantApps ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Auth.TenantApps ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Auth.TenantApps ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Auth.TenantAppUsers ADD [UserId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Auth.TenantAppUsers ADD [TenantAppId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Auth.TenantAppUsers ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Auth.TenantAppUsers ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Auth.TenantAppUsers ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Auth.TenantAppUsers ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Auth.TenantDomains ADD [TenantId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Auth.TenantDomains ADD [DomainId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Auth.TenantDomains ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Auth.TenantDomains ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Auth.TenantDomains ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Auth.TenantDomains ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Auth.Tenants ADD [Code] varchar(100) NOT NULL;
ALTER TABLE [FMS_TEST2].Auth.Tenants ADD [Name] nvarchar(200) NOT NULL;
ALTER TABLE [FMS_TEST2].Auth.Tenants ADD [Description] ntext NULL;
ALTER TABLE [FMS_TEST2].Auth.Tenants ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Auth.Tenants ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Auth.Tenants ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Auth.Tenants ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Auth.Tenants ADD [Logo] nvarchar(300) NULL;
ALTER TABLE [FMS_TEST2].Auth.UserParties ADD [UserId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Auth.UserParties ADD [PartyId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Auth.UserParties ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Auth.UserParties ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Auth.UserParties ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Auth.UserParties ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Auth.UserRoles ADD [UserId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Auth.UserRoles ADD [RoleId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Auth.UserRoles ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Auth.UserRoles ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Auth.UserRoles ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Auth.UserRoles ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Auth.Users ADD [DisplayName] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Auth.Users ADD [LogonName] varchar(50) NULL;
ALTER TABLE [FMS_TEST2].Auth.Users ADD [Password] varchar(100) NULL;
ALTER TABLE [FMS_TEST2].Auth.Users ADD [CustomerId] bigint NULL;
ALTER TABLE [FMS_TEST2].Auth.Users ADD [UserType] int NOT NULL;
ALTER TABLE [FMS_TEST2].Auth.Users ADD [FirstName] nvarchar(100) NULL;
ALTER TABLE [FMS_TEST2].Auth.Users ADD [LastName] nvarchar(100) NULL;
ALTER TABLE [FMS_TEST2].Auth.Users ADD [Photo] nvarchar(300) NULL;
ALTER TABLE [FMS_TEST2].Auth.Users ADD [Gender] bit NULL;
ALTER TABLE [FMS_TEST2].Auth.Users ADD [BirthDate] datetime NULL;
ALTER TABLE [FMS_TEST2].Auth.Users ADD [IsActive] bit NOT NULL;
ALTER TABLE [FMS_TEST2].Auth.Users ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Auth.Users ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Auth.Users ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Auth.Users ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Auth.Users ADD [TenantId] bigint NULL;
ALTER TABLE [FMS_TEST2].Auth.Users ADD [Email] varchar(200) NULL;
ALTER TABLE [FMS_TEST2].Auth.Users ADD [IsDeleted] bit NULL;
ALTER TABLE [FMS_TEST2].Auth.Users ADD [CustomerLogo] nvarchar(max) NULL;
ALTER TABLE [FMS_TEST2].Auth.Users ADD [ProfilePicture] nvarchar(max) NULL;
ALTER TABLE [FMS_TEST2].Auth.Users ADD [PersonId] bigint NULL;
ALTER TABLE [FMS_TEST2].dbo.Versions ADD [DbVersion] varchar(10) NULL;
ALTER TABLE [FMS_TEST2].dbo.Versions ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.Versions ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].dbo.Versions ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.Versions ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.Agents ADD [UserId] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.Agents ADD [JobRoleId] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.Agents ADD [FirstName] nvarchar(100) NULL;
ALTER TABLE [FMS_TEST2].Legl.Agents ADD [LastName] nvarchar(100) NULL;
ALTER TABLE [FMS_TEST2].Legl.Agents ADD [Signature] nvarchar(max) NULL;
ALTER TABLE [FMS_TEST2].Legl.Agents ADD [TenantAppId] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.Agents ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Legl.Agents ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Legl.Agents ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.Agents ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.ApprovalPersonTypes ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.ApprovalPersonTypes ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Legl.ApprovalPersonTypes ADD [Name] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Legl.ApprovalPersonTypes ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.ApprovalPersonTypes ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Legl.ContractResourceTypes ADD [ContractId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Legl.ContractResourceTypes ADD [ResourceTypeId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Legl.ContractResourceTypes ADD [SupplierAgentId] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.ContractResourceTypes ADD [ItemPriorityId] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.ContractResourceTypes ADD [SupplierName] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Legl.ContractResourceTypes ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Legl.ContractResourceTypes ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.ContractResourceTypes ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Legl.ContractResourceTypes ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.ContractResourceTypes ADD [FixDelayFinePerDay] decimal(18,4) NULL;
ALTER TABLE [FMS_TEST2].Legl.ContractResourceTypes ADD [CheckupDelayFinePerDay] decimal(18,4) NULL;
ALTER TABLE [FMS_TEST2].Legl.ContractResourceTypes ADD [CheckupPrice] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.ContractResourceTypes ADD [FixPrice] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.ContractResourceTypes ADD [AgentSupplierId] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.ContractResourceTypes ADD [AgentName] nvarchar(250) NULL;
ALTER TABLE [FMS_TEST2].Legl.Contracts ADD [Code] nvarchar(50) NULL;
ALTER TABLE [FMS_TEST2].Legl.Contracts ADD [Name] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Legl.Contracts ADD [PartyId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Legl.Contracts ADD [StartDate] datetime NOT NULL;
ALTER TABLE [FMS_TEST2].Legl.Contracts ADD [EndDate] datetime NULL;
ALTER TABLE [FMS_TEST2].Legl.Contracts ADD [Description] nvarchar(max) NULL;
ALTER TABLE [FMS_TEST2].Legl.Contracts ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Legl.Contracts ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.Contracts ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Legl.Contracts ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.Contracts ADD [MaximumFineDays] int NOT NULL;
ALTER TABLE [FMS_TEST2].Legl.Fines ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Legl.Fines ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.Fines ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Legl.Fines ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.Fines ADD [SequenceNumber] varchar(50) NULL;
ALTER TABLE [FMS_TEST2].Legl.Fines ADD [DaysCount] int NOT NULL;
ALTER TABLE [FMS_TEST2].Legl.Fines ADD [DailyValue] decimal(18,2) NOT NULL;
ALTER TABLE [FMS_TEST2].Legl.Fines ADD [TotalValue] decimal(18,2) NOT NULL;
ALTER TABLE [FMS_TEST2].Legl.Fines ADD [StatusId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Legl.Fines ADD [WorkOrderIsClosed] bit NOT NULL;
ALTER TABLE [FMS_TEST2].Legl.Fines ADD [Reasons] nvarchar(250) NULL;
ALTER TABLE [FMS_TEST2].Legl.Fines ADD [WorkOrderDate] datetime NULL;
ALTER TABLE [FMS_TEST2].Legl.Fines ADD [SiteId] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.Fines ADD [ContractId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Legl.Fines ADD [WorkOrderId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Legl.Fines ADD [ResourceTypeId] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.ItemPriorities ADD [Name] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Legl.ItemPriorities ADD [ContractId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Legl.ItemPriorities ADD [FixDelayFinePerDay] decimal(18,4) NOT NULL;
ALTER TABLE [FMS_TEST2].Legl.ItemPriorities ADD [CheckupDelayFinePerDay] decimal(18,4) NULL;
ALTER TABLE [FMS_TEST2].Legl.ItemPriorities ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Legl.ItemPriorities ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.ItemPriorities ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Legl.ItemPriorities ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.ItemPriorities ADD [CheckupPrice] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.ItemPriorities ADD [FixPrice] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.Localizables ADD [EntityId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Legl.Localizables ADD [EntityType] varchar(100) NOT NULL;
ALTER TABLE [FMS_TEST2].Legl.Localizables ADD [LocaleId] int NOT NULL;
ALTER TABLE [FMS_TEST2].Legl.Localizables ADD [ColumnName] varchar(70) NULL;
ALTER TABLE [FMS_TEST2].Legl.Localizables ADD [Value] nvarchar(max) NULL;
ALTER TABLE [FMS_TEST2].Legl.Localizables ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Legl.Localizables ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.Localizables ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Legl.Localizables ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.Parties ADD [Code] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.Parties ADD [Name] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Legl.Parties ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Legl.Parties ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.Parties ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Legl.Parties ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.RequiredApprovals ADD [WorkOrderId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Legl.RequiredApprovals ADD [SequenceNumber] varchar(50) NULL;
ALTER TABLE [FMS_TEST2].Legl.RequiredApprovals ADD [IsChecked] bit NOT NULL;
ALTER TABLE [FMS_TEST2].Legl.RequiredApprovals ADD [JobRoleId] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.RequiredApprovals ADD [Description] nvarchar(max) NULL;
ALTER TABLE [FMS_TEST2].Legl.RequiredApprovals ADD [AgentId] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.RequiredApprovals ADD [ContactId] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.RequiredApprovals ADD [SiteId] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.RequiredApprovals ADD [UseManager] bit NOT NULL;
ALTER TABLE [FMS_TEST2].Legl.RequiredApprovals ADD [UserName] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Legl.RequiredApprovals ADD [WorkOrderApproverId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Legl.RequiredApprovals ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Legl.RequiredApprovals ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.RequiredApprovals ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Legl.RequiredApprovals ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.RequiredApprovals ADD [PersonId] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.RequiredApprovals ADD [PersonType] int NULL;
ALTER TABLE [FMS_TEST2].Legl.RequiredApprovals ADD [ApproveTime] datetime NULL;
ALTER TABLE [FMS_TEST2].Legl.ResourceTypes ADD [Code] varchar(50) NULL;
ALTER TABLE [FMS_TEST2].Legl.ResourceTypes ADD [Name] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Legl.ResourceTypes ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Legl.ResourceTypes ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.ResourceTypes ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Legl.ResourceTypes ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.WorkOrderApprovalPersons ADD [WorkOrderApprovalRuleId] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.WorkOrderApprovalPersons ADD [JobRoleId] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.WorkOrderApprovalPersons ADD [AgentId] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.WorkOrderApprovalPersons ADD [UseBranchManager] bit NULL;
ALTER TABLE [FMS_TEST2].Legl.WorkOrderApprovalPersons ADD [ContactId] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.WorkOrderApprovalPersons ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Legl.WorkOrderApprovalPersons ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.WorkOrderApprovalPersons ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Legl.WorkOrderApprovalPersons ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.WorkOrderApprovalPersons ADD [PersonType] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.WorkOrderApprovalPersons ADD [Identifier] nvarchar(50) NULL;
ALTER TABLE [FMS_TEST2].Legl.WorkOrderApprovalPersons ADD [Name] nvarchar(max) NULL;
ALTER TABLE [FMS_TEST2].Legl.WorkOrderApprovalPersons ADD [ApprovalPersonTypeId] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.WorkOrderApprovalRules ADD [SiteId] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.WorkOrderApprovalRules ADD [WorkOrderTypeId] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.WorkOrderApprovalRules ADD [TypeWithChildren] bit NOT NULL;
ALTER TABLE [FMS_TEST2].Legl.WorkOrderApprovalRules ADD [WorkOrderCategoryId] int NULL;
ALTER TABLE [FMS_TEST2].Legl.WorkOrderApprovalRules ADD [ResourceFolderId] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.WorkOrderApprovalRules ADD [FolderWithChildren] bit NOT NULL;
ALTER TABLE [FMS_TEST2].Legl.WorkOrderApprovalRules ADD [ResourceTypeId] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.WorkOrderApprovalRules ADD [WarrantyStatus] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.WorkOrderApprovalRules ADD [PartyId] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.WorkOrderApprovalRules ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Legl.WorkOrderApprovalRules ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.WorkOrderApprovalRules ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Legl.WorkOrderApprovalRules ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Legl.WorkOrderApprovalRules ADD [ApplyOrder] int NOT NULL;
ALTER TABLE [FMS_TEST2].Legl.WorkOrderApprovalRules ADD [IsActive] bit NOT NULL;
ALTER TABLE [FMS_TEST2].Legl.WorkOrderApprovalRules ADD [Description] nvarchar(200) NULL;
ALTER TABLE [FMS_TEST2].Main.Agents ADD [FirstName] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Main.Agents ADD [LastName] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Main.Agents ADD [UserId] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.Agents ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.Agents ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.Agents ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.Agents ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.Attachments ADD [EntityType] nvarchar(300) NULL;
ALTER TABLE [FMS_TEST2].Main.Attachments ADD [FileType] nvarchar(300) NULL;
ALTER TABLE [FMS_TEST2].Main.Attachments ADD [FilePath] nvarchar(300) NULL;
ALTER TABLE [FMS_TEST2].Main.Attachments ADD [EntityId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Main.Attachments ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.Attachments ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.Attachments ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.Attachments ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.CheckUpFields ADD [Name] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Main.CheckUpFields ADD [FieldType] int NOT NULL;
ALTER TABLE [FMS_TEST2].Main.CheckUpFields ADD [Options] nvarchar(max) NULL;
ALTER TABLE [FMS_TEST2].Main.CheckUpFields ADD [CheckUpTypeId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Main.CheckUpFields ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.CheckUpFields ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.CheckUpFields ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.CheckUpFields ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.CheckUpItems ADD [ScheduleItemId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Main.CheckUpItems ADD [CheckupId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Main.CheckUpItems ADD [WorkOrderId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Main.CheckUpItems ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.CheckUpItems ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.CheckUpItems ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.CheckUpItems ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.CheckUps ADD [StartDate] datetime NOT NULL;
ALTER TABLE [FMS_TEST2].Main.CheckUps ADD [EndDate] datetime NOT NULL;
ALTER TABLE [FMS_TEST2].Main.CheckUps ADD [StatusId] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.CheckUps ADD [ActualStartDate] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.CheckUps ADD [ActualEndDate] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.CheckUps ADD [ScheduleId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Main.CheckUps ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.CheckUps ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.CheckUps ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.CheckUps ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.CheckUps ADD [IsCreateAutomaticWorkOrder] bit NOT NULL;
ALTER TABLE [FMS_TEST2].Main.CheckUpTypes ADD [Name] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Main.CheckUpTypes ADD [Description] nvarchar(max) NULL;
ALTER TABLE [FMS_TEST2].Main.CheckUpTypes ADD [ResourceTypeId] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.CheckUpTypes ADD [ResourceTypeFolderId] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.CheckUpTypes ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.CheckUpTypes ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.CheckUpTypes ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.CheckUpTypes ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.CheckUpTypes ADD [resourceTypeName] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Main.CheckUpTypes ADD [resourceTypeFolderName] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Main.CheckUpValues ADD [CheckUpItemId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Main.CheckUpValues ADD [CheckUpFieldId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Main.CheckUpValues ADD [Value] nvarchar(max) NULL;
ALTER TABLE [FMS_TEST2].Main.CheckUpValues ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.CheckUpValues ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.CheckUpValues ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.CheckUpValues ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.CustomFields ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.CustomFields ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.CustomFields ADD [EntityId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Main.CustomFields ADD [EntityType] varchar(50) NULL;
ALTER TABLE [FMS_TEST2].Main.CustomFields ADD [Name] varchar(150) NULL;
ALTER TABLE [FMS_TEST2].Main.CustomFields ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.CustomFields ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.CustomFields ADD [Value] nvarchar(max) NULL;
ALTER TABLE [FMS_TEST2].Main.DispatchRuleAgents ADD [AgentId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Main.DispatchRuleAgents ADD [DispatchRuleId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Main.DispatchRuleAgents ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.DispatchRuleAgents ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.DispatchRuleAgents ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.DispatchRuleAgents ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.DispatchRuleConditions ADD [Property] int NOT NULL;
ALTER TABLE [FMS_TEST2].Main.DispatchRuleConditions ADD [DispatchRuleId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Main.DispatchRuleConditions ADD [Operator] varchar(50) NULL;
ALTER TABLE [FMS_TEST2].Main.DispatchRuleConditions ADD [Value] ntext NULL;
ALTER TABLE [FMS_TEST2].Main.DispatchRuleConditions ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.DispatchRuleConditions ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.DispatchRuleConditions ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.DispatchRuleConditions ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.DispatchRules ADD [Name] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Main.DispatchRules ADD [PartyId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Main.DispatchRules ADD [StartDate] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.DispatchRules ADD [EndDate] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.DispatchRules ADD [ApplyOrder] int NOT NULL;
ALTER TABLE [FMS_TEST2].Main.DispatchRules ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.DispatchRules ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.DispatchRules ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.DispatchRules ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.DispatchRules ADD [IsActive] bit NOT NULL;
ALTER TABLE [FMS_TEST2].Main.EquipmentTypes ADD [Name] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Main.EquipmentTypes ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.EquipmentTypes ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.EquipmentTypes ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.EquipmentTypes ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.EquipmentTypes ADD [BrandName] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Main.EquipmentTypes ADD [Code] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Main.FaultTypes ADD [ResourceTypeId] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.FaultTypes ADD [Name] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Main.FaultTypes ADD [Description] ntext NULL;
ALTER TABLE [FMS_TEST2].Main.FaultTypes ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.FaultTypes ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.FaultTypes ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.FaultTypes ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.Localizables ADD [EntityId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Main.Localizables ADD [EntityType] varchar(100) NOT NULL;
ALTER TABLE [FMS_TEST2].Main.Localizables ADD [LocaleId] int NOT NULL;
ALTER TABLE [FMS_TEST2].Main.Localizables ADD [ColumnName] varchar(70) NULL;
ALTER TABLE [FMS_TEST2].Main.Localizables ADD [Value] nvarchar(max) NULL;
ALTER TABLE [FMS_TEST2].Main.Localizables ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.Localizables ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.Localizables ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.Localizables ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.MaintenanceConfig ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.MaintenanceConfig ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.MaintenanceConfig ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.MaintenanceConfig ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.MaintenanceConfig ADD [WorkOrderSequencePattern] varchar(255) NULL;
ALTER TABLE [FMS_TEST2].Main.MaintenanceConfig ADD [WorkOrderSerialMinLength] int NULL;
ALTER TABLE [FMS_TEST2].Main.Parties ADD [Name] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Main.Parties ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.Parties ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.Parties ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.Parties ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.Parties ADD [Code] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.Parties ADD [IsDeleted] bit NULL;
ALTER TABLE [FMS_TEST2].Main.PartyWorkOrderTypes ADD [WorkOrderTypeId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Main.PartyWorkOrderTypes ADD [PartyId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Main.PartyWorkOrderTypes ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.PartyWorkOrderTypes ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.PartyWorkOrderTypes ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.PartyWorkOrderTypes ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.ScheduleAgent ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.ScheduleAgent ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.ScheduleAgent ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.ScheduleAgent ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.ScheduleAgent ADD [ScheduleId] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.ScheduleAgent ADD [AgentId] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.ScheduleItems ADD [ScheduleId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Main.ScheduleItems ADD [ItemId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Main.ScheduleItems ADD [Code] varchar(50) NULL;
ALTER TABLE [FMS_TEST2].Main.ScheduleItems ADD [Name] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Main.ScheduleItems ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.ScheduleItems ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.ScheduleItems ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.ScheduleItems ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.ScheduleItems ADD [SiteId] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.ScheduleItems ADD [SerialNo] varchar(50) NULL;
ALTER TABLE [FMS_TEST2].Main.ScheduleItems ADD [ContractId] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.Schedules ADD [SequenceNumber] varchar(50) NULL;
ALTER TABLE [FMS_TEST2].Main.Schedules ADD [Name] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Main.Schedules ADD [RepeatitionIntervalType] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.Schedules ADD [RepeatitionInterval] int NOT NULL;
ALTER TABLE [FMS_TEST2].Main.Schedules ADD [LocationId] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.Schedules ADD [StartDate] datetime NOT NULL;
ALTER TABLE [FMS_TEST2].Main.Schedules ADD [EndDate] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.Schedules ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.Schedules ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.Schedules ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.Schedules ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.Schedules ADD [CheckUpTypeId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Main.Schedules ADD [PartyId] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.Schedules ADD [LocationOnly] bit NULL;
ALTER TABLE [FMS_TEST2].Main.Schedules ADD [Dates] nvarchar(max) NULL;
ALTER TABLE [FMS_TEST2].Main.Schedules ADD [LocationName] nvarchar(max) NULL;
ALTER TABLE [FMS_TEST2].Main.Schedules ADD [GracePeriod] int NULL;
ALTER TABLE [FMS_TEST2].Main.Schedules ADD [ScheduleType] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.Schedules ADD [AutomaticWorkOrder] bit NULL;
ALTER TABLE [FMS_TEST2].Main.Schedules ADD [SelectSpecificItems] bit NULL;
ALTER TABLE [FMS_TEST2].Main.Sites ADD [Name] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Main.Sites ADD [Address] ntext NULL;
ALTER TABLE [FMS_TEST2].Main.Sites ADD [Longitude] decimal(18,7) NULL;
ALTER TABLE [FMS_TEST2].Main.Sites ADD [Latitude] decimal(18,7) NULL;
ALTER TABLE [FMS_TEST2].Main.Sites ADD [PartyId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Main.Sites ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.Sites ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.Sites ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.Sites ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.Sites ADD [CityId] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.Sites ADD [IsDeleted] bit NULL;
ALTER TABLE [FMS_TEST2].Main.Statuses ADD [Name] nvarchar(50) NULL;
ALTER TABLE [FMS_TEST2].Main.Statuses ADD [EntityType] varchar(70) NULL;
ALTER TABLE [FMS_TEST2].Main.Statuses ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.Statuses ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.Statuses ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.Statuses ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.TaskTemplates ADD [Code] varchar(50) NOT NULL;
ALTER TABLE [FMS_TEST2].Main.TaskTemplates ADD [Name] nvarchar(150) NOT NULL;
ALTER TABLE [FMS_TEST2].Main.TaskTemplates ADD [FaultTypeId] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.TaskTemplates ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.TaskTemplates ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.TaskTemplates ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.TaskTemplates ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderActionLogs ADD [ActionName] nvarchar(50) NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderActionLogs ADD [ActionParameters] nvarchar(max) NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderActionLogs ADD [WorkOrderId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderActionLogs ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderActionLogs ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderActionLogs ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderActionLogs ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderActionLogs ADD [CreatorName] nvarchar(100) NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderAgents ADD [AgentId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderAgents ADD [WorkOrderId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderAgents ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderAgents ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderAgents ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderAgents ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderEquipmentTypes ADD [WorkOrderId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderEquipmentTypes ADD [EquipmentTypeId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderEquipmentTypes ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderEquipmentTypes ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderEquipmentTypes ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderEquipmentTypes ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderItems ADD [WorkOrderId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderItems ADD [ItemId] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderItems ADD [ResourceTypeId] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderItems ADD [Code] varchar(50) NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderItems ADD [Name] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderItems ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderItems ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderItems ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderItems ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderItems ADD [FaultType] nvarchar(200) NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderItems ADD [MaintenanceType] bit NOT NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderItems ADD [StatusId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderItems ADD [EndDate] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderItems ADD [ActionTaken] ntext NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderItems ADD [WarrantyStatus] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderItems ADD [ContractId] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderParts ADD [WorkOrderId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderParts ADD [PartTypeId] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderParts ADD [Amount] decimal(18,3) NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderParts ADD [Code] varchar(50) NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderParts ADD [Name] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderParts ADD [StatusId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderParts ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderParts ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderParts ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderParts ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderParts ADD [ResourceTypeId] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderParts ADD [ResourceComponentId] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderParts ADD [DeviceId] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderParts ADD [WorkOrderItemId] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderParts ADD [ReadyOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderParts ADD [ReceivedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderParts ADD [DeliveredOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderParts ADD [PurchaseRequestId] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderParts ADD [PurchaseRequestSequenceNumber] varchar(50) NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrders ADD [SequenceNumber] varchar(50) NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrders ADD [StatusId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrders ADD [Description] ntext NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrders ADD [WorkOrderTypeId] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrders ADD [AgentId] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrders ADD [PartyId] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrders ADD [StartDate] datetime NOT NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrders ADD [DueDate] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrders ADD [Priority] int NOT NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrders ADD [WorkRequestId] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrders ADD [WorkRequestSequenceNumber] varchar(50) NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrders ADD [IsScheduleTemplate] bit NOT NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrders ADD [Longitude] decimal(18,10) NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrders ADD [Latitude] decimal(18,10) NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrders ADD [Address] nvarchar(300) NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrders ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrders ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrders ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrders ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrders ADD [AssignType] int NOT NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrders ADD [TaskTemplateId] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrders ADD [SiteId] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrders ADD [CityId] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrders ADD [CityName] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrders ADD [ActualStartDate] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrders ADD [ActualEndDate] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrders ADD [CheckupId] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrders ADD [WorkOrderCategoryId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrders ADD [ActionTaken] ntext NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderTasks ADD [Name] nvarchar(300) NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderTasks ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderTasks ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderTasks ADD [TaskTemplateId] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderTasks ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderTasks ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderTasks ADD [WorkOrderId] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderTasks ADD [StatusId] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderTeams ADD [WorkOrderId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderTeams ADD [TeamId] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderTeams ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderTeams ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderTeams ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderTeams ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderTeams ADD [Name] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderTypes ADD [Name] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderTypes ADD [ParentId] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderTypes ADD [Chain] varchar(max) NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderTypes ADD [BaseTypeId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderTypes ADD [NameChain] nvarchar(max) NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderTypes ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderTypes ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderTypes ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderTypes ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Main.WorkOrderTypes ADD [Code] varchar(5) NULL;
ALTER TABLE [FMS_TEST2].Note.NotificationConfigs ADD [RoleId] bigint NULL;
ALTER TABLE [FMS_TEST2].Note.NotificationConfigs ADD [RoleName] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Note.NotificationConfigs ADD [UserId] bigint NULL;
ALTER TABLE [FMS_TEST2].Note.NotificationConfigs ADD [ResourceEventId] bigint NULL;
ALTER TABLE [FMS_TEST2].Note.NotificationConfigs ADD [Mobile] bit NOT NULL;
ALTER TABLE [FMS_TEST2].Note.NotificationConfigs ADD [Email] bit NOT NULL;
ALTER TABLE [FMS_TEST2].Note.NotificationConfigs ADD [Web] bit NOT NULL;
ALTER TABLE [FMS_TEST2].Note.NotificationConfigs ADD [IsActive] bit NOT NULL;
ALTER TABLE [FMS_TEST2].Note.NotificationConfigs ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Note.NotificationConfigs ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Note.NotificationConfigs ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Note.NotificationConfigs ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Note.Notifications ADD [Type] int NULL;
ALTER TABLE [FMS_TEST2].Note.Notifications ADD [IsRead] bit NOT NULL;
ALTER TABLE [FMS_TEST2].Note.Notifications ADD [MessageId] varchar(100) NULL;
ALTER TABLE [FMS_TEST2].Note.Notifications ADD [Parameters] nvarchar(max) NULL;
ALTER TABLE [FMS_TEST2].Note.Notifications ADD [UserId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Note.Notifications ADD [Link] nvarchar(300) NULL;
ALTER TABLE [FMS_TEST2].Note.Notifications ADD [EntityType] varchar(70) NULL;
ALTER TABLE [FMS_TEST2].Note.Notifications ADD [EntityId] bigint NULL;
ALTER TABLE [FMS_TEST2].Note.Notifications ADD [IsSent] bit NOT NULL;
ALTER TABLE [FMS_TEST2].Note.Notifications ADD [Subject] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Note.Notifications ADD [ReadOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Note.Notifications ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Note.Notifications ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Note.Notifications ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Note.Notifications ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Note.Notifications ADD [SenderName] nvarchar(50) NULL;
ALTER TABLE [FMS_TEST2].Note.Notifications ADD [SenderId] bigint NULL;
ALTER TABLE [FMS_TEST2].Note.ResourceEvents ADD [ResourceId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Note.ResourceEvents ADD [Name] varchar(50) NULL;
ALTER TABLE [FMS_TEST2].Note.ResourceEvents ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Note.ResourceEvents ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Note.ResourceEvents ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Note.ResourceEvents ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Note.Resources ADD [Name] varchar(150) NULL;
ALTER TABLE [FMS_TEST2].Note.Resources ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Note.Resources ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Note.Resources ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Note.Resources ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Note.UserRoles ADD [UserId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Note.UserRoles ADD [RoleId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Note.UserRoles ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Note.UserRoles ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Note.UserRoles ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Note.UserRoles ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Note.Users ADD [Name] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Note.Users ADD [Email] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Note.Users ADD [MobileToken] nvarchar(300) NULL;
ALTER TABLE [FMS_TEST2].Note.Users ADD [Mobile] varchar(50) NULL;
ALTER TABLE [FMS_TEST2].Note.Users ADD [UserType] int NOT NULL;
ALTER TABLE [FMS_TEST2].Note.Users ADD [PersonId] bigint NULL;
ALTER TABLE [FMS_TEST2].Note.Users ADD [PreferredLanguage] varchar(3) NULL;
ALTER TABLE [FMS_TEST2].Note.Users ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Note.Users ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Note.Users ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Note.Users ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Note.Users ADD [WebToken] nvarchar(70) NULL;
ALTER TABLE [FMS_TEST2].Publ.ContactInformations ADD [Subject] nvarchar(200) NULL;
ALTER TABLE [FMS_TEST2].Publ.ContactInformations ADD [Description] nvarchar(max) NULL;
ALTER TABLE [FMS_TEST2].Publ.ContactInformations ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Publ.ContactInformations ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Publ.ContactInformations ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Publ.ContactInformations ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Publ.FAQs ADD [Question] nvarchar(200) NULL;
ALTER TABLE [FMS_TEST2].Publ.FAQs ADD [Answer] nvarchar(max) NULL;
ALTER TABLE [FMS_TEST2].Publ.FAQs ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Publ.FAQs ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Publ.FAQs ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Publ.FAQs ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Publ.Localizables ADD [EntityId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Publ.Localizables ADD [EntityType] varchar(100) NOT NULL;
ALTER TABLE [FMS_TEST2].Publ.Localizables ADD [LocaleId] int NOT NULL;
ALTER TABLE [FMS_TEST2].Publ.Localizables ADD [ColumnName] varchar(70) NULL;
ALTER TABLE [FMS_TEST2].Publ.Localizables ADD [Value] nvarchar(max) NULL;
ALTER TABLE [FMS_TEST2].Publ.Localizables ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Publ.Localizables ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Publ.Localizables ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Publ.Localizables ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Publ.TermsandConditions ADD [Description] nvarchar(max) NULL;
ALTER TABLE [FMS_TEST2].Publ.TermsandConditions ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Publ.TermsandConditions ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Publ.TermsandConditions ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Publ.TermsandConditions ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Purc.Localizables ADD [EntityId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Purc.Localizables ADD [EntityType] varchar(100) NOT NULL;
ALTER TABLE [FMS_TEST2].Purc.Localizables ADD [LocaleId] int NOT NULL;
ALTER TABLE [FMS_TEST2].Purc.Localizables ADD [ColumnName] varchar(70) NULL;
ALTER TABLE [FMS_TEST2].Purc.Localizables ADD [Value] nvarchar(max) NULL;
ALTER TABLE [FMS_TEST2].Purc.Localizables ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Purc.Localizables ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Purc.Localizables ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Purc.Localizables ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Purc.Parties ADD [Name] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Purc.Parties ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Purc.Parties ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Purc.Parties ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Purc.Parties ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Purc.Parties ADD [Code] bigint NULL;
ALTER TABLE [FMS_TEST2].Purc.Parties ADD [IsDeleted] bit NULL;
ALTER TABLE [FMS_TEST2].Purc.PartTypes ADD [Code] varchar(50) NULL;
ALTER TABLE [FMS_TEST2].Purc.PartTypes ADD [Name] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Purc.PartTypes ADD [BrandName] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Purc.PartTypes ADD [PartNumber] varchar(100) NULL;
ALTER TABLE [FMS_TEST2].Purc.PartTypes ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Purc.PartTypes ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Purc.PartTypes ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Purc.PartTypes ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Purc.PartTypes ADD [ResourceComponentId] bigint NULL;
ALTER TABLE [FMS_TEST2].Purc.PhoneNumbers ADD [Number] varchar(15) NOT NULL;
ALTER TABLE [FMS_TEST2].Purc.PhoneNumbers ADD [SupplierId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Purc.PhoneNumbers ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Purc.PhoneNumbers ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Purc.PhoneNumbers ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Purc.PhoneNumbers ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Purc.PriceListItems ADD [PartTypeId] bigint NULL;
ALTER TABLE [FMS_TEST2].Purc.PriceListItems ADD [UnitPrice] decimal(18,3) NOT NULL;
ALTER TABLE [FMS_TEST2].Purc.PriceListItems ADD [Amount] decimal(18,3) NOT NULL;
ALTER TABLE [FMS_TEST2].Purc.PriceListItems ADD [TotalPrice] decimal(18,3) NOT NULL;
ALTER TABLE [FMS_TEST2].Purc.PriceListItems ADD [PriceListId] bigint NULL;
ALTER TABLE [FMS_TEST2].Purc.PriceListItems ADD [Code] varchar(150) NULL;
ALTER TABLE [FMS_TEST2].Purc.PriceListItems ADD [Name] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Purc.PriceListItems ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Purc.PriceListItems ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Purc.PriceListItems ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Purc.PriceListItems ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Purc.PriceListItems ADD [ResourceTypeId] bigint NULL;
ALTER TABLE [FMS_TEST2].Purc.PriceListItems ADD [PurchaseRequestItemId] bigint NULL;
ALTER TABLE [FMS_TEST2].Purc.PriceListItems ADD [DeviceSerialNo] varchar(150) NULL;
ALTER TABLE [FMS_TEST2].Purc.PriceLists ADD [SequenceNumber] varchar(50) NULL;
ALTER TABLE [FMS_TEST2].Purc.PriceLists ADD [PurchaseRequestId] bigint NULL;
ALTER TABLE [FMS_TEST2].Purc.PriceLists ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Purc.PriceLists ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Purc.PriceLists ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Purc.PriceLists ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Purc.PriceLists ADD [TotalPrice] decimal(18,3) NOT NULL;
ALTER TABLE [FMS_TEST2].Purc.PriceLists ADD [IsAccepted] bit NOT NULL;
ALTER TABLE [FMS_TEST2].Purc.PriceLists ADD [SupplierContactId] bigint NULL;
ALTER TABLE [FMS_TEST2].Purc.PriceLists ADD [SupplierId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Purc.PriceLists ADD [StatusId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchaseOrderItems ADD [PurchaseOrderId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchaseOrderItems ADD [PriceListItemId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchaseOrderItems ADD [UnitPrice] decimal(18,3) NOT NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchaseOrderItems ADD [TotalPrice] decimal(18,3) NOT NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchaseOrderItems ADD [Amount] decimal(18,3) NOT NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchaseOrderItems ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchaseOrderItems ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchaseOrderItems ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchaseOrderItems ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchaseOrders ADD [SupplierId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchaseOrders ADD [PurchaseRequestId] bigint NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchaseOrders ADD [PriceListId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchaseOrders ADD [SequenceNumber] varchar(50) NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchaseOrders ADD [TotalPrice] decimal(18,3) NOT NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchaseOrders ADD [StatusId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchaseOrders ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchaseOrders ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchaseOrders ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchaseOrders ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchaseOrders ADD [DueDate] datetime NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchaseOrders ADD [StartDate] datetime NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchaseOrders ADD [ActualDeliveryDate] datetime NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchaseRequestItems ADD [Code] varchar(100) NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchaseRequestItems ADD [Name] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchaseRequestItems ADD [Amount] decimal(18,3) NOT NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchaseRequestItems ADD [PurchaseRequestId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchaseRequestItems ADD [PartTypeId] bigint NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchaseRequestItems ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchaseRequestItems ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchaseRequestItems ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchaseRequestItems ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchaseRequestItems ADD [StatusId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchaseRequestItems ADD [ResourceTypeId] bigint NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchaseRequestItems ADD [ResourceComponentId] bigint NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchaseRequestItems ADD [ItemType] int NOT NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchaseRequestItems ADD [ResourceComponentName] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchaseRequestItems ADD [UnitPrice] decimal(18,3) NOT NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchaseRequestItems ADD [TotalPrice] decimal(18,3) NOT NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchaseRequestItems ADD [SerialNo] varchar(150) NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchaseRequestItems ADD [WorkOrderPartId] bigint NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchaseRequests ADD [WorkOrderId] bigint NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchaseRequests ADD [SequenceNumber] varchar(50) NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchaseRequests ADD [StatusId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchaseRequests ADD [DueDate] datetime NOT NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchaseRequests ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchaseRequests ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchaseRequests ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchaseRequests ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchaseRequests ADD [TotalBudget] decimal(18,3) NOT NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchaseRequests ADD [TotalPrice] decimal(18,3) NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchaseRequests ADD [WorkOrderSequenceNumber] varchar(50) NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchaseRequests ADD [PartyId] bigint NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchaseRequests ADD [SiteId] bigint NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchaseRequests ADD [SiteName] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchasingConfig ADD [PurchaseRequestPattern] varchar(150) NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchasingConfig ADD [PuchaseRequestCodeLength] int NOT NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchasingConfig ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchasingConfig ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchasingConfig ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchasingConfig ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchasingConfig ADD [PurchaseOrderPattern] varchar(150) NULL;
ALTER TABLE [FMS_TEST2].Purc.PurchasingConfig ADD [PuchaseOrderCodeLength] int NOT NULL;
ALTER TABLE [FMS_TEST2].Purc.ResourceTypes ADD [Code] varchar(50) NULL;
ALTER TABLE [FMS_TEST2].Purc.ResourceTypes ADD [Name] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Purc.ResourceTypes ADD [BrandName] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Purc.ResourceTypes ADD [PartNumber] varchar(100) NULL;
ALTER TABLE [FMS_TEST2].Purc.ResourceTypes ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Purc.ResourceTypes ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Purc.ResourceTypes ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Purc.ResourceTypes ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Purc.Statuses ADD [Name] varchar(150) NULL;
ALTER TABLE [FMS_TEST2].Purc.Statuses ADD [EntityType] varchar(150) NULL;
ALTER TABLE [FMS_TEST2].Purc.Statuses ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Purc.Statuses ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Purc.Statuses ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Purc.Statuses ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Purc.SupplierActivities ADD [ResourceTypeId] bigint NULL;
ALTER TABLE [FMS_TEST2].Purc.SupplierActivities ADD [SupplierId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Purc.SupplierActivities ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Purc.SupplierActivities ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Purc.SupplierActivities ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Purc.SupplierActivities ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Purc.SupplierActivities ADD [ResourceComponentId] bigint NULL;
ALTER TABLE [FMS_TEST2].Purc.SupplierActivities ADD [PartTypeId] bigint NULL;
ALTER TABLE [FMS_TEST2].Purc.SupplierActivities ADD [IsPrimaryAgent] bit NOT NULL;
ALTER TABLE [FMS_TEST2].Purc.SupplierActivities ADD [ResourceComponentName] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Purc.SupplierContacts ADD [Name] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Purc.SupplierContacts ADD [SupplierId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Purc.SupplierContacts ADD [Email] varchar(150) NULL;
ALTER TABLE [FMS_TEST2].Purc.SupplierContacts ADD [Address] nvarchar(max) NULL;
ALTER TABLE [FMS_TEST2].Purc.SupplierContacts ADD [IsPrimary] bit NOT NULL;
ALTER TABLE [FMS_TEST2].Purc.SupplierContacts ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Purc.SupplierContacts ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Purc.SupplierContacts ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Purc.SupplierContacts ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Purc.SupplierContacts ADD [PhoneNumber] varchar(15) NULL;
ALTER TABLE [FMS_TEST2].Purc.SupplierContacts ADD [Mobile] varchar(15) NULL;
ALTER TABLE [FMS_TEST2].Purc.SupplierContacts ADD [Extension] varchar(15) NULL;
ALTER TABLE [FMS_TEST2].Purc.SupplierDevices ADD [SupplierId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Purc.SupplierDevices ADD [ResourceTypeId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Purc.SupplierDevices ADD [IsPrimaryAgent] bit NOT NULL;
ALTER TABLE [FMS_TEST2].Purc.SupplierDevices ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Purc.SupplierDevices ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Purc.SupplierDevices ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Purc.SupplierDevices ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Purc.Suppliers ADD [Name] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Purc.Suppliers ADD [CountryId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Purc.Suppliers ADD [Address] nvarchar(max) NULL;
ALTER TABLE [FMS_TEST2].Purc.Suppliers ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Purc.Suppliers ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Purc.Suppliers ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Purc.Suppliers ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Rpt.ReportsTemplates ADD [Name] nvarchar(max) NOT NULL;
ALTER TABLE [FMS_TEST2].Rpt.ReportsTemplates ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Rpt.ReportsTemplates ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Rpt.ReportsTemplates ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Rpt.ReportsTemplates ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Rpt.ReportsTemplates ADD [RdlcTemplateName] nvarchar(max) NULL;
ALTER TABLE [FMS_TEST2].Sals.Attachments ADD [EntityType] varchar(70) NULL;
ALTER TABLE [FMS_TEST2].Sals.Attachments ADD [FileType] varchar(50) NULL;
ALTER TABLE [FMS_TEST2].Sals.Attachments ADD [FilePath] nvarchar(300) NULL;
ALTER TABLE [FMS_TEST2].Sals.Attachments ADD [EntityId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Sals.Attachments ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Sals.Attachments ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Sals.Attachments ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Sals.Attachments ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Sals.Cities ADD [Name] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Sals.Cities ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Sals.Cities ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Sals.Cities ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Sals.Cities ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Sals.Contacts ADD [FirstName] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Sals.Contacts ADD [LastName] nvarchar(100) NULL;
ALTER TABLE [FMS_TEST2].Sals.Contacts ADD [PartyId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Sals.Contacts ADD [Email] varchar(150) NULL;
ALTER TABLE [FMS_TEST2].Sals.Contacts ADD [Mobile] varchar(15) NULL;
ALTER TABLE [FMS_TEST2].Sals.Contacts ADD [Photo] nvarchar(300) NULL;
ALTER TABLE [FMS_TEST2].Sals.Contacts ADD [Gender] bit NOT NULL;
ALTER TABLE [FMS_TEST2].Sals.Contacts ADD [BirthDate] datetime NULL;
ALTER TABLE [FMS_TEST2].Sals.Contacts ADD [JobTitle] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Sals.Contacts ADD [UserId] bigint NULL;
ALTER TABLE [FMS_TEST2].Sals.Contacts ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Sals.Contacts ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Sals.Contacts ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Sals.Contacts ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Sals.Contacts ADD [CityId] bigint NULL;
ALTER TABLE [FMS_TEST2].Sals.Faults ADD [SequenceNumber] varchar(50) NULL;
ALTER TABLE [FMS_TEST2].Sals.Faults ADD [ItemId] bigint NULL;
ALTER TABLE [FMS_TEST2].Sals.Faults ADD [WorkRequestId] bigint NULL;
ALTER TABLE [FMS_TEST2].Sals.Faults ADD [FaultTypeId] bigint NULL;
ALTER TABLE [FMS_TEST2].Sals.Faults ADD [Description] int NULL;
ALTER TABLE [FMS_TEST2].Sals.Faults ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Sals.Faults ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Sals.Faults ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Sals.Faults ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Sals.Localizables ADD [EntityId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Sals.Localizables ADD [EntityType] varchar(100) NOT NULL;
ALTER TABLE [FMS_TEST2].Sals.Localizables ADD [LocaleId] int NOT NULL;
ALTER TABLE [FMS_TEST2].Sals.Localizables ADD [ColumnName] varchar(70) NULL;
ALTER TABLE [FMS_TEST2].Sals.Localizables ADD [Value] nvarchar(max) NULL;
ALTER TABLE [FMS_TEST2].Sals.Localizables ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Sals.Localizables ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Sals.Localizables ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Sals.Localizables ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Sals.Organizations ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Sals.Organizations ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Sals.Organizations ADD [Name] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Sals.Organizations ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Sals.Organizations ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Sals.Parties ADD [Name] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Sals.Parties ADD [PartyTypeId] bigint NULL;
ALTER TABLE [FMS_TEST2].Sals.Parties ADD [StreetAddress] nvarchar(200) NULL;
ALTER TABLE [FMS_TEST2].Sals.Parties ADD [Region] nvarchar(100) NULL;
ALTER TABLE [FMS_TEST2].Sals.Parties ADD [PostalCode] varchar(50) NULL;
ALTER TABLE [FMS_TEST2].Sals.Parties ADD [Phone] varchar(15) NULL;
ALTER TABLE [FMS_TEST2].Sals.Parties ADD [Fax] varchar(50) NULL;
ALTER TABLE [FMS_TEST2].Sals.Parties ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Sals.Parties ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Sals.Parties ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Sals.Parties ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Sals.Parties ADD [Code] bigint NULL;
ALTER TABLE [FMS_TEST2].Sals.Parties ADD [IsDeleted] bit NULL;
ALTER TABLE [FMS_TEST2].Sals.Parties ADD [Logo] nvarchar(max) NULL;
ALTER TABLE [FMS_TEST2].Sals.Parties ADD [OrganizationId] bigint NULL;
ALTER TABLE [FMS_TEST2].Sals.Parties ADD [SectionName] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Sals.SalesConfig ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Sals.SalesConfig ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Sals.SalesConfig ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Sals.SalesConfig ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Sals.SalesConfig ADD [WorkRequestSequencePattern] varchar(255) NULL;
ALTER TABLE [FMS_TEST2].Sals.SalesConfig ADD [WorkRequestSerialMinLength] int NULL;
ALTER TABLE [FMS_TEST2].Sals.Sites ADD [Name] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Sals.Sites ADD [Address] nvarchar(500) NULL;
ALTER TABLE [FMS_TEST2].Sals.Sites ADD [PartyId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Sals.Sites ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Sals.Sites ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Sals.Sites ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Sals.Sites ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Sals.Sites ADD [Longitude] decimal(18,10) NULL;
ALTER TABLE [FMS_TEST2].Sals.Sites ADD [Latitude] decimal(18,10) NULL;
ALTER TABLE [FMS_TEST2].Sals.Sites ADD [CityId] bigint NULL;
ALTER TABLE [FMS_TEST2].Sals.Sites ADD [IsDeleted] bit NULL;
ALTER TABLE [FMS_TEST2].Sals.Statuses ADD [Name] nvarchar(50) NULL;
ALTER TABLE [FMS_TEST2].Sals.Statuses ADD [EntityType] varchar(70) NULL;
ALTER TABLE [FMS_TEST2].Sals.Statuses ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Sals.Statuses ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Sals.Statuses ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Sals.Statuses ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Sals.WorkRequestActionLogs ADD [WorkRequestId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Sals.WorkRequestActionLogs ADD [ActionName] nvarchar(50) NULL;
ALTER TABLE [FMS_TEST2].Sals.WorkRequestActionLogs ADD [ActionParameters] nvarchar(max) NULL;
ALTER TABLE [FMS_TEST2].Sals.WorkRequestActionLogs ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Sals.WorkRequestActionLogs ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Sals.WorkRequestActionLogs ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Sals.WorkRequestActionLogs ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Sals.WorkRequestActionLogs ADD [CreatorName] nvarchar(100) NULL;
ALTER TABLE [FMS_TEST2].Sals.WorkRequestItemTypes ADD [Code] varchar(150) NULL;
ALTER TABLE [FMS_TEST2].Sals.WorkRequestItemTypes ADD [Name] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Sals.WorkRequestItemTypes ADD [WorkRequestId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Sals.WorkRequestItemTypes ADD [StatusId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Sals.WorkRequestItemTypes ADD [Amount] decimal(18,3) NOT NULL;
ALTER TABLE [FMS_TEST2].Sals.WorkRequestItemTypes ADD [ResourceTypeId] bigint NULL;
ALTER TABLE [FMS_TEST2].Sals.WorkRequestItemTypes ADD [PartTypeId] bigint NULL;
ALTER TABLE [FMS_TEST2].Sals.WorkRequestItemTypes ADD [BrandName] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Sals.WorkRequestItemTypes ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Sals.WorkRequestItemTypes ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Sals.WorkRequestItemTypes ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Sals.WorkRequestItemTypes ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Sals.WorkRequestItemTypes ADD [ItemId] bigint NULL;
ALTER TABLE [FMS_TEST2].Sals.WorkRequestItemTypes ADD [Categories] varchar(max) NULL;
ALTER TABLE [FMS_TEST2].Sals.WorkRequestItemTypes ADD [WarrantyStatus] bigint NULL;
ALTER TABLE [FMS_TEST2].Sals.WorkRequests ADD [SequenceNumber] varchar(50) NULL;
ALTER TABLE [FMS_TEST2].Sals.WorkRequests ADD [ContactId] bigint NULL;
ALTER TABLE [FMS_TEST2].Sals.WorkRequests ADD [StatusId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Sals.WorkRequests ADD [WorkRequestTypeId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Sals.WorkRequests ADD [SiteId] bigint NULL;
ALTER TABLE [FMS_TEST2].Sals.WorkRequests ADD [Details] ntext NULL;
ALTER TABLE [FMS_TEST2].Sals.WorkRequests ADD [MaintenanceType] int NULL;
ALTER TABLE [FMS_TEST2].Sals.WorkRequests ADD [SiteDepartment] nvarchar(350) NULL;
ALTER TABLE [FMS_TEST2].Sals.WorkRequests ADD [Notes] ntext NULL;
ALTER TABLE [FMS_TEST2].Sals.WorkRequests ADD [Longitude] decimal(18,10) NULL;
ALTER TABLE [FMS_TEST2].Sals.WorkRequests ADD [Latitude] decimal(18,10) NULL;
ALTER TABLE [FMS_TEST2].Sals.WorkRequests ADD [Address] nvarchar(300) NULL;
ALTER TABLE [FMS_TEST2].Sals.WorkRequests ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Sals.WorkRequests ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Sals.WorkRequests ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Sals.WorkRequests ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Sals.WorkRequests ADD [WorkOrderTypeId] bigint NULL;
ALTER TABLE [FMS_TEST2].Sals.WorkRequests ADD [WorkOrderTypeName] nvarchar(max) NULL;
ALTER TABLE [FMS_TEST2].Sals.WorkRequests ADD [CityId] bigint NULL;
ALTER TABLE [FMS_TEST2].Sals.WorkRequests ADD [Priority] int NOT NULL;
ALTER TABLE [FMS_TEST2].Sals.WorkRequests ADD [WorkOrderTypeChain] varchar(max) NULL;
ALTER TABLE [FMS_TEST2].Sals.WorkRequestTypes ADD [Name] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Sals.WorkRequestTypes ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Sals.WorkRequestTypes ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Sals.WorkRequestTypes ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Sals.WorkRequestTypes ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Wfrc.AgentEquipment ADD [AgentId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Wfrc.AgentEquipment ADD [EquipmentId] bigint NULL;
ALTER TABLE [FMS_TEST2].Wfrc.AgentEquipment ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Wfrc.AgentEquipment ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Wfrc.AgentEquipment ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Wfrc.AgentEquipment ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Wfrc.AgentLocations ADD [AgentId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Wfrc.AgentLocations ADD [PartyId] bigint NULL;
ALTER TABLE [FMS_TEST2].Wfrc.AgentLocations ADD [LocationId] bigint NULL;
ALTER TABLE [FMS_TEST2].Wfrc.AgentLocations ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Wfrc.AgentLocations ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Wfrc.AgentLocations ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Wfrc.AgentLocations ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Wfrc.AgentLocations ADD [IsBranchHead] bit NOT NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Agents ADD [AgentTypeId] int NOT NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Agents ADD [UserId] bigint NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Agents ADD [DepartmentId] bigint NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Agents ADD [Email] varchar(150) NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Agents ADD [Mobile] varchar(15) NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Agents ADD [Phone] varchar(15) NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Agents ADD [Extension] int NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Agents ADD [CityId] bigint NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Agents ADD [Region] nvarchar(100) NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Agents ADD [StreetAddress] nvarchar(200) NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Agents ADD [Graduate] nvarchar(200) NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Agents ADD [GraduationCertificate] nvarchar(300) NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Agents ADD [JobRoleId] bigint NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Agents ADD [JobTitle] nvarchar(200) NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Agents ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Agents ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Agents ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Agents ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Agents ADD [FirstName] nvarchar(100) NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Agents ADD [LastName] nvarchar(100) NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Agents ADD [Photo] nvarchar(300) NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Agents ADD [Gender] bit NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Agents ADD [BirthDate] datetime NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Agents ADD [ProfilePicture] nvarchar(max) NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Agents ADD [Signature] nvarchar(max) NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Agents ADD [TenantAppId] bigint NULL;
ALTER TABLE [FMS_TEST2].Wfrc.AgentsTeams ADD [AgentId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Wfrc.AgentsTeams ADD [TeamId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Wfrc.AgentsTeams ADD [IsTeamLeader] bit NOT NULL;
ALTER TABLE [FMS_TEST2].Wfrc.AgentsTeams ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Wfrc.AgentsTeams ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Wfrc.AgentsTeams ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Wfrc.AgentsTeams ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Cities ADD [Name] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Cities ADD [CountryId] bigint NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Cities ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Cities ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Cities ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Cities ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Cities ADD [RegionId] bigint NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Countries ADD [Name] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Countries ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Countries ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Countries ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Countries ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Departments ADD [Name] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Departments ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Departments ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Departments ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Departments ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Departments ADD [Head] nvarchar(200) NULL;
ALTER TABLE [FMS_TEST2].Wfrc.JobRoles ADD [Name] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Wfrc.JobRoles ADD [Description] nvarchar(200) NULL;
ALTER TABLE [FMS_TEST2].Wfrc.JobRoles ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Wfrc.JobRoles ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Wfrc.JobRoles ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Wfrc.JobRoles ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Localizables ADD [EntityId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Localizables ADD [EntityType] varchar(100) NOT NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Localizables ADD [LocaleId] int NOT NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Localizables ADD [ColumnName] varchar(70) NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Localizables ADD [Value] nvarchar(max) NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Localizables ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Localizables ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Localizables ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Localizables ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Locations ADD [Name] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Locations ADD [CityId] bigint NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Locations ADD [SiteId] bigint NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Locations ADD [PartyId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Locations ADD [NameChain] nvarchar(max) NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Locations ADD [Chain] varchar(max) NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Locations ADD [ParentId] bigint NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Locations ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Locations ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Locations ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Locations ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Parties ADD [Name] nvarchar(200) NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Parties ADD [Code] bigint NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Parties ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Parties ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Parties ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Parties ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Parties ADD [IsDeleted] bit NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Regions ADD [CountryId] bigint NOT NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Regions ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Regions ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Regions ADD [Name] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Regions ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Regions ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Teams ADD [Code] varchar(50) NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Teams ADD [Name] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Teams ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Teams ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Teams ADD [CreatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Teams ADD [UpdatedBy] bigint NULL;
ALTER TABLE [FMS_TEST2].Wfrc.Teams ADD [Description] nvarchar(300) NULL;
 
-- NO MODIFIED COLUMNS
 
-- ADDED CONSTRAINTS :
ALTER TABLE [FMS_TEST2].Asts.Contracts ADD CONSTRAINT FK_Contracts_Parties FOREIGN KEY (PartyId) REFERENCES Asts.Parties (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Asts.FolderWorkOrderTypes ADD CONSTRAINT FK_FolderWorkOrderTypes_ResourceTypeFolders FOREIGN KEY (ResourceTypeFolderId) REFERENCES Asts.ResourceTypeFolders (Id) 
		ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [FMS_TEST2].Asts.ItemFolders ADD CONSTRAINT FK_ItemFolders_Parties FOREIGN KEY (PartyId) REFERENCES Asts.Parties (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Asts.ItemItemFolders ADD CONSTRAINT FK_ItemItemFolders_ItemFolders FOREIGN KEY (ItemFolderId) REFERENCES Asts.ItemFolders (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Asts.ItemItemFolders ADD CONSTRAINT FK_ItemItemFolders_Items FOREIGN KEY (ItemId) REFERENCES Asts.Items (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Asts.Items ADD CONSTRAINT FK_Items_Contracts FOREIGN KEY (ContractId) REFERENCES Asts.Contracts (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Asts.Items ADD CONSTRAINT FK_Items_Locations FOREIGN KEY (LocationId) REFERENCES Asts.Locations (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Asts.Items ADD CONSTRAINT FK_Items_Parties FOREIGN KEY (PartyId) REFERENCES Asts.Parties (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Asts.Items ADD CONSTRAINT FK_Items_ResourceTypes FOREIGN KEY (ResourceTypeId) REFERENCES Asts.ResourceTypes (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Asts.Items ADD CONSTRAINT FK_Items_Statuses FOREIGN KEY (StatusId) REFERENCES Asts.Statuses (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Asts.ItemStates ADD CONSTRAINT FK_ItemStates_Items FOREIGN KEY (ItemId) REFERENCES Asts.Items (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Asts.Locations ADD CONSTRAINT FK_Locations_Sites FOREIGN KEY (SiteId) REFERENCES Asts.Sites (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Asts.PartTypes ADD CONSTRAINT FK_PartTypes_ResourceComponents FOREIGN KEY (ResourceComponentId) REFERENCES Asts.ResourceComponents (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Asts.ResourceComponents ADD CONSTRAINT FK_ResourceComponents_ResourceTypeFolders FOREIGN KEY (ResourceTypeFolderId) REFERENCES Asts.ResourceTypeFolders (Id) 
		ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [FMS_TEST2].Asts.ResourceTypePartTypes ADD CONSTRAINT FK_ResourceTypePartTypes_PartTypes FOREIGN KEY (PartTypeId) REFERENCES Asts.PartTypes (Id) 
		ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [FMS_TEST2].Asts.ResourceTypePartTypes ADD CONSTRAINT FK_ResourceTypePartTypes_ResourceTypes FOREIGN KEY (ResourceTypeId) REFERENCES Asts.ResourceTypes (Id) 
		ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [FMS_TEST2].Asts.ResourceTypeResourceTypeFolders ADD CONSTRAINT FK_ClassificationFolderItemType_ClassificationFolders FOREIGN KEY (FolderId) REFERENCES Asts.ResourceTypeFolders (Id) 
		ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [FMS_TEST2].Asts.ResourceTypeResourceTypeFolders ADD CONSTRAINT FK_ClassificationFolderResourceTypes_ResourceTypes FOREIGN KEY (ResourceTypeId) REFERENCES Asts.ResourceTypes (Id) 
		ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [FMS_TEST2].Asts.Sites ADD CONSTRAINT FK_Sites_Cities FOREIGN KEY (CityId) REFERENCES Asts.Cities (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Asts.Sites ADD CONSTRAINT FK_Sites_Parties FOREIGN KEY (PartyId) REFERENCES Asts.Parties (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Asts.Specifications ADD CONSTRAINT FK__Specifica__Resou__066DDD9B FOREIGN KEY (ResourceTypeId) REFERENCES Asts.ResourceTypes (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Asts.Stocks ADD CONSTRAINT FK_Stocks_PartTypes FOREIGN KEY (PartTypeId) REFERENCES Asts.PartTypes (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Asts.Stocks ADD CONSTRAINT FK_Stocks_ResourceTypes FOREIGN KEY (ResourceTypeId) REFERENCES Asts.ResourceTypes (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Asts.Stocks ADD CONSTRAINT FK_Stocks_StockReservations FOREIGN KEY (StockReservationId) REFERENCES Asts.StockReservations (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Asts.Stocks ADD CONSTRAINT FK_Stocks_StockTransactionItems FOREIGN KEY (StockTransactionItemId) REFERENCES Asts.StockTransactionItems (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Asts.Stocks ADD CONSTRAINT FK_Stocks_Warehouses FOREIGN KEY (WarehouseId) REFERENCES Asts.Warehouses (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Asts.StockTransactionItems ADD CONSTRAINT FK_StockTransactionItems_PartTypes FOREIGN KEY (PartTypeId) REFERENCES Asts.PartTypes (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Asts.StockTransactionItems ADD CONSTRAINT FK_StockTransactionItems_ResourceTypes FOREIGN KEY (ResourceTypeId) REFERENCES Asts.ResourceTypes (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Asts.StockTransactionItems ADD CONSTRAINT FK_StockTransactionItems_StockTransactions FOREIGN KEY (StockTransactionId) REFERENCES Asts.StockTransactions (Id) 
		ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [FMS_TEST2].Asts.StockTransactions ADD CONSTRAINT FK_StockTransactions_StockTransactionTypes FOREIGN KEY (StockTransactionTypeId) REFERENCES Asts.StockTransactionTypes (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Asts.StockTransactions ADD CONSTRAINT FK_StockTransactions_Warehouses FOREIGN KEY (WarehouseId) REFERENCES Asts.Warehouses (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Asts.Warehouses ADD CONSTRAINT FK_Warehouses_Parties FOREIGN KEY (PartyId) REFERENCES Asts.Parties (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Auth.DefaultRoles ADD CONSTRAINT FK__DefaultRo__RoleI__4984CAEC FOREIGN KEY (RoleId) REFERENCES Auth.Roles (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Auth.ResourceActions ADD CONSTRAINT FK_ResourceActions_Resources FOREIGN KEY (ResourceId) REFERENCES Auth.Resources (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Auth.Resources ADD CONSTRAINT FK_Resources_Domains FOREIGN KEY (DomainId) REFERENCES Auth.Domains (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Auth.RoleResourceActions ADD CONSTRAINT FK_RoleResourceActions_ResourceActions FOREIGN KEY (ResourceActionId) REFERENCES Auth.ResourceActions (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Auth.RoleResourceActions ADD CONSTRAINT FK_RoleResourceActions_Roles FOREIGN KEY (RoleId) REFERENCES Auth.Roles (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Auth.RoleResources ADD CONSTRAINT FK_RoleResources_Resources FOREIGN KEY (ResourceId) REFERENCES Auth.Resources (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Auth.RoleResources ADD CONSTRAINT FK_RoleResources_Roles FOREIGN KEY (RoleId) REFERENCES Auth.Roles (Id) 
		ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [FMS_TEST2].Auth.Roles ADD CONSTRAINT FK__Roles__TenantApp__4890A6B3 FOREIGN KEY (TenantAppId) REFERENCES Auth.TenantApps (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Auth.TenantAppUsers ADD CONSTRAINT FK_TenantAppUsers_TenantApps FOREIGN KEY (TenantAppId) REFERENCES Auth.TenantApps (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Auth.TenantAppUsers ADD CONSTRAINT FK_TenantAppUsers_Users FOREIGN KEY (UserId) REFERENCES Auth.Users (Id) 
		ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [FMS_TEST2].Auth.TenantDomains ADD CONSTRAINT FK_TenantDomains_Domains FOREIGN KEY (DomainId) REFERENCES Auth.Domains (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Auth.TenantDomains ADD CONSTRAINT FK_TenantDomains_Tenants FOREIGN KEY (TenantId) REFERENCES Auth.Tenants (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Auth.UserParties ADD CONSTRAINT FK_Users_UserParties FOREIGN KEY (UserId) REFERENCES Auth.Users (Id) 
		ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [FMS_TEST2].Auth.UserRoles ADD CONSTRAINT FK_UserRoles_Roles FOREIGN KEY (RoleId) REFERENCES Auth.Roles (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Auth.UserRoles ADD CONSTRAINT FK_UserRoles_Users FOREIGN KEY (UserId) REFERENCES Auth.Users (Id) 
		ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [FMS_TEST2].Auth.Users ADD CONSTRAINT FK_Users_Tenants FOREIGN KEY (TenantId) REFERENCES Auth.Tenants (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Legl.ContractResourceTypes ADD CONSTRAINT FK_ContractResourceTypes_ItemPriorities FOREIGN KEY (ItemPriorityId) REFERENCES Legl.ItemPriorities (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Legl.ContractResourceTypes ADD CONSTRAINT FK_ContractResourceTypes_ResourceTypes FOREIGN KEY (ResourceTypeId) REFERENCES Legl.ResourceTypes (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Legl.Contracts ADD CONSTRAINT FK_Contracts_Parties FOREIGN KEY (PartyId) REFERENCES Legl.Parties (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Legl.Fines ADD CONSTRAINT FK_Fines_Contracts FOREIGN KEY (ContractId) REFERENCES Legl.Contracts (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Legl.Fines ADD CONSTRAINT FK_Fines_ResourceTypes FOREIGN KEY (ResourceTypeId) REFERENCES Legl.ResourceTypes (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Legl.ItemPriorities ADD CONSTRAINT FK_ItemPriorities_Contracts FOREIGN KEY (ContractId) REFERENCES Legl.Contracts (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Legl.RequiredApprovals ADD CONSTRAINT FK_RequiredApprovals_WorkOrderApprovalPersons FOREIGN KEY (WorkOrderApproverId) REFERENCES Legl.WorkOrderApprovalPersons (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Legl.WorkOrderApprovalPersons ADD CONSTRAINT FK_WorkOrderApprovalPersons_ApprovalPersonTypes FOREIGN KEY (ApprovalPersonTypeId) REFERENCES Legl.ApprovalPersonTypes (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Legl.WorkOrderApprovalPersons ADD CONSTRAINT FK_WorkOrderApprovalPersons_WorkOrderApprovalRules FOREIGN KEY (WorkOrderApprovalRuleId) REFERENCES Legl.WorkOrderApprovalRules (Id) 
		ON UPDATE NO ACTION ON DELETE CASCADE;
ALTER TABLE [FMS_TEST2].Main.CheckUpFields ADD CONSTRAINT FK_CheckUpFields_CheckupTypes FOREIGN KEY (CheckUpTypeId) REFERENCES Main.CheckUpTypes (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Main.CheckUpItems ADD CONSTRAINT FK_CheckUpItems_CheckUps FOREIGN KEY (CheckupId) REFERENCES Main.CheckUps (Id) 
		ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [FMS_TEST2].Main.CheckUpItems ADD CONSTRAINT FK_CheckUpItems_ScheduleItems FOREIGN KEY (ScheduleItemId) REFERENCES Main.ScheduleItems (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Main.CheckUpItems ADD CONSTRAINT FK_CheckUpItems_WorkOrders FOREIGN KEY (WorkOrderId) REFERENCES Main.WorkOrders (Id) 
		ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [FMS_TEST2].Main.CheckUps ADD CONSTRAINT FK_CheckUps_Schedules FOREIGN KEY (ScheduleId) REFERENCES Main.Schedules (Id) 
		ON UPDATE NO ACTION ON DELETE CASCADE;
ALTER TABLE [FMS_TEST2].Main.CheckUps ADD CONSTRAINT FK_CheckUps_Statuses FOREIGN KEY (StatusId) REFERENCES Main.Statuses (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Main.CheckUpValues ADD CONSTRAINT FK_CheckUpValues_CheckUpFields FOREIGN KEY (CheckUpFieldId) REFERENCES Main.CheckUpFields (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Main.CheckUpValues ADD CONSTRAINT FK_CheckUpValues_CheckUpItems FOREIGN KEY (CheckUpItemId) REFERENCES Main.CheckUpItems (Id) 
		ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [FMS_TEST2].Main.DispatchRuleAgents ADD CONSTRAINT FK_DispatchRuleAgents_Agents FOREIGN KEY (AgentId) REFERENCES Main.Agents (Id) 
		ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [FMS_TEST2].Main.DispatchRuleAgents ADD CONSTRAINT FK_DispatchRuleAgents_DispatchRules FOREIGN KEY (DispatchRuleId) REFERENCES Main.DispatchRules (Id) 
		ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [FMS_TEST2].Main.DispatchRuleConditions ADD CONSTRAINT FK_DispatchRuleConditions_DispatchRules FOREIGN KEY (DispatchRuleId) REFERENCES Main.DispatchRules (Id) 
		ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [FMS_TEST2].Main.DispatchRules ADD CONSTRAINT FK_DispatchRules_Parties FOREIGN KEY (PartyId) REFERENCES Main.Parties (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Main.PartyWorkOrderTypes ADD CONSTRAINT FK_PartyWorkOrderTypes_Parties FOREIGN KEY (PartyId) REFERENCES Main.Parties (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Main.PartyWorkOrderTypes ADD CONSTRAINT FK_PartyWorkOrderTypes_WorkOrderTypes FOREIGN KEY (WorkOrderTypeId) REFERENCES Main.WorkOrderTypes (Id) 
		ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [FMS_TEST2].Main.ScheduleAgent ADD CONSTRAINT FK_ScheduleAgent_Agents FOREIGN KEY (AgentId) REFERENCES Main.Agents (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Main.ScheduleAgent ADD CONSTRAINT FK_ScheduleAgent_Schedules FOREIGN KEY (ScheduleId) REFERENCES Main.Schedules (Id) 
		ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [FMS_TEST2].Main.ScheduleItems ADD CONSTRAINT FK_ScheduleItems_Schedules FOREIGN KEY (ScheduleId) REFERENCES Main.Schedules (Id) 
		ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [FMS_TEST2].Main.ScheduleItems ADD CONSTRAINT FK_ScheduleItems_Sites FOREIGN KEY (SiteId) REFERENCES Main.Sites (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Main.Schedules ADD CONSTRAINT FK_Schedules_CheckUpTypes FOREIGN KEY (CheckUpTypeId) REFERENCES Main.CheckUpTypes (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Main.Schedules ADD CONSTRAINT FK_Schedules_Parties FOREIGN KEY (PartyId) REFERENCES Main.Parties (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Main.Sites ADD CONSTRAINT FK_Sites_Parties FOREIGN KEY (PartyId) REFERENCES Main.Parties (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Main.TaskTemplates ADD CONSTRAINT FK_TaskTemplates_FaultTypes FOREIGN KEY (FaultTypeId) REFERENCES Main.FaultTypes (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Main.WorkOrderActionLogs ADD CONSTRAINT FK_ActionLogs_WorkOrders FOREIGN KEY (WorkOrderId) REFERENCES Main.WorkOrders (Id) 
		ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [FMS_TEST2].Main.WorkOrderAgents ADD CONSTRAINT FK_WorkOrderAgents_Agents FOREIGN KEY (AgentId) REFERENCES Main.Agents (Id) 
		ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [FMS_TEST2].Main.WorkOrderAgents ADD CONSTRAINT FK_WorkOrderAgents_WorkOrders FOREIGN KEY (WorkOrderId) REFERENCES Main.WorkOrders (Id) 
		ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [FMS_TEST2].Main.WorkOrderEquipmentTypes ADD CONSTRAINT FK_WorkOrderEquipmentTypes_EquipmentTypes FOREIGN KEY (EquipmentTypeId) REFERENCES Main.EquipmentTypes (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Main.WorkOrderEquipmentTypes ADD CONSTRAINT FK_WorkOrderEquipmentTypes_WorkOrders FOREIGN KEY (WorkOrderId) REFERENCES Main.WorkOrders (Id) 
		ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [FMS_TEST2].Main.WorkOrderItems ADD CONSTRAINT FK_WorkOrderItems_Statuses FOREIGN KEY (StatusId) REFERENCES Main.Statuses (Id) 
		ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [FMS_TEST2].Main.WorkOrderItems ADD CONSTRAINT FK_WorkOrderItems_WorkOrders FOREIGN KEY (WorkOrderId) REFERENCES Main.WorkOrders (Id) 
		ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [FMS_TEST2].Main.WorkOrderParts ADD CONSTRAINT FK_WorkOrderParts_WorkOrderItems FOREIGN KEY (WorkOrderItemId) REFERENCES Main.WorkOrderItems (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Main.WorkOrders ADD CONSTRAINT FK_WorkOrders_Agents FOREIGN KEY (AgentId) REFERENCES Main.Agents (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Main.WorkOrders ADD CONSTRAINT FK_WorkOrders_Parties FOREIGN KEY (PartyId) REFERENCES Main.Parties (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Main.WorkOrders ADD CONSTRAINT FK_WorkOrders_Sites FOREIGN KEY (SiteId) REFERENCES Main.Sites (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Main.WorkOrders ADD CONSTRAINT FK_WorkOrders_Statuses FOREIGN KEY (StatusId) REFERENCES Main.Statuses (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Main.WorkOrders ADD CONSTRAINT FK_WorkOrders_WorkOrderTypes FOREIGN KEY (WorkOrderTypeId) REFERENCES Main.WorkOrderTypes (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Main.WorkOrderTasks ADD CONSTRAINT FK_WorkOrderTasks_Statuses FOREIGN KEY (StatusId) REFERENCES Main.Statuses (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Main.WorkOrderTasks ADD CONSTRAINT FK_Tasks_TaskTemplates FOREIGN KEY (TaskTemplateId) REFERENCES Main.TaskTemplates (Id) 
		ON UPDATE NO ACTION ON DELETE CASCADE;
ALTER TABLE [FMS_TEST2].Main.WorkOrderTasks ADD CONSTRAINT FK_WorkOrderTasks_WorkOrders1 FOREIGN KEY (WorkOrderId) REFERENCES Main.WorkOrders (Id) 
		ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [FMS_TEST2].Main.WorkOrderTeams ADD CONSTRAINT FK_WorkOrderTeams_WorkOrders FOREIGN KEY (WorkOrderId) REFERENCES Main.WorkOrders (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Note.NotificationConfigs ADD CONSTRAINT FK_NotificationConfigs_ResourceEvents FOREIGN KEY (ResourceEventId) REFERENCES Note.ResourceEvents (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Note.NotificationConfigs ADD CONSTRAINT FK_NotificationConfigs_Users FOREIGN KEY (UserId) REFERENCES Note.Users (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Note.Notifications ADD CONSTRAINT FK_Notifications_Users FOREIGN KEY (UserId) REFERENCES Note.Users (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Note.ResourceEvents ADD CONSTRAINT FK_ResourceEvents_Resources FOREIGN KEY (ResourceId) REFERENCES Note.Resources (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Note.UserRoles ADD CONSTRAINT FK_UserRoles_Users FOREIGN KEY (UserId) REFERENCES Note.Users (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Purc.PhoneNumbers ADD CONSTRAINT FK_PhoneNumbers_Suppliers FOREIGN KEY (SupplierId) REFERENCES Purc.Suppliers (Id) 
		ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [FMS_TEST2].Purc.PriceListItems ADD CONSTRAINT FK_PriceListItems_PartTypes FOREIGN KEY (PartTypeId) REFERENCES Purc.PartTypes (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Purc.PriceListItems ADD CONSTRAINT FK_PriceListItems_PriceLists FOREIGN KEY (PriceListId) REFERENCES Purc.PriceLists (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Purc.PriceListItems ADD CONSTRAINT FK_PriceListItems_PurchaseRequestItems FOREIGN KEY (PurchaseRequestItemId) REFERENCES Purc.PurchaseRequestItems (Id) 
		ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [FMS_TEST2].Purc.PriceListItems ADD CONSTRAINT FK_PriceListItems_ResourceTypes FOREIGN KEY (ResourceTypeId) REFERENCES Purc.ResourceTypes (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Purc.PriceLists ADD CONSTRAINT FK_PriceLists_PurchaseRequests FOREIGN KEY (PurchaseRequestId) REFERENCES Purc.PurchaseRequests (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Purc.PriceLists ADD CONSTRAINT FK_PriceLists_Statuses FOREIGN KEY (StatusId) REFERENCES Purc.Statuses (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Purc.PriceLists ADD CONSTRAINT FK_PriceLists_SupplierContacts FOREIGN KEY (SupplierContactId) REFERENCES Purc.SupplierContacts (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Purc.PriceLists ADD CONSTRAINT FK_PriceLists_Suppliers FOREIGN KEY (SupplierId) REFERENCES Purc.Suppliers (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Purc.PurchaseOrderItems ADD CONSTRAINT FK_PurchaseOrderItems_PriceListItems FOREIGN KEY (PriceListItemId) REFERENCES Purc.PriceListItems (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Purc.PurchaseOrderItems ADD CONSTRAINT FK_PurchaseOrderItems_PurchaseOrders FOREIGN KEY (PurchaseOrderId) REFERENCES Purc.PurchaseOrders (Id) 
		ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [FMS_TEST2].Purc.PurchaseOrders ADD CONSTRAINT FK_PurchaseOrders_PriceLists FOREIGN KEY (PriceListId) REFERENCES Purc.PriceLists (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Purc.PurchaseOrders ADD CONSTRAINT FK_PurchaseOrders_Statuses FOREIGN KEY (StatusId) REFERENCES Purc.Statuses (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Purc.PurchaseOrders ADD CONSTRAINT FK_PurchaseOrders_Suppliers FOREIGN KEY (SupplierId) REFERENCES Purc.Suppliers (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Purc.PurchaseRequestItems ADD CONSTRAINT FK_PurchaseRequestItems_PartTypes FOREIGN KEY (PartTypeId) REFERENCES Purc.PartTypes (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Purc.PurchaseRequestItems ADD CONSTRAINT FK_PurchaseRequestItems_PurchaseRequests FOREIGN KEY (PurchaseRequestId) REFERENCES Purc.PurchaseRequests (Id) 
		ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [FMS_TEST2].Purc.PurchaseRequestItems ADD CONSTRAINT FK_PurchaseRequestItems_ResourceTypes FOREIGN KEY (ResourceTypeId) REFERENCES Purc.ResourceTypes (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Purc.PurchaseRequestItems ADD CONSTRAINT FK_PurchaseRequestItems_Statuses FOREIGN KEY (StatusId) REFERENCES Purc.Statuses (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Purc.PurchaseRequests ADD CONSTRAINT FK_PurchaseRequests_Parties FOREIGN KEY (PartyId) REFERENCES Purc.Parties (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Purc.PurchaseRequests ADD CONSTRAINT FK_PurchaseRequests_Statuses FOREIGN KEY (StatusId) REFERENCES Purc.Statuses (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Purc.SupplierActivities ADD CONSTRAINT FK_SupplierResourceTypes_ResourceTypes FOREIGN KEY (ResourceTypeId) REFERENCES Purc.ResourceTypes (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Purc.SupplierActivities ADD CONSTRAINT FK_SupplierResourceTypes_Suppliers FOREIGN KEY (SupplierId) REFERENCES Purc.Suppliers (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Purc.SupplierContacts ADD CONSTRAINT FK_SupplierContacts_Suppliers FOREIGN KEY (SupplierId) REFERENCES Purc.Suppliers (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Purc.SupplierDevices ADD CONSTRAINT FK_SupplierDevices_ResourceTypes FOREIGN KEY (ResourceTypeId) REFERENCES Purc.ResourceTypes (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Purc.SupplierDevices ADD CONSTRAINT FK_SupplierDevices_Suppliers FOREIGN KEY (SupplierId) REFERENCES Purc.Suppliers (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Sals.Contacts ADD CONSTRAINT FK_SContacts_Cities FOREIGN KEY (CityId) REFERENCES Sals.Cities (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Sals.Contacts ADD CONSTRAINT FK_Contacts_Parties FOREIGN KEY (PartyId) REFERENCES Sals.Parties (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Sals.Faults ADD CONSTRAINT FK_Faults_WorkRequests FOREIGN KEY (WorkRequestId) REFERENCES Sals.WorkRequests (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Sals.Parties ADD CONSTRAINT FK_Parties_Organizations FOREIGN KEY (OrganizationId) REFERENCES Sals.Organizations (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Sals.Sites ADD CONSTRAINT FK_Sals_Sites_Cities FOREIGN KEY (CityId) REFERENCES Sals.Cities (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Sals.Sites ADD CONSTRAINT FK_Sites_Parties FOREIGN KEY (PartyId) REFERENCES Sals.Parties (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Sals.WorkRequestActionLogs ADD CONSTRAINT FK_WorkRequestActionLogs_WorkRequests FOREIGN KEY (WorkRequestId) REFERENCES Sals.WorkRequests (Id) 
		ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [FMS_TEST2].Sals.WorkRequestItemTypes ADD CONSTRAINT FK_WorkRequestItemTypes_Statuses FOREIGN KEY (StatusId) REFERENCES Sals.Statuses (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Sals.WorkRequestItemTypes ADD CONSTRAINT FK_WorkRequestItemTypes_WorkRequests FOREIGN KEY (WorkRequestId) REFERENCES Sals.WorkRequests (Id) 
		ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [FMS_TEST2].Sals.WorkRequests ADD CONSTRAINT FK_WorkRequests_Cities FOREIGN KEY (CityId) REFERENCES Sals.Cities (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Sals.WorkRequests ADD CONSTRAINT FK_WorkRequests_Contacts FOREIGN KEY (ContactId) REFERENCES Sals.Contacts (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Sals.WorkRequests ADD CONSTRAINT FK_WorkRequests_Sites FOREIGN KEY (SiteId) REFERENCES Sals.Sites (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Sals.WorkRequests ADD CONSTRAINT FK_WorkRequests_Statuses FOREIGN KEY (StatusId) REFERENCES Sals.Statuses (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Sals.WorkRequests ADD CONSTRAINT FK_WorkRequests_WorkRequestTypes FOREIGN KEY (WorkRequestTypeId) REFERENCES Sals.WorkRequestTypes (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Wfrc.AgentEquipment ADD CONSTRAINT FK_AgentEquipment_Agents FOREIGN KEY (AgentId) REFERENCES Wfrc.Agents (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Wfrc.AgentLocations ADD CONSTRAINT FK_AgentParties_Agents FOREIGN KEY (AgentId) REFERENCES Wfrc.Agents (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Wfrc.AgentLocations ADD CONSTRAINT FK_AgentParties_Locations FOREIGN KEY (LocationId) REFERENCES Wfrc.Locations (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Wfrc.AgentLocations ADD CONSTRAINT FK_AgentParties_Parties FOREIGN KEY (PartyId) REFERENCES Wfrc.Parties (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Wfrc.Agents ADD CONSTRAINT FK_Agents_Cities FOREIGN KEY (CityId) REFERENCES Wfrc.Cities (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Wfrc.Agents ADD CONSTRAINT FK_Agents_Departments FOREIGN KEY (DepartmentId) REFERENCES Wfrc.Departments (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Wfrc.Agents ADD CONSTRAINT FK_Agents_JobRoles FOREIGN KEY (JobRoleId) REFERENCES Wfrc.JobRoles (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Wfrc.AgentsTeams ADD CONSTRAINT FK_AgentsTeams_Agents FOREIGN KEY (AgentId) REFERENCES Wfrc.Agents (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Wfrc.AgentsTeams ADD CONSTRAINT FK_AgentsTeams_Teams FOREIGN KEY (TeamId) REFERENCES Wfrc.Teams (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Wfrc.Cities ADD CONSTRAINT FK_Cities_Countries FOREIGN KEY (CountryId) REFERENCES Wfrc.Countries (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Wfrc.Cities ADD CONSTRAINT FK_Cities_Regions FOREIGN KEY (RegionId) REFERENCES Wfrc.Regions (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Wfrc.Locations ADD CONSTRAINT FK_Locations_Parties FOREIGN KEY (PartyId) REFERENCES Wfrc.Parties (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].Wfrc.Regions ADD CONSTRAINT FK_Regions_Countries FOREIGN KEY (CountryId) REFERENCES Wfrc.Countries (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
 
-- NO MODIFIED CONSTRAINTS
 
GO
 
-- OTHER MODIFIED DATA:
-- FUNCTION  		 CREATED 	Apr  2 2019  4:59PM		Publ.GetLocalized
CREATE FUNCTION [Publ].[GetLocalized]
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
		TOP (1) @ret=Value
	from [Publ].[Localizables] 
	where 
		EntityId=@EntityId AND 
		LocaleId=@LocaleId AND 
		EntityType=@EntityType AND 
		ColumnName=@PropertyName
	ORDER BY
		Id;

	if @ret IS null
		return @default;
	
	return @ret;
END
GO
-- FUNCTION  		 CREATED 	Apr  2 2019  5:00PM		Purc.GetLocalized
CREATE FUNCTION [Purc].[GetLocalized]
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
		TOP (1) @ret=Value
	from [Purc].[Localizables] 
	where 
		EntityId=@EntityId AND 
		LocaleId=@LocaleId AND 
		EntityType=@EntityType AND 
		ColumnName=@PropertyName
	ORDER BY
		Id;

	if @ret IS null
		return @default;
	
	return @ret;
END
GO
-- FUNCTION  		 CREATED 	Apr  2 2019  5:00PM		Sals.GetLocalized
CREATE FUNCTION [Sals].[GetLocalized]
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
		TOP (1) @ret=Value
	from [Sals].[Localizables] 
	where 
		EntityId=@EntityId AND 
		LocaleId=@LocaleId AND 
		EntityType=@EntityType AND 
		ColumnName=@PropertyName
	ORDER BY
		Id;

	if @ret IS null
		return @default;
	
	return @ret;
END
GO
-- FUNCTION  		 CREATED 	Apr  2 2019  5:01PM		Wfrc.GetLocalized
CREATE FUNCTION [Wfrc].[GetLocalized]
(
	-- Add the parameters for the function here
	@EntityType  varchar(50),
	@EntityId bigint,
	@LocaleId int,
	@PropertyName varchar(50),
	@def nvarchar(max)
)
RETURNS nvarchar(max)
AS
BEGIN
	DECLARE @ret nvarchar(max)=NULL;

	select 
		TOP (1) @ret=Value
	from [Wfrc].[Localizables] 
	where 
		EntityId=@EntityId AND 
		LocaleId=@LocaleId AND 
		EntityType=@EntityType AND 
		ColumnName=@PropertyName
	ORDER BY
		Id;

	if @ret IS null
		return @def;
	
	return @ret;
END
GO
-- FUNCTION  		 CREATED 	Apr  2 2019  5:04PM		Main.GetLocalized
CREATE FUNCTION [Main].[GetLocalized]
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
	from [Main].[Localizables] 
	where 
		EntityId=@EntityId AND 
		LocaleId=@LocaleId AND 
		EntityType=@EntityType AND 
		ColumnName=@PropertyName;

	if @ret IS null
		return @default;
	
	return @ret;
END

GO
-- FUNCTION  		 CREATED 	Sep 22 2019  5:44PM		Legl.GetLocalized

create FUNCTION [Legl].[GetLocalized]
(
	-- Add the parameters for the function here
	@EntityType  varchar(50),
	@EntityId bigint,
	@LocaleId int,
	@PropertyName varchar(50),
	@def nvarchar(max)
)
RETURNS nvarchar(max)
AS
BEGIN
	DECLARE @ret nvarchar(max)=NULL;

	select 
		TOP (1) @ret=Value
	from [Legl].[Localizables] 
	where 
		EntityId=@EntityId AND 
		LocaleId=@LocaleId AND 
		EntityType=@EntityType AND 
		ColumnName=@PropertyName
	ORDER BY
		Id;

	if @ret IS null
		return @def;
	
	return @ret;
END
GO
-- FUNCTION  		 CREATED 	Apr 24 2019  3:20PM		Asts.GetNameChain
-- FUNCTION  		 MODIFIED 	May 15 2019  5:50PM		Asts.GetNameChain
CREATE FUNCTION [Asts].[GetNameChain]
(
	@EntityType varchar(50),
	@LocaleId int,
	@chain nvarchar(max)
)
RETURNS nvarchar(max)
AS
BEGIN
	declare @ColumnNameList nvarchar(max);

	if(@EntityType='Location')
		SELECT  
			@ColumnNameList = COALESCE(@ColumnNameList,'')+(CASE WHEN (@ColumnNameList is NULL) THEN '' ELSE ' / ' END) + Asts.GetLocalized(@EntityType,Id,@LocaleId,'Name',Name )
		FROM Asts.Locations 
		WHERE @chain like '%|'+convert(varchar(15),Id)+'|%';

	else if(@EntityType='ResourceTypeFolder')
	begin
		
		SELECT  
			@ColumnNameList = COALESCE(@ColumnNameList,'')+(CASE WHEN (@ColumnNameList is NULL) THEN '' ELSE ' / ' END) + Asts.GetLocalized(@EntityType,Id,@LocaleId,'Name',Name )
		FROM Asts.ResourceTypeFolders 
		where @chain like '%|'+convert(varchar(15),Id)+'|%';
		
	end;
	return @ColumnNameList;
END

GO
-- FUNCTION  		 CREATED 	Apr 24 2019  5:07PM		Main.GetNameChain
-- FUNCTION  		 MODIFIED 	May  9 2019  2:03PM		Main.GetNameChain
CREATE FUNCTION [Main].[GetNameChain]
(
	-- Add the parameters for the function here
	@EntityType varchar(50),
	@LocaleId int,
	@chain nvarchar(max)
)
RETURNS nvarchar(max)
AS
BEGIN
	declare @ColumnNameList nvarchar(max);
	if(@EntityType='WorkOrderType')

		SELECT  
			@ColumnNameList = COALESCE(@ColumnNameList,'')+(CASE WHEN (@ColumnNameList is NULL) THEN '' ELSE ' / ' END) + Main.GetLocalized(@EntityType,Id,@LocaleId,'Name',Name )
		FROM Main.WorkOrderTypes 
		WHERE @chain LIKE '%|'+convert(varchar(15),Id)+'|%';
		return @ColumnNameList;
END

GO
-- FUNCTION  		 CREATED 	Dec 26 2018  2:08PM		dbo.GenerateId
CREATE FUNCTION [dbo].[GenerateId]
(
	@time Datetime,
	@rowNum int
)
RETURNS bigint
AS
BEGIN
	DECLARE @id bigint;
	DECLARE @idString varchar(15);
	DECLARE @secs int;
	select @secs=(DATEPART(hour,@time)*3600)+(DATEPART(MINUTE,@time)*60)+DATEPART(SECOND,@time);
	if(@rowNum>999)
	SET @secs=@secs+1;
	select @idString=CONCAT(
		(YEAR(@time)-2000),
		RIGHT('000'+CAST(DATEPART(dayofyear,@time) as varchar(5)),3),
		RIGHT('00000'+CAST(@secs as varchar(5)),5),
		RIGHT('000'+CAST(@rowNum as varchar(5)),3)
	)
	RETURN CONVERT(bigint,@idString);
END

GO
-- FUNCTION  		 CREATED 	Jul 16 2018  1:11PM		dbo.fn_diagramobjects

	CREATE FUNCTION dbo.fn_diagramobjects() 
	RETURNS int
	WITH EXECUTE AS N'dbo'
	AS
	BEGIN
		declare @id_upgraddiagrams		int
		declare @id_sysdiagrams			int
		declare @id_helpdiagrams		int
		declare @id_helpdiagramdefinition	int
		declare @id_creatediagram	int
		declare @id_renamediagram	int
		declare @id_alterdiagram 	int 
		declare @id_dropdiagram		int
		declare @InstalledObjects	int

		select @InstalledObjects = 0

		select 	@id_upgraddiagrams = object_id(N'dbo.sp_upgraddiagrams'),
			@id_sysdiagrams = object_id(N'dbo.sysdiagrams'),
			@id_helpdiagrams = object_id(N'dbo.sp_helpdiagrams'),
			@id_helpdiagramdefinition = object_id(N'dbo.sp_helpdiagramdefinition'),
			@id_creatediagram = object_id(N'dbo.sp_creatediagram'),
			@id_renamediagram = object_id(N'dbo.sp_renamediagram'),
			@id_alterdiagram = object_id(N'dbo.sp_alterdiagram'), 
			@id_dropdiagram = object_id(N'dbo.sp_dropdiagram')

		if @id_upgraddiagrams is not null
			select @InstalledObjects = @InstalledObjects + 1
		if @id_sysdiagrams is not null
			select @InstalledObjects = @InstalledObjects + 2
		if @id_helpdiagrams is not null
			select @InstalledObjects = @InstalledObjects + 4
		if @id_helpdiagramdefinition is not null
			select @InstalledObjects = @InstalledObjects + 8
		if @id_creatediagram is not null
			select @InstalledObjects = @InstalledObjects + 16
		if @id_renamediagram is not null
			select @InstalledObjects = @InstalledObjects + 32
		if @id_alterdiagram  is not null
			select @InstalledObjects = @InstalledObjects + 64
		if @id_dropdiagram is not null
			select @InstalledObjects = @InstalledObjects + 128
		
		return @InstalledObjects 
	END
	
GO
-- FUNCTION  		 CREATED 	Dec 26 2018  2:07PM		dbo.NumberString
CREATE function [dbo].[NumberString](
	@val bigint,
	@minLength int
)
returns varchar(15)
begin
	declare @res varchar(15)=convert(varchar(15),@val);

	if(len(@val)<@minLength)
		select @res= RIGHT('00000000000000'+@res,@minLength)
	return @res;
end;
GO
-- FUNCTION  		 CREATED 	Apr  2 2019  4:59PM		Asts.GetLocalized
-- FUNCTION  		 MODIFIED 	May 15 2019  5:45PM		Asts.GetLocalized
CREATE FUNCTION [Asts].[GetLocalized]
(
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
		TOP (1) @ret=Value
	from [Asts].[Localizables] 
	where 
		EntityId=@EntityId AND 
		LocaleId=@LocaleId AND 
		EntityType=@EntityType AND 
		ColumnName=@PropertyName
	ORDER BY
		Id;

	if @ret IS null
		return @default;
	
	return @ret;
END

GO
-- PROCEDURE  		 CREATED 	Sep 19 2018  3:45PM		dbo.ResetSequence
CREATE PROCEDURE [dbo].[ResetSequence]
	-- Add the parameters for the stored procedure here
	@sequenceName VARCHAR(60)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @query NVARCHAR(400);
	SELECT @query=N'DROP SEQUENCE ' + @sequenceName + N'; CREATE SEQUENCE '+@sequenceName+N' START WITH 1 INCREMENT BY 1'; 
	PRINT @query;
	EXECUTE sp_executesql @query;
END
GO
-- PROCEDURE  		 CREATED 	Jul 16 2018  1:11PM		dbo.sp_helpdiagrams

	CREATE PROCEDURE dbo.sp_helpdiagrams
	(
		@diagramname sysname = NULL,
		@owner_id int = NULL
	)
	WITH EXECUTE AS N'dbo'
	AS
	BEGIN
		DECLARE @user sysname
		DECLARE @dboLogin bit
		EXECUTE AS CALLER;
			SET @user = USER_NAME();
			SET @dboLogin = CONVERT(bit,IS_MEMBER('db_owner'));
		REVERT;
		SELECT
			[Database] = DB_NAME(),
			[Name] = name,
			[ID] = diagram_id,
			[Owner] = USER_NAME(principal_id),
			[OwnerID] = principal_id
		FROM
			sysdiagrams
		WHERE
			(@dboLogin = 1 OR USER_NAME(principal_id) = @user) AND
			(@diagramname IS NULL OR name = @diagramname) AND
			(@owner_id IS NULL OR principal_id = @owner_id)
		ORDER BY
			4, 5, 1
	END
	
GO
-- PROCEDURE  		 CREATED 	Dec 26 2018  2:09PM		dbo.GenerateSequenceNumber

CREATE procedure [dbo].[GenerateSequenceNumber](
	@table varchar(255),
	@column varchar(255),
	@sPattern varchar(255),
	@minLength int,
	@param varchar(255)='',
	@out varchar(255) OUTPUT
)
AS
begin
	
	declare @max bigint=0;
	declare @pattern varchar(255);
	declare @date datetime =GETDATE();
	declare @day varchar(2)=(select dbo.NumberString(DAY(@date),2));
	declare @dayOfYear varchar(3)=(select dbo.NumberString(DATEPART(dayofyear,@date),3));
	declare @year4 varchar(4)=convert(varchar(4),YEAR(@date));
	declare @year2 varchar(2)=(select RIGHT(@year4,2));
	declare @mon varchar(2)=(select dbo.NumberString(MONTH(@date),2));
	declare @client varchar(3)=(select dbo.NumberString(5,2))

	if(@param is NULL)
		set @param='';

	select @pattern=replace(@sPattern,'{YEAR4}',@year4);
	select @pattern=replace(@sPattern,'{YEAR2}',@year2);
	select @pattern=replace(@pattern,'{MONTH}',@mon);
	select @pattern=replace(@pattern,'{DAYOFYEAR}',@dayOfYear);
	select @pattern=replace(@pattern,'{DAY}',@day);
	select @pattern=replace(@pattern,'{PARAM}',@param);

	declare @q nvarchar(max)=N'
	select 
		@max= MAX(convert(bigint, replace('+@column+','''+@pattern+''',''''))) 
	from 
		'+@table+' where '+@column+' like '''+@pattern+'[0123456789]%''';
	
	exec sp_executesql @q,N'@max bigint OUTPUT',@max OUTPUT;
	
	if(@max is null)
		select @max=0;
	select @out=concat( @pattern,dbo.NumberString(@max+1,@minLength));
	--print @out;
	
end;


GO
-- PROCEDURE  		 CREATED 	Aug  1 2018  6:33PM		dbo.GetTablesRowCount
--select * from sys.tables
create PROCEDURE dbo.GetTablesRowCount
as
begin
declare @command nvarchar(max);
declare @tables TABLE(ID int,TABLE_NAME varchar(60),RowCnt int null);
declare @i int=0;
declare @table varchar(60);
declare @c int=0;

insert into @tables (ID,TABLE_NAME)
	select 
		ROW_NUMBER() over (order by TABLE_SCHEMA+'.'+ TABLE_NAME ) ,
		TABLE_SCHEMA+'.'+ TABLE_NAME 
	from INFORMATION_SCHEMA.TABLES
	where 
		TABLE_NAME!='sysdiagrams'
	order by TABLE_SCHEMA+'.'+ TABLE_NAME 
		;

	select @i=0;
	while(@i<(select count(*) from @tables))
	begin
		select @table=TABLE_NAME from @tables where ID=@i;
		select @command='SELECT @cOut=COUNT(*) FROM '+@table;
		
		EXEC sp_executesql @command, N'@cOut nvarchar(10) OUTPUT',@cOut=@c OUTPUT;
		update @tables set RowCnt=@c where ID=@i;
		
		select @i=@i+1;
	end;

	select * from @tables where RowCnt>0;
end;
GO
-- PROCEDURE  		 CREATED 	Jul 16 2018  1:11PM		dbo.sp_upgraddiagrams

	CREATE PROCEDURE dbo.sp_upgraddiagrams
	AS
	BEGIN
		IF OBJECT_ID(N'dbo.sysdiagrams') IS NOT NULL
			return 0;
	
		CREATE TABLE dbo.sysdiagrams
		(
			name sysname NOT NULL,
			principal_id int NOT NULL,	-- we may change it to varbinary(85)
			diagram_id int PRIMARY KEY IDENTITY,
			version int,
	
			definition varbinary(max)
			CONSTRAINT UK_principal_name UNIQUE
			(
				principal_id,
				name
			)
		);


		/* Add this if we need to have some form of extended properties for diagrams */
		/*
		IF OBJECT_ID(N'dbo.sysdiagram_properties') IS NULL
		BEGIN
			CREATE TABLE dbo.sysdiagram_properties
			(
				diagram_id int,
				name sysname,
				value varbinary(max) NOT NULL
			)
		END
		*/

		IF OBJECT_ID(N'dbo.dtproperties') IS NOT NULL
		begin
			insert into dbo.sysdiagrams
			(
				[name],
				[principal_id],
				[version],
				[definition]
			)
			select	 
				convert(sysname, dgnm.[uvalue]),
				DATABASE_PRINCIPAL_ID(N'dbo'),			-- will change to the sid of sa
				0,							-- zero for old format, dgdef.[version],
				dgdef.[lvalue]
			from dbo.[dtproperties] dgnm
				inner join dbo.[dtproperties] dggd on dggd.[property] = 'DtgSchemaGUID' and dggd.[objectid] = dgnm.[objectid]	
				inner join dbo.[dtproperties] dgdef on dgdef.[property] = 'DtgSchemaDATA' and dgdef.[objectid] = dgnm.[objectid]
				
			where dgnm.[property] = 'DtgSchemaNAME' and dggd.[uvalue] like N'_EA3E6268-D998-11CE-9454-00AA00A3F36E_' 
			return 2;
		end
		return 1;
	END
	
GO
-- PROCEDURE  		 CREATED 	Jul 16 2018  1:11PM		dbo.sp_helpdiagramdefinition

	CREATE PROCEDURE dbo.sp_helpdiagramdefinition
	(
		@diagramname 	sysname,
		@owner_id	int	= null 		
	)
	WITH EXECUTE AS N'dbo'
	AS
	BEGIN
		set nocount on

		declare @theId 		int
		declare @IsDbo 		int
		declare @DiagId		int
		declare @UIDFound	int
	
		if(@diagramname is null)
		begin
			RAISERROR (N'E_INVALIDARG', 16, 1);
			return -1
		end
	
		execute as caller;
		select @theId = DATABASE_PRINCIPAL_ID();
		select @IsDbo = IS_MEMBER(N'db_owner');
		if(@owner_id is null)
			select @owner_id = @theId;
		revert; 
	
		select @DiagId = diagram_id, @UIDFound = principal_id from dbo.sysdiagrams where principal_id = @owner_id and name = @diagramname;
		if(@DiagId IS NULL or (@IsDbo = 0 and @UIDFound <> @theId ))
		begin
			RAISERROR ('Diagram does not exist or you do not have permission.', 16, 1);
			return -3
		end

		select version, definition FROM dbo.sysdiagrams where diagram_id = @DiagId ; 
		return 0
	END
	
GO
-- PROCEDURE  		 CREATED 	Feb 14 2019  3:48PM		Purc.FindSuppliers

CREATE PROCEDURE Purc.FindSuppliers
	@table dbo.RequiredItem READONLY
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    
	select * from Purc.Suppliers ss
	where	(
				select COUNT(1)
				from (
						select ac.ResourceComponentId
						from Purc.SupplierActivities ac
							join @table t on t.ResourceComponentId=ac.ResourceComponentId AND t.ResourceTypeId=ac.ResourceTypeId
						group by ac.ResourceComponentId,ac.ResourceTypeId,SupplierId 
						having SupplierId=ss.id
					)  as E
			) = (select COUNT(*) from @table)
END

GO
-- PROCEDURE  		 CREATED 	Jul 16 2018  1:11PM		dbo.sp_creatediagram

	CREATE PROCEDURE dbo.sp_creatediagram
	(
		@diagramname 	sysname,
		@owner_id		int	= null, 	
		@version 		int,
		@definition 	varbinary(max)
	)
	WITH EXECUTE AS 'dbo'
	AS
	BEGIN
		set nocount on
	
		declare @theId int
		declare @retval int
		declare @IsDbo	int
		declare @userName sysname
		if(@version is null or @diagramname is null)
		begin
			RAISERROR (N'E_INVALIDARG', 16, 1);
			return -1
		end
	
		execute as caller;
		select @theId = DATABASE_PRINCIPAL_ID(); 
		select @IsDbo = IS_MEMBER(N'db_owner');
		revert; 
		
		if @owner_id is null
		begin
			select @owner_id = @theId;
		end
		else
		begin
			if @theId <> @owner_id
			begin
				if @IsDbo = 0
				begin
					RAISERROR (N'E_INVALIDARG', 16, 1);
					return -1
				end
				select @theId = @owner_id
			end
		end
		-- next 2 line only for test, will be removed after define name unique
		if EXISTS(select diagram_id from dbo.sysdiagrams where principal_id = @theId and name = @diagramname)
		begin
			RAISERROR ('The name is already used.', 16, 1);
			return -2
		end
	
		insert into dbo.sysdiagrams(name, principal_id , version, definition)
				VALUES(@diagramname, @theId, @version, @definition) ;
		
		select @retval = @@IDENTITY 
		return @retval
	END
	
GO
-- PROCEDURE  		 CREATED 	Jul 16 2018  1:11PM		dbo.sp_renamediagram

	CREATE PROCEDURE dbo.sp_renamediagram
	(
		@diagramname 		sysname,
		@owner_id		int	= null,
		@new_diagramname	sysname
	
	)
	WITH EXECUTE AS 'dbo'
	AS
	BEGIN
		set nocount on
		declare @theId 			int
		declare @IsDbo 			int
		
		declare @UIDFound 		int
		declare @DiagId			int
		declare @DiagIdTarg		int
		declare @u_name			sysname
		if((@diagramname is null) or (@new_diagramname is null))
		begin
			RAISERROR ('Invalid value', 16, 1);
			return -1
		end
	
		EXECUTE AS CALLER;
		select @theId = DATABASE_PRINCIPAL_ID();
		select @IsDbo = IS_MEMBER(N'db_owner'); 
		if(@owner_id is null)
			select @owner_id = @theId;
		REVERT;
	
		select @u_name = USER_NAME(@owner_id)
	
		select @DiagId = diagram_id, @UIDFound = principal_id from dbo.sysdiagrams where principal_id = @owner_id and name = @diagramname 
		if(@DiagId IS NULL or (@IsDbo = 0 and @UIDFound <> @theId))
		begin
			RAISERROR ('Diagram does not exist or you do not have permission.', 16, 1)
			return -3
		end
	
		-- if((@u_name is not null) and (@new_diagramname = @diagramname))	-- nothing will change
		--	return 0;
	
		if(@u_name is null)
			select @DiagIdTarg = diagram_id from dbo.sysdiagrams where principal_id = @theId and name = @new_diagramname
		else
			select @DiagIdTarg = diagram_id from dbo.sysdiagrams where principal_id = @owner_id and name = @new_diagramname
	
		if((@DiagIdTarg is not null) and  @DiagId <> @DiagIdTarg)
		begin
			RAISERROR ('The name is already used.', 16, 1);
			return -2
		end		
	
		if(@u_name is null)
			update dbo.sysdiagrams set [name] = @new_diagramname, principal_id = @theId where diagram_id = @DiagId
		else
			update dbo.sysdiagrams set [name] = @new_diagramname where diagram_id = @DiagId
		return 0
	END
	
GO
-- PROCEDURE  		 CREATED 	Jul 16 2018  1:11PM		dbo.sp_alterdiagram

	CREATE PROCEDURE dbo.sp_alterdiagram
	(
		@diagramname 	sysname,
		@owner_id	int	= null,
		@version 	int,
		@definition 	varbinary(max)
	)
	WITH EXECUTE AS 'dbo'
	AS
	BEGIN
		set nocount on
	
		declare @theId 			int
		declare @retval 		int
		declare @IsDbo 			int
		
		declare @UIDFound 		int
		declare @DiagId			int
		declare @ShouldChangeUID	int
	
		if(@diagramname is null)
		begin
			RAISERROR ('Invalid ARG', 16, 1)
			return -1
		end
	
		execute as caller;
		select @theId = DATABASE_PRINCIPAL_ID();	 
		select @IsDbo = IS_MEMBER(N'db_owner'); 
		if(@owner_id is null)
			select @owner_id = @theId;
		revert;
	
		select @ShouldChangeUID = 0
		select @DiagId = diagram_id, @UIDFound = principal_id from dbo.sysdiagrams where principal_id = @owner_id and name = @diagramname 
		
		if(@DiagId IS NULL or (@IsDbo = 0 and @theId <> @UIDFound))
		begin
			RAISERROR ('Diagram does not exist or you do not have permission.', 16, 1);
			return -3
		end
	
		if(@IsDbo <> 0)
		begin
			if(@UIDFound is null or USER_NAME(@UIDFound) is null) -- invalid principal_id
			begin
				select @ShouldChangeUID = 1 ;
			end
		end

		-- update dds data			
		update dbo.sysdiagrams set definition = @definition where diagram_id = @DiagId ;

		-- change owner
		if(@ShouldChangeUID = 1)
			update dbo.sysdiagrams set principal_id = @theId where diagram_id = @DiagId ;

		-- update dds version
		if(@version is not null)
			update dbo.sysdiagrams set version = @version where diagram_id = @DiagId ;

		return 0
	END
	
GO
-- PROCEDURE  		 CREATED 	Jul 16 2018  1:11PM		dbo.sp_dropdiagram

	CREATE PROCEDURE dbo.sp_dropdiagram
	(
		@diagramname 	sysname,
		@owner_id	int	= null
	)
	WITH EXECUTE AS 'dbo'
	AS
	BEGIN
		set nocount on
		declare @theId 			int
		declare @IsDbo 			int
		
		declare @UIDFound 		int
		declare @DiagId			int
	
		if(@diagramname is null)
		begin
			RAISERROR ('Invalid value', 16, 1);
			return -1
		end
	
		EXECUTE AS CALLER;
		select @theId = DATABASE_PRINCIPAL_ID();
		select @IsDbo = IS_MEMBER(N'db_owner'); 
		if(@owner_id is null)
			select @owner_id = @theId;
		REVERT; 
		
		select @DiagId = diagram_id, @UIDFound = principal_id from dbo.sysdiagrams where principal_id = @owner_id and name = @diagramname 
		if(@DiagId IS NULL or (@IsDbo = 0 and @UIDFound <> @theId))
		begin
			RAISERROR ('Diagram does not exist or you do not have permission.', 16, 1)
			return -3
		end
	
		delete from dbo.sysdiagrams where diagram_id = @DiagId;
	
		return 0;
	END
	
GO
-- PROCEDURE  		 CREATED 	Jul 12 2018  3:42PM		dbo.AddAuditingColumns

CREATE PROCEDURE [dbo].[AddAuditingColumns]

AS
BEGIN
	declare @command nvarchar(max);
	declare @tables TABLE(ID int,TABLE_NAME varchar(60));
	declare @i int=0;
	declare @table varchar(60);

	insert into @tables (ID,TABLE_NAME)
	select 
		ROW_NUMBER() over (order by TABLE_NAME) ,
		TABLE_SCHEMA+'.'+ TABLE_NAME 
	from INFORMATION_SCHEMA.TABLES
	where 
		TABLE_NAME!='sysdiagrams' AND
		TABLE_SCHEMA+'.'+TABLE_NAME NOT IN (SELECT TABLE_SCHEMA+'.'+TABLE_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE COLUMN_NAME='CreatedOn');

	select @i=0;
	while(@i<(select count(*) from @tables))
	begin
		select @i=@i+1;
		select @table=TABLE_NAME from @tables where ID=@i;
		select @command='ALTER TABLE '+@table+' ADD CreatedOn Datetime null;'
		print @command;
		EXEC sp_executesql @command;
	end;
	delete from @tables

	
	insert into @tables (ID,TABLE_NAME)
	select 
		ROW_NUMBER() over (order by TABLE_NAME) ,
		TABLE_SCHEMA+'.'+ TABLE_NAME 
	from INFORMATION_SCHEMA.TABLES
	where 
		TABLE_NAME!='sysdiagrams' AND
		TABLE_SCHEMA+'.'+TABLE_NAME NOT IN (SELECT TABLE_SCHEMA+'.'+TABLE_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE COLUMN_NAME='CreatedBy');

	select @i=0;
	while(@i<(select count(*) from @tables))
	begin
		select @i=@i+1;
		select @table=TABLE_NAME from @tables where ID=@i;
		select @command='ALTER TABLE '+@table+' ADD CreatedBy bigint null;'
		print @command;
		EXEC sp_executesql @command;
	end;
	delete from @tables

	insert into @tables (ID,TABLE_NAME)
	select 
		ROW_NUMBER() over (order by TABLE_NAME) ,
		TABLE_SCHEMA+'.'+ TABLE_NAME 
	from INFORMATION_SCHEMA.TABLES
	where 
		TABLE_NAME!='sysdiagrams' AND
		TABLE_SCHEMA+'.'+TABLE_NAME NOT IN (SELECT TABLE_SCHEMA+'.'+TABLE_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE COLUMN_NAME='UpdatedOn');

	select @i=0;
	while(@i<(select count(*) from @tables))
	begin
		select @i=@i+1;
		select @table=TABLE_NAME from @tables where ID=@i;
		select @command='ALTER TABLE '+@table+' ADD UpdatedOn Datetime null;';
		print @command;
		EXEC sp_executesql @command;
	end;
	delete from @tables

	insert into @tables (ID,TABLE_NAME)
	select 
		ROW_NUMBER() over (order by TABLE_NAME) ,
		TABLE_SCHEMA+'.'+ TABLE_NAME 
	from INFORMATION_SCHEMA.TABLES
	where 
		TABLE_NAME!='sysdiagrams' AND
		TABLE_SCHEMA+'.'+TABLE_NAME NOT IN (SELECT TABLE_SCHEMA+'.'+TABLE_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE COLUMN_NAME='UpdatedBy');

	select @i=0;
	while(@i<(select count(*) from @tables))
	begin
		select @i=@i+1;
		select @table=TABLE_NAME from @tables where ID=@i;
		select @command='ALTER TABLE '+@table+' ADD UpdatedBy bigint null;';
		print @command;
		EXEC sp_executesql @command;
	end;
	delete from @tables

END;
GO
-- SEQUENCE  		 CREATED 	Jan 20 2020 11:57AM		StockTransactions_SEQ
 
GO
-- TRIGGER  		 CREATED 	Jan 15 2019 11:12AM		Locations_Chain_TR

CREATE trigger [Wfrc].[Locations_Chain_TR]
    ON [Wfrc].[Locations]
    AFTER INSERT, UPDATE 
	
	AS
		DECLARE @i int=1;
        DECLARE @id BIGINT;
		DECLARE @name NVARCHAR(max);
        
        DECLARE @chain NVARCHAR(max);
		DECLARE @nameChain NVARCHAR(max);

		DECLARE @parentId bigint;

		while(@i<=(select count(*) from inserted))
		begin
			 SELECT 
				@id=Id,
				@name=Name,
				@parentId=ParentId
			FROM (
				select ROW_NUMBER() over (order by Id) i,*
				from inserted
			) TX
			WHERE i=@i;

			SET @i=@i+1;

			IF(@parentId IS NULL)
				SELECT 
					@chain='|'+CONVERT(NVARCHAR(25),@id)+'|',
					@nameChain=@name;
				
			ELSE
				SELECT 
					@chain=Chain+CONVERT(NVARCHAR(25),@id)+'|',
					@nameChain=NameChain+' / '+@name
				FROM Locations WHERE Id=@parentId;

			UPDATE Locations 
			SET 
				Chain=@chain,
				NameChain=@nameChain
			WHERE Id=@id;
		end

GO
-- TRIGGER  		 CREATED 	Feb 12 2019  5:57PM		WorkRequests_TRG
--PurchaseRequests	: Replace with table name
--SequenceNumber	: Replace with column name
--<[PATTERN]>		: Replace with pattern
--<[EXTRA_DATA]>	: optional extra data to pattern

CREATE TRIGGER [Purc].[WorkRequests_TRG]
   ON  [Purc].[PurchaseRequests]
   AFTER INSERT
AS 
BEGIN

	DECLARE @i INT=1;
	DECLARE @id BIGINT;
	DECLARE @extraData BIGINT='';
	DECLARE @pattern VARCHAR(255)='';
	DECLARE @minLent INT=0;
	DECLARE @seq VARCHAR(50);

	select 
		TOP 1
		@pattern=PurchaseRequestPattern,
		@minLent=PuchaseRequestCodeLength
	from Purc.PurchasingConfig;

	WHILE(@i<=(SELECT Count(*) FROM inserted))
	BEGIN
		SELECT 
			@id=Id
		FROM (
			SELECT 
				ROW_NUMBER() OVER (ORDER BY Id) i,
				Id
			FROM inserted
		) INS
		WHERE i = @i;
		
		EXEC dbo.GenerateSequenceNumber 'Purc.PurchaseRequests' , 'SequenceNumber' , @pattern , @minLent , @extraData , @seq OUTPUT
		UPDATE PurchaseRequests SET SequenceNumber = @seq WHERE Id = @id;
		SET @i = @i + 1;
	END;
END;

GO
-- TRIGGER  		 CREATED 	Aug  4 2019  5:44PM		Items_TRG
CREATE TRIGGER [Asts].[Items_TRG]
   ON  [Asts].[Items]
   AFTER INSERT
AS 
BEGIN

	declare @i int=1;
	declare @id bigint;
	declare @pattern varchar(255);
	declare @minLent int;
	declare @seq varchar(50);
	declare @code varchar(100);
	
	select 		
		@minLent=ResourceTypeCodeMinLength
	from Asts.AssetsConfig

	while(@i<=(select Count(*) from inserted))
	begin
		select 
			@id=Id,
			@pattern=CodePattern,
			@code=Code
		from (
			select 
				ROW_NUMBER() over (order by Id) i,
				Id,
				CodePattern,
				Code
			from inserted
		) INS
		where i=@i;
		
		if(@pattern IS NOT NULL AND @code is NULL)
		begin
		
			exec dbo.GenerateSequenceNumber 'Asts.Items','Code',@pattern,@minLent,'',@seq OUTPUT;
			
			update Asts.Items set Code=@seq where Id=@id;
		end;
		
		set @i=@i+1;
	end;
END;

GO
-- TRIGGER  		 CREATED 	Oct 31 2018 11:59AM		Locations_Chain_TR
-- TRIGGER  		 MODIFIED 	Jun 25 2019  3:48PM		Locations_Chain_TR
-- TRIGGER  		 MODIFIED 	Jun 25 2019  1:21PM		Locations_Chain_TR
CREATE trigger [Asts].[Locations_Chain_TR]
    ON [Asts].[Locations]
    AFTER INSERT, UPDATE 
	
	AS
		DECLARE @i int=1;
        DECLARE @id BIGINT;
		DECLARE @name NVARCHAR(max);
        
        DECLARE @chain NVARCHAR(max);
		DECLARE @nameChain NVARCHAR(max);


		DECLARE @parentId bigint;

		while(@i<=(select count(*) from inserted))
		begin
			 SELECT 
				@id=Id,
				@name=Name,
				@parentId=ParentId
			FROM (
				select ROW_NUMBER() over (order by Id) i,*
				from inserted
			) TX
			WHERE i=@i;

			SET @i=@i+1;

			IF(@parentId IS NULL)
				SELECT 
					@chain='|'+CONVERT(NVARCHAR(25),@id)+'|',
					@nameChain=@name;
				
			ELSE
				SELECT 
					@chain=Chain+CONVERT(NVARCHAR(25),@id)+'|',
					@nameChain=NameChain+' / '+@name
				FROM Locations WHERE Id=@parentId;

			UPDATE Locations 
			SET 
				Chain=@chain,
				NameChain=@nameChain
			WHERE Id=@id;
		end

GO
-- TRIGGER  		 CREATED 	Oct 31 2018  4:40PM		WorkOrderTypes_Chain_TR

CREATE trigger [Main].[WorkOrderTypes_Chain_TR]
    ON [Main].[WorkOrderTypes]
    AFTER INSERT, UPDATE 
	
	AS
        DECLARE @id BIGINT;
		DECLARE @name NVARCHAR(max);
        
        DECLARE @chain NVARCHAR(max);
		DECLARE @nameChain NVARCHAR(max);

		DECLARE @parentId bigint;

        SELECT 
			@id=Id,
			@name=Name,
			@parentId=ParentId
		FROM inserted;

        IF(@parentId IS NULL)
            SELECT 
				@chain='|'+CONVERT(NVARCHAR(25),@id)+'|',
				@nameChain=@name;
				
        ELSE
            SELECT 
				@chain=Chain+CONVERT(NVARCHAR(25),@id)+'|',
				@nameChain=NameChain+' / '+@name
			FROM [WorkOrderTypes] WHERE Id=@parentId;

        UPDATE [WorkOrderTypes] 
		SET 
			Chain=@chain,
			NameChain=@nameChain
		WHERE Id=@id;


GO
-- TRIGGER  		 CREATED 	Apr 30 2019 10:51AM		Parties_TRG
CREATE TRIGGER [Sals].[Parties_TRG]
	ON Sals.Parties
	AFTER INSERT
AS
BEGIN
SET NOCOUNT ON;
declare @max bigint=(select max(Code) from Sals.Parties);
if @max is null
set @max=0;
declare @i bigint=1;
declare @id bigint=0;
while(@i<=(select count(*) from inserted))

	begin
		select 
			@id=Id
		from (
			select 
				ROW_NUMBER() over (order by Id) i,
				Id
			from inserted
		) INS
		where i=@i;
		
		update Sals.Parties set Code=(@max+@i) where Id=@id;
		set @i=@i+1;
	end;
END

GO
-- TRIGGER  		 CREATED 	Dec  6 2018  8:47PM		StockTransactions_TRG

CREATE TRIGGER [Asts].[StockTransactions_TRG]
   ON  [Asts].[StockTransactions]
   AFTER INSERT
AS 
BEGIN

	DECLARE @nowYear int=YEAR(GETDATE());
	DECLARE @nowMonth int=MONTH(GETDATE());
	DECLARE @lastYear int;
	DECLARE @lastMonth int;
	DECLARE @inc int;
	DECLARE @seq varchar(50);

	SELECT @lastYear=YEAR(MAX(CreatedOn)),@lastMonth=MONTH(MAX(CreatedOn)) FROM StockTransactions;

	if(@lastYear!=@nowYear AND @lastMonth!=@nowMonth)
		exec [dbo].[ResetSequence] 'Asts.StockTransactions_SEQ';
	
	SELECT @inc=NEXT VALUE FOR Asts.StockTransactions_SEQ;

	SELECT @seq=CONCAT('ST'+RIGHT(@nowYear,2),RIGHT(@nowMonth,2),RIGHT('00000'+CAST(@inc as varchar(5)),5));

    UPDATE StockTransactions SET SequenceNumber=@seq,CreatedOn=GETDATE() WHERE Id=(Select Id FROM inserted);

END;


GO
-- TRIGGER  		 CREATED 	Feb  3 2019  1:02PM		ResourceTypes_TRG

CREATE TRIGGER [Asts].[ResourceTypes_TRG]
   ON  [Asts].[ResourceTypes]
   AFTER INSERT,UPDATE
AS 
BEGIN

	declare @c int=(select Count(*) from inserted);
	declare @i int=1;
	declare @id bigint;
	declare @code varchar(50);
	declare @pattern varchar(255);
	declare @minLent int;
	declare @seq varchar(50);

	select 		
		@minLent=ResourceTypeCodeMinLength
	from Asts.AssetsConfig

	while(@i<=@c)
	begin
		select 
			@id=Id,
			@pattern=CodePattern,
			@code=Code
		from (
			select 
				ROW_NUMBER() over (order by Id) i,
				Id,
				CodePattern,
				Code
			from inserted
		) INS
		where i=@i;

		if(@pattern IS NOT NULL AND (@code IS NULL OR len(@code)=0))
		begin
			exec dbo.GenerateSequenceNumber 'Asts.ResourceTypes','Code',@pattern,@minLent,'',@seq OUTPUT;
			update Asts.ResourceTypes set Code=@seq where Id=@id;
		end;

		set @i=@i+1;
	end;
END;
GO
-- TRIGGER  		 CREATED 	Sep 26 2018 12:11PM		WorkRequest_TRG
CREATE TRIGGER [Sals].[WorkRequest_TRG]
   ON  [Sals].[WorkRequests]
   AFTER INSERT
AS 
BEGIN

	declare @c int=(select Count(*) from inserted);
	declare @i int=1;
	declare @id bigint;
	declare @partyCode bigint;
	declare @pattern varchar(255);
	declare @minLent int;
	declare @seq varchar(50);

	select 
		@pattern=WorkRequestSequencePattern,
		@minLent=WorkRequestSerialMinLength
	from Sals.SalesConfig;

	while(@i<=@c)
	begin
		select 
			@id=Id,
			@partyCode=PartyCode
		from (
			select 
				ROW_NUMBER() over (order by Id) i,
				Id,
				(
					select Code 
					from Sals.Parties 
					join Sals.Contacts on Sals.Contacts.PartyId=Sals.Parties.Id
					where ContactId=Sals.Contacts.Id
				) PartyCode
			from inserted
		) INS
		where i=@i;
		declare @party varchar(20)=(select dbo.NumberString(@partyCode,3));
		exec dbo.GenerateSequenceNumber 'Sals.WorkRequests','SequenceNumber',@pattern,@minLent,@party,@seq OUTPUT
		update Sals.WorkRequests set SequenceNumber=@seq where Id=@id;
		set @i=@i+1;
	end;
END;
GO
-- TRIGGER  		 CREATED 	Oct 16 2018 10:46AM		ResourceTypeFolders_Chain_TR

CREATE trigger [Asts].[ResourceTypeFolders_Chain_TR]
    ON [Asts].[ResourceTypeFolders]
    AFTER INSERT, UPDATE 
	
	AS

	SET NOCOUNT ON;

		declare @i int=1;
        DECLARE @id BIGINT;
		DECLARE @name NVARCHAR(max);
        DECLARE @chain NVARCHAR(max);
		DECLARE @nameChain NVARCHAR(max);
		DECLARE @parentId bigint;

	while(@i<=(select Count(*) from inserted))
	begin
	   SELECT 
	
			@id=Id,
			@name=Name,
			@parentId=ParentId
		FROM  (
			select 
				ROW_NUMBER() over (order by Id) i,
				Id,
				Name,
				ParentId
			from inserted
		) INS 	where i=@i;

        IF(@parentId IS NULL)
            SELECT 
				@chain='|'+CONVERT(NVARCHAR(25),@id)+'|',
				@nameChain=@name;
				
        ELSE
            SELECT 
				@chain=Chain+CONVERT(NVARCHAR(25),@id)+'|',
				@nameChain=NameChain+' / '+@name
			FROM ResourceTypeFolders WHERE Id=@parentId;

        UPDATE ResourceTypeFolders 
		SET 
			Chain=@chain,
			NameChain=@nameChain
		WHERE Id=@id;

		set @i=@i+1;
	end;






     

GO
-- TRIGGER  		 CREATED 	Feb 21 2019  3:46PM		PurchaseOrders_TRG

--PurchaseOrders	: Replace with table name
--SequenceNumber	: Replace with column name
--<[PATTERN]>		: Replace with pattern
--<[EXTRA_DATA]>	: optional extra data to pattern

CREATE TRIGGER [Purc].[PurchaseOrders_TRG]
   ON  [Purc].[PurchaseOrders]
   AFTER INSERT
AS 
BEGIN

	DECLARE @i INT=1;
	DECLARE @id BIGINT;
	DECLARE @extraData BIGINT='';
	DECLARE @pattern VARCHAR(255)='';
	DECLARE @minLent INT=0;
	DECLARE @seq VARCHAR(50);

	select 
		TOP 1
		@pattern=PurchaseOrderPattern,
		@minLent=PuchaseOrderCodeLength
	from Purc.PurchasingConfig;

	WHILE(@i<=(SELECT Count(*) FROM inserted))
	BEGIN
		SELECT 
			@id=Id
		FROM (
			SELECT 
				ROW_NUMBER() OVER (ORDER BY Id) i,
				Id
			FROM inserted
		) INS
		WHERE i = @i;
		
		EXEC dbo.GenerateSequenceNumber 'Purc.PurchaseOrders' , 'SequenceNumber' , @pattern , @minLent , @extraData , @seq OUTPUT
		UPDATE PurchaseOrders SET SequenceNumber = @seq WHERE Id = @id;
		SET @i = @i + 1;
	END;
END;


GO
-- TRIGGER  		 CREATED 	Jul 17 2019 12:25PM		WorkOrders_TRG
CREATE TRIGGER [Main].[WorkOrders_TRG]
   ON  [Main].[WorkOrders]
   AFTER INSERT
AS 
BEGIN

	declare @c int=(select Count(*) from inserted);
	declare @i int=1;
	declare @id bigint;
	declare @partyCode bigint;
	declare @woTypeCode varchar(5);
	declare @category varchar(3);
	declare @pattern varchar(50);
	declare @pat varchar(50);
	declare @minLent int;
	declare @seq varchar(50);

	select 
		@pattern=WorkOrderSequencePattern,
		@minLent=WorkOrderSerialMinLength
	from Main.MaintenanceConfig;

	while(@i<=@c)
	begin
		select 
			@id=Id,
			@partyCode=PartyCode,
			@woTypeCode=WoTypeCode,
			@category=Category
		from (
			select 
				ROW_NUMBER() over (order by Id) i,
				Id,
				(select Code from Main.Parties where Id=inserted.PartyId) PartyCode,
				(select Code from Main.WorkOrderTypes where Id=inserted.WorkOrderTypeId) WoTypeCode,
				(select CASE WHEN inserted.WorkOrderCategoryId=0 THEN 'PP' ELSE 'WO' END) Category
			from inserted
		) INS
		where i=@i;
		
		declare @cust varchar(5)=CASE WHEN @partyCode IS NULL THEN '' ELSE dbo.NumberString(@partyCode,3) END;

		select @pat=@pattern;		
		select @pat=replace(@pat,'{CUST}',@cust);
		select @pat=replace(@pat,'{WOT}',CASE WHEN @woTypeCode IS NULL THEN '' ELSE @woTypeCode END);
		select @pat=replace(@pat,'{WOC}',@category);
		
		exec dbo.GenerateSequenceNumber 'Main.WorkOrders','SequenceNumber',@pat,@minLent,@cust,@seq OUTPUT
		--print @seq;
		update Main.WorkOrders set SequenceNumber=@seq where Id=@id;
		set @i=@i+1;
	end;
END;

GO
-- TRIGGER  		 CREATED 	Feb 10 2019  2:14PM		PartTypes_TRG

CREATE TRIGGER [Asts].[PartTypes_TRG]
   ON  [Asts].[PartTypes]
   AFTER INSERT
AS 
BEGIN
	SET NOCOUNT ON;
	declare @c int=(select Count(*) from inserted);
	declare @i int=1;
	declare @id bigint;
	declare @partyCode bigint;
	declare @pattern varchar(255);
	declare @minLent int;
	declare @seq varchar(50);

	select 		
		@minLent=PartTypeCodeMinLength
	from Asts.AssetsConfig

	while(@i<=@c)
	begin
		select 
			@id=Id,
			@pattern=CodePattern
		from (
			select 
				ROW_NUMBER() over (order by Id) i,
				Id,
				CodePattern
			from inserted
		) INS
		where i=@i;

		if(@pattern IS NOT NULL)
		begin
			exec dbo.GenerateSequenceNumber 'Asts.PartTypes','Code',@pattern,@minLent,'',@seq OUTPUT;
			update Asts.PartTypes set Code=@seq where Id=@id;
		end;

		set @i=@i+1;
	end;
END;


GO
