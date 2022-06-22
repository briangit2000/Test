# Verb	DELETE
# Path	/Books{BookId}
Feature: DeleteBook

@mytag
Scenario: Delete Book
	Given I have an authenticated user
	And I have created a book
	When A request is made to delete a book
	Then OK is returned from DELETE

	Scenario: Request Failure 400
	Given I have an authenticated user
	When A request is made to delete Book by Id with no Id in url
	Then Bad Request is returned from DELETE