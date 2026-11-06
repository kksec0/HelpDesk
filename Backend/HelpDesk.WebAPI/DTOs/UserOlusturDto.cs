namespace HelpDesk.WebAPI.DTOs
{
    public class UserOlusturDto
    {
        public string AdSoyad { get; set; } = null!;

        public string Email { get; set; } = null!;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = "User"; 


    }
}
