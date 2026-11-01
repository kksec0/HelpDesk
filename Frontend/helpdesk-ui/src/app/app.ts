import { Component, signal, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { TicketService } from './services/ticket.service';
import { Ticket } from './models/ticket';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {

  protected readonly title = signal('helpdesk-ui');
  protected formAcik = signal(false);
  protected baslik = signal('');
  protected aciklama = signal('');
  protected oncelik = signal(2);

  protected tickets = signal<Ticket[]>([]);

  constructor(private ticketService: TicketService) {}

  ngOnInit() {
    this.ticketService.getTickets().subscribe({
      next: (data) => {
        this.tickets.set(data);
        console.log('API’den gelen talepler:', data);
      },
      error: (error) => {
        console.error('API hatası:', error);
      }
    });
  }

  
}