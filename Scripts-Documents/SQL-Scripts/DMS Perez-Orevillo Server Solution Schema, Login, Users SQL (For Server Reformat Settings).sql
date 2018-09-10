/********************************************************/
/* This SQL Statements is used for the					*/
/* 		DENTAL MANAGEMENT SYSTEM						*/
/*Programmed by: Judyll Mark T. Agan					*/
/*Date created: December 11, 2007						*/
/********************************************************/

-- ###################################################DATABASE SCHEMA, LOGINS, USERS, ROLES#############################################################

-- Drop a login
IF EXISTS (SELECT * FROM sys.server_principals WHERE TYPE IN ('S', 'U', 'G') AND NAME = 'dEnTaL0r1v3lloT0oTh')
BEGIN
	DROP LOGIN dEnTaL0r1v3lloT0oTh
END
GO
----------------------------------------------------

USE dbperezorevillodms
GO

-- Create a login
CREATE LOGIN dEnTaL0r1v3lloT0oTh
	WITH PASSWORD = 'g@8_f6%2CbU8!(10gHnQl',
	DEFAULT_DATABASE = dbperezorevillodms
GO
--------------------------------

-- Alter a user exclusive for the current login
ALTER USER p3r3zU$er WITH LOGIN = dEnTaL0r1v3lloT0oTh
GO
----------------------------------------------


-- ###################################################END DATABASE SCHEMA, LOGINS, USERS, ROLES#######################################################