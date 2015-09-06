CREATE FUNCTION [dbo].[GetHotProducts]
(
	@daysInterval int = 1,
	@resultCount int = 5
)
RETURNS @hotProducts TABLE
(
	Id int,
	Name nvarchar(50),
	[Description] nvarchar(250),
	Price decimal(19,5),
	SoldCount int
)
AS
BEGIN
	WITH Result(Id, Name, [Description], Price, SoldCount) AS
	(
		SELECT p.Id, p.Name, p.Description, p.Price, Sum(li.Quantity)  FROM dbo.Product p
		JOIN LineItem li on li.ProductId = p.Id
		JOIN OrderLineItem oli on oli.LineItemId = li.Id
		WHERE li.CreateTime > DATEADD(day, -@daysInterval, GETDATE())
		GROUP BY p.Id, p.Name, p.Description, p.Price
	)
	INSERT @hotProducts
	SELECT TOP(@resultCount) * FROM Result ORDER BY SoldCount DESC
	RETURN
END
