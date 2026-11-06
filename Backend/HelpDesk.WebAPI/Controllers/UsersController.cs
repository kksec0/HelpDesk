using HelpDesk.WebAPI.Data;
using HelpDesk.WebAPI.DTOs;
using HelpDesk.WebAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace HelpDesk.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public UsersController(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // Tüm kullanıcıları getir
        [HttpGet]
        public async Task<IActionResult> UsersGetir()
        {
            var users = await _userManager.Users
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
            var user = await _userManager.FindByIdAsync(id.ToString());

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
            var roleExists = await _roleManager.RoleExistsAsync(dto.Role);

            if (!roleExists)
            {
                return BadRequest("Geçersiz kullanıcı rolü.");
            }


            var user = new User
            {

                AdSoyad = dto.AdSoyad,
                Email = dto.Email,
                UserName = dto.Email

            };


            //await _context.SaveChangesAsync(); eski db ye kaydetme kodu




            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(errors);
            }


            var roleResult = await _userManager.AddToRoleAsync(user, dto.Role);

            if (!roleResult.Succeeded)
            {
                var errors = roleResult.Errors.Select(e => e.Description);
                return BadRequest(errors);
            }






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

        //Kullanıcı bilgileri güncelleme
        [HttpPut("{id}")]
        public async Task<IActionResult> UserGuncelle(int id, UserGuncelleDto dto)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound("Kullanıcı Bulunamadı");
            }

            user.AdSoyad = dto.AdSoyad;
            user.Email = dto.Email;
            user.UserName = dto.Email;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                var error = result.Errors.Select(e => e.Description);
                return BadRequest(error);
            }

            return NoContent();

        }

        //Kullanıcı durum güncelleme aktif mi pasif mi?
        [HttpPut("{id}/durum")]
        public async Task<IActionResult> UserDurumGuncelle(int id, UserDurumGuncellemeDto dto)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }
            
            user.AktifMi = dto.AktifMi;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                var error = result.Errors.Select(e => e.Description);
                return BadRequest(error);
            }

            return NoContent();
            


        }
    }
}
