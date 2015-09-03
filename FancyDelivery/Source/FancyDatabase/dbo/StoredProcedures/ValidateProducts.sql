CREATE PROCEDURE [dbo].[ValidateProducts]
AS
	UPDATE [dbo].[Product] set Name = (select [dbo].ValidateProductName(Name))
RETURN
