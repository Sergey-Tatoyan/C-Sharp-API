CREATE TABLE [dbo].[CategoryPost]
(
[Id] INT NOT NULL IDENTITY(1,1),
[CategoryId] [int] NOT NULL,
[PostId] [int] NOT NULL,

CONSTRAINT [PRK_CategoryPost] PRIMARY KEY CLUSTERED
(
[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CategoryPost] ADD  CONSTRAINT [FRK_CategoryPost_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([Id])
GO

ALTER TABLE [dbo].[CategoryPost] CHECK CONSTRAINT [FRK_CategoryPost_Category]
GO

ALTER TABLE [dbo].[CategoryPost] ADD  CONSTRAINT [FRK_CategoryPost_Post] FOREIGN KEY([PostId])
REFERENCES [dbo].[Post] ([Id])
GO

ALTER TABLE [dbo].[CategoryPost] CHECK CONSTRAINT [FRK_CategoryPost_Post]
GO
