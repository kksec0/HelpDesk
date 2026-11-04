import { Component, OnInit, signal } from '@angular/core';
import { CardModule } from 'primeng/card';
import { TableModule } from 'primeng/table';
import { TagModule } from 'primeng/tag';
import { SelectButtonModule } from 'primeng/selectbutton';
import { FormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';

import { TicketService } from '../../core/services/ticket.service';
import { Ticket } from '../../core/models/ticket';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CardModule, TableModule, TagModule, FormsModule, SelectButtonModule, InputTextModule],
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
      ikon: 'pi pi-clock color-warning'
    },
    {
      baslik: 'Çözülen',
      sayi: data.filter(t => t.durum === 3).length,
      ikon: 'pi pi-check-circle color-success'
    }
  ]};
    durumAdi(durum: number): string {
    switch (durum) {
      case 1: return 'Açık';
      case 2: return 'İşlemde';
      case 3: return 'Çözüldü';
      default: return 'Bilinmiyor';
    }
    }

    oncelikAdi(oncelik: number): string {
      switch (oncelik) {
        case 1: return 'Düşük';
        case 2: return 'Orta';
        case 3: return 'Yüksek';
        case 4: return 'Acil';
        default: return 'Bilinmiyor';
      }
    }
    sizes = [
  { name: 'Small', value: 'small' },
  { name: 'Normal', value: undefined },
  { name: 'Large', value: 'large' }
  ];

  selectedSize: 'small' | 'large' | undefined = undefined;

      
}
