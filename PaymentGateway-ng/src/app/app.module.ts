import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { AllPaymentsComponent } from './all-payments/all-payments.component';
import { AllLogsComponent } from './all-logs/all-logs.component';
import { PaymentFormComponent } from './payment-form/payment-form.component';

@NgModule({
  declarations: [
    AppComponent,
    AllPaymentsComponent,
    AllLogsComponent,
    PaymentFormComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent, AllPaymentsComponent, AllLogsComponent, PaymentFormComponent]
})
export class AppModule { }
