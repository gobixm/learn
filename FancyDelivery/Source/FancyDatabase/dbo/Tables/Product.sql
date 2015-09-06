CREATE TABLE [dbo].[Product] (
    [Id]          INT             IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50)   NOT NULL,
    [Description] NVARCHAR (250)  NOT NULL,
    [ImageName]   NVARCHAR (250)  NOT NULL,
    [Price]       DECIMAL (19, 5) NOT NULL,
    [CategoryId]  INT             NOT NULL,
    [CreateTime] DATETIME NOT NULL DEFAULT GetDate(), 
    [ModifyTime] DATETIME NOT NULL DEFAULT GetDate(), 
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK1F94D86AEE4E26ED] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Category] ([Id]) ON DELETE CASCADE
);

