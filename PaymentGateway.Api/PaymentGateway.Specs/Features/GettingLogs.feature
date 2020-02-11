Feature: GettingLogs

Scenario: I can get all logs
	Given I have the following logs stored
		| Id     | RequestMethod | RequestPath         | ResponseStatusCode | TimeStamp         |
		| 1aa23b | GET           | /api/payment        | 200                | 11-Feb-2020 17:00 |
		| 2bb34c | GET           | /api/payment/9xx87z | 200                | 11-Feb-2020 17:10 |
		| 3cc45  | GET           | /api/payment/8jk94a | 404                | 11-Feb-2020 17:12 |
	When I get all logs
	Then the log view models with the following details are returned
		| Id     | RequestMethod | RequestPath         | ResponseStatusCode | TimeStamp         |
		| 1aa23b | GET           | /api/payment        | 200                | 11-Feb-2020 17:00 |
		| 2bb34c | GET           | /api/payment/9xx87z | 200                | 11-Feb-2020 17:10 |
		| 3cc45  | GET           | /api/payment/8jk94a | 404                | 11-Feb-2020 17:12 |

Scenario: I can try to get all logs when there are none
	When I get all logs
	Then the NotFound HTTP status code is returned with no logs