# Verb	GET
# Path  /Books/{BookId}
Feature: GetBook
	

@mytag
Scenario: Request Successful - get Book
	Given I have an authenticated user
	When A request is made to get Book by Id
	Then OK is returned from GET by Id
	And Lord of the Rings is returned

	Scenario: Request Failure 500
	Given I have an authenticated user
	When A request is made to get Book by Id with Id that is not in the Database
	Then Internal Server Error is returned from GET by Id
	