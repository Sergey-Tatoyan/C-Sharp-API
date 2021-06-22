﻿CREATE TABLE [dbo].[Subscribed]
(
	[Id] INT NOT NULL IDENTITY(1,1),
	[Email] [nvarchar](100) NOT NULL
	CONSTRAINT [PRK_Subscribed] PRIMARY KEY CLUSTERED
(
[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
