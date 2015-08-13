USE [AttikaDb]
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[dbo].[Article]')
AND OBJECTPROPERTY(id, N'IsUserTable') = 1)

BEGIN
	CREATE TABLE [dbo].[Article](
		[Id] [uniqueidentifier] NOT NULL,
		[Created] [datetime] NULL,
		[Description] [nvarchar](255) NULL,
		[Text] [nvarchar](200) NULL,
		[Title] [nvarchar](100) NULL,
	PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	USE [AttikaDb]
	SET ANSI_NULLS ON
	SET QUOTED_IDENTIFIER ON
END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[dbo].[Comment]')
AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN

CREATE TABLE [dbo].[Comment](
	[Id] [uniqueidentifier] NOT NULL,
	[ArticleId] [uniqueidentifier] NOT NULL,
	[Created] [datetime] NULL,
	[Text] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK1B0AF5A7FD0FF40D] FOREIGN KEY([ArticleId])
REFERENCES [dbo].[Article] ([Id])
ON DELETE CASCADE
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK1B0AF5A7FD0FF40D]
END