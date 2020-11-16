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

DROP TABLE IF EXISTS [dbo].[tblProgDecAdvisor]
DROP TABLE IF EXISTS [dbo].[tblAdvisor]
DROP TABLE IF EXISTS [dbo].[tblStudent]
DROP TABLE IF EXISTS [dbo].[tblProgDec]
DROP TABLE IF EXISTS [dbo].[tblProgram]
DROP TABLE IF EXISTS [dbo].[tblDegreeType]
DROP TABLE IF EXISTS [dbo].[tblUser]