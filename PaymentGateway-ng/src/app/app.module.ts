import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { AllLogsComponent } from './all-logs/all-logs.component';

@NgModule({
  declarations: [
    AppComponent,
    AllLogsComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent, AllLogsComponent]
})
export class AppModule { }
