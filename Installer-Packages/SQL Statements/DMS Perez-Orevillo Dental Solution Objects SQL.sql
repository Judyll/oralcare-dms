--******************************************************--
-- This SQL Statements is used for the					--
-- 		PEREZ-OREVILLO DENTAL MANAGEMENT SYSTEM			--
--Programmed by: Judyll Mark T. Agan					--
--Date created: December 11, 2007						--
--DENTAL SOLUTIONS										--
--******************************************************--

USE dbperezorevillodms
GO

-- ###########################################DROP TABLE CONSTRAINTS ##############################################################

--verifies if the Patient_Information_Created_By_FK constraint exists--
IF OBJECT_ID('dental.patient_information', 'U') IS NOT NULL
BEGIN
	ALTER TABLE dental.patient_information
	DROP CONSTRAINT Patient_Information_Created_By_FK
END
GO
-----------------------------------------------------

--verifies if the Patient_Information_Edited_By_FK constraint exists--
IF OBJECT_ID('dental.patient_information', 'U') IS NOT NULL
BEGIN
	ALTER TABLE dental.patient_information
	DROP CONSTRAINT Patient_Information_Edited_By_FK
END
GO
-----------------------------------------------------

--verifies if the Procedure_Information_Created_By_FK constraint exists--
IF OBJECT_ID('dental.procedure_information', 'U') IS NOT NULL
BEGIN
	ALTER TABLE dental.procedure_information
	DROP CONSTRAINT Procedure_Information_Created_By_FK
END
GO
-----------------------------------------------------

--verifies if the Procedure_Information_Edited_By_FK constraint exists--
IF OBJECT_ID('dental.procedure_information', 'U') IS NOT NULL
BEGIN
	ALTER TABLE dental.procedure_information
	DROP CONSTRAINT Procedure_Information_Edited_By_FK
END
GO
-----------------------------------------------------

--verifies if the Patient_Registration_SysID_Patient_FK constraint exists--
IF OBJECT_ID('dental.patient_registration', 'U') IS NOT NULL
BEGIN
	ALTER TABLE dental.patient_registration
	DROP CONSTRAINT Patient_Registration_SysID_Patient_FK
END
GO
-----------------------------------------------------

--verifies if the Patient_Registration_Created_By_FK constraint exists--
IF OBJECT_ID('dental.patient_registration', 'U') IS NOT NULL
BEGIN
	ALTER TABLE dental.patient_registration
	DROP CONSTRAINT Patient_Registration_Created_By_FK
END
GO
-----------------------------------------------------

--verifies if the Patient_Registration_Edited_By_FK constraint exists--
IF OBJECT_ID('dental.patient_registration', 'U') IS NOT NULL
BEGIN
	ALTER TABLE dental.patient_registration
	DROP CONSTRAINT Patient_Registration_Edited_By_FK
END
GO
-----------------------------------------------------

--verifies if the Registration_Details_SysID_Registration_FK constraint exists--
IF OBJECT_ID('dental.registration_details', 'U') IS NOT NULL
BEGIN
	ALTER TABLE dental.registration_details
	DROP CONSTRAINT Registration_Details_SysID_Registration_FK
END
GO
-----------------------------------------------------

--verifies if the Registration_Details_SysID_Procedure_FK constraint exists--
IF OBJECT_ID('dental.registration_details', 'U') IS NOT NULL
BEGIN
	ALTER TABLE dental.registration_details
	DROP CONSTRAINT Registration_Details_SysID_Procedure_FK
END
GO
-----------------------------------------------------

--verifies if the Registration_Details_Created_By_FK constraint exists--
IF OBJECT_ID('dental.registration_details', 'U') IS NOT NULL
BEGIN
	ALTER TABLE dental.registration_details
	DROP CONSTRAINT Registration_Details_Created_By_FK
END
GO
-----------------------------------------------------

--verifies if the Registration_Details_Edited_By_FK constraint exists--
IF OBJECT_ID('dental.registration_details', 'U') IS NOT NULL
BEGIN
	ALTER TABLE dental.registration_details
	DROP CONSTRAINT Registration_Details_Edited_By_FK
END
GO
-----------------------------------------------------

--verifies if the Payment_Details_SysID_Registration_FK constraint exists--
IF OBJECT_ID('dental.payment_details', 'U') IS NOT NULL
BEGIN
	ALTER TABLE dental.payment_details
	DROP CONSTRAINT Payment_Details_SysID_Registration_FK
END
GO
-----------------------------------------------------

--verifies if the Payment_Details_Created_By_FK constraint exists--
IF OBJECT_ID('dental.payment_details', 'U') IS NOT NULL
BEGIN
	ALTER TABLE dental.payment_details
	DROP CONSTRAINT Payment_Details_Created_By_FK
END
GO
-----------------------------------------------------

--verifies if the Payment_Details_Edited_By_FK constraint exists--
IF OBJECT_ID('dental.payment_details', 'U') IS NOT NULL
BEGIN
	ALTER TABLE dental.payment_details
	DROP CONSTRAINT Payment_Details_Edited_By_FK
END
GO
-----------------------------------------------------

--verifies if the Payment_Details_Audit_SysID_Registration_FK constraint exists--
IF OBJECT_ID('dental.payment_details_audit', 'U') IS NOT NULL
BEGIN
	ALTER TABLE dental.payment_details_audit
	DROP CONSTRAINT Payment_Details_Audit_SysID_Registration_FK
END
GO
-----------------------------------------------------

--verifies if the Payment_Details_Audit_Deleted_By_FK constraint exists--
IF OBJECT_ID('dental.payment_details_audit', 'U') IS NOT NULL
BEGIN
	ALTER TABLE dental.payment_details_audit
	DROP CONSTRAINT Payment_Details_Audit_Deleted_By_FK
END
GO
-----------------------------------------------------

-- ########################################END DROP TABLE CONSTRAINTS ##############################################################



-- ########################################DROP DEPENDENT TABLE CONSTRAINTS ##############################################################
-- ######################################END DROP DEPENDENT TABLE CONSTRAINTS ############################################################



-- #########################################################GENERAL PROCEDURES AND FUNCTIONS##################################################
-- ######################################################END GENERAL PROCEDURES AND FUNCTIONS###################################################


-- ################################################TABLE "patient_information" OBJECTS######################################################
-- verifies if the patient_information table exists
IF OBJECT_ID('dental.patient_information', 'U') IS NOT NULL
	DROP TABLE dental.patient_information
GO

CREATE TABLE dental.patient_information 			
(
	sysid_patient varchar (50) NOT NULL 
		CONSTRAINT Patient_Information_SysID_Patient_PK PRIMARY KEY (sysid_patient)
		CONSTRAINT Patient_Information_SysID_Patient_CK CHECK (sysid_patient LIKE 'SYSPNT%'),
	last_name varchar (50) NOT NULL,
	first_name varchar (50) NOT NULL,
	middle_name varchar (50) NULL DEFAULT (''),
	home_address varchar (500) NULL DEFAULT (''),
	phone_nos varchar (100) NULL DEFAULT (''),
	birthdate datetime NULL DEFAULT (GETDATE()),
	e_mail varchar (50) NULL DEFAULT (''),
	
	medical_history varchar (5000) NULL DEFAULT (''),
	emergency_info varchar (2000) NULL DEFAULT (''),

	pic varbinary (MAX) NULL,
	extension_name varchar (10) NULL,

	created_on datetime NOT NULL DEFAULT (GETDATE()),
	created_by varchar (50) NOT NULL
		CONSTRAINT Patient_Information_Created_By_FK FOREIGN KEY REFERENCES dental.system_user_info(system_user_id) ON UPDATE NO ACTION,
	
	edited_on datetime NULL,
	edited_by varchar (50) NULL	
		CONSTRAINT Patient_Information_Edited_By_FK FOREIGN KEY REFERENCES dental.system_user_info(system_user_id) ON UPDATE NO ACTION
	
) ON [PRIMARY]
GO
------------------------------------------------------------------

-- create an index of the patient_information table
CREATE INDEX Patient_Information_SysID_Patient_Index
	ON dental.patient_information (sysid_patient ASC)
GO
------------------------------------------------------------------

/*verifies that the trigger "Patient_Information_Trigger_Instead_Delete" already exist*/
IF OBJECT_ID ('dental.Patient_Information_Trigger_Instead_Delete','TR') IS NOT NULL
   DROP TRIGGER dental.Patient_Information_Trigger_Instead_Delete
GO

CREATE TRIGGER dental.Patient_Information_Trigger_Instead_Delete
	ON  dental.patient_information
	INSTEAD OF DELETE 
	NOT FOR REPLICATION
AS 
	
	EXECUTE dental.ShowErrorMsg 'Delete operation is denied', 'Patient Information'
	
GO
/*-----------------------------------------------------------------*/

-- verifies if the procedure "InsertPatientInformation" exist
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dental' AND SPECIFIC_NAME = N'InsertPatientInformation')
   DROP PROCEDURE dental.InsertPatientInformation
GO

CREATE PROCEDURE dental.InsertPatientInformation
	
	@sysid_patient varchar (50) = '',
	@last_name varchar (50) = '',
	@first_name varchar (50) = '',
	@middle_name varchar (50) = '',
	@home_address varchar (500) = '',
	@phone_nos varchar (100) = '',
	@birthdate datetime,
	@e_mail varchar (50) = '',
	@medical_history varchar (5000) = '',
	@emergency_info varchar (2000) = '',
	@pic varbinary (MAX),
	@extension_name varchar (10),

	@created_by varchar(50) = ''
	
AS

	IF (dental.IsSystemAdminSystemUserInfo(@created_by) = 1) OR (dental.IsDentalUsersSystemUserInfo(@created_by) = 1)
	BEGIN

		INSERT INTO dental.patient_information
		(
			sysid_patient,
			last_name,
			first_name,
			middle_name,
			home_address,
			phone_nos,
			birthdate,
			e_mail,
			medical_history,
			emergency_info,
			pic,
			extension_name,
			created_by
		)
		VALUES
		(
			@sysid_patient,
			@last_name,
			@first_name,
			@middle_name,
			@home_address,
			@phone_nos,
			@birthdate,
			@e_mail,
			@medical_history,
			@emergency_info,
			@pic,
			@extension_name,
			@created_by
		)

	END
	ELSE
	BEGIN
		EXECUTE dental.ShowErrorMsg 'Insert a patient information', 'Patient Information'
	END
	
GO
-------------------------------------------------------

-- grant permission on the stored procedure
GRANT EXECUTE ON dental.InsertPatientInformation TO db_dentalusers
GO
-------------------------------------------------------------

-- verifies if the procedure "UpdatePatientInformation" exist
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dental' AND SPECIFIC_NAME = N'UpdatePatientInformation')
   DROP PROCEDURE dental.UpdatePatientInformation
GO

CREATE PROCEDURE dental.UpdatePatientInformation
	
	@sysid_patient varchar (50) = '',
	@last_name varchar (50) = '',
	@first_name varchar (50) = '',
	@middle_name varchar (50) = '',
	@home_address varchar (500) = '',
	@phone_nos varchar (100) = '',
	@birthdate datetime,
	@e_mail varchar (50) = '',
	@medical_history varchar (5000) = '',
	@emergency_info varchar (2000) = '',
	@pic varbinary (MAX),
	@extension_name varchar (10),

	@edited_by varchar(50)
	
AS

	IF (dental.IsSystemAdminSystemUserInfo(@edited_by) = 1) OR (dental.IsDentalUsersSystemUserInfo(@edited_by) = 1)
	BEGIN

		UPDATE dental.patient_information SET
			last_name = @last_name,
			first_name = @first_name,
			middle_name = @middle_name,
			home_address = @home_address,
			phone_nos = @phone_nos,
			birthdate = @birthdate,
			e_mail = @e_mail,
			medical_history = @medical_history,
			emergency_info = @emergency_info,
			pic = @pic,
			extension_name = @extension_name,
			edited_on = GETDATE(),
			edited_by = @edited_by
		WHERE
			sysid_patient = @sysid_patient

	END
	ELSE
	BEGIN
		EXECUTE dental.ShowErrorMsg 'Updated a patient information', 'Patient Information'
	END
	
GO
-------------------------------------------------------

-- grant permission on the stored procedure
GRANT EXECUTE ON dental.UpdatePatientInformation TO db_dentalusers
GO
-------------------------------------------------------------

-- verifies if the procedure "SelectPatientInformation" exist
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dental' AND SPECIFIC_NAME = N'SelectPatientInformation')
   DROP PROCEDURE dental.SelectPatientInformation
GO

CREATE PROCEDURE dental.SelectPatientInformation
	
	@query_string varchar (50) = '',
	@system_user_id varchar(50) = ''
	
AS

	IF (dental.IsSystemAdminSystemUserInfo(@system_user_id) = 1) OR (dental.IsDentalUsersSystemUserInfo(@system_user_id) = 1)
	BEGIN

		SELECT @query_string = '%' + RTRIM(LTRIM(@query_string)) + '%'

		SELECT
			sysid_patient,
			last_name,
			first_name,
			middle_name,
			home_address,
			phone_nos,
			birthdate,
			e_mail,
			medical_history,
			emergency_info
		FROM
			dental.patient_information
		WHERE
			(sysid_patient LIKE @query_string) OR (last_name LIKE @query_string) OR (first_name LIKE @query_string)
		ORDER BY
			last_name ASC

	END
	ELSE
	BEGIN
		EXECUTE dental.ShowErrorMsg 'Query a patient information', 'Patient Information'
	END
	
GO
-------------------------------------------------------

-- grant permission on the stored procedure
GRANT EXECUTE ON dental.SelectPatientInformation TO db_dentalusers
GO
-------------------------------------------------------------

-- verifies if the procedure "SelectBySysIDPatientPatientInformation" exist
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dental' AND SPECIFIC_NAME = N'SelectBySysIDPatientPatientInformation')
   DROP PROCEDURE dental.SelectBySysIDPatientPatientInformation
GO

CREATE PROCEDURE dental.SelectBySysIDPatientPatientInformation
	
	@sysid_patient varchar (50) = '',
	@system_user_id varchar(50) = ''
	
AS

	IF (dental.IsSystemAdminSystemUserInfo(@system_user_id) = 1) OR (dental.IsDentalUsersSystemUserInfo(@system_user_id) = 1)
	BEGIN

		SELECT
			sysid_patient,
			last_name,
			first_name,
			middle_name,
			home_address,
			phone_nos,
			birthdate,
			e_mail,
			medical_history,
			emergency_info
		FROM
			dental.patient_information
		WHERE
			sysid_patient = @sysid_patient
	END
	ELSE
	BEGIN
		EXECUTE dental.ShowErrorMsg 'Query a patient information', 'Patient Information'
	END
	
GO
-------------------------------------------------------

-- grant permission on the stored procedure
GRANT EXECUTE ON dental.SelectBySysIDPatientPatientInformation TO db_dentalusers
GO
-------------------------------------------------------------

-- verifies if the procedure "SelectImagePatientInformation" exist
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dental' AND SPECIFIC_NAME = N'SelectImagePatientInformation')
   DROP PROCEDURE dental.SelectImagePatientInformation
GO

CREATE PROCEDURE dental.SelectImagePatientInformation
	
	@sysid_patient varchar (50) = '',
	@system_user_id varchar(50) = ''
	
AS

	IF (dental.IsSystemAdminSystemUserInfo(@system_user_id) = 1) OR (dental.IsDentalUsersSystemUserInfo(@system_user_id) = 1)
	BEGIN

		SELECT
			sysid_patient,
			extension_name,
			pic
		FROM
			dental.patient_information
		WHERE
			sysid_patient = @sysid_patient AND NOT pic IS NULL

	END
	ELSE
	BEGIN
		EXECUTE dental.ShowErrorMsg 'Query a patient information', 'Patient Information'
	END
	
GO
-------------------------------------------------------

-- grant permission on the stored procedure
GRANT EXECUTE ON dental.SelectImagePatientInformation TO db_dentalusers
GO
-------------------------------------------------------------

-- verifies if the procedure "GetCountPatientInformation" exist
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dental' AND SPECIFIC_NAME = N'GetCountPatientInformation')
   DROP PROCEDURE dental.GetCountPatientInformation
GO

CREATE PROCEDURE dental.GetCountPatientInformation

	@system_user_id varchar (50) = ''
AS

	IF (dental.IsSystemAdminSystemUserInfo(@system_user_id) = 1) OR (dental.IsDentalUsersSystemUserInfo(@system_user_id) = 1)
	BEGIN

		SELECT COUNT(sysid_patient) FROM dental.patient_information 

	END
	BEGIN
		EXECUTE dental.ShowErrorMsg 'Query a patient information', 'Patient Information'
	END
	
GO
-------------------------------------------------------

-- grant permission on the stored procedure
GRANT EXECUTE ON dental.GetCountPatientInformation TO db_dentalusers
GO
-------------------------------------------------------------

-- verifies if the procedure "IsExistsSysIDPatientInformation" exist
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dental' AND SPECIFIC_NAME = N'IsExistsSysIDPatientInformation')
   DROP PROCEDURE dental.IsExistsSysIDPatientInformation
GO

CREATE PROCEDURE dental.IsExistsSysIDPatientInformation

	@sysid_patient varchar (50) = '',
	@system_user_id varchar (50) = ''
AS

	IF (dental.IsSystemAdminSystemUserInfo(@system_user_id) = 1) OR (dental.IsDentalUsersSystemUserInfo(@system_user_id) = 1)
	BEGIN

		SELECT dental.IsExistsSysIDPatientInfo(@sysid_patient)
	

	END
	BEGIN
		EXECUTE dental.ShowErrorMsg 'Query a patient information', 'Patient Information'
	END
	
GO
-------------------------------------------------------

-- grant permission on the stored procedure
GRANT EXECUTE ON dental.IsExistsSysIDPatientInformation TO db_dentalusers
GO
-------------------------------------------------------------

-- verifies if the "IsExistsSysIDPatientInfo" function already exist
IF OBJECT_ID (N'dental.IsExistsSysIDPatientInfo') IS NOT NULL
   DROP FUNCTION dental.IsExistsSysIDPatientInfo
GO

CREATE FUNCTION dental.IsExistsSysIDPatientInfo
(	
	@sysid_patient varchar(50) = ''
)
RETURNS bit
AS
BEGIN
	
	DECLARE @result bit

	SET @result = 0

	IF EXISTS (SELECT * FROM dental.patient_information WHERE sysid_patient = @sysid_patient)
	BEGIN
		SET @result = 1
	END	
	
	RETURN @result
END
GO
------------------------------------------------------


-- ################################################END TABLE "patient_information" OBJECTS######################################################


-- ################################################TABLE "procedure_information" OBJECTS######################################################
-- verifies if the procedure_information table exists
IF OBJECT_ID('dental.procedure_information', 'U') IS NOT NULL
	DROP TABLE dental.procedure_information
GO

CREATE TABLE dental.procedure_information 			
(
	sysid_procedure varchar (50) NOT NULL 
		CONSTRAINT Procedure_Information_SysID_Procedure_PK PRIMARY KEY (sysid_procedure)
		CONSTRAINT Procedure_Information_SysID_Procedure_CK CHECK (sysid_procedure LIKE 'SYSPRC%'),
	procedure_name varchar (200) NOT NULL
		CONSTRAINT Procedure_Information_Procedure_Name_UQ UNIQUE (procedure_name),
	amount decimal (12, 2) NOT NULL DEFAULT (0),

	created_on datetime NOT NULL DEFAULT (GETDATE()),
	created_by varchar (50) NOT NULL
		CONSTRAINT Procedure_Information_Created_By_FK FOREIGN KEY REFERENCES dental.system_user_info(system_user_id) ON UPDATE NO ACTION,
	
	edited_on datetime NULL,
	edited_by varchar (50) NULL	
		CONSTRAINT Procedure_Information_Edited_By_FK FOREIGN KEY REFERENCES dental.system_user_info(system_user_id) ON UPDATE NO ACTION
	
) ON [PRIMARY]
GO
------------------------------------------------------------------

-- create an index of the procedure_information table
CREATE INDEX Procedure_Information_SysID_Procedure_Index
	ON dental.procedure_information (sysid_procedure ASC)
GO
------------------------------------------------------------------

/*verifies that the trigger "Procedure_Information_Trigger_Instead_Delete" already exist*/
IF OBJECT_ID ('dental.Procedure_Information_Trigger_Instead_Delete','TR') IS NOT NULL
   DROP TRIGGER dental.Procedure_Information_Trigger_Instead_Delete
GO

CREATE TRIGGER dental.Procedure_Information_Trigger_Instead_Delete
	ON  dental.procedure_information
	INSTEAD OF DELETE 
	NOT FOR REPLICATION
AS 
	
	EXECUTE dental.ShowErrorMsg 'Delete operation is denied', 'Procedure Information'
	
GO
/*-----------------------------------------------------------------*/


-- verifies if the procedure "InsertProcedureInformation" exist
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dental' AND SPECIFIC_NAME = N'InsertProcedureInformation')
   DROP PROCEDURE dental.InsertProcedureInformation
GO

CREATE PROCEDURE dental.InsertProcedureInformation
	
	@sysid_procedure varchar (50) = '',
	@procedure_name varchar (200) = '',
	@amount decimal (12, 2) = 0,

	@created_by varchar(50) = ''
	
AS

	IF (dental.IsSystemAdminSystemUserInfo(@created_by) = 1)
	BEGIN

		INSERT INTO dental.procedure_information
		(
			sysid_procedure,
			procedure_name,
			amount,
			created_by
		)
		VALUES
		(
			@sysid_procedure,
			@procedure_name,
			@amount,
			@created_by
		)

	END
	ELSE
	BEGIN
		EXECUTE dental.ShowErrorMsg 'Insert a procedure information', 'Procedure Information'
	END
	
GO
-------------------------------------------------------

-- grant permission on the stored procedure
GRANT EXECUTE ON dental.InsertProcedureInformation TO db_dentalusers
GO
-------------------------------------------------------------

-- verifies if the procedure "UpdateProcedureInformation" exist
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dental' AND SPECIFIC_NAME = N'UpdateProcedureInformation')
   DROP PROCEDURE dental.UpdateProcedureInformation
GO

CREATE PROCEDURE dental.UpdateProcedureInformation
	
	@sysid_procedure varchar (50) = '',
	@procedure_name varchar (200) = '',
	@amount decimal (12, 2) = 0,

	@edited_by varchar(50) = ''
	
AS

	IF (dental.IsSystemAdminSystemUserInfo(@edited_by) = 1)
	BEGIN

		UPDATE dental.procedure_information SET
			procedure_name = @procedure_name,
			amount = @amount,
			edited_on = GETDATE(),
			edited_by = @edited_by
		WHERE
			sysid_procedure = @sysid_procedure

	END
	ELSE
	BEGIN
		EXECUTE dental.ShowErrorMsg 'Update a procedure information', 'Procedure Information'
	END
	
GO
-------------------------------------------------------

-- grant permission on the stored procedure
GRANT EXECUTE ON dental.UpdateProcedureInformation TO db_dentalusers
GO
-------------------------------------------------------------

-- verifies if the procedure "SelectProcedureInformation" exist
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dental' AND SPECIFIC_NAME = N'SelectProcedureInformation')
   DROP PROCEDURE dental.SelectProcedureInformation
GO

CREATE PROCEDURE dental.SelectProcedureInformation
	
	@system_user_id varchar(50) = ''
	
AS

	IF (dental.IsSystemAdminSystemUserInfo(@system_user_id) = 1) OR (dental.IsDentalUsersSystemUserInfo(@system_user_id) = 1)
	BEGIN

		SELECT
			sysid_procedure,
			procedure_name,
			amount
		FROM
			dental.procedure_information
		ORDER BY
			procedure_name ASC

	END
	ELSE
	BEGIN
		EXECUTE dental.ShowErrorMsg 'Query a procedure information', 'Procedure Information'
	END
	
GO
-------------------------------------------------------

-- grant permission on the stored procedure
GRANT EXECUTE ON dental.SelectProcedureInformation TO db_dentalusers
GO
-------------------------------------------------------------

-- verifies if the procedure "GetCountProcedureInformation" exist
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dental' AND SPECIFIC_NAME = N'GetCountProcedureInformation')
   DROP PROCEDURE dental.GetCountProcedureInformation
GO

CREATE PROCEDURE dental.GetCountProcedureInformation

	@system_user_id varchar (50) = ''
AS

	IF (dental.IsSystemAdminSystemUserInfo(@system_user_id) = 1)
	BEGIN

		SELECT COUNT(sysid_procedure) FROM dental.procedure_information 

	END
	BEGIN
		EXECUTE dental.ShowErrorMsg 'Query a procedure information', 'Procedure Information'
	END
	
GO
-------------------------------------------------------

-- grant permission on the stored procedure
GRANT EXECUTE ON dental.GetCountProcedureInformation TO db_dentalusers
GO
-------------------------------------------------------------

-- verifies if the procedure "IsExistsSysIDProcedureInformation" exist
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dental' AND SPECIFIC_NAME = N'IsExistsSysIDProcedureInformation')
   DROP PROCEDURE dental.IsExistsSysIDProcedureInformation
GO

CREATE PROCEDURE dental.IsExistsSysIDProcedureInformation

	@sysid_procedure varchar (50) = '',
	@system_user_id varchar (50) = ''
AS

	IF (dental.IsSystemAdminSystemUserInfo(@system_user_id) = 1)
	BEGIN

		SELECT dental.IsExistSysIDProcedureInfo(@sysid_procedure)

	END
	BEGIN
		EXECUTE dental.ShowErrorMsg 'Query a procedure information', 'Procedure Information'
	END
	
GO
-------------------------------------------------------

-- grant permission on the stored procedure
GRANT EXECUTE ON dental.IsExistsSysIDProcedureInformation TO db_dentalusers
GO
-------------------------------------------------------------

-- verifies if the procedure "IsExistsNameProcedureInformation" exist
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dental' AND SPECIFIC_NAME = N'IsExistsNameProcedureInformation')
   DROP PROCEDURE dental.IsExistsNameProcedureInformation
GO

CREATE PROCEDURE dental.IsExistsNameProcedureInformation

	@sysid_procedure varchar (50) = '',
	@procedure_name varchar (200) = '',
	@system_user_id varchar (50) = ''
AS

	IF (dental.IsSystemAdminSystemUserInfo(@system_user_id) = 1)
	BEGIN

		SELECT dental.IsExistNameProcedureInfo(@sysid_procedure, @procedure_name)

	END
	BEGIN
		EXECUTE dental.ShowErrorMsg 'Query a procedure information', 'Procedure Information'
	END
	
GO
-------------------------------------------------------

-- grant permission on the stored procedure
GRANT EXECUTE ON dental.IsExistsNameProcedureInformation TO db_dentalusers
GO
-------------------------------------------------------------

-- verifies if the "IsExistSysIDProcedureInfo" function already exist
IF OBJECT_ID (N'dental.IsExistSysIDProcedureInfo') IS NOT NULL
   DROP FUNCTION dental.IsExistSysIDProcedureInfo
GO

CREATE FUNCTION dental.IsExistSysIDProcedureInfo
(	
	@sysid_procedure varchar(50) = ''
)
RETURNS bit
AS
BEGIN
	
	DECLARE @result bit

	SET @result = 0

	IF EXISTS (SELECT * FROM dental.procedure_information WHERE sysid_procedure = @sysid_procedure)
	BEGIN
		SET @result = 1
	END	
	
	RETURN @result
END
GO
------------------------------------------------------

-- verifies if the "IsExistNameProcedureInfo" function already exist
IF OBJECT_ID (N'dental.IsExistNameProcedureInfo') IS NOT NULL
   DROP FUNCTION dental.IsExistNameProcedureInfo
GO

CREATE FUNCTION dental.IsExistNameProcedureInfo
(	
	@sysid_procedure varchar (50) = '',
	@procedure_name varchar (200) = ''
)
RETURNS bit
AS
BEGIN
	
	DECLARE @result bit

	SET @result = 0

	IF EXISTS (SELECT * FROM dental.procedure_information WHERE NOT sysid_procedure = @sysid_procedure AND procedure_name LIKE @procedure_name)
	BEGIN
		SET @result = 1
	END	
	
	RETURN @result
END
GO
------------------------------------------------------

-- ################################################END TABLE "procedure_information" OBJECTS######################################################



-- ################################################TABLE "patient_registration" OBJECTS######################################################
-- verifies if the patient_registration table exists
IF OBJECT_ID('dental.patient_registration', 'U') IS NOT NULL
	DROP TABLE dental.patient_registration
GO

CREATE TABLE dental.patient_registration 			
(
	sysid_registration varchar (50) NOT NULL 
		CONSTRAINT Patient_Registration_SysID_Registration_PK PRIMARY KEY (sysid_registration)
		CONSTRAINT Patient_Registration_SysID_Registration_CK CHECK (sysid_registration LIKE 'SYSREG%'),
	sysid_patient varchar (50) NOT NULL
		CONSTRAINT Patient_Registration_SysID_Patient_FK FOREIGN KEY REFERENCES dental.patient_information(sysid_patient) ON UPDATE NO ACTION,
	registration_date datetime NOT NULL DEFAULT (GETDATE()),

	medical_prescription varchar (8000) NULL DEFAULT (''),

	created_on datetime NOT NULL DEFAULT (GETDATE()),
	created_by varchar (50) NOT NULL
		CONSTRAINT Patient_Registration_Created_By_FK FOREIGN KEY REFERENCES dental.system_user_info(system_user_id) ON UPDATE NO ACTION,
	
	edited_on datetime NULL,
	edited_by varchar (50) NULL	
		CONSTRAINT Patient_Registration_Edited_By_FK FOREIGN KEY REFERENCES dental.system_user_info(system_user_id) ON UPDATE NO ACTION
	
) ON [PRIMARY]
GO
------------------------------------------------------------------

-- create an index of the patient_registration table
CREATE INDEX Patient_Registration_SysID_Registration_Index
	ON dental.patient_registration (sysid_registration DESC)
GO
------------------------------------------------------------------

/*verifies that the trigger "Patient_Registration_Trigger_Instead_Delete" already exist*/
IF OBJECT_ID ('dental.Patient_Registration_Trigger_Instead_Delete','TR') IS NOT NULL
   DROP TRIGGER dental.Patient_Registration_Trigger_Instead_Delete
GO

CREATE TRIGGER dental.Patient_Registration_Trigger_Instead_Delete
	ON  dental.patient_registration
	INSTEAD OF DELETE 
	NOT FOR REPLICATION
AS 
	
	EXECUTE dental.ShowErrorMsg 'Delete operation is denied', 'Patient Registration'
	
GO
/*-----------------------------------------------------------------*/

-- verifies if the procedure "InsertPatientRegistration" exist
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dental' AND SPECIFIC_NAME = N'InsertPatientRegistration')
   DROP PROCEDURE dental.InsertPatientRegistration
GO

CREATE PROCEDURE dental.InsertPatientRegistration
	
	@sysid_registration varchar (50) = '',
	@sysid_patient varchar (50) = '',
	@registration_date datetime,

	@created_by varchar(50) = ''
	
AS

	IF (dental.IsSystemAdminSystemUserInfo(@created_by) = 1) OR (dental.IsDentalUsersSystemUserInfo(@created_by) = 1)
	BEGIN

		INSERT INTO dental.patient_registration
		(
			sysid_registration,
			sysid_patient,
			registration_date,
			created_by
		)
		VALUES
		(
			@sysid_registration,
			@sysid_patient,
			@registration_date,
			@created_by
		)

	END
	ELSE
	BEGIN
		EXECUTE dental.ShowErrorMsg 'Insert a patient registration', 'Patient Registration'
	END
	
GO
-------------------------------------------------------

-- grant permission on the stored procedure
GRANT EXECUTE ON dental.InsertPatientRegistration TO db_dentalusers
GO
-------------------------------------------------------------

-- verifies if the procedure "UpdatePatientRegistration" exist
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dental' AND SPECIFIC_NAME = N'UpdatePatientRegistration')
   DROP PROCEDURE dental.UpdatePatientRegistration
GO

CREATE PROCEDURE dental.UpdatePatientRegistration
	
	@sysid_registration varchar (50) = '',
	@registration_date datetime,

	@edited_by varchar(50) = ''
	
AS

	IF (dental.IsSystemAdminSystemUserInfo(@edited_by) = 1) OR (dental.IsDentalUsersSystemUserInfo(@edited_by) = 1)
	BEGIN

		UPDATE dental.patient_registration SET
			registration_date = @registration_date,
			edited_on = GETDATE(),
			edited_by = @edited_by
		WHERE
			sysid_registration = @sysid_registration

	END
	ELSE
	BEGIN
		EXECUTE dental.ShowErrorMsg 'Update a patient registration', 'Patient Registration'
	END
	
GO
-------------------------------------------------------

-- grant permission on the stored procedure
GRANT EXECUTE ON dental.UpdatePatientRegistration TO db_dentalusers
GO
-------------------------------------------------------------

-- verifies if the procedure "UpdateMedicalPrescriptionPatientRegistration" exist
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dental' AND SPECIFIC_NAME = N'UpdateMedicalPrescriptionPatientRegistration')
   DROP PROCEDURE dental.UpdateMedicalPrescriptionPatientRegistration
GO

CREATE PROCEDURE dental.UpdateMedicalPrescriptionPatientRegistration
	
	@sysid_registration varchar (50) = '',
	@medical_prescription varchar (8000) = '',

	@edited_by varchar(50) = ''
	
AS

	IF (dental.IsSystemAdminSystemUserInfo(@edited_by) = 1) OR (dental.IsDentalUsersSystemUserInfo(@edited_by) = 1)
	BEGIN

		UPDATE dental.patient_registration SET
			medical_prescription = @medical_prescription,
			edited_on = GETDATE(),
			edited_by = @edited_by
		WHERE
			sysid_registration = @sysid_registration

	END
	ELSE
	BEGIN
		EXECUTE dental.ShowErrorMsg 'Update a patient registration', 'Patient Registration'
	END
	
GO
-------------------------------------------------------

-- grant permission on the stored procedure
GRANT EXECUTE ON dental.UpdateMedicalPrescriptionPatientRegistration TO db_dentalusers
GO
-------------------------------------------------------------

-- verifies if the procedure "SelectByPatientIDPatientRegistration" exist
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dental' AND SPECIFIC_NAME = N'SelectByPatientIDPatientRegistration')
   DROP PROCEDURE dental.SelectByPatientIDPatientRegistration
GO

CREATE PROCEDURE dental.SelectByPatientIDPatientRegistration
	
	@sysid_patient varchar (50) = '',
	@system_user_id varchar(50) = ''
	
AS

	IF (dental.IsSystemAdminSystemUserInfo(@system_user_id) = 1) OR (dental.IsDentalUsersSystemUserInfo(@system_user_id) = 1)
	BEGIN

		SELECT
			pr.sysid_registration AS sysid_registration,
			pr.registration_date AS registration_date,
			pr.medical_prescription AS medical_prescription,
			dental.GetTotalAmountPayableRegistrationDetails(pr.sysid_registration) AS amount_payable,
			dental.GetTotalAmountPaidPaymentDetails(pr.sysid_registration) AS amount_paid,
			dental.GetTotalDiscountPaymentDetails(pr.sysid_registration) AS discount,
			(dental.GetTotalAmountPayableRegistrationDetails(pr.sysid_registration) - 
							(dental.GetTotalAmountPaidPaymentDetails(pr.sysid_registration) + 
							dental.GetTotalDiscountPaymentDetails(pr.sysid_registration))) AS amount_balance		
		FROM
			dental.patient_registration AS pr
		WHERE
			pr.sysid_patient = @sysid_patient
		ORDER BY
			pr.registration_date DESC

	END
	ELSE
	BEGIN
		EXECUTE dental.ShowErrorMsg 'Query a patient registration', 'Patient Registration'
	END
	
GO
-------------------------------------------------------

-- grant permission on the stored procedure
GRANT EXECUTE ON dental.SelectByPatientIDPatientRegistration TO db_dentalusers
GO
-------------------------------------------------------------

-- verifies if the procedure "SelectByProcedureTakenPatientRegistration" exist
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dental' AND SPECIFIC_NAME = N'SelectByProcedureTakenPatientRegistration')
   DROP PROCEDURE dental.SelectByProcedureTakenPatientRegistration
GO

CREATE PROCEDURE dental.SelectByProcedureTakenPatientRegistration
	
	@sysid_patient varchar (50) = '',
	@system_user_id varchar(50) = ''
	
AS

	IF (dental.IsSystemAdminSystemUserInfo(@system_user_id) = 1) OR (dental.IsDentalUsersSystemUserInfo(@system_user_id) = 1)
	BEGIN

		SELECT
			pr.sysid_registration AS sysid_registration,
			pr.registration_date AS registration_date,
			pr.medical_prescription AS medical_prescription,
			rd.date_administered AS date_administered,
			rd.tooth_no AS tooth_no,
			rd.amount AS amount,
			pri.procedure_name AS procedure_name
		FROM
			dental.patient_registration AS pr
		INNER JOIN dental.registration_details AS rd ON rd.sysid_registration = pr.sysid_registration
		INNER JOIN dental.procedure_information AS pri ON pri.sysid_procedure = rd.sysid_procedure
		WHERE
			pr.sysid_patient = @sysid_patient
		ORDER BY
			rd.date_administered DESC

	END
	ELSE
	BEGIN
		EXECUTE dental.ShowErrorMsg 'Query a patient registration', 'Patient Registration'
	END
	
GO
-------------------------------------------------------

-- grant permission on the stored procedure
GRANT EXECUTE ON dental.SelectByProcedureTakenPatientRegistration TO db_dentalusers
GO
-------------------------------------------------------------

-- verifies if the procedure "SelectForCashReportPatientRegistration" exist
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dental' AND SPECIFIC_NAME = N'SelectForCashReportPatientRegistration')
   DROP PROCEDURE dental.SelectForCashReportPatientRegistration
GO

CREATE PROCEDURE dental.SelectForCashReportPatientRegistration
	
	@date_from datetime,
	@date_to datetime,

	@system_user_id varchar(50) = ''
	
AS

	IF (dental.IsSystemAdminSystemUserInfo(@system_user_id) = 1) OR (dental.IsDentalUsersSystemUserInfo(@system_user_id) = 1)
	BEGIN

		SELECT
			pr.sysid_registration AS sysid_registration,
			pr.sysid_patient AS sysid_patient,
			pr.registration_date AS registration_date,
			pr.medical_prescription AS medical_prescription,
			pd.receipt_no AS receipt_no,
			pd.date_paid AS date_paid,
			pd.amount AS amount,
			pti.last_name AS last_name,
			pti.first_name AS first_name,
			pti.middle_name AS middle_name		
		FROM
			dental.patient_registration AS pr
		INNER JOIN dental.payment_details AS pd ON pd.sysid_registration = pr.sysid_registration
		INNER JOIN dental.patient_information AS pti ON pti.sysid_patient = pr.sysid_patient
		WHERE
			pd.date_paid BETWEEN @date_from AND @date_to
		ORDER BY
			pd.date_paid DESC

	END
	ELSE
	BEGIN
		EXECUTE dental.ShowErrorMsg 'Query a patient registration', 'Patient Registration'
	END
	
GO
-------------------------------------------------------

-- grant permission on the stored procedure
GRANT EXECUTE ON dental.SelectForCashReportPatientRegistration TO db_dentalusers
GO
-------------------------------------------------------------

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
			dental.GetTotalAmountPayableRegistrationDetails(pr.sysid_registration) AS amount_payable,
			dental.GetTotalAmountPaidPaymentDetails(pr.sysid_registration) AS amount_paid,
			dental.GetTotalDiscountPaymentDetails(pr.sysid_registration) AS discount,
			(dental.GetTotalAmountPayableRegistrationDetails(pr.sysid_registration) - 
							(dental.GetTotalAmountPaidPaymentDetails(pr.sysid_registration) + 
							dental.GetTotalDiscountPaymentDetails(pr.sysid_registration))) AS amount_balance,
			pti.last_name AS last_name,
			pti.first_name AS first_name,
			pti.middle_name AS middle_name				
		FROM
			dental.patient_registration AS pr
		INNER JOIN dental.patient_information AS pti ON pti.sysid_patient = pr.sysid_patient
		WHERE
			(dental.GetTotalAmountPayableRegistrationDetails(pr.sysid_registration) - 
							(dental.GetTotalAmountPaidPaymentDetails(pr.sysid_registration) + 
							dental.GetTotalDiscountPaymentDetails(pr.sysid_registration))) > 0
		ORDER BY
			pr.sysid_registration DESC

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

-- verifies if the procedure "SelectForSalesPatientRegistration" exist
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dental' AND SPECIFIC_NAME = N'SelectForSalesPatientRegistration')
   DROP PROCEDURE dental.SelectForSalesPatientRegistration
GO

CREATE PROCEDURE dental.SelectForSalesPatientRegistration
	
	@date_from datetime,
	@date_to datetime,
	@sysid_procedure varchar (50) = '',	

	@system_user_id varchar(50) = ''
	
AS

	IF (dental.IsSystemAdminSystemUserInfo(@system_user_id) = 1) OR (dental.IsDentalUsersSystemUserInfo(@system_user_id) = 1)
	BEGIN

		SELECT
			pr.sysid_registration AS sysid_registration,
			pr.sysid_patient AS sysid_patient,
			pr.registration_date AS registration_date,
			pr.medical_prescription AS medical_prescription,
			rd.date_administered AS date_administered,
			rd.tooth_no AS tooth_no,
			rd.amount AS amount,			
			pti.last_name AS last_name,
			pti.first_name AS first_name,
			pti.middle_name AS middle_name,
			pri.procedure_name AS procedure_name	
		FROM
			dental.patient_registration AS pr
		INNER JOIN dental.registration_details AS rd ON rd.sysid_registration = pr.sysid_registration
		INNER JOIN dental.patient_information AS pti ON pti.sysid_patient = pr.sysid_patient
		INNER JOIN dental.procedure_information AS pri ON pri.sysid_procedure = rd.sysid_procedure
		WHERE
			rd.sysid_procedure = @sysid_procedure AND rd.date_administered BETWEEN @date_from AND @date_to
		ORDER BY
			rd.date_administered DESC

	END
	ELSE
	BEGIN
		EXECUTE dental.ShowErrorMsg 'Query a patient registration', 'Patient Registration'
	END
	
GO
-------------------------------------------------------

-- grant permission on the stored procedure
GRANT EXECUTE ON dental.SelectForSalesPatientRegistration TO db_dentalusers
GO
-------------------------------------------------------------

-- verifies if the procedure "GetCountPatientRegistration" exist
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dental' AND SPECIFIC_NAME = N'GetCountPatientRegistration')
   DROP PROCEDURE dental.GetCountPatientRegistration
GO

CREATE PROCEDURE dental.GetCountPatientRegistration

	@system_user_id varchar (50) = ''
AS

	IF (dental.IsSystemAdminSystemUserInfo(@system_user_id) = 1) OR (dental.IsDentalUsersSystemUserInfo(@system_user_id) = 1)
	BEGIN

		SELECT COUNT(sysid_registration) FROM dental.patient_registration 

	END
	BEGIN
		EXECUTE dental.ShowErrorMsg 'Query a patient registration', 'Patient Registration'
	END
	
GO
-------------------------------------------------------

-- grant permission on the stored procedure
GRANT EXECUTE ON dental.GetCountPatientRegistration TO db_dentalusers
GO
-------------------------------------------------------------

-- verifies if the procedure "IsExistsSysIDPatientRegistration" exist
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dental' AND SPECIFIC_NAME = N'IsExistsSysIDPatientRegistration')
   DROP PROCEDURE dental.IsExistsSysIDPatientRegistration
GO

CREATE PROCEDURE dental.IsExistsSysIDPatientRegistration

	@sysid_registration varchar (50) = '',
	@system_user_id varchar (50) = ''
AS

	IF (dental.IsSystemAdminSystemUserInfo(@system_user_id) = 1) OR (dental.IsDentalUsersSystemUserInfo(@system_user_id) = 1)
	BEGIN

		SELECT dental.IsExistSysIDPatientReg(@sysid_registration)

	END
	BEGIN
		EXECUTE dental.ShowErrorMsg 'Query a patient registration', 'Patient Registration'
	END
	
GO
-------------------------------------------------------

-- grant permission on the stored procedure
GRANT EXECUTE ON dental.IsExistsSysIDPatientRegistration TO db_dentalusers
GO
-------------------------------------------------------------

-- verifies if the "IsExistSysIDPatientReg" function already exist
IF OBJECT_ID (N'dental.IsExistSysIDPatientReg') IS NOT NULL
   DROP FUNCTION dental.IsExistSysIDPatientReg
GO

CREATE FUNCTION dental.IsExistSysIDPatientReg
(	
	@sysid_registration varchar(50) = ''
)
RETURNS bit
AS
BEGIN
	
	DECLARE @result bit

	SET @result = 0

	IF EXISTS (SELECT * FROM dental.patient_registration WHERE sysid_registration = @sysid_registration)
	BEGIN
		SET @result = 1
	END	
	
	RETURN @result
END
GO
------------------------------------------------------

-- ################################################END TABLE "patient_registration" OBJECTS######################################################



-- ################################################TABLE "registration_details" OBJECTS######################################################
-- verifies if the registration_details table exists
IF OBJECT_ID('dental.registration_details', 'U') IS NOT NULL
	DROP TABLE dental.registration_details
GO

CREATE TABLE dental.registration_details 			
(
	details_id bigint NOT NULL IDENTITY (1, 1)
		CONSTRAINT Registration_Details_Details_ID_PK PRIMARY KEY (details_id),
	sysid_registration varchar (50) NOT NULL
		CONSTRAINT Registration_Details_SysID_Registration_FK FOREIGN KEY REFERENCES dental.patient_registration(sysid_registration) ON UPDATE NO ACTION,
	sysid_procedure varchar (50) NOT NULL
		CONSTRAINT Registration_Details_SysID_Procedure_FK FOREIGN KEY REFERENCES dental.procedure_information(sysid_procedure) ON UPDATE NO ACTION,

	date_administered datetime NOT NULL DEFAULT (GETDATE()),
	tooth_no varchar (50) NULL DEFAULT (''),
	amount decimal (12, 2) NOT NULL DEFAULT (0),
	remarks varchar (1000) NULL,	

	created_on datetime NOT NULL DEFAULT (GETDATE()),
	created_by varchar (50) NOT NULL
		CONSTRAINT Registration_Details_Created_By_FK FOREIGN KEY REFERENCES dental.system_user_info(system_user_id) ON UPDATE NO ACTION,
	
	edited_on datetime NULL,
	edited_by varchar (50) NULL	
		CONSTRAINT Registration_Details_Edited_By_FK FOREIGN KEY REFERENCES dental.system_user_info(system_user_id) ON UPDATE NO ACTION
	
) ON [PRIMARY]
GO
------------------------------------------------------------------

-- create an index of the registration_details table
CREATE INDEX Registration_Details_Details_ID_Index
	ON dental.registration_details (details_id DESC)
GO
------------------------------------------------------------------

-- verifies if the procedure "InsertRegistrationDetails" exist
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dental' AND SPECIFIC_NAME = N'InsertRegistrationDetails')
   DROP PROCEDURE dental.InsertRegistrationDetails
GO

CREATE PROCEDURE dental.InsertRegistrationDetails
	
	@sysid_registration varchar (50) = '',
	@sysid_procedure varchar (50) = '',
	@date_administered datetime,
	@tooth_no varchar (50)  = '',
	@amount decimal (12, 2) = 0,
	@remarks varchar (1000) = '',

	@created_by varchar(50) = ''
	
AS

	IF (dental.IsSystemAdminSystemUserInfo(@created_by) = 1) OR (dental.IsDentalUsersSystemUserInfo(@created_by) = 1)
	BEGIN

		INSERT INTO dental.registration_details
		(
			sysid_registration,
			sysid_procedure,
			date_administered,
			tooth_no,
			amount,
			remarks,
			created_by
		)
		VALUES
		(
			@sysid_registration,
			@sysid_procedure,
			@date_administered,
			@tooth_no,
			@amount,
			@remarks,
			@created_by
		)

	END
	ELSE
	BEGIN
		EXECUTE dental.ShowErrorMsg 'Insert a registration details', 'Registration Details'
	END
	
GO
-------------------------------------------------------

-- grant permission on the stored procedure
GRANT EXECUTE ON dental.InsertRegistrationDetails TO db_dentalusers
GO
-------------------------------------------------------------

-- verifies if the procedure "UpdateRegistrationDetails" exist
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dental' AND SPECIFIC_NAME = N'UpdateRegistrationDetails')
   DROP PROCEDURE dental.UpdateRegistrationDetails
GO

CREATE PROCEDURE dental.UpdateRegistrationDetails
	
	@details_id bigint = 0,
	@sysid_procedure varchar (50) = '',
	@date_administered datetime,
	@tooth_no varchar (50) = '',
	@amount decimal (12, 2) = 0,
	@remarks varchar (1000) = '',

	@edited_by varchar(50) = ''
	
AS

	IF (dental.IsSystemAdminSystemUserInfo(@edited_by) = 1)
	BEGIN

		UPDATE dental.registration_details SET
			sysid_procedure = @sysid_procedure,
			date_administered = @date_administered,
			tooth_no = @tooth_no,
			amount = @amount,
			remarks = @remarks,
			edited_on = GETDATE(),
			edited_by = @edited_by
		WHERE
			details_id = @details_id
	END
	ELSE
	BEGIN
		EXECUTE dental.ShowErrorMsg 'Update a registration details', 'Registration Details'
	END
	
GO
-------------------------------------------------------

-- grant permission on the stored procedure
GRANT EXECUTE ON dental.UpdateRegistrationDetails TO db_dentalusers
GO
-------------------------------------------------------------

-- verifies if the procedure "DeleteRegistrationDetails" exist
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dental' AND SPECIFIC_NAME = N'DeleteRegistrationDetails')
   DROP PROCEDURE dental.DeleteRegistrationDetails
GO

CREATE PROCEDURE dental.DeleteRegistrationDetails
	
	@details_id bigint = 0,

	@deleted_by varchar(50) = ''
	
AS

	IF (dental.IsSystemAdminSystemUserInfo(@deleted_by) = 1)
	BEGIN

		IF EXISTS (SELECT * FROM registration_details WHERE details_id = @details_id)
		BEGIN

			DELETE FROM registration_details WHERE details_id = @details_id

		END

	END
	ELSE
	BEGIN
		EXECUTE dental.ShowErrorMsg 'Delete a registration details', 'Registration Details'
	END
	
GO
-------------------------------------------------------

-- grant permission on the stored procedure
GRANT EXECUTE ON dental.UpdateRegistrationDetails TO db_dentalusers
GO
-------------------------------------------------------------

-- verifies if the procedure "SelectByRegistrationIDRegistrationDetails" exist
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dental' AND SPECIFIC_NAME = N'SelectByRegistrationIDRegistrationDetails')
   DROP PROCEDURE dental.SelectByRegistrationIDRegistrationDetails
GO

CREATE PROCEDURE dental.SelectByRegistrationIDRegistrationDetails
	
	@sysid_registration varchar (50) = '',
	@system_user_id varchar(50) = ''
	
AS

	IF (dental.IsSystemAdminSystemUserInfo(@system_user_id) = 1) OR (dental.IsDentalUsersSystemUserInfo(@system_user_id) = 1)
	BEGIN

		SELECT
			rd.details_id AS details_id,
			rd.sysid_procedure AS sysid_procedure,
			rd.date_administered AS date_administered,
			rd.tooth_no AS tooth_no,
			rd.amount AS amount,
			rd.remarks AS remarks,
			pri.procedure_name AS procedure_name
		FROM
			dental.registration_details AS rd
		INNER JOIN dental.procedure_information AS pri ON pri.sysid_procedure = rd.sysid_procedure
		WHERE
			rd.sysid_registration = @sysid_registration
		ORDER BY
			rd.date_administered DESC

	END
	ELSE
	BEGIN
		EXECUTE dental.ShowErrorMsg 'Query a registration details', 'Registration Details'
	END
	
GO
-------------------------------------------------------

-- grant permission on the stored procedure
GRANT EXECUTE ON dental.SelectByRegistrationIDRegistrationDetails TO db_dentalusers
GO
-------------------------------------------------------------

-- verifies if the "GetTotalAmountPayableRegistrationDetails" function already exist
IF OBJECT_ID (N'dental.GetTotalAmountPayableRegistrationDetails') IS NOT NULL
   DROP FUNCTION dental.GetTotalAmountPayableRegistrationDetails
GO

CREATE FUNCTION dental.GetTotalAmountPayableRegistrationDetails
(	
	@sysid_registration varchar(50) = ''
)
RETURNS decimal (12, 2)
AS
BEGIN
	
	DECLARE @result decimal (12, 2)

	SET @result = 0

	SELECT @result = SUM(CASE WHEN NOT amount = 0 THEN amount ELSE 0 END) 
		FROM dental.registration_details WHERE sysid_registration = @sysid_registration
	
	RETURN @result
END
GO
------------------------------------------------------

-- ################################################END TABLE "registration_details" OBJECTS######################################################



-- ################################################TABLE "payment_details" OBJECTS######################################################
-- verifies if the payment_details_audit table exists
IF OBJECT_ID('dental.payment_details_audit', 'U') IS NOT NULL
	DROP TABLE dental.payment_details_audit
GO

CREATE TABLE dental.payment_details_audit 			
(
	receipt_no varchar (50) NOT NULL 
		CONSTRAINT Payment_Details_Audit_Receipt_No_PK PRIMARY KEY (receipt_no)
		CONSTRAINT Payment_Details_Audit_Receipt_No_CK CHECK (receipt_no LIKE 'RN%'),
	sysid_registration varchar (50) NOT NULL
		CONSTRAINT Payment_Details_Audit_SysID_Registration_FK FOREIGN KEY REFERENCES dental.patient_registration(sysid_registration) ON UPDATE NO ACTION,

	date_paid datetime NOT NULL DEFAULT (GETDATE()),
	amount decimal (12, 2) NOT NULL DEFAULT (0),
	discount decimal (12, 2) NULL,

	payment_type tinyint NOT NULL DEFAULT (0) -- 0 - Cash, 1 - Check, 2 - Card
		CONSTRAINT Payment_Details_Audit_Payment_Type_CK CHECK (payment_type >= 0 AND payment_type <= 2),
	bank_name varchar (50) NULL,
	check_no varchar (50) NULL,
	card_number varchar (50) NULL,
	card_type varchar (50) NULL,
	card_expire varchar (50) NULL,	

	deleted_on datetime NOT NULL DEFAULT (GETDATE()),
	deleted_by varchar (50) NOT NULL
		CONSTRAINT Payment_Details_Audit_Deleted_By_FK FOREIGN KEY REFERENCES dental.system_user_info(system_user_id) ON UPDATE NO ACTION
	
) ON [PRIMARY]
GO
------------------------------------------------------------------

-- create an index of the payment_details_audit table
CREATE INDEX Payment_Details_Audit_Receipt_No_Index
	ON dental.payment_details_audit (receipt_no DESC)
GO
------------------------------------------------------------------

-- verifies if the payment_details table exists
IF OBJECT_ID('dental.payment_details', 'U') IS NOT NULL
	DROP TABLE dental.payment_details
GO

CREATE TABLE dental.payment_details 			
(
	receipt_no varchar (50) NOT NULL 
		CONSTRAINT Payment_Details_Receipt_No_PK PRIMARY KEY (receipt_no)
		CONSTRAINT Payment_Details_Receipt_No_CK CHECK (receipt_no LIKE 'RN%'),
	sysid_registration varchar (50) NOT NULL
		CONSTRAINT Payment_Details_SysID_Registration_FK FOREIGN KEY REFERENCES dental.patient_registration(sysid_registration) ON UPDATE NO ACTION,

	date_paid datetime NOT NULL DEFAULT (GETDATE()),
	amount decimal (12, 2) NOT NULL DEFAULT (0),
	discount decimal (12, 2) NULL,

	payment_type tinyint NOT NULL DEFAULT (0) -- 0 - Cash, 1 - Check, 2 - Card
		CONSTRAINT Payment_Details_Payment_Type_CK CHECK (payment_type >= 0 AND payment_type <= 2),
	bank_name varchar (50) NULL,
	check_no varchar (50) NULL,
	card_number varchar (50) NULL,
	card_type varchar (50) NULL,
	card_expire varchar (50) NULL,	

	created_on datetime NOT NULL DEFAULT (GETDATE()),
	created_by varchar (50) NOT NULL
		CONSTRAINT Payment_Details_Created_By_FK FOREIGN KEY REFERENCES dental.system_user_info(system_user_id) ON UPDATE NO ACTION,
	
	edited_on datetime NULL,
	edited_by varchar (50) NULL	
		CONSTRAINT Payment_Details_Edited_By_FK FOREIGN KEY REFERENCES dental.system_user_info(system_user_id) ON UPDATE NO ACTION
	
) ON [PRIMARY]
GO
------------------------------------------------------------------

-- create an index of the payment_details table
CREATE INDEX Payment_Details_Receipt_No_Index
	ON dental.payment_details (receipt_no DESC)
GO
------------------------------------------------------------------

/*verifies that the trigger "Payment_Details_Trigger_Instead_Delete" already exist*/
IF OBJECT_ID ('dental.Payment_Details_Trigger_Instead_Delete','TR') IS NOT NULL
   DROP TRIGGER dental.Payment_Details_Trigger_Instead_Delete
GO

CREATE TRIGGER dental.Payment_Details_Trigger_Instead_Delete
	ON  dental.payment_details
	INSTEAD OF DELETE 
	NOT FOR REPLICATION
AS 

	DECLARE @receipt_no varchar (50)
	DECLARE @sysid_registration varchar (50)
	DECLARE @date_paid datetime
	DECLARE @amount decimal (12, 2)
	DECLARE @discount decimal (12, 2)
	DECLARE @payment_type tinyint
	DECLARE @bank_name varchar (50)
	DECLARE @check_no varchar (50)
	DECLARE @card_number varchar (50) 
	DECLARE @card_type varchar (50)
	DECLARE @card_expire varchar (50) 
	DECLARE @deleted_by varchar (50)
	
	SELECT 
		@receipt_no = receipt_no,
		@sysid_registration = sysid_registration,
		@date_paid = date_paid,
		@amount = amount,
		@discount = discount,
		@payment_type = payment_type,
		@bank_name = bank_name,
		@check_no = check_no,
		@card_number = card_number,
		@card_type = card_type,
		@card_expire = card_expire,
		@deleted_by = edited_by
	FROM DELETED	

	INSERT INTO dental.payment_details_audit
	(
		receipt_no,
		sysid_registration,
		date_paid,
		amount,
		discount,
		payment_type,
		bank_name,
		check_no,
		card_number,
		card_type,
		card_expire,
		deleted_by
	)
	VALUES
	(
		@receipt_no,
		@sysid_registration,
		@date_paid,
		@amount,
		@discount,
		@payment_type,
		@bank_name,
		@check_no,
		@card_number,
		@card_type,
		@card_expire,
		@deleted_by
	)

	DELETE FROM dental.payment_details WHERE receipt_no = @receipt_no
	
GO
/*-----------------------------------------------------------------*/


-- verifies if the procedure "InsertPaymentDetails" exist
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dental' AND SPECIFIC_NAME = N'InsertPaymentDetails')
   DROP PROCEDURE dental.InsertPaymentDetails
GO

CREATE PROCEDURE dental.InsertPaymentDetails
	
	@receipt_no varchar (50) = '',
	@sysid_registration varchar (50) = '',
	@date_paid datetime,
	@amount decimal (12, 2) = 0,
	@discount decimal (12, 2) = 0,
	@payment_type tinyint = 0,
	@bank_name varchar (50) = '',
	@check_no varchar (50) = '',
	@card_number varchar (50) = '',
	@card_type varchar (50) = '',
	@card_expire varchar (50) = '',

	@created_by varchar(50) = ''
	
AS

	IF (dental.IsSystemAdminSystemUserInfo(@created_by) = 1) OR (dental.IsDentalUsersSystemUserInfo(@created_by) = 1)
	BEGIN

		INSERT INTO dental.payment_details
		(
			receipt_no,
			sysid_registration,
			date_paid,
			amount,
			discount,
			payment_type,
			bank_name,
			check_no,
			card_number,
			card_type,
			card_expire,
			created_by
		)
		VALUES
		(
			@receipt_no,
			@sysid_registration,
			@date_paid,
			@amount,
			@discount,
			@payment_type,
			@bank_name,
			@check_no,
			@card_number,
			@card_type,
			@card_expire,
			@created_by
		)

	END
	ELSE
	BEGIN
		EXECUTE dental.ShowErrorMsg 'Insert a payment details', 'Payment Details'
	END
	
GO
-------------------------------------------------------

-- grant permission on the stored procedure
GRANT EXECUTE ON dental.InsertPaymentDetails TO db_dentalusers
GO
-------------------------------------------------------------

-- verifies if the procedure "UpdatePaymentDetails" exist
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dental' AND SPECIFIC_NAME = N'UpdatePaymentDetails')
   DROP PROCEDURE dental.UpdatePaymentDetails
GO

CREATE PROCEDURE dental.UpdatePaymentDetails
	
	@receipt_no varchar (50) = '',
	@date_paid datetime,
	@amount decimal (12, 2) = 0,
	@discount decimal (12, 2) = 0,
	@payment_type tinyint = 0,
	@bank_name varchar (50) = '',
	@check_no varchar (50) = '',
	@card_number varchar (50) = '',
	@card_type varchar (50) = '',
	@card_expire varchar (50) = '',

	@edited_by varchar(50) = ''
	
AS

	IF (dental.IsSystemAdminSystemUserInfo(@edited_by) = 1)
	BEGIN

		
		UPDATE dental.payment_details SET
			date_paid = @date_paid,
			amount = @amount,
			discount = @discount,
			payment_type = @payment_type,
			bank_name  = @bank_name,
			check_no = @check_no,
			card_number = @card_number,
			card_type = @card_type,
			card_expire = @card_expire,
			edited_on = GETDATE(),
			edited_by = @edited_by
		WHERE
			receipt_no = @receipt_no

	END
	ELSE
	BEGIN
		EXECUTE dental.ShowErrorMsg 'Update a payment details', 'Payment Details'
	END
	
GO
-------------------------------------------------------

-- grant permission on the stored procedure
GRANT EXECUTE ON dental.UpdatePaymentDetails TO db_dentalusers
GO
-------------------------------------------------------------

-- verifies if the procedure "DeletePaymentDetails" exist
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dental' AND SPECIFIC_NAME = N'DeletePaymentDetails')
   DROP PROCEDURE dental.DeletePaymentDetails
GO

CREATE PROCEDURE dental.DeletePaymentDetails
	
	@receipt_no varchar (50) = '',

	@deleted_by varchar(50) = ''
	
AS

	IF (dental.IsSystemAdminSystemUserInfo(@deleted_by) = 1)
	BEGIN
		
		UPDATE dental.payment_details SET
			edited_by = @deleted_by
		WHERE
			receipt_no = @receipt_no

		DELETE FROM dental.payment_details WHERE receipt_no = @receipt_no

	END
	ELSE
	BEGIN
		EXECUTE dental.ShowErrorMsg 'Delete a payment details', 'Payment Details'
	END
	
GO
-------------------------------------------------------

-- grant permission on the stored procedure
GRANT EXECUTE ON dental.DeletePaymentDetails TO db_dentalusers
GO
-------------------------------------------------------------

-- verifies if the procedure "SelectByRegistrationIDPaymentDetails" exist
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dental' AND SPECIFIC_NAME = N'SelectByRegistrationIDPaymentDetails')
   DROP PROCEDURE dental.SelectByRegistrationIDPaymentDetails
GO

CREATE PROCEDURE dental.SelectByRegistrationIDPaymentDetails
	
	@sysid_registration varchar (50) = '',
	@system_user_id varchar(50) = ''
	
AS

	IF (dental.IsSystemAdminSystemUserInfo(@system_user_id) = 1) OR (dental.IsDentalUsersSystemUserInfo(@system_user_id) = 1)
	BEGIN

		SELECT
			receipt_no,
			date_paid,
			amount,
			discount,
			payment_type,
			bank_name,
			check_no,
			card_number,
			card_type,
			card_expire			
		FROM
			dental.payment_details
		WHERE
			sysid_registration = @sysid_registration
		ORDER BY
			date_paid ASC

	END
	ELSE
	BEGIN
		EXECUTE dental.ShowErrorMsg 'Query a payment details', 'Payment Details'
	END
	
GO
-------------------------------------------------------

-- grant permission on the stored procedure
GRANT EXECUTE ON dental.SelectByRegistrationIDPaymentDetails TO db_dentalusers
GO
-------------------------------------------------------------

-- verifies if the procedure "GetCountPaymentDetails" exist
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dental' AND SPECIFIC_NAME = N'GetCountPaymentDetails')
   DROP PROCEDURE dental.GetCountPaymentDetails
GO

CREATE PROCEDURE dental.GetCountPaymentDetails

	@system_user_id varchar (50) = ''
AS

	IF (dental.IsSystemAdminSystemUserInfo(@system_user_id) = 1) OR (dental.IsDentalUsersSystemUserInfo(@system_user_id) = 1)
	BEGIN

		SELECT dental.CountTotalPaymentDetails()

	END
	BEGIN
		EXECUTE dental.ShowErrorMsg 'Query a payment details', 'Payment Details'
	END
	
GO
-------------------------------------------------------

-- grant permission on the stored procedure
GRANT EXECUTE ON dental.GetCountPaymentDetails TO db_dentalusers
GO
-------------------------------------------------------------

-- verifies if the procedure "IsExistsReceiptNoPaymentDetails" exist
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dental' AND SPECIFIC_NAME = N'IsExistsReceiptNoPaymentDetails')
   DROP PROCEDURE dental.IsExistsReceiptNoPaymentDetails
GO

CREATE PROCEDURE dental.IsExistsReceiptNoPaymentDetails

	@receipt_no varchar (50) = '',
	@system_user_id varchar (50) = ''
AS

	IF (dental.IsSystemAdminSystemUserInfo(@system_user_id) = 1) OR (dental.IsDentalUsersSystemUserInfo(@system_user_id) = 1)
	BEGIN

		SELECT dental.IsExistsReceiptNoPayment(@receipt_no)

	END
	BEGIN
		EXECUTE dental.ShowErrorMsg 'Query a payment details', 'Payment Details'
	END
	
GO
-------------------------------------------------------

-- grant permission on the stored procedure
GRANT EXECUTE ON dental.IsExistsReceiptNoPaymentDetails TO db_dentalusers
GO
-------------------------------------------------------------

-- verifies if the "IsExistsReceiptNoPayment" function already exist
IF OBJECT_ID (N'dental.IsExistsReceiptNoPayment') IS NOT NULL
   DROP FUNCTION dental.IsExistsReceiptNoPayment
GO

CREATE FUNCTION dental.IsExistsReceiptNoPayment
(	
	@receipt_no varchar(50) = ''
)
RETURNS bit
AS
BEGIN
	
	DECLARE @result bit

	SET @result = 0

	IF EXISTS (SELECT * FROM dental.payment_details WHERE receipt_no = @receipt_no)
	BEGIN
		SET @result = 1
	END	
	
	RETURN @result
END
GO
------------------------------------------------------

--verifies if the "CountTotalPaymentDetails" function already exist
IF OBJECT_ID (N'dental.CountTotalPaymentDetails') IS NOT NULL
   DROP FUNCTION dental.CountTotalPaymentDetails
GO

CREATE FUNCTION dental.CountTotalPaymentDetails
(		
)
RETURNS int
AS
BEGIN
	
	DECLARE @total int 
	DECLARE @total_audit int
	
	SELECT 
		@total = COUNT(receipt_no) 
	FROM 
		dental.payment_details

	SELECT 
		@total_audit = COUNT(receipt_no) 
	FROM 
		dental.payment_details_audit

	RETURN (@total + @total_audit)

END
GO
-------------------------------------------------------

-- verifies if the "GetTotalAmountPaidPaymentDetails" function already exist
IF OBJECT_ID (N'dental.GetTotalAmountPaidPaymentDetails') IS NOT NULL
   DROP FUNCTION dental.GetTotalAmountPaidPaymentDetails
GO

CREATE FUNCTION dental.GetTotalAmountPaidPaymentDetails
(	
	@sysid_registration varchar(50) = ''
)
RETURNS decimal (12, 2)
AS
BEGIN
	
	DECLARE @result decimal (12, 2)

	SET @result = 0

	IF EXISTS (SELECT * FROM dental.payment_details WHERE sysid_registration = @sysid_registration)
	BEGIN
		SELECT @result = SUM(CASE WHEN NOT amount = 0 THEN amount ELSE 0 END) 
			FROM dental.payment_details WHERE sysid_registration = @sysid_registration
	END
	
	RETURN @result
END
GO
------------------------------------------------------

-- verifies if the "GetTotalDiscountPaymentDetails" function already exist
IF OBJECT_ID (N'dental.GetTotalDiscountPaymentDetails') IS NOT NULL
   DROP FUNCTION dental.GetTotalDiscountPaymentDetails
GO

CREATE FUNCTION dental.GetTotalDiscountPaymentDetails
(	
	@sysid_registration varchar(50) = ''
)
RETURNS decimal (12, 2)
AS
BEGIN
	
	DECLARE @result decimal (12, 2)

	SET @result = 0

	IF EXISTS (SELECT * FROM dental.payment_details WHERE sysid_registration = @sysid_registration)
	BEGIN
		SELECT @result = SUM(CASE WHEN NOT discount = 0 THEN discount ELSE 0 END) 
			FROM dental.payment_details WHERE sysid_registration = @sysid_registration
	END
	
	RETURN @result
END
GO
------------------------------------------------------



-- ################################################END TABLE "payment_details" OBJECTS######################################################



-- ######################################RESTORE DEPENDENT TABLE CONSTRAINTS#############################################################
-- ###################################END RESTORE DEPENDENT TABLE CONSTRAINTS############################################################



-- ############################################INITIAL DATABASE INFORMATION#############################################################
-- ##########################################END INITIAL DATABASE INFORMATION#############################################################



