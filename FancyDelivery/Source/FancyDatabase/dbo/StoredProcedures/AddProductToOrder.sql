CREATE PROCEDURE [dbo].[AddProductToOrder]
	@orderId uniqueidentifier,
	@productId int,
	@quantity int
AS
	DECLARE @price decimal(19,5) = (SELECT Price from dbo.Product WHERE Id = @productId)	
	
	INSERT INTO dbo.LineItem(ProductId, Quantity, Price) VALUES(@productId, @quantity, @price)

	DECLARE @lineItemId int = SCOPE_IDENTITY()

	INSERT INTO dbo.OrderLineItem(OrderId, LineItemId) VALUES(@orderId, @lineItemId)

RETURN 0
