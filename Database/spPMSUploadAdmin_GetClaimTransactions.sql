USE [GWSTAGINGR1]
GO
/****** Object:  StoredProcedure [dbo].[spPMSUploadAdmin_GetClaimTransactions]    Script Date: 7/24/2015 8:25:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[spPMSUploadAdmin_GetClaimTransactions] (
	@clmClaimNumber		varchar(15)
) AS
/*=================================================================
	Procedure: spPMSUploadAdmin_GetClaimTransactions

	Gets all non-retired claims from the PMS Financial table where
the specificed claim number matches.

NOTE: Any changes to the select list here need to be added/removed
from the saving stored procedure.

===================================================================
- Description					Date		Developer
===================================================================
- Created						07/16/2015	Joshua Morgan
=================================================================*/
BEGIN
	SET NOCOUNT ON;

	SELECT	PMSPrimaryKey,
			clmClaimNumber,
			tcdCode,
			rsvMPSequence,
			mjpAlphaMajorPeril,
			rvcReserveCategory,
			lccAlphaCode,
			payTransactionCategory,
			rcvTransactionCategory,
			rsvAIA12,
			rsvAIA34,
			rsvAIA56,
			rsvInsuranceLine,
			rsvRiskUnit,
			polUnitLocationNumber,
			rsvLocationNumber,
			rsvInlandMarineUnit,
			trxAmount
	FROM	PMSFinancialUpload
	WHERE	Retired = 0
			AND clmClaimNumber LIKE @clmClaimNumber
	GROUP BY CC_TransactionID,
			clmClaimNumber,
			tcdCode,
			rsvMPSequence,
			mjpAlphaMajorPeril,
			rvcReserveCategory,
			lccAlphaCode,
			payTransactionCategory,
			rcvTransactionCategory,
			rsvAIA12,
			rsvAIA34,
			rsvAIA56,
			rsvInsuranceLine,
			rsvRiskUnit,
			polUnitLocationNumber,
			rsvLocationNumber,
			rsvInlandMarineUnit,
			trxAmount,
			PMSPrimaryKey
	ORDER BY CC_TransactionID,
			clmClaimNumber,
			tcdCode
END