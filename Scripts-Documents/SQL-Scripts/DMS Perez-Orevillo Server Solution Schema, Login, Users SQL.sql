/********************************************************/
/* This SQL Statements is used for the					*/
/* 		PEREZ-OREVILLO DENTAL MANAGEMENT SYSTEM			*/
/*Programmed by: Judyll Mark T. Agan					*/
/*Date created: December 11, 2007						*/
/********************************************************/

USE dbperezorevillodms
GO

-- ###################################################DATABASE SCHEMA, LOGINS, USERS, ROLES#############################################################

-- Drop a schema
IF EXISTS (SELECT * FROM sys.schemas WHERE NAME = 'dental')
BEGIN
	DROP SCHEMA dental
END
GO
---------------------------------------------

-- Drop a role
IF EXISTS (SELECT * FROM sys.database_principals WHERE TYPE IN ('R') AND NAME = 'db_dentalusers')
BEGIN
--	ALTER AUTHORIZATION ON SCHEMA::db_denydatareader TO db_denydatareader
--	ALTER AUTHORIZATION ON SCHEMA::db_denydatawriter TO db_denydatawriter
	EXEC sp_droprolemember db_dentalusers, p3r3zU$er
	DROP ROLE db_dentalusers
END
GO
--------------------------------------------

-- Drop a user
IF EXISTS (SELECT * FROM sys.database_principals WHERE TYPE IN ('S') AND NAME = 'p3r3zU$er') 
BEGIN
	DROP USER p3r3zU$er
END
GO
---------------------------------------------------

-- Drop a login
IF EXISTS (SELECT * FROM sys.server_principals WHERE TYPE IN ('S', 'U', 'G') AND NAME = 'dEnTaL0r1v3lloT0oTh')
BEGIN
	DROP LOGIN dEnTaL0r1v3lloT0oTh
END
GO
----------------------------------------------------

-- Create a login
CREATE LOGIN dEnTaL0r1v3lloT0oTh
	WITH PASSWORD = 'g@8_f6%2CbU8!(10gHnQl',
	DEFAULT_DATABASE = dbperezorevillodms
GO
--------------------------------

-- Create a user exclusive for the current login
CREATE USER p3r3zU$er FOR LOGIN dEnTaL0r1v3lloT0oTh WITH DEFAULT_SCHEMA = dental
GO
----------------------------------------------

-- Create a schema assigning it to a user
CREATE SCHEMA dental AUTHORIZATION p3r3zU$er
GO
------------------------------------------------

-- Create a role for the authorized user
CREATE ROLE db_dentalusers AUTHORIZATION p3r3zU$er
GO
------------------------------------------------

-- Adds a role member for the created role
EXEC sp_addrolemember db_denydatareader, db_dentalusers
EXEC sp_addrolemember db_denydatawriter, db_dentalusers
EXEC sp_addrolemember db_dentalusers, p3r3zU$er
GO

-- system views
SELECT * FROM sys.server_principals WHERE TYPE IN ('S', 'U', 'G') AND NAME = 'dEnTaL0r1v3lloT0oTh'
SELECT * FROM sys.database_principals WHERE TYPE IN ('S', 'U', 'G') AND NAME = 'p3r3zU$er'
SELECT * FROM sys.database_principals WHERE TYPE IN ('R') AND NAME = 'db_dentalusers'
SELECT * FROM sys.schemas
GO

SELECT * FROM sys.master_files
SELECT * FROM sys.servers
SELECT * FROM tempdb..sysobjects
GO

-- ###################################################END DATABASE SCHEMA, LOGINS, USERS, ROLES#######################################################