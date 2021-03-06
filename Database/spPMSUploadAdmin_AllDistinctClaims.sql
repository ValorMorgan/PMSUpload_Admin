USE [GWSTAGINGR1]
GO
/****** Object:  StoredProcedure [dbo].[spPMSUploadAdmin_AllDistinctClaims]    Script Date: 7/27/2015 8:32:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[spPMSUploadAdmin_AllDistinctClaims] (
		@search		varchar(30)
)AS

/*=============================================================
		Procedure: spPMSUploadAdmin_AllDistinctClaims

	Gets a list of every distinct claim in PMSFinancialUpload
that match the search to populate the application with an
auto-completeion list.

===============================================================
- Description						Date		Developer
===============================================================
- Created							07/24/2015	Joshua Morgan
=============================================================*/

	BEGIN
		SET NOCOUNT ON;

		;WITH processTable (
			claimNumber
		) AS (
			SELECT DISTINCT
					clmClaimNumber
			FROM	PMSFinancialUpload
			WHERE	clmClaimNumber LIKE (CASE WHEN LEN(@search) >= 6
											THEN CONCAT(LEFT(@search, 6), '%', SUBSTRING(@search, 7, (LEN(@search) - 6)), '%')
											ELSE CASE WHEN LEN(@search) BETWEEN 3 AND 5
												THEN CONCAT('%', LEFT(@search, 2), '%', SUBSTRING(@search, 3, (LEN(@search) - 2)), '%')
												ELSE CONCAT('%', @search, '%')
											END
										END)
				 OR RIGHT(clmClaimNumber, 10) LIKE CONCAT('%', @search)
		)

		SELECT TOP 30
			claimNumber
		FROM processTable
		ORDER BY claimNumber;
	END;