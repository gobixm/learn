CREATE TABLE [dbo].[CartLineItem]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [CartId] UNIQUEIDENTIFIER NOT NULL, 
    [LineItemId] INT NOT NULL, 
    CONSTRAINT [FK_CartLineItem_ToCart] FOREIGN KEY ([CartId]) REFERENCES [Cart]([Id]),
	CONSTRAINT [FK_CartLineItem_ToLineItem] FOREIGN KEY ([LineItemId]) REFERENCES [LineItem]([Id])
)
