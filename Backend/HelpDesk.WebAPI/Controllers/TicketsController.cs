using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HelpDesk.WebAPI.Data;
using HelpDesk.WebAPI.DTOs;
using HelpDesk.WebAPI.Models;
using System.Net;





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

            var tickets = await _context.Tickets
                .Where(ticket =>!ticket.SilindiMi)
                .Include(ticket => ticket.TicketOlusturan)
                .ToListAsync();

            var ticketDtos = tickets.Select(ticket => new TicketGetirDto
            {
                Id = ticket.Id,
                Baslik = ticket.Baslik,
                Aciklama = ticket.Aciklama,
                Oncelik = ticket.Oncelik,
                Durum = ticket.Durum,
                OlusturmaTarihi = ticket.OlusturmaTarihi,
                GuncellenmeTarihi = ticket.GuncellenmeTarihi,
                TicketOlusturanId = ticket.TicketOlusturanId,
                TicketOlusturanName = ticket.TicketOlusturan.AdSoyad,
            }).ToList();

            return Ok(ticketDtos);
        }

        //Belirli ticketı getir
        [HttpGet("{id}")]
        public async Task<IActionResult> TicketGetir(int id)
        {
            var ticket = await _context.Tickets.Include(ticket => ticket.TicketOlusturan).FirstOrDefaultAsync(t => t.Id == id && !t.SilindiMi);

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
                OlusturmaTarihi = ticket.OlusturmaTarihi,
                GuncellenmeTarihi = ticket.GuncellenmeTarihi,
                TicketOlusturanId = ticket.TicketOlusturanId,
                TicketOlusturanName = ticket.TicketOlusturan.AdSoyad,

            };
            return Ok(ticketDto);
        }

        //Ticket Oluşturma
        [HttpPost]
        public async Task<IActionResult> TicketOlustur(TicketOlusturmaDto dto)
        {
            var kullanıcı = await _context.Users.FirstOrDefaultAsync(user => user.Id == dto.TicketOlusturanId);

            if (kullanıcı == null)
            {
                return BadRequest("Kullanıcı bulunamadı.");
            }
            var ticket = new Ticket
            {
                Baslik = dto.Baslik,
                Aciklama = dto.Aciklama,
                Oncelik = dto.Oncelik,
                TicketOlusturanId = kullanıcı.Id,
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
                OlusturmaTarihi = ticket.OlusturmaTarihi,
                TicketOlusturanId = kullanıcı.Id,
                TicketOlusturanName = kullanıcı.AdSoyad,
            };

            return CreatedAtAction(nameof(TicketGetir), new { id = ticket.Id }, ticketDto);

        }

        //Ticket durum Güncelleme
        [HttpPut("{id}/durum")]
        public async Task<IActionResult> TicketDurumGuncelle(int id, TicketDurumGuncellemeDto dto)
        {
            var ticket = await _context.Tickets
            .FirstOrDefaultAsync(t => t.Id == id && !t.SilindiMi);


            if (ticket == null)
            {
                return NotFound();
            }

            ticket.Durum = dto.Durum;
            ticket.GuncellenmeTarihi = DateTime.UtcNow;

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

            ticket.SilindiMi = true; 

            await _context.SaveChangesAsync();

            return NoContent();




        }


        //Ticket Güncelleme
        [HttpPut("{id}")]
        public async Task<IActionResult> TicketGuncelle(TicketGuncellemeDto dto,int id) 
        {
            var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == id && !t.SilindiMi);

            if (ticket == null)
            {
                return BadRequest("Ticket Bulunumadı");
            }
            
            ticket.Baslik = dto.Baslik;
            ticket.Aciklama = dto.Aciklama;
            ticket.Oncelik = dto.Oncelik;
            ticket.GuncellenmeTarihi = DateTime.UtcNow;


            await _context.SaveChangesAsync();

            return NoContent();





        }







    }
}
