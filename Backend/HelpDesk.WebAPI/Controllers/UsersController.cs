using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HelpDesk.WebAPI.Data;
using HelpDesk.WebAPI.DTOs;
using HelpDesk.WebAPI.Models;


namespace HelpDesk.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly HelpDeskDBContext _context;

        public UsersController(HelpDeskDBContext context)
        {
            _context = context;
        }

        // Tüm kullanıcıları getir
        [HttpGet]
        public async Task<IActionResult> UsersGetir()
        {
            var users = await _context.Users
                .Select(user => new UserGetirDto
                {
                    Id = user.Id,
                    AdSoyad = user.AdSoyad,
                    Email = user.Email
                })
                .ToListAsync();

            return Ok(users);
        }

        // ID ile kullanıcı getir
        [HttpGet("{id}")]
        public async Task<IActionResult> UserGetir(int id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(user => user.Id == id);

            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            var userDto = new UserGetirDto
            {
                Id = user.Id,
                AdSoyad = user.AdSoyad,
                Email = user.Email
            };

            return Ok(userDto);
        }


        //User Oluşturma
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserOlusturDto dto)
        {
            var user = new User
            {

                AdSoyad = dto.AdSoyad,
                Email = dto.Email,

            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var userDto = new UserGetirDto
            {
                Id = user.Id,
                AdSoyad = user.AdSoyad,
                Email = user.Email
            };

            return CreatedAtAction(
                nameof(UserGetir),
                new { id = user.Id },
                userDto
            );




        }



    }
}
