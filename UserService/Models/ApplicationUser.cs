using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Models
{
    public class ApplicationUser
    {
        [Key, Required]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Name { get; set; }
        public string Bio { get; set; }

        public ApplicationUser()
        {
        }

        public ApplicationUser(string username, string name, string bio)
        {
            Username = username;
            Name = name;    
            Bio = bio;
        }
    }
}
