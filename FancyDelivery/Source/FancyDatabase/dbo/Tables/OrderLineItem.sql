CREATE TABLE [dbo].[OrderLineItem]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [OrderId] UNIQUEIDENTIFIER NOT NULL, 
    [LineItemId] INT NOT NULL, 
    CONSTRAINT [FK_OrderLineItem_ToOrder] FOREIGN KEY (OrderId) REFERENCES [Order]([Id]), 
    CONSTRAINT [FK_OrderLineItem_ToLineItem] FOREIGN KEY ([LineItemId]) REFERENCES [LineItem]([Id])
)
