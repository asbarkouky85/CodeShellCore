
CREATE TABLE [Auth].[ResourceCollections](
	[Id] [bigint] NOT NULL,
	[Name] [varchar](50) NULL,
	[ResourceId] [bigint] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [bigint] NULL,
 CONSTRAINT [PK_ResourceCollections] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [Auth].[ResourceCollections] ADD  CONSTRAINT [FK_ResourceCollections_Resources] FOREIGN KEY([ResourceId])
REFERENCES [Auth].[Resources] ([Id])
GO

ALTER TABLE [Auth].[RoleResources] ADD CollectionId bigint null;
GO

ALTER TABLE [Auth].[RoleResources] ADD  CONSTRAINT [FK_RoleResources_ResourceCollections] FOREIGN KEY([CollectionId])
REFERENCES [Auth].[ResourceCollections] ([Id])
GO

