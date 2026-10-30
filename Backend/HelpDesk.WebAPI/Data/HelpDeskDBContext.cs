using HelpDesk.WebAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace HelpDesk.WebAPI.Data
{
    public class HelpDeskDBContext : DbContext
    {
        public HelpDeskDBContext(DbContextOptions<HelpDeskDBContext> options)
            : base(options)
        {
        }

        public DbSet<Ticket> Tickets { get; set; }
    }
}
