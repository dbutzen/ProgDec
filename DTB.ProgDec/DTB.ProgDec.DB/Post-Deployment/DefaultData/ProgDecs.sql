BEGIN
	INSERT INTO [dbo].tblProgDec (Id, ProgramId, StudentId, ChangeDate)
	VALUES
	(1, 4, 2, '2019-06-30'),
	(2, 10, 1, '2019-05-15'),
	(3, 14, 3, GETDATE())
END