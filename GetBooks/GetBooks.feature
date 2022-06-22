# Verb	 GET
# Path   /Books
Feature: GetBooks


@mytag
Scenario: Get Books
	Given I have an authenticated user
	When A request is made to get Stephen King Book from API
	Then OK is returned from GET
	And The Green Mile is returned

	Scenario: Request Failure 400
	Given I have an authenticated user
	When A request is made to get Stephen King Book from API with invalid page number
	Then Bad Request is returned from GET