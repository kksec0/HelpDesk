using HelpDesk.WebAPI.Models;


namespace HelpDesk.WebAPI.DTOs

{
    public class TicketOlusturmaDto
    {
        public string Baslik {  get; set; } = string.Empty;
        public string Aciklama {  get; set; } = string.Empty;
        public TicketOncelik Oncelik { get; set; } = TicketOncelik.Orta;
    }
}
