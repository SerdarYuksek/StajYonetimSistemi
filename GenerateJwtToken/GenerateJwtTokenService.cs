using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GenerateJwtToken
{
    public class GenerateJwtTokenService
    {
        //Kullanıcı bilgilerine göre token üreten fonksiyonun interfacesi
        public interface IGenerateTokenInterface
        {
            Task<string> GenerateJwtToken(string userName, string Role);

        }

        public class GenerateTokenRepository : IGenerateTokenInterface
        {

            //Kullanıcının oturum açma ve oturumda kalmasını sağlayan tokenın üretilme fonsiyonu 
            public async Task<string>? GenerateJwtToken(string userName, string Role)
            {

                SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtExtensions.SecurityKey));
                SigningCredentials credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var Expiration = DateTime.UtcNow.AddHours(1);  //oturum süresi (1 saat)

                var tokenDescriptor = new SecurityTokenDescriptor  // Token oluşturma parametreleri
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                     new Claim(ClaimTypes.Name, userName),
                     new Claim(ClaimTypes.Role, Role),
                    }),
                    Expires = Expiration,
                    SigningCredentials = credentials,
                };

                JwtSecurityTokenHandler tokenHandler = new();
                var AccessToken = tokenHandler.CreateToken(tokenDescriptor);  //Token Oluşturma
                return await Task.FromResult(tokenHandler.WriteToken(AccessToken));
            }
        }

    }
}

