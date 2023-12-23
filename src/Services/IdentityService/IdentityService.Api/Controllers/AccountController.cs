using FluentValidation;
using FluentValidation.Results;
using IdentityService.Api.Manager;
using IdentityService.Api.ValidationRules;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserService.Api.Model;

namespace IdentityService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly EMailRepository _eMailRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly UserRegisterValidation _validationRules;

        public AccountController(EMailRepository eMailRepository, UserManager<AppUser> userManager, UserRegisterValidation validationRules)
        {
            _eMailRepository = eMailRepository;
            _userManager = userManager;
            _validationRules = validationRules;
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

    }
}
