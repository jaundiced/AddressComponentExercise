USE [Projects]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spGetAddress]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[spGetAddress]
GO

USE [Projects]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Description:	Retrieves Address By ID (int) 
-- =============================================
CREATE PROCEDURE [dbo].[spGetAddress]
	
	@addressID INT
AS
BEGIN
	
	SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;   

    
	SELECT	a.[Id], a.Name, a.Company, a.AddressLine1, a.AddressLine2, a.City, a.StateId, a.Zip,
			s.Abbreviation, s.Name
	  FROM	[dbo].[AppCore_Address] a INNER JOIN [dbo].AppCore_State s
	    ON	s.Id = a.StateId
	WHERE a.[Id]  = @addressID;
END

GO


