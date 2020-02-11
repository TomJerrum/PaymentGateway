import { Component } from '@angular/core';
import { Log } from '../models/log';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'all-logs',
  templateUrl: './all-logs.component.html',
  styleUrls: []
})
export class AllLogsComponent {
  logs: Log[] = [];
  
  constructor(private http: HttpClient) { }

  getLogs() {
    this.http.get<Log[]>('http://localhost:5000/api/log')
      .subscribe(
        data => this.logs = data,
        error => this.logs = []
      );
  }

  clear() {
    this.logs = [];
  }
}
