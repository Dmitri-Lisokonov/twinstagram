namespace Shared.DTO.User
{
    public class ApplicationUserDto
    {
        public Guid? Id { get; set; }

        public string? Username { get; set; }
        
        public string? Name { get; set; }
        
        public string? Bio { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Token { get; set; }

    }
}
