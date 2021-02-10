-- NO ADDED TABLES
 
-- ADDED COLUMNS :
ALTER TABLE [FMS_TEST2].dbo.Adresses ADD [EntityId] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.Adresses ADD [PropertyName] varchar(50) NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.Adresses ADD [EntityType] varchar(50) NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.Adresses ADD [Text] nvarchar(300) NULL;
ALTER TABLE [FMS_TEST2].dbo.Adresses ADD [LocaleId] int NULL;
ALTER TABLE [FMS_TEST2].dbo.Adresses ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.Adresses ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.Adresses ADD [UpdatedBy] int NULL;
ALTER TABLE [FMS_TEST2].dbo.Agencies ADD [Name] nvarchar(250) NULL;
ALTER TABLE [FMS_TEST2].dbo.Agencies ADD [MinistryId] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.Agencies ADD [Address] nvarchar(250) NULL;
ALTER TABLE [FMS_TEST2].dbo.Agencies ADD [Description] ntext NULL;
ALTER TABLE [FMS_TEST2].dbo.Agencies ADD [Long] decimal(18,10) NULL;
ALTER TABLE [FMS_TEST2].dbo.Agencies ADD [Lat] decimal(18,10) NULL;
ALTER TABLE [FMS_TEST2].dbo.Agencies ADD [GovernorateId] int NULL;
ALTER TABLE [FMS_TEST2].dbo.Agencies ADD [WebSite] varchar(250) NULL;
ALTER TABLE [FMS_TEST2].dbo.Agencies ADD [IsActive] bit NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.Agencies ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.Agencies ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.Agencies ADD [UpdatedBy] int NULL;
ALTER TABLE [FMS_TEST2].dbo.Agencies ADD [Logo] varchar(250) NULL;
ALTER TABLE [FMS_TEST2].dbo.Contacts ADD [Identifier] nvarchar(200) NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.Contacts ADD [Type] varchar(50) NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.Contacts ADD [AgencyId] int NULL;
ALTER TABLE [FMS_TEST2].dbo.Contacts ADD [MinistryId] int NULL;
ALTER TABLE [FMS_TEST2].dbo.Contacts ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.Contacts ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.Contacts ADD [UpdatedBy] int NULL;
ALTER TABLE [FMS_TEST2].dbo.Descriptions ADD [EntityId] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.Descriptions ADD [PropertyName] varchar(50) NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.Descriptions ADD [EntityType] varchar(50) NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.Descriptions ADD [Text] ntext NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.Descriptions ADD [LocaleId] int NULL;
ALTER TABLE [FMS_TEST2].dbo.Descriptions ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.Descriptions ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.Descriptions ADD [UpdatedBy] int NULL;
ALTER TABLE [FMS_TEST2].dbo.DomainPages ADD [Name] varchar(50) NULL;
ALTER TABLE [FMS_TEST2].dbo.DomainPages ADD [DomainId] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.DomainPages ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.DomainPages ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.DomainPages ADD [UpdatedBy] int NULL;
ALTER TABLE [FMS_TEST2].dbo.DomainPages ADD [EntityType] varchar(60) NULL;
ALTER TABLE [FMS_TEST2].dbo.Domains ADD [Name] varchar(50) NULL;
ALTER TABLE [FMS_TEST2].dbo.Domains ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.Domains ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.Domains ADD [UpdatedBy] int NULL;
ALTER TABLE [FMS_TEST2].dbo.Governorates ADD [NameId] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.Governorates ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.Governorates ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.Governorates ADD [UpdatedBy] int NULL;
ALTER TABLE [FMS_TEST2].dbo.ImprovementFields ADD [Name] nvarchar(150) NULL;
ALTER TABLE [FMS_TEST2].dbo.ImprovementFields ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.ImprovementFields ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.ImprovementFields ADD [UpdatedBy] int NULL;
ALTER TABLE [FMS_TEST2].dbo.Locales ADD [Name] nvarchar(255) NULL;
ALTER TABLE [FMS_TEST2].dbo.Locales ADD [Symbol] varchar(5) NULL;
ALTER TABLE [FMS_TEST2].dbo.Locales ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.Locales ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.Locales ADD [UpdatedBy] int NULL;
ALTER TABLE [FMS_TEST2].dbo.Messages ADD [Title] nvarchar(200) NULL;
ALTER TABLE [FMS_TEST2].dbo.Messages ADD [Description] ntext NULL;
ALTER TABLE [FMS_TEST2].dbo.Messages ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.Messages ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.Messages ADD [UpdatedBy] int NULL;
ALTER TABLE [FMS_TEST2].dbo.Ministries ADD [Name] nvarchar(255) NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.Ministries ADD [Description] ntext NULL;
ALTER TABLE [FMS_TEST2].dbo.Ministries ADD [Address] nvarchar(355) NULL;
ALTER TABLE [FMS_TEST2].dbo.Ministries ADD [Mission] ntext NULL;
ALTER TABLE [FMS_TEST2].dbo.Ministries ADD [Vision] ntext NULL;
ALTER TABLE [FMS_TEST2].dbo.Ministries ADD [Message] ntext NULL;
ALTER TABLE [FMS_TEST2].dbo.Ministries ADD [Long] decimal(18,10) NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.Ministries ADD [Lat] decimal(18,10) NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.Ministries ADD [Logo] nvarchar(200) NULL;
ALTER TABLE [FMS_TEST2].dbo.Ministries ADD [ViewOrder] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.Ministries ADD [IsActive] bit NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.Ministries ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.Ministries ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.Ministries ADD [UpdatedBy] int NULL;
ALTER TABLE [FMS_TEST2].dbo.Ministries ADD [LogoThumb] nvarchar(200) NULL;
ALTER TABLE [FMS_TEST2].dbo.Ministries ADD [Url] varchar(200) NULL;
ALTER TABLE [FMS_TEST2].dbo.Ministries ADD [OrganizationTypeId] int NULL;
ALTER TABLE [FMS_TEST2].dbo.MinistryFaqs ADD [Question] nvarchar(250) NULL;
ALTER TABLE [FMS_TEST2].dbo.MinistryFaqs ADD [Answer] ntext NULL;
ALTER TABLE [FMS_TEST2].dbo.MinistryFaqs ADD [MinistryId] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.MinistryFaqs ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.MinistryFaqs ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.MinistryFaqs ADD [UpdatedBy] int NULL;
ALTER TABLE [FMS_TEST2].dbo.MinistryFaqs ADD [IsApproved] bit NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.MinistryNews ADD [Title] nvarchar(255) NULL;
ALTER TABLE [FMS_TEST2].dbo.MinistryNews ADD [Description] ntext NULL;
ALTER TABLE [FMS_TEST2].dbo.MinistryNews ADD [MinistryId] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.MinistryNews ADD [Image] nvarchar(300) NULL;
ALTER TABLE [FMS_TEST2].dbo.MinistryNews ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.MinistryNews ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.MinistryNews ADD [UpdatedBy] int NULL;
ALTER TABLE [FMS_TEST2].dbo.MinistryNews ADD [Url] varchar(200) NULL;
ALTER TABLE [FMS_TEST2].dbo.MinistryNews ADD [IsApproved] bit NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.MinistryPages ADD [MinistryId] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.MinistryPages ADD [DomainPageId] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.MinistryPages ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.MinistryPages ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.MinistryPages ADD [UpdatedBy] int NULL;
ALTER TABLE [FMS_TEST2].dbo.MinistryProjects ADD [Title] nvarchar(250) NULL;
ALTER TABLE [FMS_TEST2].dbo.MinistryProjects ADD [Description] ntext NULL;
ALTER TABLE [FMS_TEST2].dbo.MinistryProjects ADD [MinistryId] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.MinistryProjects ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.MinistryProjects ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.MinistryProjects ADD [UpdatedBy] int NULL;
ALTER TABLE [FMS_TEST2].dbo.MinistryProjects ADD [IsApproved] bit NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.MinistryUserPages ADD [MinistryUserId] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.MinistryUserPages ADD [DomainPageId] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.MinistryUserPages ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.MinistryUserPages ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.MinistryUserPages ADD [UpdatedBy] int NULL;
ALTER TABLE [FMS_TEST2].dbo.MinistryUserPages ADD [Privilege] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.MinistryUsers ADD [MinistryId] int NULL;
ALTER TABLE [FMS_TEST2].dbo.MinistryUsers ADD [UserId] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.MinistryUsers ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.MinistryUsers ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.MinistryUsers ADD [UpdatedBy] int NULL;
ALTER TABLE [FMS_TEST2].dbo.Names ADD [Text] nvarchar(255) NULL;
ALTER TABLE [FMS_TEST2].dbo.Names ADD [LocaleId] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.Names ADD [EntityId] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.Names ADD [PropertyName] varchar(50) NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.Names ADD [EntityType] varchar(50) NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.Names ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.Names ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.Names ADD [UpdatedBy] int NULL;
ALTER TABLE [FMS_TEST2].dbo.OrganizationTypes ADD [Name] nvarchar(50) NULL;
ALTER TABLE [FMS_TEST2].dbo.OrganizationTypes ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.OrganizationTypes ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.OrganizationTypes ADD [UpdatedBy] int NULL;
ALTER TABLE [FMS_TEST2].dbo.ServiceCategories ADD [Name] nvarchar(250) NULL;
ALTER TABLE [FMS_TEST2].dbo.ServiceCategories ADD [MinistryId] int NULL;
ALTER TABLE [FMS_TEST2].dbo.ServiceCategories ADD [Description] ntext NULL;
ALTER TABLE [FMS_TEST2].dbo.ServiceCategories ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.ServiceCategories ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.ServiceCategories ADD [UpdatedBy] int NULL;
ALTER TABLE [FMS_TEST2].dbo.ServiceCommentFields ADD [ServiceCommentId] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.ServiceCommentFields ADD [ImprovementFieldId] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.ServiceCommentFields ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.ServiceCommentFields ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.ServiceCommentFields ADD [UpdatedBy] int NULL;
ALTER TABLE [FMS_TEST2].dbo.ServiceComments ADD [CommentText] nvarchar(250) NULL;
ALTER TABLE [FMS_TEST2].dbo.ServiceComments ADD [Rating] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.ServiceComments ADD [ServiceId] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.ServiceComments ADD [UserId] int NULL;
ALTER TABLE [FMS_TEST2].dbo.ServiceComments ADD [ImprovementText] nvarchar(250) NULL;
ALTER TABLE [FMS_TEST2].dbo.ServiceComments ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.ServiceComments ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.ServiceComments ADD [UpdatedBy] int NULL;
ALTER TABLE [FMS_TEST2].dbo.ServiceConstraints ADD [Name] nvarchar(250) NULL;
ALTER TABLE [FMS_TEST2].dbo.ServiceConstraints ADD [Description] ntext NULL;
ALTER TABLE [FMS_TEST2].dbo.ServiceConstraints ADD [ServiceId] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.ServiceConstraints ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.ServiceConstraints ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.ServiceConstraints ADD [UpdatedBy] int NULL;
ALTER TABLE [FMS_TEST2].dbo.ServiceRequirements ADD [Name] nvarchar(250) NULL;
ALTER TABLE [FMS_TEST2].dbo.ServiceRequirements ADD [IsActive] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.ServiceRequirements ADD [ServiceId] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.ServiceRequirements ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.ServiceRequirements ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.ServiceRequirements ADD [UpdatedBy] int NULL;
ALTER TABLE [FMS_TEST2].dbo.ServiceRequirements ADD [Provider] nvarchar(200) NULL;
ALTER TABLE [FMS_TEST2].dbo.Services ADD [Name] nvarchar(250) NULL;
ALTER TABLE [FMS_TEST2].dbo.Services ADD [Description] ntext NULL;
ALTER TABLE [FMS_TEST2].dbo.Services ADD [AgencyId] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.Services ADD [TimeConsumption] int NULL;
ALTER TABLE [FMS_TEST2].dbo.Services ADD [Cost] decimal(18,3) NULL;
ALTER TABLE [FMS_TEST2].dbo.Services ADD [IsOnline] bit NULL;
ALTER TABLE [FMS_TEST2].dbo.Services ADD [ServiceCategoryId] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.Services ADD [IsActive] bit NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.Services ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.Services ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.Services ADD [UpdatedBy] int NULL;
ALTER TABLE [FMS_TEST2].dbo.ServiceSteps ADD [Name] nvarchar(250) NULL;
ALTER TABLE [FMS_TEST2].dbo.ServiceSteps ADD [ServiceId] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.ServiceSteps ADD [ViewOrder] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.ServiceSteps ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.ServiceSteps ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.ServiceSteps ADD [UpdatedBy] int NULL;
ALTER TABLE [FMS_TEST2].dbo.ServiceUrls ADD [ServiceId] int NULL;
ALTER TABLE [FMS_TEST2].dbo.ServiceUrls ADD [Url] varchar(250) NULL;
ALTER TABLE [FMS_TEST2].dbo.ServiceUrls ADD [Name] nvarchar(250) NULL;
ALTER TABLE [FMS_TEST2].dbo.ServiceUrls ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.ServiceUrls ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.ServiceUrls ADD [UpdatedBy] int NULL;
ALTER TABLE [FMS_TEST2].dbo.UpdateLogs ADD [MinistryId] int NULL;
ALTER TABLE [FMS_TEST2].dbo.UpdateLogs ADD [ChangedEntityType] varchar(60) NULL;
ALTER TABLE [FMS_TEST2].dbo.UpdateLogs ADD [ChangedEntityId] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.UpdateLogs ADD [ChangeAction] varchar(50) NULL;
ALTER TABLE [FMS_TEST2].dbo.UpdateLogs ADD [Note] nvarchar(200) NULL;
ALTER TABLE [FMS_TEST2].dbo.UpdateLogs ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.UpdateLogs ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.UpdateLogs ADD [UpdatedBy] int NULL;
ALTER TABLE [FMS_TEST2].dbo.Users ADD [Name] nvarchar(250) NULL;
ALTER TABLE [FMS_TEST2].dbo.Users ADD [LogonName] varchar(50) NULL;
ALTER TABLE [FMS_TEST2].dbo.Users ADD [Password] varchar(100) NULL;
ALTER TABLE [FMS_TEST2].dbo.Users ADD [IsActive] int NULL;
ALTER TABLE [FMS_TEST2].dbo.Users ADD [UserTypeId] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.Users ADD [Email] varchar(240) NULL;
ALTER TABLE [FMS_TEST2].dbo.Users ADD [Phone] varchar(11) NULL;
ALTER TABLE [FMS_TEST2].dbo.Users ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.Users ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.Users ADD [UpdatedBy] int NULL;
ALTER TABLE [FMS_TEST2].dbo.UserTypes ADD [Name] nvarchar(250) NULL;
ALTER TABLE [FMS_TEST2].dbo.UserTypes ADD [SystemEntity] varchar(50) NULL;
ALTER TABLE [FMS_TEST2].dbo.UserTypes ADD [CreatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.UserTypes ADD [UpdatedOn] datetime NULL;
ALTER TABLE [FMS_TEST2].dbo.UserTypes ADD [UpdatedBy] int NULL;
 
-- MODIFIED COLUMNS :
ALTER TABLE [FMS_TEST2].dbo.Adresses ALTER COLUMN [Id] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.Agencies ALTER COLUMN [Id] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.Contacts ALTER COLUMN [Id] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.Descriptions ALTER COLUMN [Id] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.DomainPages ALTER COLUMN [Id] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.Domains ALTER COLUMN [Id] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.Governorates ALTER COLUMN [Id] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.ImprovementFields ALTER COLUMN [Id] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.Locales ALTER COLUMN [Id] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.Messages ALTER COLUMN [Id] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.Ministries ALTER COLUMN [Id] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.MinistryFaqs ALTER COLUMN [Id] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.MinistryNews ALTER COLUMN [Id] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.MinistryPages ALTER COLUMN [Id] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.MinistryProjects ALTER COLUMN [Id] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.MinistryUserPages ALTER COLUMN [Id] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.MinistryUsers ALTER COLUMN [Id] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.Names ALTER COLUMN [Id] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.OrganizationTypes ALTER COLUMN [Id] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.ServiceCategories ALTER COLUMN [Id] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.ServiceCommentFields ALTER COLUMN [Id] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.ServiceComments ALTER COLUMN [Id] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.ServiceConstraints ALTER COLUMN [Id] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.ServiceRequirements ALTER COLUMN [Id] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.Services ALTER COLUMN [Id] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.ServiceSteps ALTER COLUMN [Id] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.ServiceUrls ALTER COLUMN [Id] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.UpdateLogs ALTER COLUMN [Id] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.Users ALTER COLUMN [Id] int NOT NULL;
ALTER TABLE [FMS_TEST2].dbo.UserTypes ALTER COLUMN [Id] int NOT NULL;
 
-- ADDED CONSTRAINTS :
ALTER TABLE [FMS_TEST2].dbo.Adresses ADD CONSTRAINT FK_Adresses_Locales FOREIGN KEY (LocaleId) REFERENCES dbo.Locales (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].dbo.Agencies ADD CONSTRAINT FK_Agencies_Governorates FOREIGN KEY (GovernorateId) REFERENCES dbo.Governorates (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].dbo.Agencies ADD CONSTRAINT FK_Agencies_Ministries FOREIGN KEY (MinistryId) REFERENCES dbo.Ministries (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].dbo.Contacts ADD CONSTRAINT FK_Contacts_Agencies FOREIGN KEY (AgencyId) REFERENCES dbo.Agencies (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].dbo.Contacts ADD CONSTRAINT FK_Contacts_Ministries FOREIGN KEY (MinistryId) REFERENCES dbo.Ministries (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].dbo.Descriptions ADD CONSTRAINT FK_Descriptions_Locales FOREIGN KEY (LocaleId) REFERENCES dbo.Locales (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].dbo.DomainPages ADD CONSTRAINT FK_DomainPages_Domain FOREIGN KEY (DomainId) REFERENCES dbo.Domains (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].dbo.Ministries ADD CONSTRAINT FK_OrganizationTypes_Ministries FOREIGN KEY (OrganizationTypeId) REFERENCES dbo.OrganizationTypes (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].dbo.MinistryFaqs ADD CONSTRAINT FK_MinistryFaqs_Ministries FOREIGN KEY (MinistryId) REFERENCES dbo.Ministries (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].dbo.MinistryNews ADD CONSTRAINT FK_MinistryNews_Ministries FOREIGN KEY (MinistryId) REFERENCES dbo.Ministries (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].dbo.MinistryPages ADD CONSTRAINT FK_MinistryPages_DomainPages FOREIGN KEY (DomainPageId) REFERENCES dbo.DomainPages (Id) 
		ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [FMS_TEST2].dbo.MinistryPages ADD CONSTRAINT FK_MinistryPages_Ministries FOREIGN KEY (MinistryId) REFERENCES dbo.Ministries (Id) 
		ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [FMS_TEST2].dbo.MinistryProjects ADD CONSTRAINT FK_MinistryProjects_Ministries FOREIGN KEY (MinistryId) REFERENCES dbo.Ministries (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].dbo.MinistryUserPages ADD CONSTRAINT FK_MinistryUserPages_DomainPages FOREIGN KEY (DomainPageId) REFERENCES dbo.DomainPages (Id) 
		ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [FMS_TEST2].dbo.MinistryUserPages ADD CONSTRAINT FK_MinistryUserPages_MinistryUsers FOREIGN KEY (MinistryUserId) REFERENCES dbo.MinistryUsers (Id) 
		ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [FMS_TEST2].dbo.MinistryUsers ADD CONSTRAINT FK_MinistryUsers_Ministries FOREIGN KEY (MinistryId) REFERENCES dbo.Ministries (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].dbo.MinistryUsers ADD CONSTRAINT FK_MinistryUsers_Users FOREIGN KEY (UserId) REFERENCES dbo.Users (Id) 
		ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [FMS_TEST2].dbo.Names ADD CONSTRAINT FK_NameLocales_Locales FOREIGN KEY (LocaleId) REFERENCES dbo.Locales (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].dbo.ServiceCategories ADD CONSTRAINT FK_ServiceCategories_Ministries FOREIGN KEY (MinistryId) REFERENCES dbo.Ministries (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].dbo.ServiceCommentFields ADD CONSTRAINT FK_ServiceCommentFields_ImprovementFields FOREIGN KEY (ImprovementFieldId) REFERENCES dbo.ImprovementFields (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].dbo.ServiceCommentFields ADD CONSTRAINT FK_ServiceCommentFields_ServiceComments FOREIGN KEY (ServiceCommentId) REFERENCES dbo.ServiceComments (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].dbo.ServiceComments ADD CONSTRAINT FK_ServiceComments_Services FOREIGN KEY (ServiceId) REFERENCES dbo.Services (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].dbo.ServiceComments ADD CONSTRAINT FK_ServiceComments_Users FOREIGN KEY (UserId) REFERENCES dbo.Users (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].dbo.ServiceConstraints ADD CONSTRAINT FK_ServiceConstraints_Services FOREIGN KEY (ServiceId) REFERENCES dbo.Services (Id) 
		ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [FMS_TEST2].dbo.ServiceRequirements ADD CONSTRAINT FK_ServiceRequirements_Services FOREIGN KEY (ServiceId) REFERENCES dbo.Services (Id) 
		ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [FMS_TEST2].dbo.Services ADD CONSTRAINT FK_Services_Agencies FOREIGN KEY (AgencyId) REFERENCES dbo.Agencies (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].dbo.Services ADD CONSTRAINT FK_Services_ServiceCategories FOREIGN KEY (ServiceCategoryId) REFERENCES dbo.ServiceCategories (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE [FMS_TEST2].dbo.ServiceSteps ADD CONSTRAINT FK_ServiceSteps_Services FOREIGN KEY (ServiceId) REFERENCES dbo.Services (Id) 
		ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [FMS_TEST2].dbo.ServiceUrls ADD CONSTRAINT FK_ServiceUrls_Services FOREIGN KEY (ServiceId) REFERENCES dbo.Services (Id) 
		ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [FMS_TEST2].dbo.UpdateLogs ADD CONSTRAINT FK_UpdateLogs_Ministries FOREIGN KEY (MinistryId) REFERENCES dbo.Ministries (Id) 
		ON UPDATE CASCADE ON DELETE SET NULL;
ALTER TABLE [FMS_TEST2].dbo.Users ADD CONSTRAINT FK_Users_UserTypes FOREIGN KEY (UserTypeId) REFERENCES dbo.UserTypes (Id) 
		ON UPDATE NO ACTION ON DELETE NO ACTION;
 
-- NO MODIFIED CONSTRAINTS
 
GO
 
-- OTHER MODIFIED DATA:
-- FUNCTION  		 CREATED 	Jan  8 2018 11:04AM		dbo.fn_diagramobjects

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
-- FUNCTION  		 CREATED 	Jan  8 2018 11:50AM		dbo.GetName
CREATE FUNCTION [dbo].[GetName]
(
	-- Add the parameters for the function here
	@EntityType  varchar(50),
	@EntityId int,
	@LocaleId int,
	@PropertyName varchar(50),
	@default nvarchar(max)
)
RETURNS nvarchar(max)
AS
BEGIN
	DECLARE @ret nvarchar(max)=NULL;

	select 
		TOP 1 @ret=[Text] 
	from Names 
	where 
		EntityId=@EntityId AND 
		LocaleId=@LocaleId AND 
		EntityType=@EntityType AND 
		PropertyName=@PropertyName;

	if @ret IS null
		return @default;
	
	return @ret;
END
GO
-- FUNCTION  		 CREATED 	Jan  2 2020  3:24PM		dbo.NumberString2
CREATE function [dbo].[NumberString2]()
RETURNS VARCHAR(15)
AS
begin
	--declare @res varchar(15)=convert(varchar(15),@val);

	--if(len(@val)<@minLength)
	--	select @res= RIGHT('00000000000000'+@res,@minLength)
	return '';-- @res;
end
GO
-- FUNCTION  		 CREATED 	Jan  8 2018  2:56PM		dbo.GetDescription
CREATE FUNCTION [dbo].[GetDescription]
(
	-- Add the parameters for the function here
	@EntityType  varchar(50),
	@EntityId int,
	@LocaleId int,
	@PropertyName varchar(50),
	@default nvarchar(max)
)
RETURNS nvarchar(max)
AS
BEGIN
	DECLARE @ret nvarchar(max);

	select 
		TOP 1 @ret=[Text] 
	from Descriptions 
	where 
		EntityId=@EntityId AND 
		LocaleId=@LocaleId AND 
		EntityType=@EntityType AND 
		PropertyName=@PropertyName;

	if(@ret IS null)
		return @default;
	
	return @ret;
END
GO
-- FUNCTION  		 CREATED 	Jan  8 2018  2:57PM		dbo.GetAddress
CREATE FUNCTION [dbo].[GetAddress]
(
	-- Add the parameters for the function here
	@EntityType  varchar(50),
	@EntityId int,
	@LocaleId int,
	@PropertyName varchar(50),
	@default nvarchar(max)
)
RETURNS nvarchar(max)
AS
BEGIN
	DECLARE @ret nvarchar(max);

	select 
		TOP 1 @ret=[Text] 
	from Adresses 
	where 
		EntityId=@EntityId AND 
		LocaleId=@LocaleId AND 
		EntityType=@EntityType AND 
		PropertyName=@PropertyName;

	if(@ret IS null)
		return @default;
	
	return @ret;
END
GO
-- PROCEDURE  		 CREATED 	Feb 25 2018  6:25PM		dbo.AddAuditingColumns
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE AddAuditingColumns

AS
BEGIN
	declare @command nvarchar(max);
	declare @tables TABLE(ID int,TABLE_NAME varchar(60));
	declare @i int=0;
	declare @table varchar(60);

	insert into @tables (ID,TABLE_NAME)
	select 
		ROW_NUMBER() over (order by TABLE_NAME) ,
		TABLE_NAME 
	from INFORMATION_SCHEMA.TABLES
	where 
		TABLE_NAME!='sysdiagrams' AND
		TABLE_NAME NOT IN (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE COLUMN_NAME='CreatedOn');

	while(@i<(select count(*) from @tables))
	begin
		select @i=@i+1;
		select @table=TABLE_NAME from @tables where ID=@i;
		select @command='ALTER TABLE '+@table+' ADD CreatedOn Datetime null;'
		EXEC sp_executesql @command;
		select @command='ALTER TABLE '+@table+' ADD UpdatedOn Datetime null;'
		EXEC sp_executesql @command;
		select @command='ALTER TABLE '+@table+' ADD UpdatedBy int null;'
		EXEC sp_executesql @command;

	end;
END

GO
-- PROCEDURE  		 CREATED 	Jan  8 2018 11:04AM		dbo.sp_upgraddiagrams

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
-- PROCEDURE  		 CREATED 	Jan  8 2018 11:04AM		dbo.sp_helpdiagrams

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
-- PROCEDURE  		 CREATED 	Jan  8 2018 11:04AM		dbo.sp_helpdiagramdefinition

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
-- PROCEDURE  		 CREATED 	Jan  8 2018 11:04AM		dbo.sp_creatediagram

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
-- PROCEDURE  		 CREATED 	Jan  8 2018 11:04AM		dbo.sp_renamediagram

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
-- PROCEDURE  		 CREATED 	Jan  8 2018 11:04AM		dbo.sp_alterdiagram

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
-- PROCEDURE  		 CREATED 	Jan  8 2018 11:04AM		dbo.sp_dropdiagram

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
