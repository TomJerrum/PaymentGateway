Feature: SubmittingAPayment

Scenario: I can submit a payment and it is successful
	Given I receive the following response from the bank service
		| PaymentId | PaymentStatus | ProcessedDate     |
		| 1aa23b    | Successful    | 10-Feb-2020 21:00 |
	When I submit the following payment
		| CardNumber | ExpiryDate  | Amount | Currency | CVV |
		| 12345678   | 01-Aug-2020 | 100.00 | GBP      | 123 |
	Then the payment id '1aa23b' is returned
	And the following payments are stored
		| Id     | Status     | ProcessedDate     | CardNumber | ExpiryDate  | Amount | Currency | CVV |
		| 1aa23b | Successful | 10-Feb-2020 21:00 | 12345678   | 01-Aug-2020 | 100.00 | GBP      | 123 |

Scenario: I can submit a payment and it is unsuccessful
	Given I receive the following response from the bank service
		| PaymentId | PaymentStatus | ProcessedDate     |
		| 1aa23b    | Unsuccessful  | 10-Feb-2020 21:00 |
	When I submit the following payment
		| CardNumber | ExpiryDate  | Amount | Currency | CVV |
		| 12345678   | 01-Aug-2020 | 100.00 | GBP      | 123 |
	Then the payment id '1aa23b' is returned
	And the following payments are stored
		| Id     | Status       | ProcessedDate     | CardNumber | ExpiryDate  | Amount | Currency | CVV |
		| 1aa23b | Unsuccessful | 10-Feb-2020 21:00 | 12345678   | 01-Aug-2020 | 100.00 | GBP      | 123 |