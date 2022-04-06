using System.ComponentModel.DataAnnotations;

namespace AuthenticationServer.Controllers
{
    public class RegisterUserModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        public RegisterUserModel(string username, string password, string email)
        {
            Username = username;
            Password = password;
            Email = email;
        }
    }
}