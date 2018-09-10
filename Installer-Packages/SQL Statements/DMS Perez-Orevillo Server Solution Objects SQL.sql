--******************************************************--
-- This SQL Statements is used for the					--
-- 		PEREZ-OREVILLO DENTAL MANAGEMENT SYSTEM			--
--Programmed by: Judyll Mark T. Agan					--
--Date created: December 11, 2007						--
--SERVER SOLUTIONS										--
--******************************************************--

USE dbperezorevillodms
GO

-- ###########################################DROP TABLE CONSTRAINTS ##############################################################

--verifies if the System_User_Info_Access_Code_FK constraint exists--
IF OBJECT_ID('dental.system_user_info', 'U') IS NOT NULL
BEGIN
	ALTER TABLE dental.system_user_info
	DROP CONSTRAINT System_User_Info_Access_Code_FK
END
GO
-----------------------------------------------------

--verifies if the System_User_Info_Created_By_FK constraint exists--
IF OBJECT_ID('dental.system_user_info', 'U') IS NOT NULL
BEGIN
	ALTER TABLE dental.system_user_info
	DROP CONSTRAINT System_User_Info_Created_By_FK
END
GO
-----------------------------------------------------

--verifies if the System_User_Info_Edited_By_FK constraint exists--
IF OBJECT_ID('dental.system_user_info', 'U') IS NOT NULL
BEGIN
	ALTER TABLE dental.system_user_info
	DROP CONSTRAINT System_User_Info_Edited_By_FK
END
GO
-----------------------------------------------------


-- ########################################END DROP TABLE CONSTRAINTS ##############################################################



-- ########################################DROP DEPENDENT TABLE CONSTRAINTS ##############################################################

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

--verifies if the Payment_Details_Audit_Deleted_By_FK constraint exists--
IF OBJECT_ID('dental.payment_details_audit', 'U') IS NOT NULL
BEGIN
	ALTER TABLE dental.payment_details_audit
	DROP CONSTRAINT Payment_Details_Audit_Deleted_By_FK
END
GO
-----------------------------------------------------


-- ######################################END DROP DEPENDENT TABLE CONSTRAINTS ############################################################



-- #########################################################GENERAL PROCEDURES AND FUNCTIONS##################################################

-- verifies if the "ShowErrorMsg" procedure already exist
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dental' AND SPECIFIC_NAME = N'ShowErrorMsg')
   DROP PROCEDURE dental.ShowErrorMsg
GO

CREATE PROCEDURE dental.ShowErrorMsg

	@request varchar(200) = '',
	@table varchar(200) = ''

AS
	
	RAISERROR (N'%s \n%s %s \n%s %s \n\n%s', 10, 128, N'The server DENIED the current request.', 'Query: ',
				@request, 'Table: ', @table, N'Please contact the system administrator.') WITH NOWAIT

GO
----------------------------------------------------

-- verifies if the "GetServerDateTime" procedure already exist
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dental' AND SPECIFIC_NAME = N'GetServerDateTime')
   DROP PROCEDURE dental.GetServerDateTime
GO

CREATE PROCEDURE dental.GetServerDateTime
AS
	SELECT GETDATE()	
GO
----------------------------------------------------

-- grant permission on the stored procedure
GRANT EXECUTE ON dental.GetServerDateTime TO db_dentalusers
GO
-------------------------------------------------------------

-- ######################################################END GENERAL PROCEDURES AND FUNCTIONS###################################################



-- ################################################TABLE "system_access_code" OBJECTS######################################################

-- verifies if the system_access_code table exists
IF OBJECT_ID('dental.system_access_code', 'U') IS NOT NULL
	DROP TABLE dental.system_access_code
GO

CREATE TABLE dental.system_access_code 			
(
	access_code varchar (50) NOT NULL DEFAULT('')		-- @u^xBe$#HygPQ!&^FdCm<?IPaSq%{|!gFcI+_Te@ - System Administrator 
														-- @%PlMQ[!`;R25*2cX[$6nBQi+cTla#HB^-=QpZs@ - Dental Users
		CONSTRAINT System_Access_Code_Access_Code_PK PRIMARY KEY (access_code),
	access_index tinyint NOT NULL IDENTITY (0, 1),	
	access_description varchar (100) NOT NULL
		CONSTRAINT System_Access_Code_Access_Description_UQ UNIQUE (access_description)
	
) ON [PRIMARY]
GO
--------------------------------------------------------

-- verifies if the procedure "SelectSystemAccessCode" exist
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dental' AND SPECIFIC_NAME = N'SelectSystemAccessCode')
   DROP PROCEDURE dental.SelectSystemAccessCode
GO

CREATE PROCEDURE dental.SelectSystemAccessCode
	
	@system_user_id varchar(50) = ''
	
AS

	IF (dental.IsSystemAdminSystemUserInfo(@system_user_id) = 1)
	BEGIN

		SELECT
			access_code,
			access_index,
			access_description
		FROM
			dental.system_access_code
		ORDER BY
			access_index ASC
	END
	ELSE
	BEGIN
		EXECUTE dental.ShowErrorMsg 'Query system access code', 'System Access Code'
	END
	
GO
-------------------------------------------------------

-- grant permission on the stored procedure
GRANT EXECUTE ON dental.SelectSystemAccessCode TO db_dentalusers
GO
-------------------------------------------------------------

-- ################################################END TABLE "system_access_code" OBJECTS######################################################


-- ################################################TABLE "system_user_info" OBJECTS######################################################
-- verifies if the system_user_info table exists
IF OBJECT_ID('dental.system_user_info', 'U') IS NOT NULL
	DROP TABLE dental.system_user_info
GO

CREATE TABLE dental.system_user_info 			
(
	system_user_id varchar (50) NOT NULL 
		CONSTRAINT System_User_Info_System_User_ID_PK PRIMARY KEY (system_user_id),
	system_user_name varchar (50) NOT NULL,
	system_user_password varchar (50) NOT NULL,
	system_user_status bit NOT NULL DEFAULT (0),		-- 1 - Access Grant  0 - Access Denied 
	access_code varchar (50) NOT NULL 
		CONSTRAINT System_User_Info_Access_Code_FK FOREIGN KEY REFERENCES dental.system_access_code(access_code) ON UPDATE NO ACTION,
		
	last_name varchar (50) NOT NULL,
	first_name varchar (50) NOT NULL,
	middle_name varchar (50) NULL DEFAULT (''),
	position varchar (50) NULL DEFAULT (''),

	created_on datetime NOT NULL DEFAULT (GETDATE()),
	created_by varchar (50) NOT NULL
		CONSTRAINT System_User_Info_Created_By_FK FOREIGN KEY REFERENCES dental.system_user_info(system_user_id) ON UPDATE NO ACTION,
	
	edited_on datetime NULL,
	edited_by varchar (50) NULL	
		CONSTRAINT System_User_Info_Edited_By_FK FOREIGN KEY REFERENCES dental.system_user_info(system_user_id) ON UPDATE NO ACTION
	
) ON [PRIMARY]
GO
------------------------------------------------------------------

-- create an index of the system_user_info table using system_user_id as index key
CREATE INDEX System_User_Info_System_User_ID_Index
	ON dental.system_user_info (system_user_id ASC)
GO
------------------------------------------------------------------

-- verifies that the trigger "System_User_Info_Trigger_Instead_Insert" already exist
IF OBJECT_ID ('dental.System_User_Info_Trigger_Instead_Insert','TR') IS NOT NULL
   DROP TRIGGER dental.System_User_Info_Trigger_Instead_Insert
GO
		
CREATE TRIGGER dental.System_User_Info_Trigger_Instead_Insert
	ON  dental.system_user_info
	INSTEAD OF INSERT
	NOT FOR REPLICATION
AS 

	DECLARE @system_user_id varchar(50)
	DECLARE @system_user_name varchar(50)
	DECLARE @system_user_password varchar(50)
	DECLARE @system_user_status bit
	DECLARE @access_code varchar(50)
	DECLARE @last_name varchar(50)
	DECLARE @first_name varchar(50)
	DECLARE @middle_name varchar(50)
	DECLARE @position varchar(50) 
	DECLARE @created_by varchar(50)
	
	DECLARE @status varchar(20)
	
	SELECT 
		@system_user_id = system_user_id,
		@system_user_name = system_user_name,
		@system_user_password = system_user_password,
		@system_user_status = system_user_status,
		@access_code = access_code,
		@last_name = last_name,
		@first_name = first_name,
		@middle_name = middle_name,
		@position = position,
		@created_by = created_by		
	FROM INSERTED

	IF NOT EXISTS (SELECT * FROM system_user_info WHERE system_user_id = @system_user_id) AND
		EXISTS (SELECT * FROM system_user_info WHERE system_user_id = @created_by)
	BEGIN

		INSERT INTO dental.system_user_info
		(
			system_user_id,
			system_user_name,
			system_user_password,
			system_user_status,
			access_code,
			last_name,
			first_name,
			middle_name,
			position,
			created_by
		)
		VALUES
		(
			@system_user_id,
			@system_user_name,
			@system_user_password,
			@system_user_status,
			@access_code,
			@last_name,
			@first_name,
			@middle_name,
			@position,
			@created_by
		)		
	
	END
	ELSE
	BEGIN
		EXECUTE dental.ShowErrorMsg 'Insert a system user', 'System Users'
	END

GO
-------------------------------------------------------------------

-- verifies that the trigger "System_User_Info_Trigger_Instead_Update" already exist
IF OBJECT_ID ('dental.System_User_Info_Trigger_Instead_Update','TR') IS NOT NULL
   DROP TRIGGER dental.System_User_Info_Trigger_Instead_Update
GO

CREATE TRIGGER dental.System_User_Info_Trigger_Instead_Update
	ON  dental.system_user_info
	INSTEAD OF UPDATE
	NOT FOR REPLICATION
AS 

	DECLARE @system_user_id varchar(50)
	DECLARE @system_user_name varchar(50)
	DECLARE @system_user_password varchar(50)
	DECLARE @system_user_status bit
	DECLARE @access_code varchar(50)
	DECLARE @last_name varchar(50)
	DECLARE @first_name varchar(50)
	DECLARE @middle_name varchar(50)
	DECLARE @position varchar(50) 
	DECLARE @edited_by varchar(50)
	
	DECLARE @status varchar(20)
	
	SELECT 
		@system_user_id = system_user_id,
		@system_user_name = system_user_name,
		@system_user_password = system_user_password,
		@system_user_status = system_user_status,
		@access_code = access_code,
		@last_name = last_name,
		@first_name = first_name,
		@middle_name = middle_name,
		@position = position,
		@edited_by = edited_by		
	FROM INSERTED

	IF EXISTS (SELECT * FROM system_user_info WHERE system_user_id = @system_user_id) AND
		EXISTS (SELECT * FROM system_user_info WHERE system_user_id = @edited_by)
	BEGIN

		UPDATE system_user_info SET
			system_user_name = @system_user_name,
			system_user_password = @system_user_password,
			system_user_status = @system_user_status,
			access_code = @access_code,
			last_name = @last_name,
			first_name = @first_name,
			middle_name = @middle_name,
			position = @position,
			edited_on = GETDATE(),
			edited_by = NULL
		WHERE
			system_user_id = @system_user_id		

	END
	ELSE
	BEGIN
		EXECUTE dental.ShowErrorMsg 'Update a system user', 'System Users'
	END

GO
---------------------------------------------------------------------

-- verifies that the trigger "System_User_Info_Trigger_Instead_Delete" already exist
IF OBJECT_ID ('dental.System_User_Info_Trigger_Instead_Delete','TR') IS NOT NULL
   DROP TRIGGER dental.System_User_Info_Trigger_Instead_Delete
GO

CREATE TRIGGER dental.System_User_Info_Trigger_Instead_Delete
	ON  dental.system_user_info
	INSTEAD OF DELETE
	NOT FOR REPLICATION
AS 

	EXECUTE dental.ShowErrorMsg 'Delete a system user', 'System Users'
	
GO
-------------------------------------------------------------------------

-- verifies if the procedure "InsertSystemUserInfo" exist
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dental' AND SPECIFIC_NAME = N'InsertSystemUserInfo')
   DROP PROCEDURE dental.InsertSystemUserInfo
GO

CREATE PROCEDURE dental.InsertSystemUserInfo
	
	@system_user_id varchar(50) = '',
	@system_user_name varchar(50) = '',
	@system_user_password varchar(50) = '',
	@system_user_status bit = 0,
	@access_code varchar(50) = '',
	@last_name varchar(50) = '',
	@first_name varchar(50) = '',
	@middle_name varchar(50) = '',
	@position varchar(50) = '',

	@created_by varchar(50)
	
AS

	IF dental.IsSystemAdminSystemUserInfo(@created_by) = 1
	BEGIN

		INSERT INTO dental.system_user_info
		(
			system_user_id,
			system_user_name,
			system_user_password,
			system_user_status,
			access_code,
			last_name,
			first_name,
			middle_name,
			position,
			created_by
		)
		VALUES
		(
			@system_user_id,
			@system_user_name,
			@system_user_password,
			@system_user_status,
			@access_code,
			@last_name,
			@first_name,
			@middle_name,
			@position,
			@created_by
		)		

	END
	ELSE
	BEGIN
		EXECUTE dental.ShowErrorMsg 'Insert a system user', 'System Users'
	END
	
GO
-------------------------------------------------------

-- grant permission on the stored procedure
GRANT EXECUTE ON dental.InsertSystemUserInfo TO db_dentalusers
GO
-------------------------------------------------------------

-- verifies if the procedure "UpdateSystemUserInfo" exist
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dental' AND SPECIFIC_NAME = N'UpdateSystemUserInfo')
   DROP PROCEDURE dental.UpdateSystemUserInfo
GO

CREATE PROCEDURE dental.UpdateSystemUserInfo
	
	@system_user_id varchar(50) = '',
	@system_user_name varchar(50) = '',
	@system_user_password varchar(50) = '',
	@system_user_status bit = 0,
	@access_code varchar(50) = '',
	@last_name varchar(50) = '',
	@first_name varchar(50) = '',
	@middle_name varchar(50) = '',
	@position varchar(50) = '',

	@edited_by varchar(50)
	
AS

	IF dental.IsSystemAdminSystemUserInfo(@edited_by) = 1
	BEGIN

		UPDATE dental.system_user_info SET
			system_user_name = @system_user_name,
			system_user_password = @system_user_password,
			system_user_status = @system_user_status,
			access_code = @access_code,
			last_name = @last_name,
			first_name = @first_name,
			middle_name = @middle_name,
			position = @position,
			edited_on = GETDATE(),
			edited_by = @edited_by
		WHERE
			system_user_id = @system_user_id			

	END
	ELSE
	BEGIN
		EXECUTE dental.ShowErrorMsg 'Update a system user', 'System Users'
	END
	
GO
-------------------------------------------------------

-- grant permission on the stored procedure
GRANT EXECUTE ON dental.UpdateSystemUserInfo TO db_dentalusers
GO
-------------------------------------------------------------

-- verifies if the procedure "SelectSystemUserInfo" exist
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dental' AND SPECIFIC_NAME = N'SelectSystemUserInfo')
   DROP PROCEDURE dental.SelectSystemUserInfo
GO

CREATE PROCEDURE dental.SelectSystemUserInfo
	
	@system_user_id varchar(50) = ''
	
AS

	IF (dental.IsSystemAdminSystemUserInfo(@system_user_id) = 1)
	BEGIN

		SELECT
			system_user_id,
			system_user_name,			
			system_user_password,
			system_user_status,
			access_code,
			last_name,
			first_name,
			middle_name,
			position
		FROM
			dental.system_user_info
		WHERE
			NOT system_user_id = '#Ix@%*_!YzPqQrU$-+wNU*$-QeW#'
		ORDER BY
			system_user_name ASC
	END
	ELSE
	BEGIN
		EXECUTE dental.ShowErrorMsg 'Query system user information', 'System User Information'
	END
	
GO
-------------------------------------------------------

-- grant permission on the stored procedure
GRANT EXECUTE ON dental.SelectSystemUserInfo TO db_dentalusers
GO
-------------------------------------------------------------

-- verifies if the procedure "SelectForLogInSystemUserInfo" exist
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dental' AND SPECIFIC_NAME = N'SelectForLogInSystemUserInfo')
   DROP PROCEDURE dental.SelectForLogInSystemUserInfo
GO

CREATE PROCEDURE dental.SelectForLogInSystemUserInfo
	
	@system_user_name varchar(50) = '',
	@system_user_password varchar(50) = ''

AS

	SELECT 
		system_user_id,
		system_user_name,
		system_user_password,
		system_user_status,
		access_code,
		last_name,
		first_name,
		middle_name,
		position
	FROM 
		dental.system_user_info 
	WHERE 
		system_user_name = @system_user_name AND 
		system_user_password = @system_user_password AND 
		system_user_status = 1
	
GO
---------------------------------------------------------

-- grant permission on the stored procedure
GRANT EXECUTE ON dental.SelectForLogInSystemUserInfo TO db_dentalusers
GO
-------------------------------------------------------------

-- verifies if the procedure "IsExistsIDSystemUserInformation" exist
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dental' AND SPECIFIC_NAME = N'IsExistsIDSystemUserInformation')
   DROP PROCEDURE dental.IsExistsIDSystemUserInformation
GO

CREATE PROCEDURE dental.IsExistsIDSystemUserInformation
	
	@new_user_id varchar(50) = '',
	@system_user_id varchar(50) = ''
	
AS

	IF (dental.IsSystemAdminSystemUserInfo(@system_user_id) = 1)
	BEGIN

		SELECT dental.IsExistsIDSystemUserInfo(@new_user_id)

	END
	ELSE
	BEGIN
		EXECUTE dental.ShowErrorMsg 'Query system user information', 'System User Information'
	END
	
GO
-------------------------------------------------------

-- grant permission on the stored procedure
GRANT EXECUTE ON dental.IsExistsIDSystemUserInformation TO db_dentalusers
GO
-------------------------------------------------------------

-- verifies if the procedure "IsExistsNameSystemUserInformation" exist
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dental' AND SPECIFIC_NAME = N'IsExistsNameSystemUserInformation')
   DROP PROCEDURE dental.IsExistsNameSystemUserInformation
GO

CREATE PROCEDURE dental.IsExistsNameSystemUserInformation
	
	@system_user_name varchar(50) = '',
	@system_user_password varchar(50) = '',
	@system_user_id varchar(50) = ''
	
AS

	IF (dental.IsSystemAdminSystemUserInfo(@system_user_id) = 1)
	BEGIN

		SELECT dental.IsExistsNameSystemUserInfo(@system_user_name, @system_user_password, @system_user_id)

	END
	ELSE
	BEGIN
		EXECUTE dental.ShowErrorMsg 'Query system user information', 'System User Information'
	END
	
GO
-------------------------------------------------------

-- grant permission on the stored procedure
GRANT EXECUTE ON dental.IsExistsNameSystemUserInformation TO db_dentalusers
GO
-------------------------------------------------------------

-- verifies if the "IsExistsIDSystemUserInfo" function already exist
IF OBJECT_ID (N'dental.IsExistsIDSystemUserInfo') IS NOT NULL
   DROP FUNCTION dental.IsExistsIDSystemUserInfo
GO

CREATE FUNCTION dental.IsExistsIDSystemUserInfo
(	
	@new_user_id varchar(50) = ''
)
RETURNS bit
AS
BEGIN
	
	DECLARE @result bit

	SET @result = 0

	IF EXISTS (SELECT * FROM dental.system_user_info WHERE system_user_id = @new_user_id)
	BEGIN
		SET @result = 1
	END
	ELSE
	BEGIN
		SET @result = 0
	END
	
	RETURN @result
END
GO
------------------------------------------------------

-- verifies if the "IsExistsNameSystemUserInfo" function already exist
IF OBJECT_ID (N'dental.IsExistsNameSystemUserInfo') IS NOT NULL
   DROP FUNCTION dental.IsExistsNameSystemUserInfo
GO

CREATE FUNCTION dental.IsExistsNameSystemUserInfo
(	
	@system_user_name varchar(50) = '',
	@system_user_password varchar(50) = '',
	@system_user_id varchar(50) = ''

)
RETURNS bit
AS
BEGIN
	
	DECLARE @result bit

	SET @result = 0
	
	IF EXISTS (SELECT * FROM dental.system_user_info WHERE system_user_name LIKE @system_user_name OR 
				system_user_password LIKE @system_user_password AND NOT system_user_id = @system_user_id)
	BEGIN
		SET @result = 1
	END
	ELSE
	BEGIN
		SET @result = 0
	END
	
	RETURN @result

END
GO
------------------------------------------------------

-- verifies if the "IsSystemAdminSystemUserInfo" function already exist
IF OBJECT_ID (N'dental.IsSystemAdminSystemUserInfo') IS NOT NULL
   DROP FUNCTION dental.IsSystemAdminSystemUserInfo
GO

CREATE FUNCTION dental.IsSystemAdminSystemUserInfo
(	
	@system_user_id varchar(50) = ''
)
RETURNS bit
AS
BEGIN
	
	DECLARE @result bit

	IF EXISTS (SELECT * FROM dental.system_user_info WHERE system_user_id = @system_user_id AND access_code = '@u^xBe$#HygPQ!&^FdCm<?IPaSq%{|!gFcI+_Te@'
					AND system_user_status = 1)
	BEGIN
		SET @result = 1
	END
	ELSE
	BEGIN
		SET @result = 0
	END
	
	RETURN @result
END
GO
------------------------------------------------------

-- verifies if the "IsDentalUsersSystemUserInfo" function already exist
IF OBJECT_ID (N'dental.IsDentalUsersSystemUserInfo') IS NOT NULL
   DROP FUNCTION dental.IsDentalUsersSystemUserInfo
GO

CREATE FUNCTION dental.IsDentalUsersSystemUserInfo
(	
	@system_user_id varchar(50) = ''
)
RETURNS bit
AS
BEGIN
	
	DECLARE @result bit

	IF EXISTS (SELECT * FROM dental.system_user_info WHERE system_user_id = @system_user_id AND access_code = '@%PlMQ[!`;R25*2cX[$6nBQi+cTla#HB^-=QpZs@'
					AND system_user_status = 1)
	BEGIN
		SET @result = 1
	END
	ELSE
	BEGIN
		SET @result = 0
	END
	
	RETURN @result
END
GO
------------------------------------------------------

-- ##############################################END TABLE "system_access_code" OBJECTS######################################################




-- ######################################RESTORE DEPENDENT TABLE CONSTRAINTS#############################################################

--verifies if the Patient_Information_Created_By_FK constraint exists--
IF OBJECT_ID('dental.patient_information', 'U') IS NOT NULL
BEGIN
	ALTER TABLE dental.patient_information WITH NOCHECK
	ADD CONSTRAINT Patient_Information_Created_By_FK FOREIGN KEY (created_by) REFERENCES dental.system_user_info(system_user_id) ON UPDATE NO ACTION
END
GO
-----------------------------------------------------

--verifies if the Patient_Information_Edited_By_FK constraint exists--
IF OBJECT_ID('dental.patient_information', 'U') IS NOT NULL
BEGIN
	ALTER TABLE dental.patient_information WITH NOCHECK
	ADD CONSTRAINT Patient_Information_Edited_By_FK FOREIGN KEY (edited_by) REFERENCES dental.system_user_info(system_user_id) ON UPDATE NO ACTION
END
GO
-----------------------------------------------------

--verifies if the Procedure_Information_Created_By_FK constraint exists--
IF OBJECT_ID('dental.procedure_information', 'U') IS NOT NULL
BEGIN
	ALTER TABLE dental.procedure_information WITH NOCHECK
	ADD CONSTRAINT Procedure_Information_Created_By_FK FOREIGN KEY (created_by) REFERENCES dental.system_user_info(system_user_id) ON UPDATE NO ACTION
END
GO
-----------------------------------------------------

--verifies if the Procedure_Information_Edited_By_FK constraint exists--
IF OBJECT_ID('dental.procedure_information', 'U') IS NOT NULL
BEGIN
	ALTER TABLE dental.procedure_information WITH NOCHECK
	ADD CONSTRAINT Procedure_Information_Edited_By_FK FOREIGN KEY (edited_by) REFERENCES dental.system_user_info(system_user_id) ON UPDATE NO ACTION
END
GO
-----------------------------------------------------

--verifies if the Patient_Registration_Created_By_FK constraint exists--
IF OBJECT_ID('dental.patient_registration', 'U') IS NOT NULL
BEGIN
	ALTER TABLE dental.patient_registration WITH NOCHECK
	ADD CONSTRAINT Patient_Registration_Created_By_FK FOREIGN KEY (created_by) REFERENCES dental.system_user_info(system_user_id) ON UPDATE NO ACTION
END
GO
-----------------------------------------------------

--verifies if the Patient_Registration_Edited_By_FK constraint exists--
IF OBJECT_ID('dental.patient_registration', 'U') IS NOT NULL
BEGIN
	ALTER TABLE dental.patient_registration WITH NOCHECK
	ADD CONSTRAINT Patient_Registration_Edited_By_FK FOREIGN KEY (edited_by) REFERENCES dental.system_user_info(system_user_id) ON UPDATE NO ACTION
END
GO
-----------------------------------------------------

--verifies if the Registration_Details_Created_By_FK constraint exists--
IF OBJECT_ID('dental.registration_details', 'U') IS NOT NULL
BEGIN
	ALTER TABLE dental.registration_details WITH NOCHECK
	ADD CONSTRAINT Registration_Details_Created_By_FK FOREIGN KEY (created_by) REFERENCES dental.system_user_info(system_user_id) ON UPDATE NO ACTION
END
GO
-----------------------------------------------------

--verifies if the Registration_Details_Edited_By_FK constraint exists--
IF OBJECT_ID('dental.registration_details', 'U') IS NOT NULL
BEGIN
	ALTER TABLE dental.registration_details WITH NOCHECK
	ADD CONSTRAINT Registration_Details_Edited_By_FK FOREIGN KEY (edited_by) REFERENCES dental.system_user_info(system_user_id) ON UPDATE NO ACTION
END
GO
-----------------------------------------------------

--verifies if the Payment_Details_Created_By_FK constraint exists--
IF OBJECT_ID('dental.payment_details', 'U') IS NOT NULL
BEGIN
	ALTER TABLE dental.payment_details WITH NOCHECK
	ADD CONSTRAINT Payment_Details_Created_By_FK FOREIGN KEY (created_by) REFERENCES dental.system_user_info(system_user_id) ON UPDATE NO ACTION
END
GO
-----------------------------------------------------

--verifies if the Payment_Details_Edited_By_FK constraint exists--
IF OBJECT_ID('dental.payment_details', 'U') IS NOT NULL
BEGIN
	ALTER TABLE dental.payment_details WITH NOCHECK
	ADD CONSTRAINT Payment_Details_Edited_By_FK FOREIGN KEY (edited_by) REFERENCES dental.system_user_info(system_user_id) ON UPDATE NO ACTION
END
GO
-----------------------------------------------------

--verifies if the Payment_Details_Audit_Deleted_By_FK constraint exists--
IF OBJECT_ID('dental.payment_details_audit', 'U') IS NOT NULL
BEGIN
	ALTER TABLE dental.payment_details_audit WITH NOCHECK
	ADD CONSTRAINT Payment_Details_Audit_Deleted_By_FK FOREIGN KEY (deleted_by) REFERENCES dental.system_user_info(system_user_id) ON UPDATE NO ACTION
END
GO
-----------------------------------------------------



-- ###################################END RESTORE DEPENDENT TABLE CONSTRAINTS############################################################



-- ############################################INITIAL DATABASE INFORMATION#############################################################

-- for system_access_code
INSERT INTO dental.system_access_code(access_code, access_description) VALUES ('@u^xBe$#HygPQ!&^FdCm<?IPaSq%{|!gFcI+_Te@', 'System Administrator')
INSERT INTO dental.system_access_code(access_code, access_description) VALUES ('@%PlMQ[!`;R25*2cX[$6nBQi+cTla#HB^-=QpZs@', 'Dental Users')
GO

DISABLE TRIGGER dental.System_User_Info_Trigger_Instead_Insert ON dental.system_user_info
GO
INSERT INTO dental.system_user_info(system_user_id, system_user_name, system_user_password, system_user_status, access_code, last_name, first_name, middle_name, position, created_by)
	VALUES ('#Ix@%*_!YzPqQrU$-+wNU*$-QeW#', 'Judyll', 'd3nt@l', 1, '@u^xBe$#HygPQ!&^FdCm<?IPaSq%{|!gFcI+_Te@', 'Agan', 'Judyll Mark', 'Tinguha', 'Solution Developer', '#Ix@%*_!YzPqQrU$-+wNU*$-QeW#')
GO			
ENABLE TRIGGER dental.System_User_Info_Trigger_Instead_Insert ON dental.system_user_info
GO


-- ##########################################END INITIAL DATABASE INFORMATION#############################################################


