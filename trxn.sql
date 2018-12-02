USE [HotelDatabase]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Transaction](
	[Tid] [char] (16) NOT NULL,
	[Uid] [int] NOT NULL,
	[Rid] [int] NOT NULL,
	[CardNumber] [varchar](45) NOT NULL,
	[NameOnCard] [varchar](256) NOT NULL,
	[UnitPrice] [decimal](10,0) NOT NULL,
	[TotalPrice] [decimal](10,0) NOT NULL,
	[CardCategory] [tinyint] NOT NULL,
	[CardCatergoryName] [varchar](50) NOT NULL,
	[ExpDate] [varchar](16) NOT NULL,
 CONSTRAINT [Tid] PRIMARY KEY(Tid),
 CONSTRAINT FK_Trxn_Res FOREIGN KEY(Rid) REFERENCES reservation_table(Reservation_ID),
 CONSTRAINT FK_Trxn_User FOREIGN KEY(Uid) REFERENCES [User](UserId))
GO



