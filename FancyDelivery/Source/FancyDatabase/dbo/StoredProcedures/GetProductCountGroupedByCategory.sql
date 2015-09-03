CREATE PROCEDURE [dbo].[GetProductCountGroupedByCategory]
AS
SELECT c.Id, c.Name, COUNT(p.Id)
	FROM dbo.Product p 
	INNER JOIN dbo.Category c on c.Id = p.CategoryId
	GROUP BY C.Id, c.Name

