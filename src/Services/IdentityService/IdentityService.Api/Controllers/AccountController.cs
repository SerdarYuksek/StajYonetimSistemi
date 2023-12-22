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
        private readonly UserManager<AppUser> _userManager;

        public AccountController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult UserSignUp()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> UserSignUp(string role ,AppUser u)
        {
            if (role == "Personal")
            {
                Personal personalData = new Personal
                {
                    FirstName = u.PersonalData.FirstName,
                    Surname = u.PersonalData.Surname,
                    TCNO = u.PersonalData.TCNO,
                    PersonalNo = u.PersonalData.PersonalNo,
                    Title = u.PersonalData.FirstName,
                    Gender = u.PersonalData.Gender,
                    Email = u.PersonalData.Email,
                    PhoneNumber = u.PersonalData.PhoneNumber,
                };
                AppUser user = new AppUser()
                {
                    PersonalData = u.PersonalData
                };

                var result = await _userManager.CreateAsync(user, u.PersonalData.Password);

                await _userManager.AddClaimAsync(user, new Claim("UserName", user.UserName));
                await _userManager.AddClaimAsync(user, new Claim("Role", "Personal"));
                await _userManager.AddClaimAsync(user, new Claim("UserId", user.Id.ToString()));
  
                if (result.Succeeded)
                {
                   return Ok(new { Message = "Kullanıcı bilgileri başarıyla eklendi." });
                }
                else
                {
                    return BadRequest(); //Fluent Validationdan gelen hata mesajları
                }
            }
            else if (role == "Student")
            {
                Student personalData = new Student
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

                AppUser user = new AppUser()
                {
                    StudentData = u.StudentData
                };

                var result = await _userManager.CreateAsync(user, u.StudentData.Password);

                if (result.Succeeded)
                {
                    return Ok(new { Message = "Kullanıcı bilgileri başarıyla eklendi." });
                }
                else
                {
                    return BadRequest(result.Errors); //Fluent Validationdan gelen hata mesajları
                }
            }
            return Ok();
        }
    }
}
