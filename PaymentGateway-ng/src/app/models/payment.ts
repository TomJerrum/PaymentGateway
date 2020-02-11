export class Payment {
    id: string;
    status: string;
    processedDate: Date;
    cardNumber: string;
    expiryDate: Date;
    amount: Number;
    currency: string;
    cvv: string;
}