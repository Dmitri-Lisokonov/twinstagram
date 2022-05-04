using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationService.Util
{
    public class JwtBuilder
    {
		public string GenerateToken(string userId, IList<string> roles)
		{
			var mySecret = "MySecretTwinstagramKey";
			var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));
			var myIssuer = "Twinstagram";
			var myAudience = "Twinstagram";
			var tokenHandler = new JwtSecurityTokenHandler();
			var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, userId));

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            
            var tokenDescriptor = new SecurityTokenDescriptor
			{ 
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.UtcNow.AddDays(1),
				Issuer = myIssuer,
				Audience = myAudience,
				SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}
	}
}
