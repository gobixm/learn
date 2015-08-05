Feature: DeleteArticle
	In order to remove article completely
	As a regular user
	I want to see artile removed from store

Scenario: Delete article request came
	Given I got working article service
	And I have article with valid id in store
	When this service recieved "delete article request with valid id"	
	And service handles "delete article request with valid id"
	Then this article should dissapear from store

Scenario: Delete article request came with nonexisting id
	Given I got working article service
	And I have article with valid id in store
	When this service recieved "delete article request with nonexisting id"
	Then this service should throw no error when handles "delete article request with nonexisting id"
