import { Component, OnInit } from '@angular/core';
import { Payment } from '../models/payment';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'all-payments',
  templateUrl: './all-payments.component.html',
  styleUrls: []
})
export class AllPaymentsComponent implements OnInit {

  payments: Payment[] = [];

  constructor(private http: HttpClient) { }

  async ngOnInit() {
    this.getPayments();
  }

  getPayments() {
    this.http.get<Payment[]>('https://localhost:5001/api/payment')
      .subscribe(
        data => this.payments = data,
        error => this.payments = []
      );
  }
}
