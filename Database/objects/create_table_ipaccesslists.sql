USE [antri]
GO

CREATE TABLE [dbo].[ipaccesslists](
	[ipaddress] [nchar](15) NOT NULL,
	[allowed] [bit] NOT NULL,
	[keterangan] [varchar](50) NULL,
 CONSTRAINT [PK_ipaccesslist] PRIMARY KEY CLUSTERED 
(
	[ipaddress] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO