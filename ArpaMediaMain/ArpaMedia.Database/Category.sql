CREATE TABLE [dbo].[Category](
	[Id] [int] NOT NULL IDENTITY(1,1),
	[Name] [nvarchar](100) NOT NULL,
	[DisplayOrder] [int] NOT NULL,
	[IsMenuItem] [bit] NOT NULL,
	[IsCalendarItem] [bit] NOT NULL,
	[IsHomeItem] [bit] NOT NULL,
	[LanguageId] [int] NOT NULL,
	[Created] [datetime2](7) NOT NULL,
	[LastModified] [datetime2](7) NOT NULL,
	[ParentCategoryId] [int] NULL,
 CONSTRAINT [PRK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Category] ADD  CONSTRAINT [FRK_Category_Language] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Language] ([Id])
GO

ALTER TABLE [dbo].[Category] ADD  CONSTRAINT [FRK_Category_Category] FOREIGN KEY([ParentCategoryId])
REFERENCES [dbo].[Category] ([Id])
GO

ALTER TABLE [dbo].[Category] CHECK CONSTRAINT [FRK_Category_Category]
GO

ALTER TABLE [dbo].[Category]
  ADD CONSTRAINT UNQ_ParentCategory_Name UNIQUE(ParentCategoryId, Name);


