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

ALTER TABLE [dbo].[CustomFields]  WITH CHECK ADD  CONSTRAINT [FK_CustomFields_Pages] FOREIGN KEY([PageId])
REFERENCES [dbo].[Pages] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

alter table Pages Add ParentId bigint null;
alter table Tenants add ParentId bigint null;

GO

update Pages 
	set ParentId=(select Id from Pages pp where pp.TenantId=1 AND pp.ViewPath=Pages.ViewPath)
where TenantId!=1;

select * from Pages where TenantId=1;