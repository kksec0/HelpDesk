using HelpDesk.WebAPI.Models;


namespace HelpDesk.WebAPI.DTOs
{
    public class TicketGetirDto
    {
        public int Id { get; set; }
        public string Baslik { get; set; } = string.Empty;
        public string Aciklama { get; set; } = string.Empty;
        public TicketOncelik Oncelik { get; set; }
        public TicketDurum Durum { get; set; }
        public DateTime OlusturmaTarihi { get; set; }


    }
}
