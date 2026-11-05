import { Component, OnInit, signal } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { TicketService } from '../../core/services/ticket.service';
import { Ticket } from '../../core/models/ticket';
import { TableModule } from 'primeng/table';
import { DatePipe } from '@angular/common';
import { TagModule } from 'primeng/tag';



@Component({
  selector: 'app-tickets',
  imports: [ButtonModule, TableModule, DatePipe, TagModule],
  standalone: true,
  templateUrl: './tickets.html',
  styleUrl: './tickets.css',
})
export class Tickets implements OnInit {

    tickets = signal<Ticket[]>([]);
    yukleniyor = signal(true);
    hata = signal('');

    constructor(private ticketService: TicketService) {}

    ngOnInit(): void {
    this.ticketService.getTickets().subscribe({
      next: (data) => {
        this.tickets.set(data);
        this.yukleniyor.set(false);
      },
      error: () => {
        this.hata.set('Ticket kayıtları yüklenirken bir hata oluştu.');
        this.yukleniyor.set(false);
      }
    });

    
  }

  oncelikEtiketi(oncelik: number): string {
  switch (oncelik) {
    case 1: return 'Düşük';
    case 2: return 'Orta';
    case 3: return 'Yüksek';
    case 4: return 'Acil';
    default: return 'Bilinmiyor';
  }
}

  durumEtiketi(durum: number): string {
  switch (durum) {
    case 1: return 'Açık';
    case 2: return 'İşlemde';
    case 3: return 'Çözüldü';
    default: return 'Bilinmiyor';
  }
}


}


