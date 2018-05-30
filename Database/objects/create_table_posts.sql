USE [antri]
GO

CREATE TABLE [dbo].[posts](
	[name] [nchar](32) NOT NULL,
	[keterangan] [varchar](255) NULL,
	[numberprefix] [char](2) NULL,
 CONSTRAINT [PK_posts] PRIMARY KEY CLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
