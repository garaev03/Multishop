using System.ComponentModel.DataAnnotations;

namespace MultiShop.Dtos.AppUserDtos
{
    public class RegisterDto
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
