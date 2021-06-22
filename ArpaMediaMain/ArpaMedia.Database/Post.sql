CREATE TABLE [dbo].[Post](
[Id] [int] NOT NULL IDENTITY(1,1),
[Title] nvarchar(255) NOT NULL,
[Description] nvarchar(4000) NOT NULL,
[PublishedDate] datetime NOT NULL,
[Created] [datetime2](7) NOT NULL,
[LastModified] [datetime2](7) NOT NULL,
[PostImage] nvarchar(2048) NULL ,
[AudioFile] nvarchar(2048) NULL ,
[VideoFile] nvarchar(2048) NULL
CONSTRAINT [PRK_Post] PRIMARY KEY CLUSTERED
(
[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
