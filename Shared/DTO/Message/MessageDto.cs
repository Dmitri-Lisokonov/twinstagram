namespace Shared.DTO.Message
{
    public class MessageDto
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
