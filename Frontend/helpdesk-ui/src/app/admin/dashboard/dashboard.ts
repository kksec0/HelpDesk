import { Component, OnInit, signal } from '@angular/core';
import { CardModule } from 'primeng/card';
import { TableModule } from 'primeng/table';
import { TagModule } from 'primeng/tag';

import { TicketService } from '../../core/services/ticket.service';
import { Ticket } from '../../core/models/ticket';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CardModule, TableModule, TagModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css'
})
export class Dashboard implements OnInit {

  constructor(private ticketService: TicketService) {}

  tickets = signal<Ticket[]>([]);
  yukleniyor = signal(true);
  hata = signal('');

  ngOnInit(): void {
    this.ticketService.getTickets().subscribe({
      next: (data) => {
        this.tickets.set(data);
        this.yukleniyor.set(false);
      },
      error: (error) => {
        console.error('Ticket API hatası:', error);
        this.hata.set('Ticket verileri yüklenemedi.');
        this.yukleniyor.set(false);
      }
    });
  }

  get istatistikler() {
  const data = this.tickets();

  return [
    {
      baslik: 'Toplam Ticket',
      sayi: data.length,
      ikon: 'pi pi-ticket'
    },
    {
      baslik: 'Açık Ticket',
      sayi: data.filter(t => t.durum === 1).length,
      ikon: 'pi pi-inbox'
    },
    {
      baslik: 'İşlemde',
      sayi: data.filter(t => t.durum === 2).length,
      ikon: 'pi pi-clock'
    },
    {
      baslik: 'Çözülen',
      sayi: data.filter(t => t.durum === 3).length,
      ikon: 'pi pi-check-circle'
    }
  ];
}

}