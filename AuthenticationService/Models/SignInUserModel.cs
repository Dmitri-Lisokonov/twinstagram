namespace AuthenticationService.Models
{
    public class SignInUserModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public SignInUserModel(string userName, string password)
        {
            this.UserName = userName;
            this.Password = password;
        }
    }
}
