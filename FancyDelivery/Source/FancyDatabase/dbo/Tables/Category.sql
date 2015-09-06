CREATE TABLE [dbo].[Category] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Code] NVARCHAR (20) NOT NULL,
    [Name] NVARCHAR (50) NOT NULL,
    [CreateTime] DATETIME NOT NULL DEFAULT GetDate(), 
    [ModifyTime] DATETIME NOT NULL DEFAULT GetDate(), 
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

