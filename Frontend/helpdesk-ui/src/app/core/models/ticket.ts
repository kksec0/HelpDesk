export interface Ticket {
  id: number;
  baslik: string;
  aciklama: string;
  oncelik: number;
  durum: number;
  olusturmaTarihi: string;
  guncellenmeTarihi: string | null;
  ticketOlusturanId: number;
  ticketOlusturanName: string;
}

export interface TicketOlustur {
  baslik: string;
  aciklama: string;
  oncelik: number;
}