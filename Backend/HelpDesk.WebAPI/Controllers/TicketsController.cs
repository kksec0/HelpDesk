using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HelpDesk.WebAPI.Data;
using HelpDesk.WebAPI.DTOs;





namespace HelpDesk.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly HelpDeskDBContext _context;

        public TicketsController(HelpDeskDBContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> TicketlarıGetir()
        {
            var tickets = await _context.Tickets.ToListAsync();

            var ticketDtos = tickets.Select(ticket => new TicketGetirDto

            {
                Id = ticket.Id,
                Baslik = ticket.Baslik,
                Aciklama = ticket.Aciklama,
                Oncelik = ticket.Oncelik,
                Durum = ticket.Durum,
                OlusturmaTarihi = ticket.OlusturmaTarihi,
            }).ToList();

            return Ok(ticketDtos);
        }
    }
}
