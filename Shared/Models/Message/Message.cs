using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Models.Message
{
    public class Message
    {
        [Key, Required]
        public Guid Id { get; private set; }

        [Required]
        public Guid UserId { get; private set; }

        public string Description { get; private set; }

        [Required]
        public string Image { get; private set; }
        
        [Required]
        public DateTime CreatedDate { get; private set; }
        
        public Message(Guid userId, string description, string image, DateTime createdDate)
        {
            UserId = userId;
            Description = description;
            Image = image;
            CreatedDate = createdDate;
        }
    }
}
