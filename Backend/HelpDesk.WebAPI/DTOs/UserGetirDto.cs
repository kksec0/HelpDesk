namespace HelpDesk.WebAPI.DTOs
{
    public class UserGetirDto
    {
        public int Id { get; set; }
        public string AdSoyad { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool AktifMi {  get; set; }
        public string Role { get; set; } = string.Empty;

    }
}
