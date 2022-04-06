namespace UserService.Models.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public string ?Password { get; set; }
        public string ?Token { get; set; }
        public string ?ProfileImage { get; set; }

        public UserDTO(int id, string username, string name, string bio)
        {
            Id = id;
            Username = username;
            Name = name;
            Bio = bio;
        }
    }
}
