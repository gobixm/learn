Feature: GetArticleHeaders
	In order to view list of available articles
	As a simple user
	I want to be told the list of article headers

Scenario: View list of articles
	Given I got working article service
	And I have article with valid id in store	
	When this service recieved "get article headers request"	
	And service handles "get article headers request"
	Then i can see article header in "get article response"
