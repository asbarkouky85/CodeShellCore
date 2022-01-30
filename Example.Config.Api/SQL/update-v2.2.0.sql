CREATE TABLE [dbo].[NavigationGroups](
	[Id] [bigint] NOT NULL,
	[Name] [nvarchar](150) NULL,
	[ParentId] [bigint] NULL,
	[Chain] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [bigint] NULL,
 CONSTRAINT [PK_NavigationGroups] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NavigationPages]    Script Date: 9/15/2019 11:24:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NavigationPages](
	[Id] [bigint] NOT NULL,
	[PageId] [bigint] NULL,
	[DisplayOrder] [int] NOT NULL,
	[NavigationGroupId] [bigint] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [bigint] NULL,
 CONSTRAINT [PK_NavigationPages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[NavigationPages]  WITH CHECK ADD  CONSTRAINT [FK_NavigationPages_NavigationGroups] FOREIGN KEY([NavigationGroupId])
REFERENCES [dbo].[NavigationGroups] ([Id])
GO
ALTER TABLE [dbo].[NavigationPages] CHECK CONSTRAINT [FK_NavigationPages_NavigationGroups]
GO
ALTER TABLE [dbo].[NavigationPages]  WITH CHECK ADD  CONSTRAINT [FK_NavigationPages_Pages] FOREIGN KEY([PageId])
REFERENCES [dbo].[Pages] ([Id])
GO
ALTER TABLE [dbo].[NavigationPages] CHECK CONSTRAINT [FK_NavigationPages_Pages]
GO

--2.2.17
alter table Tenants add BaseStyle nvarchar(100) null;

--2.2.19
alter table Pages drop constraint DF_Pages_AppearsInNavigation;
alter table Pages drop column AppearsInNavigation;
alter table Pages drop column DisplayName;
alter table Pages add IsHomePage bit null;
