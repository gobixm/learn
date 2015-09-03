IF '$(DeployTestData)' = 'True'
BEGIN
  :r FillTestData.sql
END

EXECUTE [dbo].[ValidateProducts]