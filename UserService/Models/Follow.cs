using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Models
{
    public class Follow
    {
        [Key, Required]
        public int Id { get; set; }
        public string Username { get; set; }
        public string UsernameToFollow { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Follow()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            
        }
        public Follow(string username, string usernameToFollow)
        {
            Username = username;
            UsernameToFollow = usernameToFollow;
        }
    }
}
