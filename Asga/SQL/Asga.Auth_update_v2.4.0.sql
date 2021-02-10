alter table Auth.Users add AppId bigint null;
GO
update Auth.Users set AppId = ta.TenantAppId
from Auth.Users join 
	Auth.TenantAppUsers ta on Users.Id=ta.UserId;

GO

drop table Auth.TenantAppUsers;

GO

CREATE TABLE [Auth].[Apps](
	[Id] [bigint] NOT NULL,
	[Name] [varchar](150) NULL,
	[DisplayName] [nvarchar](150) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [bigint] NULL,
 CONSTRAINT [PK_TenantApps] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

insert into Auth.Apps 
	(	[Id],[Name],[DisplayName],[CreatedOn],[CreatedBy],[UpdatedOn],[UpdatedBy])
select	[Id],[Name],[DisplayName],[CreatedOn],[CreatedBy],[UpdatedOn],[UpdatedBy]
from Auth.TenantApps;

GO

alter table Auth.Users add constraint FK_Users_Apps foreign key (AppId) references Auth.Apps (Id);

GO

drop table Auth.TenantApps;
drop table Auth.TenantDomains;
drop table Auth.UserParties;
drop table Auth.DefaultRoles;