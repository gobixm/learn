CREATE TABLE [dbo].[Product] (
    [Id]          INT             IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50)   NULL,
    [Description] NVARCHAR (250)  NULL,
    [ImageName]   NVARCHAR (250)  NULL,
    [Price]       DECIMAL (19, 5) NULL,
    [CategoryId]  INT             NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK1F94D86AEE4E26ED] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Category] ([Id]) ON DELETE CASCADE
);

