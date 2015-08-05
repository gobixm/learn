Feature: AddArticleComment
	In order to add comment to article
	As a nasty commenter
	I want to see my comment seen to others

Scenario: Add valid comment
	Given I got working article service
	And I have article with valid id in store
	When this service recieved "addcomment request with valid id"	
	And service handles "addcomment request with valid id"
	Then this comment should be seen in valid article

Scenario: Add invalid comment
	Given I got working article service
	And I have article with valid id in store
	When this service recieved "addcomment request with invalid text"	
	And service handles "addcomment request with invalid text"
	Then this comment should not be stored
