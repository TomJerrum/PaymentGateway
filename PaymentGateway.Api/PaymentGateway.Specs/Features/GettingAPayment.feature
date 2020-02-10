Feature: GettingAPayment

Scenario Outline: I can get a payment by its id
	Given I have the following payments stored
		| Id     | Status       | ProcessedDate     | CardNumber | ExpiryDate  | Amount  | Currency | CVV |
		| 1aa23b | Unsuccessful | 10-Feb-2020 21:00 | 12345678   | 01-Aug-2020 | 100.00  | GBP      | 123 |
		| 2bb34c | Successful   | 10-Feb-2020 21:30 | 87354321   | 31-Feb-2020 | 2500.00 | EUR      | 456 |
		| 3cc45d | Successful   | 09-Feb-2020 05:45 | 12341234   | 31-Jan-2021 | 1250.00 | USD      | 789 |
	When I get the payment with the id '<PaymentId>'
	Then the payment view model with the following details is returned
		| Id          | Status   | ProcessedDate   | CardNumber   | ExpiryDate   | Amount   | Currency   | CVV   |
		| <PaymentId> | <Status> | <ProcessedDate> | <CardNumber> | <ExpiryDate> | <Amount> | <Currency> | <CVV> |

	Examples:
		| PaymentId | Status       | ProcessedDate     | CardNumber | ExpiryDate  | Amount  | Currency | CVV |
		| 3cc45d    | Successful   | 09-Feb-2020 05:45 | 12341234   | 31-Jan-2021 | 1250.00 | USD      | 789 |
		| 1aa23b    | Unsuccessful | 10-Feb-2020 21:00 | 12345678   | 01-Aug-2020 | 100.00  | GBP      | 123 |
		| 2bb34c    | Successful   | 10-Feb-2020 21:30 | 87354321   | 31-Feb-2020 | 2500.00 | EUR      | 456 |