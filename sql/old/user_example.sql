USE DBCorpManagement
GO
INSERT INTO [dbo].[user] ([lastname], [firstname], [matricule], [idprofilelevel], [pointarticle], [gradepoint], [status], [lastupdatepoint]) VALUES ('Toto', 'Wiwi', 11223344, 1, 0, 3, 1, GETDATE());
GO
