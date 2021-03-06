USE [GWSTAGINGR1]
GO
/****** Object:  StoredProcedure [dbo].[spPMSUploadAdmin_GetClaimTransactions]    Script Date: 7/29/2015 10:11:47 AM ******/
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
			namClaimantNumber,
			trxTransactionDate,
			mjpAlphaMajorPeril,
			rvcReserveCategory,
			lccAlphaCode,
			tcdCode,
			trxAmount,
			rsvRiskUnit,
			rsvInlandMarineUnit,
			payTransactionCategory,
			rcvTransactionCategory,
			rsvAIA12,
			rsvAIA34,
			rsvAIA56,
			rsvInsuranceLine,
			rsvRiskUnitSequence,
			rsvRiskGroup,
			rsvRiskGroupSequence,
			polUnitLocationNumber,
			rsvLocationNumber,
			rsvMPSequence
	FROM	PMSFinancialUpload
	WHERE	Retired = 0
			AND clmClaimNumber LIKE @clmClaimNumber
	GROUP BY CC_TransactionID,
			clmClaimNumber,
			namClaimantNumber,
			trxTransactionDate,
			mjpAlphaMajorPeril,
			rvcReserveCategory,
			lccAlphaCode,
			tcdCode,
			trxAmount,
			rsvRiskUnit,
			rsvInlandMarineUnit,
			payTransactionCategory,
			rcvTransactionCategory,
			rsvAIA12,
			rsvAIA34,
			rsvAIA56,
			rsvInsuranceLine,
			rsvRiskUnitSequence,
			rsvRiskGroup,
			rsvRiskGroupSequence,
			polUnitLocationNumber,
			rsvLocationNumber,
			rsvMPSequence,
			PMSPrimaryKey
	ORDER BY namClaimantNumber,
			trxTransactionDate
END