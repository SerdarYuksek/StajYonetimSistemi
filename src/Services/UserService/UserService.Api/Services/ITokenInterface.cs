using UserService.Api.Model;

namespace UserService.Api.Services
{
    //Kullanıcı bilgilerine göre token üreten fonksiyonun interfacesi
    public interface ITokenInterface
    {
        Task<string> GenerateJwtToken(string role, AppUser user, IConfiguration configuration);

    }
}
