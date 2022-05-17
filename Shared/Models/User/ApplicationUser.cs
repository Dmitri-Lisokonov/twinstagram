using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Models.User
{
    public class ApplicationUser
    {
        [Key, Required]
        public Guid Id { get; private set; }

        [Required]
        public string Username { get; private set; }

        public string Name { get; set; }

        public string Bio { get; set; }
        
        public string ProfilePicture { get; set; }

        public ApplicationUser(Guid id, string username, string name, string bio, string profilePicture)
        {
            Id = id;
            Username = username;
            Name = name;
            Bio = bio;
            ProfilePicture = profilePicture;
        }
        
    }
}
