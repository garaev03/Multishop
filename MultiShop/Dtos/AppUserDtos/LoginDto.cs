using System.ComponentModel;

namespace MultiShop.Dtos.AppUserDtos
{
    public class LoginDto
    {
        public string UserName { get; set; }
        [PasswordPropertyText]
        public string Password { get; set; }
    }
}
