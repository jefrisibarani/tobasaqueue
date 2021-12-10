USE [antri]
GO

CREATE TABLE [dbo].[logins](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [char](50) NOT NULL,
	[password] [char](64) NOT NULL,
	[expired] [datetime] NOT NULL,
	[active] [bit] NOT NULL,
 CONSTRAINT [PK_logins] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UQ_logins] UNIQUE NONCLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[logins] ADD  CONSTRAINT [DF_logins_expired]  DEFAULT (dateadd(year,(1),getdate())) FOR [expired]
GO

ALTER TABLE [dbo].[logins] ADD  CONSTRAINT [DF_logins_active]  DEFAULT ((1)) FOR [active]
GO
