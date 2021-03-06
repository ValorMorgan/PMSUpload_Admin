USE [GWSTAGINGR1]
GO
/****** Object:  StoredProcedure [dbo].[spPMSUploadAdmin_SaveClaimTransaction]    Script Date: 7/29/2015 10:16:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[spPMSUploadAdmin_SaveClaimTransaction]
	@CurrentUser			varchar(30),
	@PMSPrimaryKey          int,
	@clmClaimNumber         varchar(15),
	@namClaimantNumber		int,
	@trxTransactionDate		datetime,
	@mjpAlphaMajorPeril     varchar(3),
	@rvcReserveCategory     varchar(1),
	@lccAlphaCode           varchar(2),
	@tcdCode				varchar(2),
	@trxAmount              money,
	@rsvRiskUnit            varchar(7),
	@rsvInlandMarineUnit    varchar(8),
	@payTransactionCategory char(2),
	@rcvTransactionCategory char(2),
	@rsvAIA12               varchar(2),
	@rsvAIA34               varchar(2),
	@rsvAIA56               varchar(2),
	@rsvInsuranceLine       varchar(2),
	@rsvRiskUnitSequence	char(2),
	@rsvRiskGroup			varchar(3),
	@rsvRiskGroupSequence	varchar(3),
	@polUnitLocationNumber  varchar(4),
	@rsvLocationNumber      varchar(4),
	@rsvMPSequence          char(2)
AS

/*=======================================================================
		Procedure: spPMSUploadAdmin_SaveClaimTransaction

	Takes in the set of parameters from PMSUpload_Admin and matches
the primary key and the claim number with PMSFinancialUpload to update
the data here.

NOTE: Any changes to spPMSUploadAdmin_GetClaimTransactions need to
be reflected in this procedure (i.e. order change of parameters or new
parameters).  Areas for this are the parameters above and where the
comment "Update the new record" exists.

=========================================================================
- Description								Date		Developer
=========================================================================
- Created									07/23/2015	Joshua Morgan
=======================================================================*/

	BEGIN TRY
		BEGIN TRANSACTION;
		SET NOCOUNT ON;

		DECLARE @ModifiedDate datetime;
		SET		@ModifiedDate = GETDATE();

		/*===============================================================
								PMSFinancialUpload
		===============================================================*/

		-- Perform check for newer data exists
		IF (SELECT TOP 1 PMSPrimaryKey
			FROM PMSFinancialUpload
			WHERE clmClaimNumber = @clmClaimNumber
				AND ABS(trxAmount) = ABS(@trxAmount)
			ORDER BY ModifiedDate DESC) > @PMSPrimaryKey
			BEGIN;
			THROW 50001, 'Another user recently updated this record and your data is out of date.  Please use the refresh button and try again.', 16;
			END;

		-- Insert a new record (had to select all but PrimaryKey)
		INSERT INTO PMSFinancialUpload
		SELECT TOP 1
			[adrAdditionalAddressInformation],[adrCity],[adrStreetAddress]
			,[adrZip],[adrZipPlusFour],[braBranchNumber]
			,[ccdCatastropheCode],[ccnClaimNumber],[claimNameState]
			,[clmClaimNumber],[clmDateReported],[clmFungusIndicator]
			,[clmLossDate],[clmLossDescription],[clmLossTime]
			,[clmOccurrence],[expSupervisorLogin],[gndGender]
			,[indPaymentAmount],[indPaymentFromDate],[indPaysLeft]
			,[lccAlphaCode],[mfcClaimSystem],[mjpAlphaMajorPeril]
			,[mscBankNumber],[mscCompanyNumber],[namBusinessName]
			,[namClaimantNumber],[namFirstName],[namLastName]
			,[namMiddleName],[OrignalPaymentCode],[payAttorneySettlement]
			,[payDraftNumber],[payLumpSumInd],[payMailTo]
			,[payPayee_venID],[payPayeeName1],[payPayTo1]
			,[payPayTo2],[payTransactionCategory],[polGroupNumber]
			,[polLocation],[polModule],[polPolicyNumber]
			,[polUnitLocationNumber],[pyfDescription]
			,[rcvAttorneySettlement],[rcvCheckNumber]
			,[rcvPayeeName],[rcvReason],[rcvTIN_venID]
			,[rcvTransactionCategory],[reserveAdjuster],[rsvAccidentState]
			,[rsvAIA12],[rsvAIA34],[rsvAIA56]
			,[rsvClassCode],[rsvEffectiveDate],[rsvFactoredReserveInd]
			,[rsvInlandMarineUnit],[rsvInsuranceLine],[rsvJurisdictionState]
			,[rsvLocationNumber],[rsvMPSequence],[rsvOriginalReserve]
			,[rsvRiskGroup],[rsvRiskGroupSequence],[rsvRiskUnit]
			,[rsvRiskUnitSequence],[rsvSubLocationNumber],[rsvTypeDisability]
			,[rsvTypeExposure],[rvcReserveCategory],[sbtCheckNumber]
			,[sbtPayerName],[slcDescription],[tcdCode]
			,[trxAmount],[trxBalance],[trxLRReserveFlag]
			,[trxMFActionCode],[trxMFBaseTransaction],[trxMFSequenceNum]
			,[trxTransactionDate],[trxUploadTimeStamp],[uspCICSOpID]
			,[uspLoginName],[CC_TransactionID],[CC_TransactionCreateTime]
			,[CC_TransactionUpdateTime],[CC_DateAdded],[CC_DateUpdated]
			,[BatchID],[PrimaryAdjuster],[OriginalTran]
			,[GWLossCause],[GWCostCategory],[mjpMajorPeril]
			,[mjpNumericMajorPeril],[GWReserveCategory],[GWExposureId]
			,[GWClaimId],0 /*Retired*/,[ModifiedBy],[ModifiedDate]
		FROM	PMSFinancialUpload
		WHERE	PMSPrimaryKey = @PMSPrimaryKey;

		-- Update old record
		UPDATE PMSFinancialUpload
		SET Retired = 1,
			trxUploadTimeStamp = (CASE WHEN trxUploadTimeStamp IS NULL
										THEN @ModifiedDate
										ELSE trxUploadTimeStamp
									END)
		FROM PMSFinancialUpload
		WHERE
			PMSPrimaryKey = @PMSPrimaryKey;

		-- Update the new record
		UPDATE PMSFinancialUpload
		SET ModifiedBy = @CurrentUser,
			ModifiedDate = @ModifiedDate,
			trxUploadTimeStamp = NULL,
			namClaimantNumber = @namClaimantNumber,
			trxTransactionDate = @trxTransactionDate,
			mjpAlphaMajorPeril = @mjpAlphaMajorPeril,
			rvcReserveCategory = @rvcReserveCategory,
			lccAlphaCode = @lccAlphaCode,
			tcdCode = @tcdCode,
			trxAmount = @trxAmount,
			rsvRiskUnit = @rsvRiskUnit,
			rsvInlandMarineUnit = @rsvInlandMarineUnit,
			payTransactionCategory = @payTransactionCategory,
			rcvTransactionCategory = @rcvTransactionCategory,
			rsvAIA12 = @rsvAIA12,
			rsvAIA34 = @rsvAIA34,
			rsvAIA56 = @rsvAIA56,
			rsvInsuranceLine = @rsvInsuranceLine,
			rsvRiskUnitSequence = @rsvRiskUnitSequence,
			rsvRiskGroup = @rsvRiskGroup,
			rsvRiskGroupSequence = @rsvRiskGroupSequence,
			polUnitLocationNumber = @polUnitLocationNumber,
			rsvLocationNumber = @rsvLocationNumber,
			rsvMPSequence = @rsvMPSequence
		FROM PMSFinancialUpload
		WHERE
			PMSPrimaryKey != @PMSPrimaryKey
		AND	clmClaimNumber = @clmClaimNumber
		AND ABS(trxAmount) = ABS(@trxAmount)
		AND Retired = 0;

		-- Catch and update any older records that should be retired but aren't
		UPDATE PMSFinancialUpload
		SET
			Retired = 1
		FROM PMSFinancialUpload
		WHERE
			clmClaimNumber = @clmClaimNumber
		AND ABS(trxAmount) = ABS(@trxAmount)
		AND ModifiedDate != @ModifiedDate

		/*===============================================================
									PMSExposures
		===============================================================*/

		-- Check to update PMSExposures and insert a new record
		IF (SELECT	COUNT(Exposure.CC_TransactionID)
			FROM	PMSFinancialUpload AS Financial
					INNER JOIN PMSExposures AS Exposure
					ON Financial.CC_TransactionID = Exposure.CC_TransactionID
			WHERE	Financial.PMSPrimaryKey = @PMSPrimaryKey) > 0
			BEGIN

			-- Get the primary key for the old record
			DECLARE @ExposuresPrimaryKey int;
			SET		@ExposuresPrimaryKey = (SELECT TOP 1 Exposure.PMSPrimaryKey
											FROM	PMSExposures AS Exposure
													INNER JOIN PMSFinancialUpload AS Financial
													ON Exposure.CC_TransactionID = Financial.CC_TransactionID
														AND Financial.PMSPrimaryKey = @PMSPrimaryKey
											WHERE	Exposure.clmClaimNumber = @clmClaimNumber
													AND ABS(Exposure.trxAmount) = ABS(@trxAmount)
											ORDER BY Exposure.PMSPrimaryKey DESC);

			-- Insert a new record (had to select all but PrimaryKey)
			INSERT INTO PMSExposures
			SELECT TOP 1
			   [adrAdditionalAddressInformation],[adrCity],[adrStreetAddress]
			  ,[adrZip],[adrZipPlusFour],[braBranchNumber]
			  ,[ccdCatastropheCode],[ccnClaimNumber],[claimNameState]
			  ,[clmClaimNumber],[clmDateReported],[clmFungusIndicator]
			  ,[clmLossDate],[clmLossDescription],[clmLossTime]
			  ,[clmOccurrence],[expSupervisorLogin],[gndGender]
			  ,[indPaymentAmount],[indPaymentFromDate],[indPaysLeft]
			  ,[lccAlphaCode],[mfcClaimSystem],[mjpAlphaMajorPeril]
			  ,[mscBankNumber],[mscCompanyNumber],[namBusinessName]
			  ,[namClaimantNumber],[namFirstName],[namLastName]
			  ,[namMiddleName],[OrignalPaymentCode],[payAttorneySettlement]
			  ,[payDraftNumber],[payLumpSumInd],[payMailTo]
			  ,[payPayee_venID],[payPayeeName1],[payPayTo1]
			  ,[payPayTo2],[payTransactionCategory],[polGroupNumber]
			  ,[polLocation],[polModule],[polPolicyNumber]
			  ,[polUnitLocationNumber],[pyfDescription],[rcvAttorneySettlement]
			  ,[rcvCheckNumber],[rcvPayeeName],[rcvReason]
			  ,[rcvTIN_venID],[rcvTransactionCategory],[reserveAdjuster]
			  ,[rsvAccidentState],[rsvAIA12],[rsvAIA34]
			  ,[rsvAIA56],[rsvClassCode],[rsvEffectiveDate]
			  ,[rsvFactoredReserveInd],[rsvInlandMarineUnit],[rsvInsuranceLine]
			  ,[rsvJurisdictionState],[rsvLocationNumber],[rsvMPSequence]
			  ,[rsvOriginalReserve],[rsvRiskGroup],[rsvRiskGroupSequence]
			  ,[rsvRiskUnit],[rsvRiskUnitSequence],[rsvSubLocationNumber]
			  ,[rsvTypeDisability],[rsvTypeExposure],[rvcReserveCategory]
			  ,[sbtCheckNumber],[sbtPayerName],[slcDescription]
			  ,[tcdCode],[trxAmount],[trxBalance]
			  ,[trxLRReserveFlag],[trxMFActionCode],[trxMFBaseTransaction]
			  ,[trxMFSequenceNum],[trxTransactionDate],[trxUploadTimeStamp]
			  ,[uspCICSOpID],[uspLoginName],[CC_TransactionID]
			  ,[CC_TransactionCreateTime],[CC_TransactionUpdateTime],[CC_DateAdded]
			  ,[CC_DateUpdated],[BatchID],[PrimaryAdjuster]
			  ,[OriginalTran],[GWLossCause],[GWCostCategory]
			  ,[mjpMajorPeril],[mjpNumericMajorPeril],[GWReserveCategory]
			  ,[GWExposureId],[GWClaimId],0 /*Retired*/
			  ,[ModifiedBy],[ModifiedDate]
			FROM	PMSExposures
			WHERE	PMSPrimaryKey = @ExposuresPrimaryKey

			-- Update the latest CC_TransactionID record
			UPDATE	PMSExposures
			SET	Retired = 1,
				trxUploadTimeStamp = CASE WHEN trxUploadTimeStamp IS NULL
										THEN @ModifiedDate
										ELSE trxUploadTimeStamp
									END
			FROM	PMSExposures
			WHERE	PMSPrimaryKey = @ExposuresPrimaryKey;

			-- Update the new record with the new data
			UPDATE PMSExposures
			SET ModifiedBy = @CurrentUser,
				ModifiedDate = @ModifiedDate,
				namClaimantNumber = @namClaimantNumber,
				trxTransactionDate = @trxTransactionDate,
				mjpAlphaMajorPeril = @mjpAlphaMajorPeril,
				rvcReserveCategory = @rvcReserveCategory,
				lccAlphaCode = @lccAlphaCode,
				tcdCode = @tcdCode,
				trxAmount = @trxAmount,
				rsvRiskUnit = @rsvRiskUnit,
				rsvInlandMarineUnit = @rsvInlandMarineUnit,
				payTransactionCategory = @payTransactionCategory,
				rcvTransactionCategory = @rcvTransactionCategory,
				rsvAIA12 = @rsvAIA12,
				rsvAIA34 = @rsvAIA34,
				rsvAIA56 = @rsvAIA56,
				rsvInsuranceLine = @rsvInsuranceLine,
				rsvRiskUnitSequence = @rsvRiskUnitSequence,
				rsvRiskGroup = @rsvRiskGroup,
				rsvRiskGroupSequence = @rsvRiskGroupSequence,
				polUnitLocationNumber = @polUnitLocationNumber,
				rsvLocationNumber = @rsvLocationNumber,
				rsvMPSequence = @rsvMPSequence
			FROM PMSExposures
			WHERE
				PMSPrimaryKey != @ExposuresPrimaryKey
			AND	clmClaimNumber = @clmClaimNumber
			AND ABS(trxAmount) = ABS(@trxAmount)
			AND Retired = 0;

			-- Catch and update any older records that should be retired but aren't
			UPDATE PMSExposures
			SET
				Retired = 1
			FROM PMSExposures
			WHERE
				clmClaimNumber = @clmClaimNumber
			AND ABS(trxAmount) = ABS(@trxAmount)
			AND ModifiedDate != @ModifiedDate

			END

		/*===============================================================
									Finished
		===============================================================*/

		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		THROW;
	END CATCH