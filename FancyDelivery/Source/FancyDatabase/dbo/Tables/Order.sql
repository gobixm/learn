CREATE TABLE [dbo].[Order]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Address] NVARCHAR(200) NOT NULL, 
    [Email] NVARCHAR(200) NOT NULL, 
    [OrderStateId] INT NOT NULL, 
    [CreateTime] DATETIME NOT NULL DEFAULT GetDate(), 
    [ModifyTime] DATETIME NOT NULL DEFAULT GetDate(), 
    CONSTRAINT [FK_Order_ToOrderState] FOREIGN KEY ([OrderStateId]) REFERENCES [OrderState]([Id]) 
)
