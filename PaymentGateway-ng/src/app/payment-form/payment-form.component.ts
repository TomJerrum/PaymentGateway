import { Component } from '@angular/core';
import { PaymentModel } from '../models/paymentModel';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';

@Component({
  selector: 'payment-form',
  templateUrl: './payment-form.component.html',
  styleUrls: []
})
export class PaymentFormComponent {
  model: PaymentModel = new PaymentModel;

  constructor(private http: HttpClient) { }

  submitPayment() {
    this.http.post('https://localhost:5001/api/payment', this.model).subscribe(data => {}, error => {});
  }
}
