namespace HelpDesk.WebAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string AdSoyad { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public ICollection<Ticket> OlusturduguTicketlar { get; set; }
            = new List<Ticket>();


    }
}
