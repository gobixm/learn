Feature: NewArticle
	In order to publish new article
	As a tester
	I want to see new article was added to store

Scenario: Valid new article request came
	Given I got working article service
	When this service recieved "valid new article request"
	And service handles "valid new article request"
	Then desired article appeared in store

Scenario: Invalid new article request came
	Given I got working article service
	When this service recieved "invalid new article request"
	Then service fails to handle "invalid new article request"

Scenario: Request with long article title came
	Given I got working article service
	When this service recieved "long title request"
	Then service fails to handle "invalid new article request"

Scenario: Request with long article text came
	Given I got working article service
	When this service recieved "long text request"
	Then service fails to handle "invalid new article request"