using System.ComponentModel.DataAnnotations;

namespace UserService.Models
{
    public class User
    {
        [Key, Required]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Name { get; set; }
        public string Bio { get; set; }
        [Required]
        public string PasswordSalt { get; set; }
        [Required]
        public string Password { get; set; }

        public User()
        {

        }

        public User(int id, string username, string name, string bio)
        {
            Id = id;
            Username = username;
            Name = name;    
            Bio = bio;
        }
    }
}
