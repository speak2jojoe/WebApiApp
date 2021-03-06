SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[firstname] [varchar](max) NOT NULL,
	[lastname] [varchar](max) NOT NULL,
	[gender] [varchar](50) NOT NULL,
	[date_of_birth] [datetime] NOT NULL,
	[date_created] [datetime] NOT NULL,
	[date_updated] [datetime] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auth]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Auth](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](max) NOT NULL,
	[password] [varchar](max) NOT NULL,
 CONSTRAINT [PK_Auth] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
