using FluentValidation.Results;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserService.Api.Manager;
using UserService.Api.Model;
using UserService.Api.ValidationRules;

namespace UserService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly EMailRepository _eMailRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly UserRegisterValidation _validationRules;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(EMailRepository eMailRepository, UserManager<AppUser> userManager, UserRegisterValidation validationRules, SignInManager<AppUser> signInManager)
        {
            _eMailRepository = eMailRepository;
            _userManager = userManager;
            _validationRules = validationRules;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult UserSignUp()
        {
            return Ok();
        }

        [HttpPost]
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
                        await _userManager.CreateAsync(u, u.PersonalData.Password);
                        break;

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
                        await _userManager.CreateAsync(u, u.StudentData.Password);
                        break;

                    default:
                        return BadRequest(new { Message = "Geçersiz rol değeri." });
                }

                return Ok(new { Message = $"{role} Kaydı Yapıldı." });
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

        [HttpGet]
        public IActionResult UserSıgnIn()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> UserSignIn(string role, LoginViewModel loginView)
        {
            role = role.ToLower();

            switch (role)
            {
                case "personal":
                    var personalResult = await _signInManager.PasswordSignInAsync(loginView.PersonalNo, loginView.Password, false, true);
                    if (personalResult.Succeeded)
                    {
                        var user = await _userManager.FindByIdAsync(loginView.Id.ToString());
                        // Personal rolü için bir claim ekleyin
                        var personalClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, role),

                };

                        return Ok(new { Message = $"{role} Girişi Yapıldı." });
                    }
                    else
                    {
                        return BadRequest(new { Message = "Personel Numarası veya şifre yanlış." });
                    }


                case "student":
                    var studentResult = await _signInManager.PasswordSignInAsync(loginView.StudentNo, loginView.Password, false, true);
                    if (studentResult.Succeeded)
                    {
                        var user = await _userManager.FindByIdAsync(loginView.Id.ToString());
                        // Personal rolü için bir claim ekleyin
                        var personalClaims = new List<Claim>
                        {
                                 new Claim(ClaimTypes.Name, user.UserName),
                                 new Claim(ClaimTypes.Role, role),

                        };

                        return Ok(new { Message = $"{role} Girişi Yapıldı." });
                    }
                    else
                    {
                        return BadRequest(new { Message = "Öğrenci numarası veya şifre yanlış." });
                    }


                default:
                    return BadRequest(new { Message = "Geçersiz rol değeri." });
            }

        }

        //Sistemden Çıkış Fonksiyonu
        [HttpGet]
        public async Task<IActionResult> UserSignOut()
        {
            // Kullanıcıyı çıkış yap
            await HttpContext.SignOutAsync("stajyonetim.Auth");

            return Ok(new { Message = "Çıkış yapıldı." });
        }

        //Şifre Yenileme İşlemi için Mail Çağırma
        [HttpGet]
        public IActionResult SendNewPasswordMail()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> SendNewPasswordMail(string role, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            role = role.ToLower();

            switch (role)
            {
                case "personal":
                    if (user.PersonalData.Email == email)
                    {
                        _eMailRepository.EMailLinkSend(email);
                        return Ok(new { Message = "EMail Başarıyla Gönderilmiştir." });
                    }
                    else
                    {
                        return BadRequest(new { Message = "Mail Adresi Sisteme Kayıtlı Değildir." });
                    }

                case "student":
                    if (user.StudentData.Email == email)
                    {
                        _eMailRepository.EMailLinkSend(email);
                        return Ok(new { Message = "EMail Başarıyla Gönderilmiştir." });
                    }
                    else
                    {
                        return BadRequest(new { Message = "Geçersiz rol değeri." });
                    }

                default:
                    return BadRequest(new { Message = "Mail Adresi Sisteme Kayıtlı Değildir." });

            }

        }

        //Personel Şifre sıfırlama fonksiyonu kodları
        [HttpGet]
        public IActionResult UserPasswordReset()
        {
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UserResetPassword(PasswordResetVİewModel resetPasswordViewModel)
        {

            var user = await _userManager.FindByEmailAsync(resetPasswordViewModel.Email);

            if (user == null)
            {
                return BadRequest(new { Message = "Kullanıcı bulunamadı." });
            }

            // Şifre sıfırlama işlemi için kullanıcıya ait token'i kullanarak yeni şifreyi ayarla
            var result = await _userManager.ResetPasswordAsync(user, resetPasswordViewModel.Token, resetPasswordViewModel.Password);

            // İşlem başarılı ise OK döndür
            if (result.Succeeded)
            {
                return Ok(new { Message = "Şifre başarıyla Değiştirildi." });
            }
            else
            {
                // İşlem başarısız ise hataları BadRequest ile döndür
                var errors = result.Errors.Select(e => e.Description).ToList();
                return BadRequest(new { Errors = errors });
            }
        }
    }
}
