CREATE TABLE [dbo].[LineItem]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [Quantity] INT NOT NULL DEFAULT 1, 
    [Price] DECIMAL(19, 5) NOT NULL, 
    [CreateTime] DATETIME NOT NULL DEFAULT GetDate(), 
    [ModifyTime] DATETIME NOT NULL DEFAULT GetDate(), 
    [ProductId] INT NOT NULL, 
    CONSTRAINT [FK_LineItem_ToProduct] FOREIGN KEY ([ProductId]) REFERENCES [Product]([Id])
)
