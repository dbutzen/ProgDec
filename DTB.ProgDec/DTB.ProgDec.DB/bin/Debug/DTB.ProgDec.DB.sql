﻿/*
Deployment script for DTB.ProgDec.DB

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "DTB.ProgDec.DB"
:setvar DefaultFilePrefix "DTB.ProgDec.DB"
:setvar DefaultDataPath "C:\Users\Dan\AppData\Local\Microsoft\VisualStudio\SSDT\DTB.ProgDec"
:setvar DefaultLogPath "C:\Users\Dan\AppData\Local\Microsoft\VisualStudio\SSDT\DTB.ProgDec"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

DROP TABLE IF EXISTS [dbo].[tblStudent]
DROP TABLE IF EXISTS [dbo].[tblProgDec]
DROP TABLE IF EXISTS [dbo].[tblProgram]
DROP TABLE IF EXISTS [dbo].[tblDegreeType]
GO

GO
/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

BEGIN
	INSERT INTO [dbo].tblStudent (Id, FirstName, LastName, StudentId)
	VALUES
	(1, 'Mickey', 'Mouse', '123456789'),
	(2, 'Minnie', 'Mouse', '111222333'),
	(3, 'Donald', 'Duck', '987654321'),
	(4, 'Claribelle', 'Cow', '333444555')
END
BEGIN
	INSERT INTO [dbo].tblDegreeType (ID, Description)
	VALUES 
	(1, 'Associate Degree'),
	(2, 'Technical Diploma'),
	(3, 'Certificate')
END
BEGIN
	INSERT INTO [dbo].tblProgram (Id, Description, DegreeTypeId)
	VALUES
	(1, 'Computer Support Specialist', 1),
	(2, 'Network Specialist', 1),
	(3, 'Network System Admin', 1),
	(4, 'Software Development', 1),
	(5, 'Web Development and Design', 1),
	(6, 'Help Desk Specialist', 2),
	(7, 'Database', 3),
	(8, 'Desktop Support', 3),
	(9, 'IT Security', 3),
	(10, 'Mobile Application Development', 3),
	(11, 'Network Administration', 3),
	(12, 'Network Infrastructure', 3),
	(13, 'PC Programming', 3),
	(14, 'Web Design', 2),
	(15, 'Web Design', 3),
	(16, 'Web Development', 3)
END

BEGIN
	INSERT INTO [dbo].tblProgDec (Id, ProgramId, StudentId, ChangeDate)
	VALUES
	(1, 4, 2, '2019-06-30'),
	(2, 10, 1, '2019-05-15'),
	(3, 14, 3, GETDATE())
END
GO

GO
PRINT N'Update complete.';


GO
