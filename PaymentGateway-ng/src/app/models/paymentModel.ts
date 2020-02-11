export class PaymentModel {
    cardNumber: string = '';
    expiryDate: Date = new Date(2021, 12, 31);
    amount: Number = 0;
    currency: string = '';
    cvv: string = '';
}