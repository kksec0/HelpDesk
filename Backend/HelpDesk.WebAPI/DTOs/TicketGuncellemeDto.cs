using HelpDesk.WebAPI.Models;

namespace HelpDesk.WebAPI.DTOs
{
    public class TicketGuncellemeDto
    {
        public string Baslik { get; set; } = string.Empty;
        public string Aciklama { get; set; } = string.Empty;
        public TicketOncelik Oncelik { get; set; }
    }
}
