# Verb	POST
# Path	/Books
Feature: CreateBook
	

@mytag
Scenario: Create a book
	Given I have an authenticated user
	When A request is made to create a book
	Then OK is returned from POST
	And The id is in the response

Scenario: Request Failure 400
	Given I have an authenticated user
	When A request is made to create a book with no title
	Then Bad Request is returned