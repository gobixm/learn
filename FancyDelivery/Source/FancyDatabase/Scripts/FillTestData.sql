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

END
