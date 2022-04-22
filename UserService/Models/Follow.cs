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

        public Follow()
        {
            
        }
        public Follow(string username, string usernameToFollow)
        {
            Username = username;
            UsernameToFollow = usernameToFollow;
        }
    }
}
