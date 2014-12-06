
USE [Projects]
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('AppCore_Address') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE AppCore_Address
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('AppCore_State') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE AppCore_State
GO


CREATE TABLE AppCore_State ( 
	Id int identity(1,1)  NOT NULL,
	Abbreviation nvarchar(50) NOT NULL,
	Name nvarchar(100) NOT NULL
)
GO

ALTER TABLE AppCore_State ADD CONSTRAINT PK_AppCore_State 
	PRIMARY KEY CLUSTERED (Id)
GO



CREATE TABLE AppCore_Address ( 
	Id int identity(1,1)  NOT NULL,
	Name nvarchar(150) NOT NULL,
	Company nvarchar(150),
	AddressLine1 nvarchar(150) NOT NULL,
	AddressLine2 nvarchar(150),
	City nvarchar(100) NOT NULL,
	StateId int NOT NULL,
	Zip nvarchar(50) NOT NULL
)
GO

ALTER TABLE AppCore_Address ADD CONSTRAINT PK_AppCore_Address 
	PRIMARY KEY CLUSTERED (Id)
GO

ALTER TABLE AppCore_Address ADD CONSTRAINT FK_AppCore_Address__AppCore_State 
	FOREIGN KEY (StateId) REFERENCES AppCore_State (Id)
GO



set identity_insert AppCore_State  on
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (1, 'AK',	'Alaska');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (2, 'AL','Alabama');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (3,'AR',	'Arkansas');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (4,'AZ',	'Arizona');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (5,'CA',	'California');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (6,'CO',	'Colorado');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (7,'CT',	'Connecticut');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (8,'DC',	'District of Columbia');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (9,'DE',	'Delaware');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (10,'FL',	'Florida');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (11,'GA',	'Georgia');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (12,'HI',	'Hawaii');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (13,'IA',	'Iowa');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (14,'ID',	'Idaho');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (15,'IL',	'Illinois');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (16,'IN',	'Indiana');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (17,'KS',	'Kansas');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (18,'KY',	'Kentucky');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (19,'LA',	'Louisiana');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (20,'MA',	'Massachusetts');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (21,'MD',	'Maryland');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (22,'ME',	'Maine');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (23,'MI',	'Michigan');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (24, 'MN',	'Minnesota');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (25, 'MO',	'Missouri');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (26, 'MS',	'Mississippi');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (27, 'MT',	'Montana');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (28, 'NC',	'North Carolina');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (29, 'ND',	'North Dakota');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (30, 'NE',	'Nebraska');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (31, 'NH',	'New Hampshire');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (32, 'NJ',	'New Jersey');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (33, 'NM',	'New Mexico');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (34, 'NV',	'Nevada');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (35, 'NY',	'New York');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (36, 'OH',	'Ohio');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (37, 'OK',	'Oklahoma');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (38, 'OR',	'Oregon');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (39, 'PA',	'Pennsylvania');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (40, 'RI',	'Rhode Island');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (41, 'SC',	'South Carolina');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (42, 'SD',	'South Dakota');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (43, 'TN',	'Tennessee');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (44, 'TX',	'Texas');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (45, 'UT',	'Utah');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (46, 'VA',	'Virginia');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (47, 'VT',	'Vermont');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (48, 'WA',	'Washington');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (49, 'WI',	'Wisconsin');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (50, 'WV',	'West Virginia');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (51, 'WY',	'Wyoming');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (52, 'AB',	'Alberta');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (53, 'BC',	'British Columbia');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (54, 'MB',	'Manitoba');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (55, 'NF',	'Newfoundland');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (56, 'NK',	'New Brunswick');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (57, 'NS',	'Nova Scotia');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (58, 'ON',	'Ontario');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (59, 'PE',	'Prince Edward Island');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (60, 'PQ',	'Quebec');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (61, 'SN',	'Saskatchewan');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (62, 'NT',	'Norhtwest Territories');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (63, 'YT',	'Yukon Territory');
INSERT INTO AppCore_State ([Id],[Abbreviation],[Name]) VALUES (64, 'NU',	'Nunavut');
set identity_insert AppCore_State  off

-- Add 3 address records
INSERT INTO [Projects].[dbo].[AppCore_Address] ([Name] ,[Company] ,[AddressLine1] ,[AddressLine2] ,[City] ,[StateId] ,[Zip])
     VALUES ('Charlie Kelly' ,'Paddy''s Pub','228 Race St' ,null,'Philadelphia', 39,'19106');

INSERT INTO [Projects].[dbo].[AppCore_Address] ([Name] ,[Company] ,[AddressLine1] ,[AddressLine2] ,[City] ,[StateId] ,[Zip])
     VALUES ('Dennis Reynolds','Paddy''s Pub','228 Race St',null ,'Philadelphia', 39 ,'19106');
	 
INSERT INTO [Projects].[dbo].[AppCore_Address] ([Name] ,[Company] ,[AddressLine1] ,[AddressLine2] ,[City] ,[StateId] ,[Zip])
     VALUES ('Mac Macdonald','Paddy''s Pub','228 Race St',null ,'Philadelphia',39,'19106');
