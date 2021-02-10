
create function [dbo].[CompareVersion](
	@v1 varchar(100),
	@v2 varchar(100)
)
returns int
AS
begin
declare @result int;

    select @result=case 
    when CONVERT(int, LEFT(@v1, CHARINDEX('.', @v1)-1)) < CONVERT(int, LEFT(@v2, CHARINDEX('.', @v2)-1)) then 2
    when CONVERT(int, LEFT(@v1, CHARINDEX('.', @v1)-1)) > CONVERT(int, LEFT(@v2, CHARINDEX('.', @v2)-1)) then 1
    when CONVERT(int, substring(@v1, CHARINDEX('.', @v1)+1, LEN(@v1))) < CONVERT(int, substring(@v2, CHARINDEX('.', @v2)+1, LEN(@v1))) then 2
    when CONVERT(int, substring(@v1, CHARINDEX('.', @v1)+1, LEN(@v1))) > CONVERT(int, substring(@v2, CHARINDEX('.', @v2)+1, LEN(@v1))) then 1
    else 0 end;
return @result
end;



GO
/****** Object:  UserDefinedFunction [dbo].[GenerateId]    Script Date: 4/7/2020 6:20:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[GenerateId]
(
	@time Datetime,
	@rowNum int
)
RETURNS bigint
AS
BEGIN
	-- Declare the return variable here
	DECLARE @id bigint;
	DECLARE @idString varchar(15);
	DECLARE @secs int;
	select @secs=(DATEPART(hour,@time)*3600)+(DATEPART(MINUTE,@time)*60)+DATEPART(SECOND,@time);
	if @rowNum>999 
		begin
			
			select @secs=@secs+(@rowNum/1000);
			select @rowNum=@rowNum%1000;
		end

	select @idString=CONCAT(
		(YEAR(@time)-2000),
		RIGHT('000'+CAST(DATEPART(dayofyear,@time) as varchar(5)),3),
		RIGHT('00000'+CAST(@secs as varchar(5)),5),
		RIGHT('000'+CAST(@rowNum as varchar(5)),3)
	)
	
	-- Return the result of the function
	RETURN CONVERT(bigint,@idString);

END




GO
/****** Object:  UserDefinedFunction [dbo].[GetCount]    Script Date: 4/7/2020 6:20:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[GetCount]
(
	-- Add the parameters for the function here
	@table varchar(70)
)
RETURNS varchar(100)
AS
BEGIN

	RETURN '('+@table+')';

END




GO
/****** Object:  UserDefinedFunction [dbo].[GetPath]    Script Date: 4/7/2020 6:20:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[GetPath]
(
	@EntityType varchar(50),
	@chain nvarchar(max)
	
)
RETURNS nvarchar(max)
AS
BEGIN
	declare @ColumnNameList nvarchar(max);

	if(@EntityType='Domain')
		SELECT  
			@ColumnNameList = COALESCE(@ColumnNameList,'')+(CASE WHEN (@ColumnNameList is NULL) THEN '' ELSE '/' END) +Name
		FROM Domains 
		WHERE @chain like '%|'+convert(varchar(15),Id)+'|%';

	return @ColumnNameList;
END




GO
/****** Object:  Table [dbo].[Clients]    Script Date: 4/7/2020 6:20:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Clients](
	[Id] [bigint] NOT NULL,
	[Identifier] [varchar](100) NULL,
	[Secret] [varchar](100) NULL,
	[Address] [varchar](300) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[UpdatedBy] [bigint] NULL,
 CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Controls]    Script Date: 4/7/2020 6:20:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Controls](
	[Id] [bigint] NOT NULL,
	[ControlType] [varchar](50) NULL,
	[DesignParameters] [ntext] NULL,
	[ParentControl] [bigint] NULL,
	[PageCategoryId] [bigint] NULL,
	[DomainEntityPropertyId] [bigint] NULL,
	[Identifier] [varchar](200) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[UpdatedBy] [bigint] NULL,
 CONSTRAINT [PK_PageControls] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ControlValidators]    Script Date: 4/7/2020 6:20:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ControlValidators](
	[Id] [bigint] NOT NULL,
	[ControlId] [bigint] NOT NULL,
	[ValidatorId] [bigint] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[UpdatedBy] [bigint] NULL,
 CONSTRAINT [PK_ControlValidators] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CustomFields]    Script Date: 4/7/2020 6:20:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomFields](
	[Id] [bigint] NOT NULL,
	[Name] [varchar](150) NULL,
	[Type] [varchar](50) NULL,
	[PageId] [bigint] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [bigint] NULL,
 CONSTRAINT [PK_CustomFields] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DomainEntities]    Script Date: 4/7/2020 6:20:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DomainEntities](
	[Id] [bigint] NOT NULL,
	[Name] [varchar](50) NULL,
	[DomainId] [bigint] NOT NULL,
	[IsSystem] [bit] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[UpdatedBy] [bigint] NULL,
 CONSTRAINT [PK_DomainEntities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DomainEntityCollections]    Script Date: 4/7/2020 6:20:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DomainEntityCollections](
	[Id] [bigint] NOT NULL,
	[Name] [varchar](50) NULL,
	[DomainEntityId] [bigint] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[UpdatedBy] [bigint] NULL,
 CONSTRAINT [PK_DomainEntityCollections] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DomainEntityProperties]    Script Date: 4/7/2020 6:20:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DomainEntityProperties](
	[Id] [bigint] NOT NULL,
	[DomainEntityId] [bigint] NULL,
	[Name] [nvarchar](50) NULL,
	[IsSystem] [bit] NOT NULL,
	[DataType] [varchar](50) NULL,
	[ReferenceEntityId] [bigint] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[UpdatedBy] [bigint] NULL,
 CONSTRAINT [PK_DomainEntityProperties] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Domains]    Script Date: 4/7/2020 6:20:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Domains](
	[Id] [bigint] NOT NULL,
	[Name] [varchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[UpdatedBy] [bigint] NULL,
	[ParentId] [bigint] NULL,
	[Chain] [varchar](max) NULL,
	[NameChain] [nvarchar](max) NULL,
 CONSTRAINT [PK_Domains] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EntityCollectionConditions]    Script Date: 4/7/2020 6:20:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EntityCollectionConditions](
	[Id] [bigint] NOT NULL,
	[DomainEntityCollectionId] [bigint] NULL,
	[Property] [varchar](100) NULL,
	[Value] [nvarchar](300) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[UpdatedBy] [bigint] NULL,
 CONSTRAINT [PK_EntityCollectionConditions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[NavigationGroups]    Script Date: 4/7/2020 6:20:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
/****** Object:  Table [dbo].[NavigationPages]    Script Date: 4/7/2020 6:20:13 PM ******/
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
/****** Object:  Table [dbo].[PageCategories]    Script Date: 4/7/2020 6:20:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PageCategories](
	[Id] [bigint] NOT NULL,
	[Name] [varchar](50) NULL,
	[DomainEntityId] [bigint] NULL,
	[BaseComponent] [varchar](300) NULL,
	[ViewPath] [varchar](300) NULL,
	[ScriptPath] [varchar](300) NULL,
	[ResourceId] [bigint] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[UpdatedBy] [bigint] NULL,
	[Layout] [varchar](150) NULL,
	[DomainId] [bigint] NULL,
 CONSTRAINT [PK_PageCategories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PageCategoryParameters]    Script Date: 4/7/2020 6:20:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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
/****** Object:  Table [dbo].[PageControls]    Script Date: 4/7/2020 6:20:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PageControls](
	[Id] [bigint] NOT NULL,
	[ControlId] [bigint] NOT NULL,
	[PageId] [bigint] NOT NULL,
	[Accessability] [tinyint] NOT NULL,
	[SourceCollectionId] [bigint] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[UpdatedBy] [bigint] NULL,
	[Persistent] [bit] NULL,
 CONSTRAINT [PK_PageControls_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PageControlValidators]    Script Date: 4/7/2020 6:20:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PageControlValidators](
	[Id] [bigint] NOT NULL,
	[PageControlId] [bigint] NOT NULL,
	[ValidatorId] [bigint] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[UpdatedBy] [bigint] NULL,
 CONSTRAINT [PK_PageControlValidators] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PageParameters]    Script Date: 4/7/2020 6:20:13 PM ******/
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
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [bigint] NULL,
	[UseDefault] [bit] NOT NULL,
 CONSTRAINT [PK_PageParameters] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PageRoutes]    Script Date: 4/7/2020 6:20:13 PM ******/
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
/****** Object:  Table [dbo].[Pages]    Script Date: 4/7/2020 6:20:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Pages](
	[Id] [bigint] NOT NULL,
	[Name] [varchar](50) NULL,
	[ViewPath] [varchar](300) NULL,
	[Apps] [varchar](max) NULL,
	[ViewParams] [ntext] NULL,
	[ResourceId] [bigint] NULL,
	[Layout] [varchar](200) NULL,
	[PrivilegeType] [varchar](50) NULL,
	[ResourceActionId] [bigint] NULL,
	[SpecialPermission] [varchar](50) NULL,
	[SourceCollectionId] [bigint] NULL,
	[PageCategoryId] [bigint] NULL,
	[RouteParameters] [varchar](100) NULL,
	[TenantDomainId] [bigint] NULL,
	[HasRoute] [bit] NOT NULL,
	[CanEmbed] [bit] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[UpdatedBy] [bigint] NULL,
	[DefaultAccessibility] [int] NOT NULL,
	[Presistant] [bit] NULL,
	[DomainId] [bigint] NOT NULL,
	[TenantId] [bigint] NOT NULL,
	[IsHomePage] [bit] NULL,
	[ParentId] [bigint] NULL,
 CONSTRAINT [PK_Pages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ResourceActions]    Script Date: 4/7/2020 6:20:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ResourceActions](
	[Id] [bigint] NOT NULL,
	[Name] [varchar](150) NULL,
	[ResourceId] [bigint] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
	[TenantId] [bigint] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[UpdatedBy] [bigint] NULL,
 CONSTRAINT [PK_ResourceActions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Resources]    Script Date: 4/7/2020 6:20:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Resources](
	[Id] [bigint] NOT NULL,
	[Name] [varchar](150) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[UpdatedBy] [bigint] NULL,
	[ServiceName] [varchar](50) NULL,
	[DomainId] [bigint] NULL,
 CONSTRAINT [PK_Resources_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TenantApps]    Script Date: 4/7/2020 6:20:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TenantApps](
	[Id] [bigint] NOT NULL,
	[Name] [varchar](150) NULL,
	[DisplayName] [nvarchar](150) NULL,
	[TenantId] [bigint] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [bigint] NULL,
	[DashboardUrl] [nvarchar](300) NULL,
 CONSTRAINT [PK_TenantApps] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Tenants]    Script Date: 4/7/2020 6:20:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Tenants](
	[Id] [bigint] NOT NULL,
	[Code] [varchar](100) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Description] [ntext] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
	[ConnectionString] [text] NULL,
	[CreatedBy] [bigint] NULL,
	[UpdatedBy] [bigint] NULL,
	[IsActive] [bit] NOT NULL,
	[Logo] [nvarchar](300) NULL,
	[MainComponentBase] [nvarchar](300) NULL,
	[BaseStyle] [nvarchar](100) NULL,
	[Version] [varchar](100) NULL,
	[ParentId] [bigint] NULL,
 CONSTRAINT [PK_Tenants] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Users]    Script Date: 4/7/2020 6:20:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [bigint] NOT NULL,
	[LogonName] [varchar](150) NULL,
	[DisplayName] [nvarchar](150) NULL,
	[Password] [nvarchar](100) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[UpdatedBy] [bigint] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Validators]    Script Date: 4/7/2020 6:20:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Validators](
	[Id] [bigint] NOT NULL,
	[Type] [varchar](150) NULL,
	[CalendarType] [varchar](50) NULL,
	[MinValue] [varchar](50) NULL,
	[MaxValue] [varchar](50) NULL,
	[MinLength] [int] NULL,
	[MaxLength] [int] NULL,
	[Pattern] [varchar](150) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[UpdatedBy] [bigint] NULL,
 CONSTRAINT [PK_Validators] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Controls]  WITH CHECK ADD  CONSTRAINT [FK_Controls_DomainEntityProperties] FOREIGN KEY([DomainEntityPropertyId])
REFERENCES [dbo].[DomainEntityProperties] ([Id])
GO
ALTER TABLE [dbo].[Controls] CHECK CONSTRAINT [FK_Controls_DomainEntityProperties]
GO
ALTER TABLE [dbo].[Controls]  WITH CHECK ADD  CONSTRAINT [FK_Controls_PageCategories] FOREIGN KEY([PageCategoryId])
REFERENCES [dbo].[PageCategories] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Controls] CHECK CONSTRAINT [FK_Controls_PageCategories]
GO
ALTER TABLE [dbo].[Controls]  WITH CHECK ADD  CONSTRAINT [FK_PageControls_PageControls] FOREIGN KEY([ParentControl])
REFERENCES [dbo].[Controls] ([Id])
GO
ALTER TABLE [dbo].[Controls] CHECK CONSTRAINT [FK_PageControls_PageControls]
GO
ALTER TABLE [dbo].[ControlValidators]  WITH CHECK ADD  CONSTRAINT [FK_ControlValidators_Controls] FOREIGN KEY([ControlId])
REFERENCES [dbo].[Controls] ([Id])
GO
ALTER TABLE [dbo].[ControlValidators] CHECK CONSTRAINT [FK_ControlValidators_Controls]
GO
ALTER TABLE [dbo].[ControlValidators]  WITH CHECK ADD  CONSTRAINT [FK_ControlValidators_Validators] FOREIGN KEY([ValidatorId])
REFERENCES [dbo].[Validators] ([Id])
GO
ALTER TABLE [dbo].[ControlValidators] CHECK CONSTRAINT [FK_ControlValidators_Validators]
GO
ALTER TABLE [dbo].[CustomFields]  WITH CHECK ADD  CONSTRAINT [FK_CustomFields_Pages] FOREIGN KEY([PageId])
REFERENCES [dbo].[Pages] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CustomFields] CHECK CONSTRAINT [FK_CustomFields_Pages]
GO
ALTER TABLE [dbo].[DomainEntities]  WITH CHECK ADD  CONSTRAINT [FK_DomainEntities_Domains] FOREIGN KEY([DomainId])
REFERENCES [dbo].[Domains] ([Id])
GO
ALTER TABLE [dbo].[DomainEntities] CHECK CONSTRAINT [FK_DomainEntities_Domains]
GO
ALTER TABLE [dbo].[DomainEntityCollections]  WITH CHECK ADD  CONSTRAINT [FK_DomainEntityCollections_DomainEntities] FOREIGN KEY([DomainEntityId])
REFERENCES [dbo].[DomainEntities] ([Id])
GO
ALTER TABLE [dbo].[DomainEntityCollections] CHECK CONSTRAINT [FK_DomainEntityCollections_DomainEntities]
GO
ALTER TABLE [dbo].[DomainEntityProperties]  WITH CHECK ADD  CONSTRAINT [FK_DomainEntityProperties_DomainEntities] FOREIGN KEY([DomainEntityId])
REFERENCES [dbo].[DomainEntities] ([Id])
GO
ALTER TABLE [dbo].[DomainEntityProperties] CHECK CONSTRAINT [FK_DomainEntityProperties_DomainEntities]
GO
ALTER TABLE [dbo].[DomainEntityProperties]  WITH CHECK ADD  CONSTRAINT [FK_DomainEntityProperties_DomainEntities1] FOREIGN KEY([ReferenceEntityId])
REFERENCES [dbo].[DomainEntities] ([Id])
GO
ALTER TABLE [dbo].[DomainEntityProperties] CHECK CONSTRAINT [FK_DomainEntityProperties_DomainEntities1]
GO
ALTER TABLE [dbo].[EntityCollectionConditions]  WITH CHECK ADD  CONSTRAINT [FK_EntityCollectionConditions_DomainEntityCollections] FOREIGN KEY([DomainEntityCollectionId])
REFERENCES [dbo].[DomainEntityCollections] ([Id])
GO
ALTER TABLE [dbo].[EntityCollectionConditions] CHECK CONSTRAINT [FK_EntityCollectionConditions_DomainEntityCollections]
GO
ALTER TABLE [dbo].[NavigationPages]  WITH CHECK ADD  CONSTRAINT [FK_NavigationPages_NavigationGroups] FOREIGN KEY([NavigationGroupId])
REFERENCES [dbo].[NavigationGroups] ([Id])
GO
ALTER TABLE [dbo].[NavigationPages] CHECK CONSTRAINT [FK_NavigationPages_NavigationGroups]
GO
ALTER TABLE [dbo].[NavigationPages]  WITH CHECK ADD  CONSTRAINT [FK_NavigationPages_Pages] FOREIGN KEY([PageId])
REFERENCES [dbo].[Pages] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[NavigationPages] CHECK CONSTRAINT [FK_NavigationPages_Pages]
GO
ALTER TABLE [dbo].[PageCategories]  WITH CHECK ADD  CONSTRAINT [FK_PageCategories_DomainEntities1] FOREIGN KEY([DomainEntityId])
REFERENCES [dbo].[DomainEntities] ([Id])
GO
ALTER TABLE [dbo].[PageCategories] CHECK CONSTRAINT [FK_PageCategories_DomainEntities1]
GO
ALTER TABLE [dbo].[PageCategories]  WITH CHECK ADD  CONSTRAINT [FK_PageCategories_Domains] FOREIGN KEY([DomainId])
REFERENCES [dbo].[Domains] ([Id])
GO
ALTER TABLE [dbo].[PageCategories] CHECK CONSTRAINT [FK_PageCategories_Domains]
GO
ALTER TABLE [dbo].[PageCategories]  WITH CHECK ADD  CONSTRAINT [FK_PageCategories_Resources] FOREIGN KEY([ResourceId])
REFERENCES [dbo].[Resources] ([Id])
GO
ALTER TABLE [dbo].[PageCategories] CHECK CONSTRAINT [FK_PageCategories_Resources]
GO
ALTER TABLE [dbo].[PageCategoryParameters]  WITH CHECK ADD  CONSTRAINT [FK_PageCategoryParameters_PageCategories] FOREIGN KEY([PageCategoryId])
REFERENCES [dbo].[PageCategories] ([Id])
GO
ALTER TABLE [dbo].[PageCategoryParameters] CHECK CONSTRAINT [FK_PageCategoryParameters_PageCategories]
GO
ALTER TABLE [dbo].[PageControls]  WITH CHECK ADD  CONSTRAINT [FK_PageControls_Controls] FOREIGN KEY([ControlId])
REFERENCES [dbo].[Controls] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PageControls] CHECK CONSTRAINT [FK_PageControls_Controls]
GO
ALTER TABLE [dbo].[PageControls]  WITH CHECK ADD  CONSTRAINT [FK_PageControls_DomainEntityCollections] FOREIGN KEY([SourceCollectionId])
REFERENCES [dbo].[DomainEntityCollections] ([Id])
GO
ALTER TABLE [dbo].[PageControls] CHECK CONSTRAINT [FK_PageControls_DomainEntityCollections]
GO
ALTER TABLE [dbo].[PageControls]  WITH CHECK ADD  CONSTRAINT [FK_PageControls_Pages] FOREIGN KEY([PageId])
REFERENCES [dbo].[Pages] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PageControls] CHECK CONSTRAINT [FK_PageControls_Pages]
GO
ALTER TABLE [dbo].[PageControlValidators]  WITH CHECK ADD  CONSTRAINT [FK_PageControlValidators_PageControls] FOREIGN KEY([PageControlId])
REFERENCES [dbo].[PageControls] ([Id])
GO
ALTER TABLE [dbo].[PageControlValidators] CHECK CONSTRAINT [FK_PageControlValidators_PageControls]
GO
ALTER TABLE [dbo].[PageControlValidators]  WITH CHECK ADD  CONSTRAINT [FK_PageControlValidators_Validators] FOREIGN KEY([ValidatorId])
REFERENCES [dbo].[Validators] ([Id])
GO
ALTER TABLE [dbo].[PageControlValidators] CHECK CONSTRAINT [FK_PageControlValidators_Validators]
GO
ALTER TABLE [dbo].[PageParameters]  WITH CHECK ADD  CONSTRAINT [FK_PageParameters_PageCategoryParameters] FOREIGN KEY([PageCategoryParameterId])
REFERENCES [dbo].[PageCategoryParameters] ([Id])
GO
ALTER TABLE [dbo].[PageParameters] CHECK CONSTRAINT [FK_PageParameters_PageCategoryParameters]
GO
ALTER TABLE [dbo].[PageParameters]  WITH CHECK ADD  CONSTRAINT [FK_PageParameters_Pages] FOREIGN KEY([PageId])
REFERENCES [dbo].[Pages] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PageParameters] CHECK CONSTRAINT [FK_PageParameters_Pages]
GO
ALTER TABLE [dbo].[PageRoutes]  WITH CHECK ADD  CONSTRAINT [FK_PageRoutes_Pages] FOREIGN KEY([PageId])
REFERENCES [dbo].[Pages] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PageRoutes] CHECK CONSTRAINT [FK_PageRoutes_Pages]
GO
ALTER TABLE [dbo].[Pages]  WITH CHECK ADD  CONSTRAINT [FK_Pages_DomainEntityCollections1] FOREIGN KEY([SourceCollectionId])
REFERENCES [dbo].[DomainEntityCollections] ([Id])
GO
ALTER TABLE [dbo].[Pages] CHECK CONSTRAINT [FK_Pages_DomainEntityCollections1]
GO
ALTER TABLE [dbo].[Pages]  WITH CHECK ADD  CONSTRAINT [FK_Pages_Domains] FOREIGN KEY([DomainId])
REFERENCES [dbo].[Domains] ([Id])
GO
ALTER TABLE [dbo].[Pages] CHECK CONSTRAINT [FK_Pages_Domains]
GO
ALTER TABLE [dbo].[Pages]  WITH CHECK ADD  CONSTRAINT [FK_Pages_PageCategories] FOREIGN KEY([PageCategoryId])
REFERENCES [dbo].[PageCategories] ([Id])
GO
ALTER TABLE [dbo].[Pages] CHECK CONSTRAINT [FK_Pages_PageCategories]
GO
ALTER TABLE [dbo].[Pages]  WITH CHECK ADD  CONSTRAINT [FK_Pages_ResourceActions] FOREIGN KEY([ResourceActionId])
REFERENCES [dbo].[ResourceActions] ([Id])
GO
ALTER TABLE [dbo].[Pages] CHECK CONSTRAINT [FK_Pages_ResourceActions]
GO
ALTER TABLE [dbo].[Pages]  WITH CHECK ADD  CONSTRAINT [FK_Pages_Resources] FOREIGN KEY([ResourceId])
REFERENCES [dbo].[Resources] ([Id])
GO
ALTER TABLE [dbo].[Pages] CHECK CONSTRAINT [FK_Pages_Resources]
GO
ALTER TABLE [dbo].[Pages]  WITH CHECK ADD  CONSTRAINT [FK_Pages_Tenants] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenants] ([Id])
GO
ALTER TABLE [dbo].[Pages] CHECK CONSTRAINT [FK_Pages_Tenants]
GO
ALTER TABLE [dbo].[ResourceActions]  WITH CHECK ADD  CONSTRAINT [FK_ResourceActions_Resources] FOREIGN KEY([ResourceId])
REFERENCES [dbo].[Resources] ([Id])
GO
ALTER TABLE [dbo].[ResourceActions] CHECK CONSTRAINT [FK_ResourceActions_Resources]
GO
ALTER TABLE [dbo].[ResourceActions]  WITH CHECK ADD  CONSTRAINT [FK_ResourceActions_Tenants] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenants] ([Id])
GO
ALTER TABLE [dbo].[ResourceActions] CHECK CONSTRAINT [FK_ResourceActions_Tenants]
GO
ALTER TABLE [dbo].[Resources]  WITH CHECK ADD  CONSTRAINT [FK_Resources_Domains] FOREIGN KEY([DomainId])
REFERENCES [dbo].[Domains] ([Id])
GO
ALTER TABLE [dbo].[Resources] CHECK CONSTRAINT [FK_Resources_Domains]
GO
ALTER TABLE [dbo].[TenantApps]  WITH CHECK ADD  CONSTRAINT [FK_TenantApps_Tenants] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenants] ([Id])
GO
ALTER TABLE [dbo].[TenantApps] CHECK CONSTRAINT [FK_TenantApps_Tenants]
GO
/****** Object:  StoredProcedure [dbo].[AddAuditingColumns]    Script Date: 4/7/2020 6:20:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

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
/****** Object:  StoredProcedure [dbo].[ChangeAccessibilty]    Script Date: 4/7/2020 6:20:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[ChangeAccessibilty](
	@pageId bigint,
	@identifier varchar(60),
	@access int
)
as
begin
	update PageControls 
		set
			Accessability=@access
		from PageControls
			join Pages on Pages.Id=PageControls.PageId
		where 
			PageId=@pageId AND
			ControlId in (
				select Id
				from Controls
				where 
					(
						Identifier=@identifier OR 
						(@identifier='ALL' AND Accessability>=@access)
					) AND
					PageCategoryId=Pages.PageCategoryId 
					
			);
end;



GO
/****** Object:  StoredProcedure [dbo].[DeleteTemplate]    Script Date: 4/7/2020 6:20:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[DeleteTemplate](
@templateId bigint=1
)
as
	delete
	from
		Pages
	where PageCategoryId =@templateId;

	delete 
	from 
		PageControls 
	where PageId in (
		select Id
		from Pages
		where PageCategoryId=@templateId);

	delete 
	from 
		Controls
	where PageCategoryId=@templateId;




GO
/****** Object:  StoredProcedure [dbo].[SetCollectionId]    Script Date: 4/7/2020 6:20:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SetCollectionId] (
	@pageId bigint,
	@identifier varchar(100),
	@collectionId bigint
)
AS
BEGIN
	update PageControls 
		set
			SourceCollectionId=@collectionId
		from PageControls
			join Pages on Pages.Id=PageControls.PageId
		where 
			PageId=@pageId AND
			ControlId in (
				select Id
				from Controls
				where 
					(
						Identifier=@identifier
					) AND
					PageCategoryId=Pages.PageCategoryId 
					
			);
END




GO
/****** Object:  StoredProcedure [dbo].[SyncDBs]    Script Date: 4/7/2020 6:20:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--[FMS.Configuration_Yousry]
--[FMS.Configuration_v1.7]
CREATE PROCEDURE [dbo].[SyncDBs] (
	 @db1 nvarchar(100),
	 @db2 nvarchar(100),
	 @id varchar(15)='1',
	 @res varchar(max)=null OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @q nvarchar(max);
	DECLARE @result TABLE(
		SourceDB varchar(150),
		TargetDB varchar(150),
		AddedPages int,
		UpdatedPages int,
		AddedPageControls int,
		UpdatedPageControls int
	);

	INSERT @result 
		select
			@db1,@db2,
			0,0,0,0;

	SET @q=N'insert '+@db2+N'.dbo.TenantDomains
		select * from '+@db1+N'.dbo.TenantDomains
		where Id NOT IN ( select Id from '+@db2+N'.dbo.TenantDomains )';

	EXEC sp_executesql @q;print @q;print CONCAT('TenantDomains : ',@@ROWCOUNT);

	SET @q=N'insert '+@db2+N'.dbo.DomainEntities
		select * from '+@db1+N'.dbo.DomainEntities
		where Id NOT IN ( select Id from '+@db2+N'.dbo.DomainEntities )';

	EXEC sp_executesql @q;print @q;print CONCAT('DomainEntities : ',@@ROWCOUNT);

	SET @q=N'insert '+@db2+N'.dbo.PageCategories
		select * from 
		'+@db1+N'.dbo.PageCategories
		WHERE Id NOT IN ( select Id from '+@db2+N'.dbo.PageCategories )';
	
	EXEC sp_executesql @q;print @q;print CONCAT('PageCategories : ',@@ROWCOUNT);

	SET @q=N'insert '+@db2+N'.dbo.Resources
		select * from '+@db1+N'.dbo.Resources
		where Id NOT IN ( select Id from '+@db2+N'.dbo.Resources )';
	
	EXEC sp_executesql @q;print @q;print CONCAT('Resources : ',@@ROWCOUNT);

	SET @q=N'insert '+@db2+N'.dbo.ResourceActions
		select * from '+@db1+N'.dbo.ResourceActions
		where Id NOT IN ( select Id from '+@db2+N'.dbo.ResourceActions )';
	
	EXEC sp_executesql @q;print @q;print CONCAT('ResourceActions : ',@@ROWCOUNT);

	SET @q=N'insert '+@db2+N'.dbo.DomainEntityCollections
		select * from '+@db1+N'.dbo.DomainEntityCollections
		where Id NOT IN ( select Id from '+@db2+N'.dbo.DomainEntityCollections )';
	
	EXEC sp_executesql @q;print @q;print CONCAT('DomainEntityCollections : ',@@ROWCOUNT);

	SET @q=N'insert '+@db2+N'.dbo.DomainEntityProperties
		select * from '+@db1+N'.dbo.DomainEntityProperties
		where Id NOT IN ( select Id from '+@db2+N'.dbo.DomainEntityProperties )';
	
	EXEC sp_executesql @q;print @q;print CONCAT('DomainEntityProperties : ',@@ROWCOUNT);

	SET @q=N'insert '+@db2+N'.dbo.Controls
		select * from '+@db1+N'.dbo.Controls
		where Id NOT IN ( select Id from '+@db2+N'.dbo.Controls )';
	
	EXEC sp_executesql @q;print @q;print CONCAT('Controls : ',@@ROWCOUNT);

	SET @q=N'insert '+@db2+N'.dbo.Pages
		select * from '+@db1+N'.dbo.Pages
		where 
			TenantDomainId IN (
				select Id from '+@db1+N'.dbo.TenantDomains
				where TenantId='+@id+N'
			) AND 
			Id NOT IN ( select Id from '+@db2+N'.dbo.Pages )';
	
	EXEC sp_executesql @q;print @q;print CONCAT('Pages Created : ',@@ROWCOUNT);

	UPDATE @result set AddedPages=@@ROWCOUNT;

	SET @q=N'insert '+@db2+N'.dbo.PageControls
		select * from '+@db1+N'.dbo.PageControls
		where 
			PageId IN (
				select P.Id 
				from '+@db1+N'.dbo.Pages P
					join '+@db1+N'.dbo.TenantDomains TD on TD.Id=P.TenantDomainId
				where TenantId='+@id+N'
			) AND 
			Id NOT IN ( select Id from '+@db2+N'.dbo.PageControls )';
	
	EXEC sp_executesql @q;print @q;print CONCAT('PageControls Created : ',@@ROWCOUNT);

	UPDATE @result set AddedPageControls=@@ROWCOUNT;

	SET @q=N'update '+@db2+N'.dbo.Pages set
		Apps=tx.Apps,
		ViewParams=tx.ViewParams,
		Layout=tx.Layout,
		PrivilegeType=tx.PrivilegeType,
		ResourceActionId=tx.ResourceActionId,
		SpecialPermission=tx.SpecialPermission,
		SourceCollectionId=tx.SourceCollectionId,
		RouteParameters=tx.RouteParameters
	from '+@db2+N'.dbo.Pages
		join '+@db1+N'.dbo.Pages tx on tx.Id=Pages.Id AND
		tx.UpdatedOn > '+@db2+N'.dbo.Pages.UpdatedOn';
	
	EXEC sp_executesql @q;print @q;print CONCAT('Pages Updates : ',@@ROWCOUNT);

	UPDATE @result set UpdatedPages=@@ROWCOUNT;

	SET @q=N'update '+@db2+N'.dbo.PageControls set 
		Accessability=Existing.Accessability,
		SourceCollectionId=Existing.SourceCollectionId
	from '+@db2+N'.dbo.PageControls
		join '+@db2+N'.dbo.Controls on Controls.Id=PageControls.ControlId
		join '+@db2+N'.dbo.Pages on Pages.Id=PageControls.PageId
		join '+@db2+N'.dbo.TenantDomains on TenantDomains.Id=Pages.TenantDomainId 
		join (
			select 
				ViewPath,
				Identifier,
				Accessability,
				TenantDomains.TenantId,
				PageControls.SourceCollectionId,
				PageControls.UpdatedOn
			from '+@db1+N'.dbo.PageControls
				join '+@db1+N'.dbo.Controls on Controls.Id=PageControls.ControlId
				join '+@db1+N'.dbo.Pages on Pages.Id=PageControls.PageId
				join '+@db1+N'.dbo.TenantDomains on TenantDomains.Id=Pages.TenantDomainId 	
		) as Existing on 
			Pages.ViewPath=Existing.ViewPath AND
			Controls.Identifier=Existing.Identifier AND
			Existing.UpdatedOn > '+@db2+N'.dbo.PageControls.UpdatedOn
	where 
		TenantDomains.TenantId='+@id;
	
	EXEC sp_executesql @q;print @q;print CONCAT('PageControls Updates : ',@@ROWCOUNT);

	UPDATE @result set UpdatedPageControls=@@ROWCOUNT;

		select @res=CONCAT(
			'{"SourceDB":"',SourceDB,'"',
			',"TargetDB":"',TargetDB,'"',
			',"AddedPages":',AddedPages,
			',"UpdatedPages":',UpdatedPages,
			',"AddedPageControls":',AddedPageControls,
			',"UpdatedPageControls":',UpdatedPageControls,
			'}')
		from @result;

	SELECT * FROM @result;
END;




GO
/****** Object:  StoredProcedure [dbo].[SyncTenants]    Script Date: 4/7/2020 6:20:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SyncTenants]
	-- Add the parameters for the stored procedure here
	 @sourceTenant bigint,
	@targetTenant bigint,
	@res varchar(max)=null OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;

	DECLARE @count int=0;
	DECLARE @message nvarchar(max);
	DECLARE @result TABLE(
		SourceTenant varchar(150),
		TargetTenant varchar(150),
		AddedPages int,
		UpdatedPages int,
		AddedPageControls int,
		UpdatedPageControls int,
		NavigationPages int
	);

	INSERT @result 
		select
			(SELECT Code FROM Tenants WHERE Id=@sourceTenant), 
			(SELECT Code FROM Tenants WHERE Id=@targetTenant), 
			0,0,0,0,0;

	begin transaction;
	INSERT INTO dbo.Pages
	([Id]
		,[Name]
		,[ViewPath]
		,[ViewParams]
		,[ResourceId]
		,[Layout]
		,[PrivilegeType]
		,[ResourceActionId]
		,[SpecialPermission]
		,[SourceCollectionId]
		,[DefaultAccessibility]
		,[PageCategoryId]
		,[RouteParameters]
		,[TenantId]
		,[DomainId]
		,[Apps]
		,[HasRoute]
		,[CanEmbed]
		,[CreatedOn]
		,[UpdatedOn]
		,[CreatedBy]
		,[UpdatedBy])
	SELECT 
		dbo.GenerateId(GETDATE(),ROW_NUMBER() OVER (ORDER BY Ps.Id))
		,[Name]
		,[ViewPath]
		,[ViewParams]
		,[ResourceId]
		,[Layout]
		,[PrivilegeType]
		,[ResourceActionId]
		,[SpecialPermission]
		,[SourceCollectionId]
		,[DefaultAccessibility]
		,[PageCategoryId]
		,[RouteParameters]
		,@targetTenant
		,[DomainId]
		,[Apps]
		,[HasRoute]
		,[CanEmbed]
		,GETDATE()
		,GETDATE()
		,1
		,1
	FROM 
		dbo.Pages Ps
	WHERE 
		TenantId=@sourceTenant AND 
		Ps.ViewPath NOT IN (
			SELECT Pages.ViewPath 
			FROM Pages
			WHERE TenantId=@targetTenant
		);

	UPDATE @result set AddedPages=@@ROWCOUNT;
	print 'Create pages';
	Commit;
-----------------------------------------------------------------------------------------------

	UPDATE targetPage
	SET
		Apps=tx.Apps,
		ViewParams=(CASE 
						WHEN targetPage.Presistant=1 THEN targetPage.ViewParams 
						ELSE tx.ViewParams 
					END),
		Layout=tx.Layout,
		PrivilegeType=tx.PrivilegeType,
		ResourceActionId=tx.ResourceActionId,
		SpecialPermission=tx.SpecialPermission,
		SourceCollectionId=tx.SourceCollectionId,
		RouteParameters=tx.RouteParameters,
		DefaultAccessibility=tx.DefaultAccessibility,
		PageCategoryId=tx.PageCategoryId,
		ResourceId=tx.ResourceId,
		CanEmbed=tx.CanEmbed,
		HasRoute=tx.HasRoute
	FROM Pages targetPage
		join (
			SELECT *
			FROM Pages 
			WHERE TenantId=@sourceTenant
		) tx ON tx.ViewPath=targetPage.ViewPath
	WHERE 
		targetPage.TenantId=@targetTenant;

	UPDATE @result set UpdatedPages=@@ROWCOUNT;
	print 'Update pages';
	
-----------------------------------------------------------------------------------------------

	INSERT INTO [dbo].[PageControls]
			   ([Id]
			   ,[ControlId]
			   ,[PageId]
			   ,[Accessability]
			   ,[SourceCollectionId]
			   ,[CreatedOn]
			   ,[UPDATEdOn]
			   ,[CreatedBy]
			   ,[UPDATEdBy])
		 SELECT
			(dbo.GenerateId(GETDATE(),row_number() OVER ( ORDER BY PageControls.Id))) Id
			,ControlId
			,targetPages.Id
			,Accessability
			,PageControls.SourceCollectionId
			,GETDATE() CreatedOn
			,GETDATE() UPDATEdOn
			,1 CreatedBy
			,1 UPDATEdBy
		FROM PageControls
			join Pages srcPages on srcPages.Id=PageControls.PageId
			join Pages targetPages on 
				targetPages.ViewPath=srcPages.ViewPath AND
				targetPages.TenantId=@targetTenant
		WHERE 
			srcPages.TenantId=@sourceTenant AND
			NOT EXISTS (
				SELECT 1
				FROM PageControls tarControls
					join Pages tarPages on tarPages.Id=tarControls.PageId
				WHERE 
					tarControls.ControlId=PageControls.ControlId AND
					tarPages.ViewPath=srcPages.ViewPath AND
					tarPages.TenantId=@targetTenant 
			);

	UPDATE @result set AddedPageControls=@@ROWCOUNT;
	print 'Create controls';

-----------------------------------------------------------------------------------------------

	UPDATE PageControls
	SET 
		Accessability=Existing.Accessability,
		SourceCollectionId=Existing.SourceCollectionId
	FROM PageControls
		join Controls on Controls.Id=PageControls.ControlId
		join Pages on Pages.Id=PageControls.PageId
		join (
			SELECT 
				ViewPath,
				Identifier,
				Accessability,
				PageControls.SourceCollectionId,
				PageControls.UpdatedOn
			FROM PageControls
				join Controls on Controls.Id=PageControls.ControlId
				join Pages on Pages.Id=PageControls.PageId
			WHERE
				TenantId=@sourceTenant
		) as Existing on 
			Pages.ViewPath=Existing.ViewPath AND
			Controls.Identifier=Existing.Identifier AND
			PageControls.Persistent = 0
	WHERE 
		Pages.TenantId=@targetTenant;

	UPDATE @result set UpdatedPageControls=@@ROWCOUNT;
	print 'Update controls';

-----------------------------------------------------------------------------------------------

	INSERT INTO NavigationPages (Id,PageId,DisplayOrder,NavigationGroupId,CreatedOn,CreatedBy,UpdatedOn,UpdatedBy)
	SELECT 
		dbo.GenerateId(GETDATE(),ROW_NUMBER() over (order by srcNavs.Id))
		,tarPages.[Id]
		,0
		,[NavigationGroupId]
		,GETDATE()
		,1
		,GETDATE()
		,1
	FROM [dbo].[NavigationPages] srcNavs
		join [dbo].Pages srcPages on srcPages.Id = srcNavs.PageId
		join [dbo].Pages tarPages on tarPages.ViewPath = srcPages.ViewPath AND tarPages.TenantId=@targetTenant
	WHERE
		srcPages.TenantId = @sourceTenant AND
		not exists (
			SELECT 1
			FROM [dbo].[NavigationPages] tarNavs
			WHERE 
				tarNavs.PageId=tarPages.Id
		);

	UPDATE @result set NavigationPages=@@ROWCOUNT;
	print 'Update controls';

	select @res=CONCAT(
		'{"SourceTenant":"',SourceTenant,'"',
		',"TargetTenant":"',TargetTenant,'"',
		',"AddedPages":',AddedPages,
		',"UpdatedPages":',UpdatedPages,
		',"AddedPageControls":',AddedPageControls,
		',"UpdatedPageControls":',UpdatedPageControls,
		',"NavigationPages":',NavigationPages,
		'}')
	from @result;

	SELECT * FROM @result;
END

GO
/****** Object:  Trigger [dbo].[Domains_Chain_TR]    Script Date: 4/7/2020 6:20:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE trigger [dbo].[Domains_Chain_TR]
    ON [dbo].[Domains]
    AFTER INSERT, UPDATE 
	
	AS
		SET NOCOUNT ON;
		DECLARE @i int=1;
        DECLARE @id BIGINT;
        DECLARE @chain NVARCHAR(max);
		DECLARE @name NVARCHAR(max);
		DECLARE @nameChain NVARCHAR(max);
		DECLARE @parentId bigint;

		while(@i<=(select count(*) from inserted))
		begin
			 SELECT 
				@id=Id,
				@parentId=ParentId,
				@name=Name
			FROM (
				select ROW_NUMBER() over (order by Id) i,*
				from inserted
			) TX
			WHERE i=@i;

			SET @i=@i+1;

			IF(@parentId IS NULL)
				SELECT 
					@chain='|'+CONVERT(NVARCHAR(25),@id)+'|',
					@nameChain='/'+@name+'/'
			ELSE
				SELECT 
					@chain=Chain+CONVERT(NVARCHAR(25),@id)+'|',
					@nameChain=NameChain+@name+'/'
				FROM Domains WHERE Id=@parentId;

			UPDATE Domains 
			SET 
				Chain=@chain,
				NameChain=@nameChain
			WHERE Id=@id;
		end;



GO
USE [master]
GO
ALTER DATABASE [Configurator.Config] SET  READ_WRITE 
GO
