CREATE TABLE [dbo].[OrderState]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Code] NCHAR(20) NOT NULL, 
    [Name] NCHAR(100) NOT NULL, 
    [Description] NVARCHAR(MAX) NOT NULL
)
