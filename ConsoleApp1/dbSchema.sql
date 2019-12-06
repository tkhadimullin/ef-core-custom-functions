CREATE DATABASE [test]
GO
USE [test]
GO
CREATE TABLE [dbo].[Models](
	[Id] [int] NOT NULL,
	[Encrypted] [varbinary](max) NOT NULL,
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

INSERT [dbo].[Models] ([Id], [Encrypted], [Table2Id]) VALUES (1, 0x02000000BA726EA234C6655BDA75F83424C3BF43C41CE630012BB4EB4F1F15B3421D0F3A280C9185F9A5088243F7A7091D56BBD8, 1)
GO
INSERT [dbo].[Table2] ([Id], [IsSomething]) VALUES (1, 1)
GO