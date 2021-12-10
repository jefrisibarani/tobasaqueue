USE [antri]
GO

CREATE TABLE [dbo].[stations](
	[name] [nchar](32) NOT NULL,
	[post] [nchar](32) NOT NULL,
	[keterangan] [varchar](255) NULL,
	[canlogin] [bit] NOT NULL,
 CONSTRAINT [PK_stations] PRIMARY KEY CLUSTERED 
(
	[name] ASC,
	[post] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[stations]  WITH CHECK ADD  CONSTRAINT [FK_stations_posts] FOREIGN KEY([post])
REFERENCES [dbo].[posts] ([name])
GO

ALTER TABLE [dbo].[stations] CHECK CONSTRAINT [FK_stations_posts]
GO

ALTER TABLE [dbo].[stations] ADD  CONSTRAINT [DF_stations_canlogin]  DEFAULT ((1)) FOR [canlogin]
GO
