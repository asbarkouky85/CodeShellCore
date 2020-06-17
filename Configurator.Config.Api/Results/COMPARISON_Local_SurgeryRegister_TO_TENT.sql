-- NO ADDED TABLES
 
-- ADDED COLUMNS :
ALTER TABLE [TENT].Auth.Apps ADD [Name] varchar(150) NULL;
ALTER TABLE [TENT].Auth.Apps ADD [DisplayName] nvarchar(150) NULL;
ALTER TABLE [TENT].Auth.Apps ADD [CreatedOn] datetime NULL;
ALTER TABLE [TENT].Auth.Apps ADD [CreatedBy] bigint NULL;
ALTER TABLE [TENT].Auth.Apps ADD [UpdatedOn] datetime NULL;
ALTER TABLE [TENT].Auth.Apps ADD [UpdatedBy] bigint NULL;
ALTER TABLE [TENT].Auth.Domains ADD [Name] varchar(50) NULL;
ALTER TABLE [TENT].Auth.Domains ADD [CreatedOn] datetime NULL;
ALTER TABLE [TENT].Auth.Domains ADD [UpdatedOn] datetime NULL;
ALTER TABLE [TENT].Auth.Domains ADD [CreatedBy] bigint NULL;
ALTER TABLE [TENT].Auth.Domains ADD [UpdatedBy] bigint NULL;
ALTER TABLE [TENT].Auth.ResourceActions ADD [Name] varchar(150) NULL;
ALTER TABLE [TENT].Auth.ResourceActions ADD [ResourceId] bigint NOT NULL;
ALTER TABLE [TENT].Auth.ResourceActions ADD [CreatedOn] datetime NULL;
ALTER TABLE [TENT].Auth.ResourceActions ADD [UpdatedOn] datetime NULL;
ALTER TABLE [TENT].Auth.ResourceActions ADD [CreatedBy] bigint NULL;
ALTER TABLE [TENT].Auth.ResourceActions ADD [UpdatedBy] bigint NULL;
ALTER TABLE [TENT].Auth.Resources ADD [Name] varchar(150) NULL;
ALTER TABLE [TENT].Auth.Resources ADD [DomainId] bigint NOT NULL;
ALTER TABLE [TENT].Auth.Resources ADD [CreatedOn] datetime NULL;
ALTER TABLE [TENT].Auth.Resources ADD [UpdatedOn] datetime NULL;
ALTER TABLE [TENT].Auth.Resources ADD [CreatedBy] bigint NULL;
ALTER TABLE [TENT].Auth.Resources ADD [UpdatedBy] bigint NULL;
ALTER TABLE [TENT].Auth.RoleResourceActions ADD [RoleId] bigint NOT NULL;
ALTER TABLE [TENT].Auth.RoleResourceActions ADD [ResourceActionId] bigint NOT NULL;
ALTER TABLE [TENT].Auth.RoleResourceActions ADD [CreatedOn] datetime NULL;
ALTER TABLE [TENT].Auth.RoleResourceActions ADD [UpdatedOn] datetime NULL;
ALTER TABLE [TENT].Auth.RoleResourceActions ADD [CreatedBy] bigint NULL;
ALTER TABLE [TENT].Auth.RoleResourceActions ADD [UpdatedBy] bigint NULL;
ALTER TABLE [TENT].Auth.RoleResources ADD [RoleId] bigint NOT NULL;
ALTER TABLE [TENT].Auth.RoleResources ADD [ResourceId] bigint NOT NULL;
ALTER TABLE [TENT].Auth.RoleResources ADD [CanInsert] bit NOT NULL;
ALTER TABLE [TENT].Auth.RoleResources ADD [CanUpdate] bit NOT NULL;
ALTER TABLE [TENT].Auth.RoleResources ADD [CanDelete] bit NOT NULL;
ALTER TABLE [TENT].Auth.RoleResources ADD [CanViewDetails] bit NOT NULL;
ALTER TABLE [TENT].Auth.RoleResources ADD [CreatedOn] datetime NULL;
ALTER TABLE [TENT].Auth.RoleResources ADD [UpdatedOn] datetime NULL;
ALTER TABLE [TENT].Auth.RoleResources ADD [CreatedBy] bigint NULL;
ALTER TABLE [TENT].Auth.RoleResources ADD [UpdatedBy] bigint NULL;
ALTER TABLE [TENT].Auth.Roles ADD [Name] nvarchar(150) NULL;
ALTER TABLE [TENT].Auth.Roles ADD [TenantDomainId] bigint NULL;
ALTER TABLE [TENT].Auth.Roles ADD [Description] nvarchar(300) NULL;
ALTER TABLE [TENT].Auth.Roles ADD [CreatedOn] datetime NULL;
ALTER TABLE [TENT].Auth.Roles ADD [UpdatedOn] datetime NULL;
ALTER TABLE [TENT].Auth.Roles ADD [CreatedBy] bigint NULL;
ALTER TABLE [TENT].Auth.Roles ADD [UpdatedBy] bigint NULL;
ALTER TABLE [TENT].Auth.Roles ADD [IsUserRole] bit NOT NULL;
ALTER TABLE [TENT].Auth.Roles ADD [TenantAppId] bigint NULL;
ALTER TABLE [TENT].Auth.Tenants ADD [Code] varchar(100) NOT NULL;
ALTER TABLE [TENT].Auth.Tenants ADD [Name] nvarchar(200) NOT NULL;
ALTER TABLE [TENT].Auth.Tenants ADD [Description] ntext NULL;
ALTER TABLE [TENT].Auth.Tenants ADD [CreatedOn] datetime NULL;
ALTER TABLE [TENT].Auth.Tenants ADD [UpdatedOn] datetime NULL;
ALTER TABLE [TENT].Auth.Tenants ADD [CreatedBy] bigint NULL;
ALTER TABLE [TENT].Auth.Tenants ADD [UpdatedBy] bigint NULL;
ALTER TABLE [TENT].Auth.UserEntityLinks ADD [EntityName] varchar(60) NULL;
ALTER TABLE [TENT].Auth.UserEntityLinks ADD [EntityId] bigint NOT NULL;
ALTER TABLE [TENT].Auth.UserEntityLinks ADD [UserId] bigint NOT NULL;
ALTER TABLE [TENT].Auth.UserEntityLinks ADD [CreatedOn] datetime NULL;
ALTER TABLE [TENT].Auth.UserEntityLinks ADD [CreatedBy] bigint NULL;
ALTER TABLE [TENT].Auth.UserEntityLinks ADD [UpdatedOn] datetime NULL;
ALTER TABLE [TENT].Auth.UserEntityLinks ADD [UpdatedBy] bigint NULL;
ALTER TABLE [TENT].Auth.UserRoles ADD [UserId] bigint NOT NULL;
ALTER TABLE [TENT].Auth.UserRoles ADD [RoleId] bigint NOT NULL;
ALTER TABLE [TENT].Auth.UserRoles ADD [CreatedOn] datetime NULL;
ALTER TABLE [TENT].Auth.UserRoles ADD [UpdatedOn] datetime NULL;
ALTER TABLE [TENT].Auth.UserRoles ADD [CreatedBy] bigint NULL;
ALTER TABLE [TENT].Auth.UserRoles ADD [UpdatedBy] bigint NULL;
ALTER TABLE [TENT].Auth.Users ADD [Name] nvarchar(150) NULL;
ALTER TABLE [TENT].Auth.Users ADD [LogonName] varchar(50) NULL;
ALTER TABLE [TENT].Auth.Users ADD [Password] varchar(100) NULL;
ALTER TABLE [TENT].Auth.Users ADD [PersonId] bigint NULL;
ALTER TABLE [TENT].Auth.Users ADD [UserType] int NOT NULL;
ALTER TABLE [TENT].Auth.Users ADD [IsActive] bit NOT NULL;
ALTER TABLE [TENT].Auth.Users ADD [IsDeleted] bit NOT NULL;
ALTER TABLE [TENT].Auth.Users ADD [Mobile] varchar(50) NULL;
ALTER TABLE [TENT].Auth.Users ADD [Email] varchar(200) NULL;
ALTER TABLE [TENT].Auth.Users ADD [Photo] nvarchar(300) NULL;
ALTER TABLE [TENT].Auth.Users ADD [Gender] bit NULL;
ALTER TABLE [TENT].Auth.Users ADD [BirthDate] datetime NULL;
ALTER TABLE [TENT].Auth.Users ADD [TenantId] bigint NULL;
ALTER TABLE [TENT].Auth.Users ADD [AppId] bigint NULL;
ALTER TABLE [TENT].Auth.Users ADD [CreatedOn] datetime NULL;
ALTER TABLE [TENT].Auth.Users ADD [UpdatedOn] datetime NULL;
ALTER TABLE [TENT].Auth.Users ADD [CreatedBy] bigint NULL;
ALTER TABLE [TENT].Auth.Users ADD [UpdatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.Addresses ADD [BuildingNo] int NULL;
ALTER TABLE [TENT].dbo.Addresses ADD [StreetName] nvarchar(150) NULL;
ALTER TABLE [TENT].dbo.Addresses ADD [CityId] bigint NULL;
ALTER TABLE [TENT].dbo.Addresses ADD [Notes] nvarchar(max) NULL;
ALTER TABLE [TENT].dbo.Addresses ADD [CreatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.Addresses ADD [CreatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.Addresses ADD [UpdatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.Addresses ADD [UpdatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.AnesthesiaTypes ADD [Name] nvarchar(100) NULL;
ALTER TABLE [TENT].dbo.AnesthesiaTypes ADD [CreatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.AnesthesiaTypes ADD [CreatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.AnesthesiaTypes ADD [UpdatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.AnesthesiaTypes ADD [UpdatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.AssitantTypes ADD [Name] nvarchar(150) NULL;
ALTER TABLE [TENT].dbo.AssitantTypes ADD [CreatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.AssitantTypes ADD [CreatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.AssitantTypes ADD [UpdatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.AssitantTypes ADD [UpdatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.Attachments ADD [Description] nvarchar(300) NULL;
ALTER TABLE [TENT].dbo.Attachments ADD [EntityType] varchar(70) NULL;
ALTER TABLE [TENT].dbo.Attachments ADD [FileType] varchar(50) NULL;
ALTER TABLE [TENT].dbo.Attachments ADD [FilePath] nvarchar(300) NULL;
ALTER TABLE [TENT].dbo.Attachments ADD [EntityId] bigint NOT NULL;
ALTER TABLE [TENT].dbo.Attachments ADD [CreatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.Attachments ADD [CreatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.Attachments ADD [UpdatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.Attachments ADD [UpdatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.Cities ADD [Name] nvarchar(150) NULL;
ALTER TABLE [TENT].dbo.Cities ADD [CountryId] bigint NOT NULL;
ALTER TABLE [TENT].dbo.Cities ADD [CreatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.Cities ADD [CreatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.Cities ADD [UpdatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.Cities ADD [UpdatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.Clinics ADD [Name] nvarchar(150) NULL;
ALTER TABLE [TENT].dbo.Clinics ADD [AddressId] bigint NOT NULL;
ALTER TABLE [TENT].dbo.Clinics ADD [Phone] varchar(50) NULL;
ALTER TABLE [TENT].dbo.Clinics ADD [CreatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.Clinics ADD [CreatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.Clinics ADD [UpdatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.Clinics ADD [UpdatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.Clinics ADD [OwnerDoctorId] bigint NULL;
ALTER TABLE [TENT].dbo.Clinics ADD [VisitPrice] decimal(18,3) NULL;
ALTER TABLE [TENT].dbo.Clinics ADD [CheckupPeriod] int NOT NULL;
ALTER TABLE [TENT].dbo.Consumables ADD [Name] nvarchar(150) NULL;
ALTER TABLE [TENT].dbo.Consumables ADD [UnitCost] decimal(18,0) NOT NULL;
ALTER TABLE [TENT].dbo.Consumables ADD [CreatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.Consumables ADD [CreatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.Consumables ADD [UpdatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.Consumables ADD [UpdatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.Countries ADD [Name] nvarchar(150) NULL;
ALTER TABLE [TENT].dbo.Countries ADD [CreatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.Countries ADD [CreatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.Countries ADD [UpdatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.Countries ADD [UpdatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.Countries ADD [RegionName] nvarchar(150) NULL;
ALTER TABLE [TENT].dbo.Disorders ADD [Name] nvarchar(200) NOT NULL;
ALTER TABLE [TENT].dbo.Disorders ADD [CreatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.Disorders ADD [CreatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.Disorders ADD [UpdatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.Disorders ADD [UpdatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.Doctors ADD [Name] nvarchar(150) NULL;
ALTER TABLE [TENT].dbo.Doctors ADD [Mobile] varchar(50) NULL;
ALTER TABLE [TENT].dbo.Doctors ADD [Phone] varchar(50) NULL;
ALTER TABLE [TENT].dbo.Doctors ADD [AddressId] bigint NOT NULL;
ALTER TABLE [TENT].dbo.Doctors ADD [Email] nvarchar(150) NULL;
ALTER TABLE [TENT].dbo.Doctors ADD [CreatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.Doctors ADD [CreatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.Doctors ADD [UpdatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.Doctors ADD [UpdatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.Doctors ADD [UserId] bigint NULL;
ALTER TABLE [TENT].dbo.EmployeeClinics ADD [EmployeeId] bigint NOT NULL;
ALTER TABLE [TENT].dbo.EmployeeClinics ADD [ClinicId] bigint NOT NULL;
ALTER TABLE [TENT].dbo.EmployeeClinics ADD [CreatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.EmployeeClinics ADD [CreatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.EmployeeClinics ADD [UpdatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.EmployeeClinics ADD [UpdatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.Employees ADD [Name] nvarchar(150) NULL;
ALTER TABLE [TENT].dbo.Employees ADD [Mobile] varchar(14) NULL;
ALTER TABLE [TENT].dbo.Employees ADD [UserId] bigint NULL;
ALTER TABLE [TENT].dbo.Employees ADD [Email] nvarchar(100) NULL;
ALTER TABLE [TENT].dbo.Employees ADD [CreatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.Employees ADD [CreatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.Employees ADD [UpdatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.Employees ADD [UpdatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.ExpenseTypes ADD [Name] nvarchar(150) NULL;
ALTER TABLE [TENT].dbo.ExpenseTypes ADD [SystemEntity] varchar(50) NULL;
ALTER TABLE [TENT].dbo.ExpenseTypes ADD [CreatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.ExpenseTypes ADD [CreatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.ExpenseTypes ADD [UpdatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.ExpenseTypes ADD [UpdatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.ExternalPeople ADD [Name] nvarchar(150) NULL;
ALTER TABLE [TENT].dbo.ExternalPeople ADD [Mobile] varchar(15) NULL;
ALTER TABLE [TENT].dbo.ExternalPeople ADD [AddressId] bigint NOT NULL;
ALTER TABLE [TENT].dbo.ExternalPeople ADD [Email] varchar(150) NULL;
ALTER TABLE [TENT].dbo.ExternalPeople ADD [UserId] bigint NULL;
ALTER TABLE [TENT].dbo.ExternalPeople ADD [AssistanceTypeId] bigint NULL;
ALTER TABLE [TENT].dbo.ExternalPeople ADD [OperationCost] decimal(18,2) NOT NULL;
ALTER TABLE [TENT].dbo.ExternalPeople ADD [CreatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.ExternalPeople ADD [CreatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.ExternalPeople ADD [UpdatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.ExternalPeople ADD [UpdatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.Hospitals ADD [Name] nvarchar(150) NULL;
ALTER TABLE [TENT].dbo.Hospitals ADD [Phone] varchar(50) NULL;
ALTER TABLE [TENT].dbo.Hospitals ADD [AddressId] bigint NOT NULL;
ALTER TABLE [TENT].dbo.Hospitals ADD [CreatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.Hospitals ADD [CreatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.Hospitals ADD [UpdatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.Hospitals ADD [UpdatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.InsuranceDeductions ADD [InsuranceProviderId] bigint NULL;
ALTER TABLE [TENT].dbo.InsuranceDeductions ADD [Percentage] decimal(18,3) NOT NULL;
ALTER TABLE [TENT].dbo.InsuranceDeductions ADD [Amount] decimal(18,3) NOT NULL;
ALTER TABLE [TENT].dbo.InsuranceDeductions ADD [OperationId] bigint NULL;
ALTER TABLE [TENT].dbo.InsuranceDeductions ADD [VisitId] bigint NULL;
ALTER TABLE [TENT].dbo.InsuranceDeductions ADD [CreatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.InsuranceDeductions ADD [CreatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.InsuranceDeductions ADD [UpdatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.InsuranceDeductions ADD [UpdatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.InsuranceProviders ADD [Name] nvarchar(150) NULL;
ALTER TABLE [TENT].dbo.InsuranceProviders ADD [Percentage] decimal(18,5) NOT NULL;
ALTER TABLE [TENT].dbo.InsuranceProviders ADD [CreatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.InsuranceProviders ADD [CreatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.InsuranceProviders ADD [UpdatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.InsuranceProviders ADD [UpdatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.Localizables ADD [EntityId] bigint NOT NULL;
ALTER TABLE [TENT].dbo.Localizables ADD [EntityType] varchar(100) NOT NULL;
ALTER TABLE [TENT].dbo.Localizables ADD [LocaleId] int NOT NULL;
ALTER TABLE [TENT].dbo.Localizables ADD [ColumnName] varchar(70) NULL;
ALTER TABLE [TENT].dbo.Localizables ADD [Value] nvarchar(max) NULL;
ALTER TABLE [TENT].dbo.Localizables ADD [CreatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.Localizables ADD [CreatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.Localizables ADD [UpdatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.Localizables ADD [UpdatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.OperationAssistants ADD [OperationId] bigint NOT NULL;
ALTER TABLE [TENT].dbo.OperationAssistants ADD [ExternalPersonId] bigint NOT NULL;
ALTER TABLE [TENT].dbo.OperationAssistants ADD [StatusId] bigint NOT NULL;
ALTER TABLE [TENT].dbo.OperationAssistants ADD [Cost] decimal(18,3) NOT NULL;
ALTER TABLE [TENT].dbo.OperationAssistants ADD [CreatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.OperationAssistants ADD [CreatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.OperationAssistants ADD [UpdatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.OperationAssistants ADD [UpdatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.OperationConsumables ADD [ConsumableTypeId] bigint NOT NULL;
ALTER TABLE [TENT].dbo.OperationConsumables ADD [OperationId] bigint NOT NULL;
ALTER TABLE [TENT].dbo.OperationConsumables ADD [UnitCost] decimal(18,2) NOT NULL;
ALTER TABLE [TENT].dbo.OperationConsumables ADD [Amount] decimal(18,2) NOT NULL;
ALTER TABLE [TENT].dbo.OperationConsumables ADD [TotalCost] decimal(18,2) NOT NULL;
ALTER TABLE [TENT].dbo.OperationConsumables ADD [CreatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.OperationConsumables ADD [CreatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.OperationConsumables ADD [UpdatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.OperationConsumables ADD [UpdatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.OperationExpenses ADD [OperationId] bigint NOT NULL;
ALTER TABLE [TENT].dbo.OperationExpenses ADD [ExpenseTypeId] bigint NOT NULL;
ALTER TABLE [TENT].dbo.OperationExpenses ADD [CreatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.OperationExpenses ADD [CreatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.OperationExpenses ADD [UpdatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.OperationExpenses ADD [UpdatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.OperationExpenses ADD [Amount] decimal(18,2) NOT NULL;
ALTER TABLE [TENT].dbo.OperationExpenses ADD [EntityId] bigint NULL;
ALTER TABLE [TENT].dbo.OperationExpenses ADD [StatusId] bigint NOT NULL;
ALTER TABLE [TENT].dbo.OperationExpenses ADD [Description] nvarchar(200) NULL;
ALTER TABLE [TENT].dbo.Operations ADD [Code] varchar(50) NULL;
ALTER TABLE [TENT].dbo.Operations ADD [DoctorId] bigint NOT NULL;
ALTER TABLE [TENT].dbo.Operations ADD [SurgeryRoomId] bigint NULL;
ALTER TABLE [TENT].dbo.Operations ADD [PatientId] bigint NOT NULL;
ALTER TABLE [TENT].dbo.Operations ADD [StatusId] bigint NOT NULL;
ALTER TABLE [TENT].dbo.Operations ADD [SurgeryPositionId] bigint NULL;
ALTER TABLE [TENT].dbo.Operations ADD [AnesthesiaTypeId] bigint NULL;
ALTER TABLE [TENT].dbo.Operations ADD [OperationTypeId] bigint NOT NULL;
ALTER TABLE [TENT].dbo.Operations ADD [PlannedOperationTime] datetime NULL;
ALTER TABLE [TENT].dbo.Operations ADD [ActualOperationTime] datetime NULL;
ALTER TABLE [TENT].dbo.Operations ADD [Duration] int NULL;
ALTER TABLE [TENT].dbo.Operations ADD [Description] nvarchar(max) NULL;
ALTER TABLE [TENT].dbo.Operations ADD [Complications] nvarchar(max) NULL;
ALTER TABLE [TENT].dbo.Operations ADD [Result] nvarchar(max) NULL;
ALTER TABLE [TENT].dbo.Operations ADD [TotalCost] decimal(18,3) NOT NULL;
ALTER TABLE [TENT].dbo.Operations ADD [TotalPrice] decimal(18,3) NOT NULL;
ALTER TABLE [TENT].dbo.Operations ADD [PaymentAmount] decimal(18,3) NOT NULL;
ALTER TABLE [TENT].dbo.Operations ADD [CreatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.Operations ADD [CreatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.Operations ADD [UpdatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.Operations ADD [UpdatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.OperationTypes ADD [Name] nvarchar(150) NULL;
ALTER TABLE [TENT].dbo.OperationTypes ADD [CreatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.OperationTypes ADD [CreatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.OperationTypes ADD [UpdatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.OperationTypes ADD [UpdatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.Patients ADD [Name] nvarchar(150) NULL;
ALTER TABLE [TENT].dbo.Patients ADD [Phone] nvarchar(50) NULL;
ALTER TABLE [TENT].dbo.Patients ADD [Mobile] nvarchar(50) NULL;
ALTER TABLE [TENT].dbo.Patients ADD [AddressId] bigint NOT NULL;
ALTER TABLE [TENT].dbo.Patients ADD [Email] nvarchar(150) NULL;
ALTER TABLE [TENT].dbo.Patients ADD [Code] varchar(50) NULL;
ALTER TABLE [TENT].dbo.Patients ADD [BirthDate] date NULL;
ALTER TABLE [TENT].dbo.Patients ADD [Gender] bit NOT NULL;
ALTER TABLE [TENT].dbo.Patients ADD [CreatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.Patients ADD [CreatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.Patients ADD [UpdatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.Patients ADD [UpdatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.Patients ADD [ReferalTypeId] bigint NULL;
ALTER TABLE [TENT].dbo.ReferralTypes ADD [Name] nvarchar(150) NULL;
ALTER TABLE [TENT].dbo.ReferralTypes ADD [CreatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.ReferralTypes ADD [CreatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.ReferralTypes ADD [UpdatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.ReferralTypes ADD [UpdatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.Statuses ADD [Name] nchar NULL;
ALTER TABLE [TENT].dbo.Statuses ADD [CreatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.Statuses ADD [CreatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.Statuses ADD [UpdatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.Statuses ADD [UpdatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.SurgeryPositions ADD [Name] nvarchar(150) NULL;
ALTER TABLE [TENT].dbo.SurgeryPositions ADD [CreatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.SurgeryPositions ADD [CreatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.SurgeryPositions ADD [UpdatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.SurgeryPositions ADD [UpdatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.SurgeryRooms ADD [Name] nvarchar(150) NULL;
ALTER TABLE [TENT].dbo.SurgeryRooms ADD [HospitalId] bigint NULL;
ALTER TABLE [TENT].dbo.SurgeryRooms ADD [CreatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.SurgeryRooms ADD [CreatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.SurgeryRooms ADD [UpdatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.SurgeryRooms ADD [UpdatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.SurgeryRooms ADD [OperationCost] decimal(18,2) NOT NULL;
ALTER TABLE [TENT].dbo.VisitLogs ADD [ActionType] int NOT NULL;
ALTER TABLE [TENT].dbo.VisitLogs ADD [ReferenceId] bigint NULL;
ALTER TABLE [TENT].dbo.VisitLogs ADD [Text] nvarchar(150) NULL;
ALTER TABLE [TENT].dbo.VisitLogs ADD [VisitId] bigint NOT NULL;
ALTER TABLE [TENT].dbo.VisitLogs ADD [CreatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.VisitLogs ADD [CreatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.VisitLogs ADD [UpdatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.VisitLogs ADD [UpdatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.Visits ADD [SequenceNumber] varchar(50) NULL;
ALTER TABLE [TENT].dbo.Visits ADD [VisitTime] datetime NOT NULL;
ALTER TABLE [TENT].dbo.Visits ADD [ParentVisitId] bigint NULL;
ALTER TABLE [TENT].dbo.Visits ADD [PatientId] bigint NOT NULL;
ALTER TABLE [TENT].dbo.Visits ADD [Description] nvarchar(max) NULL;
ALTER TABLE [TENT].dbo.Visits ADD [ClinicId] bigint NULL;
ALTER TABLE [TENT].dbo.Visits ADD [EmployeeId] bigint NULL;
ALTER TABLE [TENT].dbo.Visits ADD [StatusId] bigint NOT NULL;
ALTER TABLE [TENT].dbo.Visits ADD [Price] decimal(18,3) NOT NULL;
ALTER TABLE [TENT].dbo.Visits ADD [PaymentStatus] int NOT NULL;
ALTER TABLE [TENT].dbo.Visits ADD [Priority] int NULL;
ALTER TABLE [TENT].dbo.Visits ADD [VisitTypeId] bigint NOT NULL;
ALTER TABLE [TENT].dbo.Visits ADD [ArrivalTime] datetime NULL;
ALTER TABLE [TENT].dbo.Visits ADD [StartTime] datetime NULL;
ALTER TABLE [TENT].dbo.Visits ADD [EndTime] datetime NULL;
ALTER TABLE [TENT].dbo.Visits ADD [PaymentAmount] decimal(18,2) NULL;
ALTER TABLE [TENT].dbo.Visits ADD [CreatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.Visits ADD [UpdatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.Visits ADD [CreatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.Visits ADD [UpdatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.Visits ADD [DisorderId] bigint NULL;
ALTER TABLE [TENT].dbo.Visits ADD [EntryOrderId] int NULL;
ALTER TABLE [TENT].dbo.VisitTypes ADD [Name] varchar(50) NULL;
ALTER TABLE [TENT].dbo.VisitTypes ADD [CreatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.VisitTypes ADD [CreatedBy] bigint NULL;
ALTER TABLE [TENT].dbo.VisitTypes ADD [UpdatedOn] datetime NULL;
ALTER TABLE [TENT].dbo.VisitTypes ADD [UpdatedBy] bigint NULL;
 
-- NO MODIFIED COLUMNS
 
-- ADDED CONSTRAINTS :
ALTER TABLE [TENT].Auth.ResourceActions ADD CONSTRAINT FK_ResourceActions_Resources FOREIGN KEY (ResourceId) REFERENCES Auth.Resources (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [TENT].Auth.Resources ADD CONSTRAINT FK_Resources_Domains FOREIGN KEY (DomainId) REFERENCES Auth.Domains (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [TENT].Auth.RoleResourceActions ADD CONSTRAINT FK_RoleResourceActions_ResourceActions FOREIGN KEY (ResourceActionId) REFERENCES Auth.ResourceActions (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [TENT].Auth.RoleResourceActions ADD CONSTRAINT FK_RoleResourceActions_Roles FOREIGN KEY (RoleId) REFERENCES Auth.Roles (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [TENT].Auth.RoleResources ADD CONSTRAINT FK_RoleResources_Resources FOREIGN KEY (ResourceId) REFERENCES Auth.Resources (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [TENT].Auth.RoleResources ADD CONSTRAINT FK_RoleResources_Roles FOREIGN KEY (RoleId) REFERENCES Auth.Roles (Id) 
		ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [TENT].Auth.Roles ADD CONSTRAINT FK__Roles__TenantApp__14270015 FOREIGN KEY (TenantAppId) REFERENCES Auth.Apps (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [TENT].Auth.UserEntityLinks ADD CONSTRAINT FK_UserEntityLinks_Users FOREIGN KEY (UserId) REFERENCES Auth.Users (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [TENT].Auth.UserRoles ADD CONSTRAINT FK_UserRoles_Roles FOREIGN KEY (RoleId) REFERENCES Auth.Roles (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [TENT].Auth.UserRoles ADD CONSTRAINT FK_UserRoles_Users FOREIGN KEY (UserId) REFERENCES Auth.Users (Id) 
		ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [TENT].Auth.Users ADD CONSTRAINT FK_Users_Apps FOREIGN KEY (AppId) REFERENCES Auth.Apps (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [TENT].Auth.Users ADD CONSTRAINT FK_Users_Tenants FOREIGN KEY (TenantId) REFERENCES Auth.Tenants (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [TENT].dbo.Addresses ADD CONSTRAINT FK_Addresses_Cities FOREIGN KEY (CityId) REFERENCES dbo.Cities (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [TENT].dbo.Cities ADD CONSTRAINT FK_Cities_Countries FOREIGN KEY (CountryId) REFERENCES dbo.Countries (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [TENT].dbo.Clinics ADD CONSTRAINT FK_Clinics_Addresses FOREIGN KEY (AddressId) REFERENCES dbo.Addresses (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [TENT].dbo.Clinics ADD CONSTRAINT FK_Clinics_Doctors FOREIGN KEY (OwnerDoctorId) REFERENCES dbo.Doctors (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [TENT].dbo.Doctors ADD CONSTRAINT FK_Doctors_Addresses FOREIGN KEY (AddressId) REFERENCES dbo.Addresses (Id) 
		ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [TENT].dbo.EmployeeClinics ADD CONSTRAINT FK_EmployeeClinics_Clinics FOREIGN KEY (ClinicId) REFERENCES dbo.Clinics (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [TENT].dbo.EmployeeClinics ADD CONSTRAINT FK_EmployeeClinics_Employees FOREIGN KEY (EmployeeId) REFERENCES dbo.Employees (Id) 
		ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [TENT].dbo.ExternalPeople ADD CONSTRAINT FK_ExternalPeople_Addresses FOREIGN KEY (AddressId) REFERENCES dbo.Addresses (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [TENT].dbo.ExternalPeople ADD CONSTRAINT FK_ExternalPeople_AssitantTypes FOREIGN KEY (AssistanceTypeId) REFERENCES dbo.AssitantTypes (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [TENT].dbo.Hospitals ADD CONSTRAINT FK_Hospitals_Addresses FOREIGN KEY (AddressId) REFERENCES dbo.Addresses (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [TENT].dbo.InsuranceDeductions ADD CONSTRAINT FK_InsuranceDeductions_InsuranceProviders FOREIGN KEY (InsuranceProviderId) REFERENCES dbo.InsuranceProviders (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [TENT].dbo.InsuranceDeductions ADD CONSTRAINT FK_InsuranceDeductions_Operations FOREIGN KEY (OperationId) REFERENCES dbo.Operations (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [TENT].dbo.InsuranceDeductions ADD CONSTRAINT FK_InsuranceDeductions_Visits FOREIGN KEY (VisitId) REFERENCES dbo.Visits (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [TENT].dbo.OperationAssistants ADD CONSTRAINT FK_OperationAssistants_ExternalPeople FOREIGN KEY (ExternalPersonId) REFERENCES dbo.ExternalPeople (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [TENT].dbo.OperationAssistants ADD CONSTRAINT FK_OperationAssistants_Operations FOREIGN KEY (OperationId) REFERENCES dbo.Operations (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [TENT].dbo.OperationConsumables ADD CONSTRAINT FK_OperationConsumables_Consumables FOREIGN KEY (ConsumableTypeId) REFERENCES dbo.Consumables (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [TENT].dbo.OperationConsumables ADD CONSTRAINT FK_OperationConsumables_Operations FOREIGN KEY (OperationId) REFERENCES dbo.Operations (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [TENT].dbo.OperationExpenses ADD CONSTRAINT FK_OperationExpenses_ExpenseTypes FOREIGN KEY (ExpenseTypeId) REFERENCES dbo.ExpenseTypes (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [TENT].dbo.OperationExpenses ADD CONSTRAINT FK_OperationCosts_Operations FOREIGN KEY (OperationId) REFERENCES dbo.Operations (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [TENT].dbo.Operations ADD CONSTRAINT FK_Operations_AnesthesiaTypes FOREIGN KEY (AnesthesiaTypeId) REFERENCES dbo.AnesthesiaTypes (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [TENT].dbo.Operations ADD CONSTRAINT FK_Operations_Doctors FOREIGN KEY (DoctorId) REFERENCES dbo.Doctors (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [TENT].dbo.Operations ADD CONSTRAINT FK_Operations_OperationTypes FOREIGN KEY (OperationTypeId) REFERENCES dbo.OperationTypes (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [TENT].dbo.Operations ADD CONSTRAINT FK_Operations_Patients FOREIGN KEY (PatientId) REFERENCES dbo.Patients (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [TENT].dbo.Operations ADD CONSTRAINT FK_Operations_Statuses FOREIGN KEY (StatusId) REFERENCES dbo.Statuses (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [TENT].dbo.Operations ADD CONSTRAINT FK_Operations_SurgeryPositions FOREIGN KEY (SurgeryPositionId) REFERENCES dbo.SurgeryPositions (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [TENT].dbo.Operations ADD CONSTRAINT FK_Operations_SurgeryRooms FOREIGN KEY (SurgeryRoomId) REFERENCES dbo.SurgeryRooms (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [TENT].dbo.Patients ADD CONSTRAINT FK_Patients_Addresses FOREIGN KEY (AddressId) REFERENCES dbo.Addresses (Id) 
		ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [TENT].dbo.Patients ADD CONSTRAINT FK_Patients_ReferralTypes FOREIGN KEY (ReferalTypeId) REFERENCES dbo.ReferralTypes (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [TENT].dbo.SurgeryRooms ADD CONSTRAINT FK_SurgeryRooms_Hospitals FOREIGN KEY (HospitalId) REFERENCES dbo.Hospitals (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [TENT].dbo.Visits ADD CONSTRAINT FK_Visits_Clinics FOREIGN KEY (ClinicId) REFERENCES dbo.Clinics (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [TENT].dbo.Visits ADD CONSTRAINT FK_Visits_Disorders FOREIGN KEY (DisorderId) REFERENCES dbo.Disorders (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [TENT].dbo.Visits ADD CONSTRAINT FK_Visits_Employees FOREIGN KEY (EmployeeId) REFERENCES dbo.Employees (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [TENT].dbo.Visits ADD CONSTRAINT FK_Visits_Patients FOREIGN KEY (PatientId) REFERENCES dbo.Patients (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [TENT].dbo.Visits ADD CONSTRAINT FK_Visits_Statuses FOREIGN KEY (StatusId) REFERENCES dbo.Statuses (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [TENT].dbo.Visits ADD CONSTRAINT FK_Visits_VisitTypes FOREIGN KEY (VisitTypeId) REFERENCES dbo.VisitTypes (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
 
-- NO MODIFIED CONSTRAINTS
 
GO
 
-- OTHER MODIFIED DATA:
-- FUNCTION  		 CREATED 	Oct  4 2019 10:15PM		dbo.fn_diagramobjects

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
-- FUNCTION  		 CREATED 	Oct 18 2019  8:54AM		dbo.NumberString

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
-- PROCEDURE  		 CREATED 	Oct  5 2019 12:55AM		dbo.AddAuditingColumns
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
-- PROCEDURE  		 CREATED 	Oct 18 2019  8:53AM		dbo.GenerateSequenceNumber
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
	declare @day varchar(2)=(select dbo.NumberString(DAY(GETDATE()),2));
	declare @dayOfYear varchar(3)=(select dbo.NumberString(DATEPART(dayofyear,GETDATE()),3));
	declare @year4 varchar(4)=convert(varchar(4),YEAR(GETDATE()));
	declare @year2 varchar(2)=(select RIGHT(@year4,2));
	declare @mon varchar(2)=(select dbo.NumberString(MONTH(GETDATE()),2));
	declare @client varchar(3)=(select dbo.NumberString(5,2))

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
		'+@table+' where '+@column+' like '''+@pattern+'%''';

	exec sp_executesql @q,N'@max bigint OUTPUT',@max OUTPUT;

	if(@max is null)
		select @max=0;
	select @out=concat( @pattern,dbo.NumberString(@max+1,@minLength));
	--print @out;
	
end;

GO
-- PROCEDURE  		 CREATED 	Oct  4 2019 10:15PM		dbo.sp_upgraddiagrams

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
-- PROCEDURE  		 CREATED 	Oct  4 2019 10:15PM		dbo.sp_helpdiagrams

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
-- PROCEDURE  		 CREATED 	Oct  4 2019 10:15PM		dbo.sp_helpdiagramdefinition

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
-- PROCEDURE  		 CREATED 	Oct  4 2019 10:15PM		dbo.sp_creatediagram

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
-- PROCEDURE  		 CREATED 	Oct  4 2019 10:15PM		dbo.sp_renamediagram

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
-- PROCEDURE  		 CREATED 	Oct  4 2019 10:15PM		dbo.sp_alterdiagram

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
-- PROCEDURE  		 CREATED 	Oct  4 2019 10:15PM		dbo.sp_dropdiagram

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
-- TRIGGER  		 CREATED 	Nov 26 2019  9:53PM		Visits_TRG
CREATE trigger [dbo].[Visits_TRG]
on dbo.Visits
after insert
as
begin
	declare @i int=0;
	declare @id bigint=0;
	declare @code varchar(20);
	while(exists(select 1 from inserted where Id>@id))
	begin
		select 
			@id=Id 
		from (
			select Id,ROW_NUMBER() over (order by Id) rowNum from inserted
		) ins
		where rowNum=@i;

		exec dbo.GenerateSequenceNumber 'Visits','SequenceNumber','V{YEAR2}{MONTH}-',4,'',@code output;

		update Visits
		set SequenceNumber=@code
		where Id=@id;

		set @i=@i+1;
	end
end

GO
-- TRIGGER  		 CREATED 	Oct 18 2019  8:58AM		Patients_TRG
CREATE trigger Patients_TRG
on Patients
after insert
as
begin
	declare @i int=0;
	declare @id bigint=0;
	declare @code varchar(20);
	while(exists(select 1 from inserted where Id>@id))
	begin
		select 
			@id=Id 
		from (
			select Id,ROW_NUMBER() over (order by Id) rowNum from inserted
		) ins
		where rowNum=@i;

		exec dbo.GenerateSequenceNumber 'Patients','Code','P{YEAR2}-',4,'',@code output;

		update Patients
		set Code=@code
		where Id=@id;

		set @i=@i+1;
	end
end
GO