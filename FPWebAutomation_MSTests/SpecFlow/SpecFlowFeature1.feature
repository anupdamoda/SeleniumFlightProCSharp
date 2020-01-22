Feature: Security JOT Token Expiry

@SecurityJOTokenPositiveScenario
Scenario: SecurityJOTToken_01
	Given the user is trying to access data in our system
	And the JOT token is not expired
	When the user is already logged in
	Then the information is shared

@SecurityJOTTokenPositiveScenario
	Scenario: SecurityJOTToken_02
Given the user is trying to access data in our system
When the JOT token is already expired
Then the system returns an error
