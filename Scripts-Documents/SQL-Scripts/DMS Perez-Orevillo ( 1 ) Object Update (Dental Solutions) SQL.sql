--Version 1.0.0
--
--DATABASE OBJECT UPDATE Created On - (03.18.10)   

USE dbperezorevillodms
GO

-- verifies if the procedure "SelectForAccountsReceivablePatientRegistration" exist
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dental' AND SPECIFIC_NAME = N'SelectForAccountsReceivablePatientRegistration')
   DROP PROCEDURE dental.SelectForAccountsReceivablePatientRegistration
GO

CREATE PROCEDURE dental.SelectForAccountsReceivablePatientRegistration
	
	@system_user_id varchar(50) = ''
	
AS

	IF (dental.IsSystemAdminSystemUserInfo(@system_user_id) = 1) OR (dental.IsDentalUsersSystemUserInfo(@system_user_id) = 1)
	BEGIN

		SELECT
			pr.sysid_registration AS sysid_registration,
			pr.sysid_patient AS sysid_patient,
			pr.registration_date AS registration_date,
			pr.medical_prescription AS medical_prescription,
			inner_amount_payable.amount_payable AS amount_payable,
			inner_amount_paid.amount_paid AS amount_paid,
			inner_amount_paid.discount AS discount,
			inner_amount_payable.amount_payable - (inner_amount_paid.amount_paid + inner_amount_paid.discount) AS amount_balance,
			pti.last_name AS last_name,
			pti.first_name AS first_name,
			pti.middle_name AS middle_name
		FROM
			dental.patient_registration AS pr		
		INNER JOIN dental.patient_information AS pti ON pti.sysid_patient = pr.sysid_patient
		INNER JOIN
			(
				SELECT
					rd.sysid_registration AS sysid_registration,
					SUM(rd.amount) AS amount_payable
				FROM
					dental.registration_details AS rd
				GROUP BY
					rd.sysid_registration
				
			) AS inner_amount_payable ON inner_amount_payable.sysid_registration = pr.sysid_registration
		INNER JOIN
			(
				SELECT
					pd.sysid_registration AS sysid_registration,
					SUM(pd.amount) AS amount_paid,
					SUM(pd.discount) AS discount
				FROM
					dental.payment_details AS pd
				GROUP BY
					pd.sysid_registration
				
			) AS inner_amount_paid ON inner_amount_paid.sysid_registration = pr.sysid_registration
		WHERE
			((inner_amount_payable.amount_payable - (inner_amount_paid.amount_paid + inner_amount_paid.discount)) > 0)
		UNION ALL
		SELECT
			pr.sysid_registration AS sysid_registration,
			pr.sysid_patient AS sysid_patient,
			pr.registration_date AS registration_date,
			pr.medical_prescription AS medical_prescription,
			inner_amount_payable.amount_payable AS amount_payable,
			0.00 AS amount_paid,
			0.00 AS discount,
			inner_amount_payable.amount_payable AS amount_balance,
			pti.last_name AS last_name,
			pti.first_name AS first_name,
			pti.middle_name AS middle_name
		FROM
			dental.patient_registration AS pr		
		INNER JOIN dental.patient_information AS pti ON pti.sysid_patient = pr.sysid_patient
		INNER JOIN
			(
				SELECT
					rd.sysid_registration AS sysid_registration,
					SUM(rd.amount) AS amount_payable
				FROM
					dental.registration_details AS rd
				GROUP BY
					rd.sysid_registration
				
			) AS inner_amount_payable ON inner_amount_payable.sysid_registration = pr.sysid_registration
		LEFT JOIN
			(
				SELECT
					pd.sysid_registration AS sysid_registration,
					SUM(pd.amount) AS amount_paid,
					SUM(pd.discount) AS discount
				FROM
					dental.payment_details AS pd
				GROUP BY
					pd.sysid_registration
				
			) AS inner_amount_paid ON inner_amount_paid.sysid_registration = pr.sysid_registration
		WHERE
			(inner_amount_paid.sysid_registration IS NULL)
		ORDER BY
			registration_date ASC

	END
	ELSE
	BEGIN
		EXECUTE dental.ShowErrorMsg 'Query a patient registration', 'Patient Registration'
	END
	
GO
-------------------------------------------------------

-- grant permission on the stored procedure
GRANT EXECUTE ON dental.SelectForAccountsReceivablePatientRegistration TO db_dentalusers
GO
-------------------------------------------------------------
