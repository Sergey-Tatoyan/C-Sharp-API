CREATE TABLE [dbo].[Banner](
[Id] [int] NOT NULL IDENTITY(1,1),
[Title] nvarchar(255) NOT NULL,
[SmallText] nvarchar(60) NULL,
[ImageUrl] nvarchar(2048) NULL, 
[DisplayOrder] [int] NOT NULL,
[BannerTypeId] [int] NOT NULL

CONSTRAINT [PRK_Banner] PRIMARY KEY CLUSTERED
(
[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Banner] ADD  CONSTRAINT [FRK_Banner_BannerType] FOREIGN KEY([BannerTypeId])
REFERENCES [dbo].[BannerType] ([Id])
GO
