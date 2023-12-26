using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserService.Api.Model;
using UserService.Api.Services;

namespace UserService.Api.Manager
{
    public class TokenRepository : ITokenInterface
    {
        //Kullanıcının oturum açma ve oturumda kalmasını sağlayan tokenın üretilme fonsiyonu 
        public async Task<string> GenerateJwtToken(string role, AppUser user, IConfiguration configuration)
        {

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"]));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var Expiration = DateTime.UtcNow.AddHours(1);  //oturum süresi (1 saat)

            var tokenDescriptor = new SecurityTokenDescriptor  // Token oluşturma parametreleri
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                     new Claim(ClaimTypes.Name, user.UserName),
                     new Claim(ClaimTypes.Role, role),
                }),
                Expires = Expiration,
                SigningCredentials = credentials
            };

            JwtSecurityTokenHandler tokenHandler = new();
            var AccessToken = tokenHandler.CreateToken(tokenDescriptor);  //Token Oluşturma

            switch (role)  //Rol kontrolüne göre tokenları veritabanına kaydetme
            {
                case "personal":
                    user.PersonalData.Token = AccessToken.ToString();
                    break;
                case "student":
                    user.StudentData.Token = AccessToken.ToString();
                    break;
                default:
                    break;
            }

            return await Task.FromResult(tokenHandler.WriteToken(AccessToken));
        }
    }
}
