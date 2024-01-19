using FluentValidation.Results;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using UserService.Api.Model;
using UserService.Api.Service;
using UserService.Api.Services;
using UserService.Api.Services.UserService.Api.Services;
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
        private readonly CrudGenericRepository<AppUser> _userGenericRepo;
        private readonly IConfiguration _configuration;
        private readonly TokenRepository _tokenRepo;

        //Account Controllerın Constructerına kullanılmak üzere nesneler dahil edildi
        public AccountController(EMailRepository eMailRepository, UserManager<AppUser> userManager, UserRegisterValidation validationRules, SignInManager<AppUser> signInManager, CrudGenericRepository<AppUser> userGenericRepo, TokenRepository tokenRepo, IConfiguration configuration)
        {
            _eMailRepository = eMailRepository;
            _userManager = userManager;
            _validationRules = validationRules;
            _signInManager = signInManager;
            _userGenericRepo = userGenericRepo;
            _tokenRepo = tokenRepo;
            _configuration = configuration;
        }

        //Kullanıcı kayıt ekranını getirme
        [HttpGet("UserSignUp")]
        public IActionResult UserSignUp()
        {
            return Ok(new { Message = "kayıt sayfası ekrana geldi" });
        }

        //Kullanıcının kayıt olması
        [HttpPost("UserSignUp")]
        public async Task<IActionResult> UserSignUp(AppUser u)
        {
            ValidationResult result = _validationRules.Validate(u);

            if (result.IsValid)
            {
                AppUser userData = new AppUser
                {
                    FirstName = u.FirstName,
                    Surname = u.Surname,
                    TCNO = u.TCNO,
                    PersonalNo = u.PersonalNo,
                    StudentNo = u.StudentNo,
                    Class = u.Class,
                    Role = u.Role,
                    Title = u.Title,
                    Gender = u.Gender,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    UserName = u.FirstName + u.Surname
                };
                u.UserName = u.FirstName + u.Surname;
                var User = await _userManager.CreateAsync(u, u.Password);
                if (User.Succeeded)
                {
                    return Ok(new { Message = $"{u.Role} Kaydı Yapıldı." });
                }
                else
                {
                    return BadRequest(new { Message = $"{u.Role} Kaydı Yapılamadı." });
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
        public IActionResult UserSignIn()
        {
            return Ok(new { Message = "Giriş sayfası ekrana geldi" });
        }

        //Kullanıcı giriş ekranından gelen bilgiler ve rol bilgisine göre kullanıcının sisteme giriş yapması
        [HttpPost("UserSigIn")]
        public async Task<IActionResult> UserSignIn(LoginViewModel loginView)
        {
            var user = await _userManager.FindByIdAsync(loginView.Id.ToString());


            if (user.Role == "Personal" && user.PersonalNo == loginView.PersonalNo) //rol ve personel onayı kontrolü
            {
                var personalResult = await _signInManager.PasswordSignInAsync(user, loginView.Password, false, false); //Personel bilgilerinin kontrolü
                return personalResult.Succeeded ? Ok(new { token = _tokenRepo.GenerateJwtToken(user.Role, user, _configuration) }) : Unauthorized(); //Oturum kontrolü jwt token ile sağlanmıştır

            }
            else if (user.Role == "Student" && user.RegistrationCheck == true && user.StudentNo == loginView.StudentNo) //rol ve personel onayı kontrolü
            {
                var studentResult = await _signInManager.PasswordSignInAsync(user, loginView.Password, false, true); //Student bilgilerinin kontrolü
                return studentResult.Succeeded ? Ok(new { token = _tokenRepo.GenerateJwtToken(user.Role, user, _configuration) }) : Unauthorized(); //Oturum kontrolü jwt token ile sağlanmıştır
            }
            else
            {
                return BadRequest(new { Message = "Geçersiz rol değeri veya Personel Onayı verilmemiş." });
            }
        }

        //Sistemden çıkış yapma 
        [HttpGet("UserLogOut")]
        public async Task<IActionResult> UserLogOut()
        {

            await HttpContext.SignOutAsync(JwtBearerDefaults.AuthenticationScheme); //Sistemden çıkış yaptıran ve tutulan cookieleri silme

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
        public async Task<IActionResult> SendNewPasswordMail(string email)
        {
            if (email != null)
            {
                _eMailRepository.EMailLinkSend(email); //mail gönderme fonksiyonu
                return Ok(new { Message = "EMail Başarıyla Gönderilmiştir." });
            }
            return BadRequest(new { Message = "Mail adresi boş." });
        }

        //Kullanıcı Şifre sıfırlama ekranının getirilmesi 
        [HttpGet("UserPasswordReset")]
        public IActionResult UserPasswordReset()
        {
            return Ok();
        }

        //Kullanıcı Şifre sıfırlama ekranından gelen bilgilere ve rol bilgisine göre şifreyi sıfırlama 
        [HttpPut("UserPasswordReset")]
        public async Task<IActionResult> UserResetPassword(AppUser u)
        {
            ValidationResult validationresult = _validationRules.Validate(u);

            if (validationresult.IsValid)
            {
                var user = await _userManager.FindByIdAsync(u.Id.ToString());

                if (user == null)
                {
                    return BadRequest(new { Message = "Kullanıcı bulunamadı." });
                }

                // Şifre sıfırlama işlemi için kullanıcıya ait token'i kullanarak yeni şifreyi ayarla
                var userResult = await _userManager.ResetPasswordAsync(user, u.Token, u.Password);

                // İşlem başarılı ise OK döndür
                if (userResult.Succeeded)
                {
                    return Ok(new { Message = "Şifre başarıyla Değiştirildi." });
                }
                else
                {
                    // İşlem başarısız ise hataları BadRequest ile döndür
                    var errors = userResult.Errors.Select(e => e.Description).ToList();
                    return BadRequest(new { Errors = errors });
                }
            }
            else
            {
                return BadRequest(new { Message = "Validasyon Yapılamadı." });
            }

        }
    }
}
