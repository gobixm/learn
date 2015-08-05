Feature: DeleteArticleComment
	In order to delete nasty comments
	As a anministrator
	I want to remove comment from article

Scenario: Delete comment
	Given I got working article service
	And I have article with valid id in store	
	When this service recieved "delete article comment request"	
	And service handles "delete article comment request"
	Then this comment should dissaper from article scope
