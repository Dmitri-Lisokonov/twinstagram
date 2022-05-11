using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Models.User
{
    public class AuthenticationUser : IdentityUser
    {
        public string Name { get; private set; }

    }
}
