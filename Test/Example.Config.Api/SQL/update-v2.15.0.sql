exec sp_rename 'dbo.DomainEntityCollections', 'ResourceCollections';
GO
exec sp_rename 'dbo.TenantApps','Apps';
GO
exec sp_rename 'dbo.EntityCollectionConditions','ResourceCollectionConditions';
GO

alter table ResourceCollections drop constraint FK_DomainEntityCollections_DomainEntities;
alter table ResourceCollections drop column DomainEntityId;

alter table PageCategories drop constraint FK_PageCategories_DomainEntities1;
alter table PageCategories drop column DomainEntityId;
alter table PageCategories drop column ScriptPath;

alter table Controls drop constraint FK_Controls_DomainEntityProperties;
alter table Controls drop column DomainEntityPropertyId;

alter table Pages drop column TenantDomainId;
alter table Pages drop column Presistant;

drop table DomainEntityProperties;
drop table DomainEntities;

drop procedure ChangeAccessibilty;
drop procedure DeleteTemplate;
drop procedure SetCollectionId;
drop function GetCount;

alter table ResourceCollections add ResourceId bigint null;
alter table ResourceCollections add constraint FK_ResourceCollections_Resources foreign key (ResourceId) references Resources (Id)
ON UPDATE CASCADE
ON DELETE CASCADE; 
GO

CREATE TABLE [dbo].[CustomTexts](
	[Id] [bigint] NOT NULL,
	[Code] [varchar](100) NULL,
	[Value] [nvarchar](50) NULL,
	[Locale] [varchar](5) NULL,
	[TenantId] [bigint] NOT NULL,
	[Type] [int] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [bigint] NULL,
 CONSTRAINT [PK_CustomTexts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CustomTexts] ADD  CONSTRAINT [FK_CustomTexts_Tenants] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenants] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

alter table Apps drop constraint FK_TenantApps_Tenants;
alter table Apps add constraint FK_Apps_Tenants foreign key (TenantId) references Tenants (Id)
ON UPDATE CASCADE
ON DELETE CASCADE;
GO

alter table NavigationPages drop constraint FK_NavigationPages_NavigationGroups;
alter table NavigationPages add constraint FK_NavigationPages_NavigationGroups foreign key (NavigationGroupId) references NavigationGroups (Id)
ON UPDATE CASCADE
ON DELETE CASCADE;
GO

alter table PageCategoryParameters drop constraint FK_PageCategoryParameters_PageCategories;
alter table PageCategoryParameters add constraint FK_PageCategoryParameters_PageCategories foreign key (PageCategoryId) references PageCategories (Id)
ON UPDATE CASCADE
ON DELETE CASCADE;
GO

alter table PageParameters drop constraint FK_PageParameters_Pages;
alter table PageParameters add constraint FK_PageParameters_Pages foreign key (PageId) references Pages (Id)
ON UPDATE CASCADE
ON DELETE CASCADE;
GO

