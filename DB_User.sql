USE [Projects]
GO

IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N'appcore_user')
DROP USER [appcore_user]
GO

USE [Projects]
GO

CREATE USER [appcore_user] FOR LOGIN [appcore_user] WITH DEFAULT_SCHEMA=[dbo]
GO

