Feature: UserFeature

Scenario: Get all users
	Given I Get all users
	Then the result should contains a list of users
