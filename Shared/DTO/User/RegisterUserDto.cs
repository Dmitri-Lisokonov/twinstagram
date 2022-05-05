using System.ComponentModel.DataAnnotations;

namespace Shared.DTO.User
{
    public class RegisterUserDto
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }
    }
}