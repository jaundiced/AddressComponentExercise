USE [master]
GO

IF  EXISTS (SELECT * FROM sys.server_principals WHERE name = N'appcore_user')
DROP LOGIN [appcore_user]
GO

CREATE LOGIN [appcore_user] WITH PASSWORD=N'appcore1234', DEFAULT_DATABASE=[Projects], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO



USE [Projects]
GO

IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N'appcore_user')
DROP USER [appcore_user]
GO

USE [Projects]
GO

CREATE USER [appcore_user] FOR LOGIN [appcore_user] WITH DEFAULT_SCHEMA=[dbo]
GO

