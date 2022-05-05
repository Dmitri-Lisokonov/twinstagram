using System.ComponentModel.DataAnnotations;

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

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Message()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {

        }
        public Message(Guid userId, string description, string image, DateTime createdDate)
        {
            UserId = userId;
            Description = description;
            Image = image;
            CreatedDate = createdDate;
        }
    }
}
