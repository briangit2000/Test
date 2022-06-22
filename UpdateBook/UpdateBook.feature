# Verb	PUT
# Path  /Books
Feature: UpdateBooks


@mytag
Scenario: Update Book
	Given I have an authenticated user
	When A request is made to update book title and publisher
	Then OK is returned from UPDATE

	Scenario: Request Failure 400
	Given I have an authenticated user
	When A request is made to update book title and publisher with invalid id
	Then Bad Request is returned from UPDATE