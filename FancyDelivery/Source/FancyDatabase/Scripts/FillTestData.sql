IF NOT EXISTS(SELECT * FROM [dbo].[Category])
BEGIN

ALTER TABLE [dbo].[Category] NOCHECK CONSTRAINT ALL
SET IDENTITY_INSERT [dbo].[Category] ON

INSERT INTO [dbo].[Category] (Id, Code, Name)
VALUES
(1, 'burger', 'burgers'),
(2, 'pizza', 'pizzas'),
(3, 'drink', 'drinks'),
(4, 'pie', 'pies')

SET IDENTITY_INSERT [dbo].[Category] OFF
ALTER TABLE [dbo].[Category] WITH CHECK CHECK CONSTRAINT ALL

END

IF NOT EXISTS(SELECT * FROM [dbo].[Product])
BEGIN

ALTER TABLE [dbo].[Product] NOCHECK CONSTRAINT ALL

EXECUTE CreateDummyProductsForCategory @CategoryId = 1
EXECUTE CreateDummyProductsForCategory @CategoryId = 2
EXECUTE CreateDummyProductsForCategory @CategoryId = 3
EXECUTE CreateDummyProductsForCategory @CategoryId = 4

ALTER TABLE [dbo].[Product] WITH CHECK CHECK CONSTRAINT ALL

IF NOT EXISTS(SELECT * FROM [dbo].[OrderState] WHERE Id = 1)
	INSERT INTO [dbo].[OrderState] VALUES (1, 'placed', 'Pending', 'Order placed and waiting for processing.')

IF NOT EXISTS(SELECT * FROM [dbo].[OrderState] WHERE Id = 2)
	INSERT INTO [dbo].[OrderState] VALUES (2, 'shipped', 'In Delivery', 'Order paked and sent to listed address.')

IF NOT EXISTS(SELECT * FROM [dbo].[OrderState] WHERE Id = 3)
	INSERT INTO [dbo].[OrderState] VALUES (3, 'delivered', 'Completed', 'Order delivered to destination address.')

IF NOT EXISTS(SELECT * FROM [dbo].[OrderState] WHERE Id = 4)
	INSERT INTO [dbo].[OrderState] VALUES (4, 'cancelled', 'Cancelled', 'Order cancelled.')

END

IF NOT EXISTS(SELECT * FROM [dbo].[LineItem]) 
BEGIN
	ALTER TABLE [dbo].[LineItem] NOCHECK CONSTRAINT ALL
	INSERT INTO [dbo].[Order] (Id, Address, Email, OrderStateId)
		VALUES('11111111-1111-1111-1111-111111111111', 'address1', 'email1', 1),
			  ('22222222-2222-2222-2222-222222222222', 'address2', 'email2', 2),
			  ('33333333-3333-3333-3333-333333333333', 'address3', 'email3', 3),
			  ('44444444-4444-4444-4444-444444444444', 'address4', 'email4', 4)
	ALTER TABLE [dbo].[LineItem] WITH CHECK CHECK CONSTRAINT ALL

	ALTER TABLE [dbo].[OrderLineItem] NOCHECK CONSTRAINT ALL
	ALTER TABLE [dbo].[LineItem] NOCHECK CONSTRAINT ALL
	
	EXECUTE dbo.AddProductToOrder @orderId = '11111111-1111-1111-1111-111111111111', @productId = 1, @quantity = 5
	EXECUTE dbo.AddProductToOrder @orderId = '11111111-1111-1111-1111-111111111111', @productId = 2, @quantity = 1
	EXECUTE dbo.AddProductToOrder @orderId = '11111111-1111-1111-1111-111111111111', @productId = 3, @quantity = 1

	EXECUTE dbo.AddProductToOrder @orderId = '22222222-2222-2222-2222-222222222222', @productId = 1, @quantity = 3
	EXECUTE dbo.AddProductToOrder @orderId = '22222222-2222-2222-2222-222222222222', @productId = 2, @quantity = 1
	EXECUTE dbo.AddProductToOrder @orderId = '22222222-2222-2222-2222-222222222222', @productId = 3, @quantity = 1

	EXECUTE dbo.AddProductToOrder @orderId = '33333333-3333-3333-3333-333333333333', @productId = 1, @quantity = 1
	EXECUTE dbo.AddProductToOrder @orderId = '33333333-3333-3333-3333-333333333333', @productId = 2, @quantity = 3
	EXECUTE dbo.AddProductToOrder @orderId = '33333333-3333-3333-3333-333333333333', @productId = 3, @quantity = 1

	EXECUTE dbo.AddProductToOrder @orderId = '44444444-4444-4444-4444-444444444444', @productId = 1, @quantity = 2
	EXECUTE dbo.AddProductToOrder @orderId = '44444444-4444-4444-4444-444444444444', @productId = 2, @quantity = 1
	EXECUTE dbo.AddProductToOrder @orderId = '44444444-4444-4444-4444-444444444444', @productId = 3, @quantity = 10
		
	ALTER TABLE [dbo].[LineItem] WITH CHECK CHECK CONSTRAINT ALL
	ALTER TABLE [dbo].[OrderLineItem] WITH CHECK CHECK CONSTRAINT ALL
END

