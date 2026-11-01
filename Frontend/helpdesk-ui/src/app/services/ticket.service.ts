import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Ticket } from '../models/ticket';

@Injectable({
  providedIn: 'root',
})
export class TicketService {

  private apiUrl = 'https://localhost:7293/api/Tickets';

  constructor(private http: HttpClient) {

  }
  getTickets() {
  return this.http.get<Ticket[]>(this.apiUrl);
  }




}
