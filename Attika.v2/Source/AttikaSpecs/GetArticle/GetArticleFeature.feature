Feature: GetArticleFeature
	In order to view published article
	As a consumer
	I want to see article title and description

Scenario: View existing article
	Given I got working article service
	And I have article with valid id in store	
	When this service recieved "get article request"	
	And service handles "get article request"
	Then i can see article text and title
