
CREATE TABLE [dbo].[PageCategoryParameters](
	[Id] [bigint] NOT NULL,
	[Name] [varchar](50) NULL,
	[Type] [int] NOT NULL,
	[DefaultValue] [varchar](max) NULL,
	[PageCategoryId] [bigint] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [bigint] NULL,
 CONSTRAINT [PK_PageCategoryParameters] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PageParameters]    Script Date: 1/23/2020 2:51:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PageParameters](
	[Id] [bigint] NOT NULL,
	[PageCategoryParameterId] [bigint] NOT NULL,
	[PageId] [bigint] NOT NULL,
	[LinkedPageId] [bigint] NULL,
	[ParameterValue] [nvarchar](max) NULL,
	[UseDefault] [bit] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [bigint] NULL,
 CONSTRAINT [PK_PageParameters] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PageRoutes]    Script Date: 1/23/2020 2:51:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PageRoutes](
	[Id] [bigint] NOT NULL,
	[PageId] [bigint] NOT NULL,
	[ListUrl] [bigint] NULL,
	[AddUrl] [bigint] NULL,
	[EditUrl] [bigint] NULL,
	[DetailsUrl] [bigint] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [bigint] NULL,
 CONSTRAINT [PK_PageRoutes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[PageCategoryParameters]  WITH CHECK ADD  CONSTRAINT [FK_PageCategoryParameters_PageCategories] FOREIGN KEY([PageCategoryId])
REFERENCES [dbo].[PageCategories] ([Id])
GO
ALTER TABLE [dbo].[PageCategoryParameters] CHECK CONSTRAINT [FK_PageCategoryParameters_PageCategories]
GO
ALTER TABLE [dbo].[PageParameters]  WITH CHECK ADD  CONSTRAINT [FK_PageParameters_PageCategoryParameters] FOREIGN KEY([PageCategoryParameterId])
REFERENCES [dbo].[PageCategoryParameters] ([Id])
GO
ALTER TABLE [dbo].[PageParameters] CHECK CONSTRAINT [FK_PageParameters_PageCategoryParameters]
GO
ALTER TABLE [dbo].[PageParameters]  WITH CHECK ADD  CONSTRAINT [FK_PageParameters_Pages] FOREIGN KEY([PageId])
REFERENCES [dbo].[Pages] ([Id])
GO
ALTER TABLE [dbo].[PageParameters] CHECK CONSTRAINT [FK_PageParameters_Pages]
GO
ALTER TABLE [dbo].[PageRoutes]  WITH CHECK ADD  CONSTRAINT [FK_PageRoutes_Pages] FOREIGN KEY([PageId])
REFERENCES [dbo].[Pages] ([Id])
GO
ALTER TABLE [dbo].[PageRoutes] CHECK CONSTRAINT [FK_PageRoutes_Pages]
GO
