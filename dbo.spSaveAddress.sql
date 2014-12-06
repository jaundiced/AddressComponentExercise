USE [Projects]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spSaveAddress]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[spSaveAddress]
GO

USE [Projects]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Description:	Saves Address 
-- =============================================
CREATE PROCEDURE [dbo].[spSaveAddress]
	@addressId INT = 0,
	@name VARCHAR(150),
	@company VARCHAR(150) = NULL,
	@address1 VARCHAR(150),
	@address2 VARCHAR(150) = NULL,
	@city VARCHAR(100),
	@stateId INT,
	@zip VARCHAR(50)
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION;
			BEGIN
				IF(@addressId > 0)
				BEGIN
					UPDATE	[dbo].[AppCore_Address]
					  SET	Name = @name, 
							Company = @company, 
							AddressLine1 = @address1, 
							AddressLine2 = @address2, 
							City = @city, 
							StateId = @stateId, 
							Zip = @zip
					 WHERE	[Id] = @addressId;
				END
				
				IF(@addressId < 1)
				BEGIN
					INSERT [dbo].[AppCore_Address](Name, Company, AddressLine1, AddressLine2, City, StateId, Zip)
					VALUES(@name,@company,@address1,@address2,@city,@stateId,@zip);
				END
			END
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
		ROLLBACK TRANSACTION

		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT 
			@ErrorMessage = ERROR_MESSAGE(),
			@ErrorSeverity = ERROR_SEVERITY(),
			@ErrorState = ERROR_STATE();

		RAISERROR (@ErrorMessage,
				   @ErrorSeverity,
				   @ErrorState);
	END CATCH
END

GO


