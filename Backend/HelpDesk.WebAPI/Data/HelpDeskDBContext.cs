using HelpDesk.WebAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.WebAPI.Data
{
    public class HelpDeskDBContext
        : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public HelpDeskDBContext(
            DbContextOptions<HelpDeskDBContext> options)
            : base(options)
        {
        }

        public DbSet<Ticket> Tickets { get; set; }
    }
}