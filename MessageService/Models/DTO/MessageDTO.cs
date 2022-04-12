using System.ComponentModel.DataAnnotations;

namespace MessageService.Models.DTO
{
    public class MessageDTO
    {
        public int? Id { get; set; }

        public int? UserId { get; set; }

        public string? Description { get; set; }

        public string? Image { get; set; }

        public DateTime? CreatedDate { get; set; }

        public MessageDTO(int id, int userId, string description, string image, DateTime createdDate)
        {
            Id = id;
            UserId = userId;
            Description = description;
            Image = image;
            CreatedDate = createdDate;
        }
    }
}
