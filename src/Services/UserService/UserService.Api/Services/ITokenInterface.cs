using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserService.Api.Model;

namespace UserService.Api.Services
{

    //Kullanıcı bilgilerine göre token üreten fonksiyonun interfacesi
    public interface ITokenInterface
    {
        Task<string> GenerateJwtToken(string role, AppUser user, IConfiguration configuration);

    }

    public class TokenRepository : ITokenInterface
    {
        private CrudGenericRepository<AppUser> _userGenericRepo;

        public TokenRepository(CrudGenericRepository<AppUser> userGenericRepo)
        {
            _userGenericRepo = userGenericRepo;
        }

        //Kullanıcının oturum açma ve oturumda kalmasını sağlayan tokenın üretilme fonsiyonu 
        public async Task<string>? GenerateJwtToken(string role, AppUser user, IConfiguration configuration)
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

            _userGenericRepo.UTokenSave(user, AccessToken.ToString());


            return await Task.FromResult(tokenHandler.WriteToken(AccessToken));

        }
    }
}
