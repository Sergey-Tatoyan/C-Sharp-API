﻿CREATE TABLE [User](
	[UserId] [int] NOT NULL IDENTITY(1,1),
	[Email] [nvarchar](100) NOT NULL,
	[PasswordSalt] [binary](20) NOT NULL,
	[PasswordKey] [binary](20) NOT NULL,
	[Created] [datetime2](7) NOT NULL,
	[LastModified] [datetime2](7) NOT NULL,
 CONSTRAINT [PRK_tbUser] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UNQ_tbUser_Email] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
