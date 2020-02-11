import { Component } from '@angular/core';
import { Payment } from '../models/payment';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'all-payments',
  templateUrl: './all-payments.component.html',
  styleUrls: []
})
export class AllPaymentsComponent{

  payments: Payment[] = [];

  constructor(private http: HttpClient) { }

  getPayments() {
    this.http.get<Payment[]>('https://localhost:5001/api/payment')
      .subscribe(
        data => this.payments = data,
        error => this.payments = []
      );
  }

  clear() {
    this.payments = [];
  }
}
