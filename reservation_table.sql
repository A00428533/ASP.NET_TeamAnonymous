USE [test]
GO

/****** Object:  Table [dbo].[reservation_table]    Script Date: 2018-11-29 5:31:31 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[reservation_table](
	[Reservation_ID] [int] IDENTITY(1,1) NOT NULL,
	[Check_in_date] [date] NOT NULL,
	[Check_out_date] [date] NOT NULL,
	[No_of_rooms] [int] NOT NULL,
	[No_of_guests] [int] NOT NULL,
 CONSTRAINT [PK_reservation_table] PRIMARY KEY CLUSTERED 
(
	[Reservation_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
