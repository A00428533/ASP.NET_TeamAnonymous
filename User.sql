USE [HotelDatabase]
GO

/****** Object:  Table [dbo].[User]    Script Date: 2018-11-29 6:27:33 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[First Name] [varchar](50) NULL,
	[Last Name] [varchar](50) NULL,
	[Street Number] [int] NULL,
	[City] [varchar](50) NULL,
	[Province/State] [varchar](50) NULL,
	[Country] [varchar](50) NULL,
	[Postal Code] [varchar](50) NULL,
	[Phone Number] [varchar](50) NULL,
	[E-mail Address] [varchar](50) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


