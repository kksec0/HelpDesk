namespace HelpDesk.WebAPI.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        public string Baslik { get; set; } = string.Empty;

        public string Aciklama { get; set; } = string.Empty;

        public TicketOncelik Oncelik { get; set; } = TicketOncelik.Orta;

        public TicketDurum Durum { get; set; } = TicketDurum.Acik;

        public DateTime OlusturmaTarihi { get; set; } = DateTime.UtcNow;
    }

    public enum TicketOncelik
    {
        Dusuk = 1,
        Orta = 2,
        Yuksek = 3,
        Acil = 4,
    }

    public enum TicketDurum
    {
        Acik = 1,
        Islemde = 2,
        Cozuldu = 3,
    }
}