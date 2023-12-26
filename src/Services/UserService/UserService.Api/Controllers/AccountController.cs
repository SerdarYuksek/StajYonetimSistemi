using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using UserService.Api.Manager;
using UserService.Api.Model;
using UserService.Api.ValidationRules;

namespace UserService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    // Controllerlarda kullanılmak üzere nesneler oluşturuldu
    public class AccountController : ControllerBase
    {
        private readonly EMailRepository _eMailRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly UserRegisterValidation _validationRules;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;

        //Account Controllerın Constructerına kullanılmak üzere nesneler dahil edildi
        public AccountController(EMailRepository eMailRepository, UserManager<AppUser> userManager, UserRegisterValidation validationRules, SignInManager<AppUser> signInManager, IConfiguration configuration)
        {
            _eMailRepository = eMailRepository;
            _userManager = userManager;
            _validationRules = validationRules;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        //Kullanıcı kayıt ekranını getirme
        [HttpGet("UserSignUp")]
        public IActionResult UserSignUp()
        {
            return Ok();
        }

        //Kullanıcı kayıt ekranından gelen bilgiler ve rol bilgisine göre sisteme (veritabanına) kaydedilmesi
        [HttpPost("UserSignUp")]
        public async Task<IActionResult> UserSignUp(string role, AppUser u)
        {
            ValidationResult result = _validationRules.Validate(u);
            role = role.ToLower();

            if (result.IsValid)
            {
                switch (role)
                {
                    case "personal":
                        Personal personalData = new Personal
                        {
                            FirstName = u.PersonalData.FirstName,
                            Surname = u.PersonalData.Surname,
                            TCNO = u.PersonalData.TCNO,
                            PersonalNo = u.PersonalData.PersonalNo,
                            Title = u.PersonalData.Title,
                            Gender = u.PersonalData.Gender,
                            Email = u.PersonalData.Email,
                            PhoneNumber = u.PersonalData.PhoneNumber,
                        };

                        u.PersonalData = personalData; // AppUser içinde PersonalData'ya atama yap
                       var personalUser =  await _userManager.CreateAsync(u, u.PersonalData.Password);
                        if (personalUser.Succeeded)
                        {
                            return Ok(new { Message = $"{role} Kaydı Yapıldı." });
                        }
                        else
                        {
                            return BadRequest(new { Message = $"{role} Kaydı Yapılamadı." });
                        }
                     

                    case "student":
                        Student studentData = new Student
                        {
                            FirstName = u.StudentData.FirstName,
                            Surname = u.StudentData.Surname,
                            TCNO = u.StudentData.TCNO,
                            StudentNo = u.StudentData.StudentNo,
                            Gender = u.StudentData.Gender,
                            Email = u.StudentData.Email,
                            PhoneNumber = u.StudentData.PhoneNumber,
                            Class = u.StudentData.Class,
                        };
                        u.StudentData = studentData; // AppUser içinde StudentData'ya atama yap
                        var studentUser = await _userManager.CreateAsync(u, u.StudentData.Password);
                        if (studentUser.Succeeded)
                        {
                            return Ok(new { Message = $"{role} Kaydı Yapıldı." });
                        }
                        else
                        {
                            return BadRequest(new { Message = $"{role} Kaydı Yapılamadı." });
                        }

                    default:
                        return BadRequest(new { Message = "Geçersiz rol değeri." });
                }

            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }

            return BadRequest(new { Message = "Validasyon Yapılmadı." });
        }

        //Kullanıcı giriş ekranını getirme
        [HttpGet("UserSignIn")]
        public IActionResult UserSıgnIn()
        {
            return Ok();
        }

        //Kullanıcı giriş ekranından gelen bilgiler ve rol bilgisine göre kullanıcının sisteme giriş yapması
        [HttpPost("UserSigIn")]
        public async Task<IActionResult> UserSignIn(string role, LoginViewModel loginView)
        {
            role = role.ToLower();
            var user = await _userManager.FindByIdAsync(loginView.Id.ToString());

            if (role == "personal" && user.PersonalData.RegistrationCheck==true) //rol ve personel onayı kontrolü
            {
                var personalResult = await _signInManager.PasswordSignInAsync(loginView.PersonalNo, loginView.Password, false, true); //Personel bilgilerinin kontrolü
                return personalResult.Succeeded ? Ok(new { token = GenerateJwtToken(role, user, _configuration) }) : Unauthorized(); //Oturum kontrolü jwt token ile sağlanmıştır
               
            }
            else if (role == "student" && user.StudentData.RegistrationCheck == true) //rol ve personel onayı kontrolü
            {
                var studentResult = await _signInManager.PasswordSignInAsync(loginView.StudentNo, loginView.Password, false, true); //Student bilgilerinin kontrolü
                return studentResult.Succeeded ? Ok(new { token =  GenerateJwtToken(role, user, _configuration) }) : Unauthorized(); //Oturum kontrolü jwt token ile sağlanmıştır
            }
            else
            {
                return BadRequest(new { Message = "Geçersiz rol değeri veya Personel Onayı verilmemiş." });
            }
        }

        
        private string GenerateJwtToken(string role, AppUser user, IConfiguration configuration)
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

            return tokenHandler.WriteToken(AccessToken);
        }

        //Sistemden çıkış yapma 
        [HttpGet("UserLogOut")]
        public async Task<IActionResult> UserLogOut()
        {
           
            await HttpContext.SignOutAsync("stajyonetim.Auth"); //Sistemden çıkış yaptıran ve tutulan cookieleri silme

            return Ok(new { Message = "Çıkış yapıldı." });
        }

        //Şifre Yenileme İşlemi için Mail ekranını Çağırma
        [HttpGet("SendNewPasswordMail")]
        public IActionResult SendNewPasswordMail()
        {
            return Ok();
        }

        //Mail ekranından gelen bilgilere ve rol bilgisine göre kullanıcı mailine şifre sıfırlama bağlantısı gönderme
        [HttpPost("SendNewPasswordMail")]
        public async Task<IActionResult> SendNewPasswordMail(string role, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            role = role.ToLower();

            switch (role) //rol kontrolü
            {
                case "personal":
                    if (user.PersonalData.Email == email) //mail kontrolü
                    {
                        _eMailRepository.EMailLinkSend(email); //mail gönderme fonksiyonu
                        return Ok(new { Message = "EMail Başarıyla Gönderilmiştir." });
                    }
                    else
                    {
                        return BadRequest(new { Message = "Mail Adresi Sisteme Kayıtlı Değildir." });
                    }

                case "student":
                    if (user.StudentData.Email == email)
                    {
                        _eMailRepository.EMailLinkSend(email); //mail gönderme fonksiyonu
                        return Ok(new { Message = "EMail Başarıyla Gönderilmiştir." });
                    }
                    else
                    {
                        return BadRequest(new { Message = "Mail Adresi Sisteme Kayıtlı Değildir." });
                    }

                default:
                    return BadRequest(new { Message = "Geçersiz rol değeri." });


            }

        }

        //Kullanıcı Şifre sıfırlama ekranının getirilmesi 
        [HttpGet("UserPasswordReset")]
        public IActionResult UserPasswordReset()
        {
            return Ok();
        }

        //Kullanıcı Şifre sıfırlama ekranından gelen bilgilere ve rol bilgisine göre şifreyi sıfırlama 
        [HttpPut("UserPasswordReset")]
        public async Task<IActionResult> UserResetPassword(string role, AppUser u)
        {
            role = role.ToLower();
            ValidationResult validationresult = _validationRules.Validate(u);

            if (validationresult.IsValid)
            {
                switch (role)
                {
                    case "personal":
                        var personaluser = await _userManager.FindByEmailAsync(u.PersonalData.Email);

                        if (personaluser == null)
                        {
                            return BadRequest(new { Message = "Kullanıcı bulunamadı." });
                        }

                        // Şifre sıfırlama işlemi için kullanıcıya ait token'i kullanarak yeni şifreyi ayarla
                        var personalresult = await _userManager.ResetPasswordAsync(personaluser, u.PersonalData.Token, u.StudentData.Password);

                        // İşlem başarılı ise OK döndür
                        if (personalresult.Succeeded)
                        {
                            return Ok(new { Message = "Şifre başarıyla Değiştirildi." });
                        }
                        else
                        {
                            // İşlem başarısız ise hataları BadRequest ile döndür
                            var errors = personalresult.Errors.Select(e => e.Description).ToList();
                            return BadRequest(new { Errors = errors });
                        }

                    case "student":

                        var studentuser = await _userManager.FindByEmailAsync(u.StudentData.Email);

                        if (studentuser == null)
                        {
                            return BadRequest(new { Message = "Kullanıcı bulunamadı." });
                        }

                        // Şifre sıfırlama işlemi için kullanıcıya ait token'i kullanarak yeni şifreyi ayarla
                        var studentresult = await _userManager.ResetPasswordAsync(studentuser, u.StudentData.Token, u.StudentData.Password);
         
                        if (studentresult.Succeeded)
                        {
                            return Ok(new { Message = "Şifre başarıyla Değiştirildi." });
                        }
                        else
                        {
                            var errors = studentresult.Errors.Select(e => e.Description).ToList();
                            return BadRequest(new { Errors = errors });
                        }

                    default:

                        return BadRequest(new { Message = "Geçersiz rol değeri." });
                }
            }
            else
            {
                return BadRequest(new { Message = "Validasyon Yapılamadı." });
            }

        }
    }
}
