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
        public Follow(int id, string username, string usernameToFollow)
        {
            Id = id;
            Username = username;
            UsernameToFollow = usernameToFollow;
        }
    }
}
