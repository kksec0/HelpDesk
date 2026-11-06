
using Microsoft.AspNetCore.Identity;

namespace HelpDesk.WebAPI.Models
{
    public class User : IdentityUser<int>
    {
        public string AdSoyad { get; set; } = string.Empty;

        public bool IlkGirisMi { get; set; } = true;

        public bool AktifMi { get; set; } = true;

        public ICollection<Ticket> OlusturduguTicketlar { get; set; }
            = new List<Ticket>();
    }
}
