using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HelpDesk.WebAPI.Data;
using HelpDesk.WebAPI.DTOs;
using HelpDesk.WebAPI.Models;





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

        //Ticket listesini getir
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

        //Belirli ticketı getir
        [HttpGet("{id}")]
        public async Task<IActionResult> TicketGetir(int id)
        {
            var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == id);

            if (ticket == null)
            {
                return NotFound();

            }

            var ticketDto = new TicketGetirDto
            {
                Id = ticket.Id,
                Baslik = ticket.Baslik,
                Aciklama = ticket.Aciklama,
                Oncelik = ticket.Oncelik,
                Durum = ticket.Durum,
                OlusturmaTarihi = ticket.OlusturmaTarihi
            };
            return Ok(ticketDto);
        }

        //Ticket Oluşturma
        [HttpPost]
        public async Task<IActionResult> TicketOlustur(TicketOlusturmaDto dto)
        {
            var ticket = new Ticket
            {
                Baslik = dto.Baslik,
                Aciklama = dto.Aciklama,
                Oncelik = dto.Oncelik
            };

            _context.Tickets.Add(ticket);

            await _context.SaveChangesAsync();

            var ticketDto = new TicketGetirDto
            {
                Id = ticket.Id,
                Baslik = ticket.Baslik,
                Aciklama = ticket.Aciklama,
                Oncelik = ticket.Oncelik,
                Durum = ticket.Durum,
                OlusturmaTarihi = ticket.OlusturmaTarihi
            };

            return CreatedAtAction(nameof(TicketGetir), new { id = ticket.Id }, ticketDto);

        }

        //Ticket durum Güncelleme
        [HttpPut("{id}/durum")]
        public async Task<IActionResult> TicketGuncelle(int id, TicketDurumGuncellemeDto dto)
        {
            var ticket = await _context.Tickets
            .FirstOrDefaultAsync(t => t.Id == id);


            if (ticket == null)
            {
                return NotFound();
            }

            ticket.Durum = dto.Durum;

            await _context.SaveChangesAsync();

            return NoContent();


        }

        //Ticket Silme
        [HttpDelete("{id}")]
        public async Task<IActionResult> TicketSilme(int id)
        {
            var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == id);


            if (ticket == null)
            {
                return NotFound();
            }

            _context.Tickets.Remove(ticket);

            await _context.SaveChangesAsync();

            return NoContent();




        }







    }
}
