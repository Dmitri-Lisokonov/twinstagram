using System.ComponentModel.DataAnnotations;

namespace MessageService.Models
{
    public class Message
    {
        [Key, Required]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        public string Description { get; set; }

        [Required]
        public string? Image { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Message()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {

        }
        public Message(int userId, string description, string image, DateTime createdDate)
        {
            UserId = userId;
            Description = description;
            Image = image;
            CreatedDate = createdDate;
        }
    }
}
