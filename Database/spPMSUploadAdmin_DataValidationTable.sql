USE [GWSTAGINGR1]
GO
/****** Object:  StoredProcedure [dbo].[spPMSUploadAdmin_DataValidationTable]    Script Date: 7/24/2015 12:35:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[spPMSUploadAdmin_DataValidationTable]
AS

/*=========================================================================
		Procedure: spPMSUploadAdmin_DataValidationTable

	Retrieves every column of type char or varchar from PMSFinancialUpload
along with the column length and if that length is absolute (ie char) or
not (ie varchar).
===========================================================================
- Description							Date		Developer
===========================================================================
- Created								07/24/2015	Joshua Morgan
=========================================================================*/

	BEGIN
		SET NOCOUNT ON;
		SELECT
			COLUMN_NAME AS 'columnName',
			CHARACTER_MAXIMUM_LENGTH AS 'columnLength',
			CASE WHEN DATA_TYPE LIKE 'char'
				THEN 1
				ELSE 0
			END AS 'absoluteLength'
		FROM INFORMATION_SCHEMA.COLUMNS
		WHERE TABLE_NAME = N'PMSFinancialUpload'
		  AND CHARACTER_MAXIMUM_LENGTH IS NOT NULL
		ORDER BY
			COLUMN_NAME;
	END;