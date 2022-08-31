Feature: Get All Contacts

Given I have an application up and running with contacts
in the database, I must be able to get all contacts from the database

Scenario: Get all the contacts
	Given I have an application up and running
	When I call the  endpoint 'api/contacts' to get all contacts 
	Then must have a list with more than one contact with success
