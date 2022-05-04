using System.ComponentModel.DataAnnotations;

namespace Shared.Models.User
{
    public class ApplicationUser
    {
        [Key, Required]
        public Guid Id { get; private set; }
        
        [Required]
        public string Username { get; private set; }
        
        [Required]
        public string Name { get; private set; }
        
        public string Bio { get; private set; }
        
        public string ProfilePicture { get; private set; }

        public ApplicationUser(string username, string name, string bio, string profilePicture)
        {
            Id = Guid.NewGuid();
            Username = username;
            Name = name;    
            Bio = bio;
            ProfilePicture = profilePicture;
        }
    }
}
