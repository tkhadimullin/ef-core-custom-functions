CREATE DATABASE [test]
GO
USE [test]
GO
CREATE TABLE [dbo].[Models](
	[Id] [int] NOT NULL,
	[Encrypted] [varbinary](max) NOT NULL,
	[Encrypted2] [varbinary](max) NOT NULL,
	[Table2Id] [int] NULL,
 CONSTRAINT [PK_Model] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
))

GO
CREATE TABLE [dbo].[Table2](
	[Id] [int] NOT NULL,
	[IsSomething] [bit] NOT NULL,
 CONSTRAINT [PK_Table2] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
))
GO

CREATE SYMMETRIC KEY [TestKeyWithPassword] WITH ALGORITHM = AES_128 ENCRYPTION BY PASSWORD = 'TestPassword!@#'
GO
OPEN SYMMETRIC KEY [TestKeyWithPassword] DECRYPTION BY PASSWORD = 'TestPassword!@#'
GO
INSERT [dbo].[Models] ([Id], [Encrypted], [Encrypted2], [Table2Id]) 
VALUES (
	1, 
	ENCRYPTBYPASSPHRASE('TestPassword', 'Encrypted with Passphrapse'), 
	ENCRYPTBYKEY(key_GUID('TestKeyWithPassword'), 'Encrypted with Symmetric Key With Password'), 
	1)
GO
INSERT [dbo].[Table2] ([Id], [IsSomething]) VALUES (1, 1)
GO