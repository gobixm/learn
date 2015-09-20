CREATE PROCEDURE [dbo].[GetDailyBalance]
AS
	WITH Balance as
	(
	SELECT o.Id as OrderId, o.CreateTime as OrderedDate, p.Name as ProductName, li.Price * li.Quantity as Amount  
	FROM dbo.LineItem li
	JOIN dbo.Product p on p.Id = li.ProductId
	JOIN dbo.OrderLineItem oli on oli.LineItemId = li.Id
	JOIN dbo.[Order] o on o.Id = oli.OrderId
	)
	SELECT *, SUM(Amount) OVER(PARTITION BY OrderedDate ORDER BY OrderId ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) as Total
	FROM Balance

RETURN
