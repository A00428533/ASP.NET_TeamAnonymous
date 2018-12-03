CREATE TABLE `hotel_transaction` (
  `ID` char(16) NOT NULL,
  `CardNumber` varchar(45) NOT NULL,
  `NameOnCard` varchar(256) NOT NULL,
  `CardCategory` tinyint(4) NOT NULL DEFAULT '0' COMMENT '0:Unknown,1:MasterCard,2:Visa,3.American Express',
  `UnitPrice` decimal(10,0) NOT NULL,
  `Quantity` int(11) NOT NULL,
  `TotalPrice` decimal(10,0) NOT NULL,
  `ExpDate` varchar(16) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `CreatedBy` varchar(45) NOT NULL,
  `CreditCardTypeName` varchar(50) DEFAULT NULL COMMENT '0:Unknown,1:MasterCard,2:Visa,3.American Express',
  `ReservationId` int(11) NOT NULL,
  `UserId` int(11) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB；