USE [antri]
GO

CREATE TABLE [dbo].[runningtexts](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[station_name] [nchar](32) NOT NULL,
	[sticky] [bit] NOT NULL,
	[active] [bit] NOT NULL,
	[running_text] [varchar](255) NOT NULL,
 CONSTRAINT [PK_runningtexts] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[runningtexts] ADD  CONSTRAINT [DF_runningtexts_sticky]  DEFAULT ((0)) FOR [sticky]
GO

ALTER TABLE [dbo].[runningtexts] ADD  CONSTRAINT [DF_runningtexts_active]  DEFAULT ((1)) FOR [active]
GO
