CREATE PROCEDURE [dbo].[CreateDummyProductsForCategory]
	@CategoryId INT = 0	
AS
	DECLARE @categoryCode VARCHAR(50)
	SELECT @categoryCode = Code FROM [dbo].[Category] WHERE Id=@CategoryId
	
	DECLARE @products TABLE(
		Name varchar(50),
		[Description] varchar(250),
		ImageName varchar(250),
		Price decimal(19,5)		
	)

	DECLARE @row_count INT = 0
	WHILE @row_count<50 
	BEGIN
		INSERT INTO [dbo].Product (Name, [Description], ImageName, Price, CategoryId)
			VALUES(@categoryCode + CAST(@row_count as varchar(10)), 
					@categoryCode  + CAST(@row_count as varchar(10)) + ' description',
					@categoryCode + '_dummy_image.png',
					@row_count * 10 * @CategoryId,
					@CategoryId)
		SET @row_count = @row_count + 1
	END
RETURN
