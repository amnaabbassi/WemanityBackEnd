Feature: Users

@mytag
Scenario: Get all User
	Given I whould Get all users
	Then the result should contains all users

Scenario: Login
	Given I whould Login with amna and coucou
	Then the result should be ok