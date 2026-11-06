import { Component, OnInit, signal } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { TicketService } from '../../core/services/ticket.service';
import { Ticket } from '../../core/models/ticket';
import { TableModule } from 'primeng/table';
import { DatePipe } from '@angular/common';
import { TagModule } from 'primeng/tag';
import { DialogModule } from 'primeng/dialog';
import { FormsModule } from '@angular/forms';
import { TextareaModule } from 'primeng/textarea';
import {InputTextModule} from 'primeng/inputtext';
import { SelectModule } from 'primeng/select';



@Component({
  selector: 'app-tickets',
  imports: [ButtonModule, TableModule, DatePipe, TagModule, DialogModule, FormsModule, TextareaModule, InputTextModule, SelectModule],
  standalone: true,
  templateUrl: './tickets.html',
  styleUrl: './tickets.css',
})
export class Tickets implements OnInit {

    tickets = signal<Ticket[]>([]);
    yukleniyor = signal(true);
    hata = signal('');
    dialogVisible = signal(false);

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

 // Etiketleri döndüren fonksiyonlar
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

  
 // Yeni ticket oluşturma dialogunu açan fonksiyon
    yeniTicketAc(): void {
      this.dialogVisible.set(true);
    }

    yeniTicket = {
    baslik: '',
    aciklama: '',
    oncelik: 2
    };

    oncelikSecenekleri = [
      { label: 'Düşük', value: 1 },
      { label: 'Orta', value: 2 },
      { label: 'Yüksek', value: 3 },
      { label: 'Acil', value: 4 }
    ];

  // Yeni ticket oluşturma fonksiyonu  
  ticketOlustur(): void {

  if (!this.yeniTicket.baslik.trim() || !this.yeniTicket.aciklama.trim()) {
    return;
  }

  this.ticketService.createTicket(this.yeniTicket).subscribe({
    next: (data) => {
      this.tickets.update(tickets => [...tickets, data]);

      this.dialogVisible.set(false);

      this.yeniTicket = {
        baslik: '',
        aciklama: '',
        oncelik: 2
      };
    },
    error: (error) => {
      console.error('Ticket oluşturulamadı:', error);
    }
    });

    }  


}


