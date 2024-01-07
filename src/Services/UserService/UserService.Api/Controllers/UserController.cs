using Microsoft.AspNetCore.Mvc;
using System.Data;
using UserService.Api.Context;
using UserService.Api.Model;
using UserService.Api.Services;
using X.PagedList;


namespace UserService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //Generic Classta yapılan CRUD işlemleri ve Identity kütüphanesi için nesneler oluşturuldu 
        private UserIdentityDbContext DBContext;
        private CrudGenericRepository<AppUser> _userGenericRepo;

        //Oluşturulan nesneler constructırda tanımlandı
        public UserController(UserIdentityDbContext dbContext, CrudGenericRepository<AppUser> userGenericRepo)
        {
            DBContext = dbContext;
            _userGenericRepo = userGenericRepo;
        }

        // Rol Bilgisine Göre Kullanıcıların Listelenmesi
        [HttpGet("UserList")]
        public IActionResult UserList(string roles, int page = 1)
        {
            if (roles != null)
            {
                // Rolü "Personal" Olan Kullanıcıların Listelenmesi
                if (roles.Contains("Personal"))
                {
                    var personals = _userGenericRepo.UGetListAll().Where(x => x.Role == roles).ToPagedList(page, 5);
                    var responseModelList = personals.Select(user => new PersonalListResponseModel
                    {
                        UserName = user.UserName,
                        PersonalNo = user.PersonalNo,
                        Email = user.Email,
                        Title = user.Title
                    }).ToList();

                    return Ok(responseModelList);
                }
                // Rolü "Student" Olan Kullanıcıların Listelenmesi
                else if (roles.Contains("Student"))
                {
                    var students = _userGenericRepo.UGetListAll().Where(x => x.Role == roles).ToPagedList(page, 5);
                    var responseModelList = students.Select(user => new StudentListResponseModel
                    {
                        UserName = user.UserName,
                        StudentNo = user.StudentNo,
                        Email = user.Email
                    }).ToList();

                    return Ok(responseModelList);
                }
            }
            // Belirli bir role uymadığı durumda hata mesajı
            return BadRequest("Belirtilen role uyan kullanıcılar bulunamadı.");
        }

        // Kullanıcıların Silinmesi
        [HttpDelete("UserDel/{id}")]
        public IActionResult UserDel(int id)
        {
            var user = _userGenericRepo.UGetById(id);
            _userGenericRepo.UDelete(user);
            return Ok(new { Message = "Kişi başarıyla silindi." });
        }

        //Rol Bilgisine Göre Kullanıcıların Güncellenmesi
        [HttpGet("UserUpdate/{id}")]
        public IActionResult UserUpdate(int id)
        {
            var user = _userGenericRepo.UGetById(id);

            // Rolü Personal olan Kullanıcı Bilgilerinin Ekrana Getirilmesi
            if (user.Role == "Personal")
            {
                return Ok(new PersonalUpdateResponseModel
                {
                    UserName = user.UserName,
                    TCNO = user.TCNO,
                    Gender = user.Gender,
                    PersonalNo = user.PersonalNo,
                    Email = user.Email,
                    Tittle = user.Title,
                });
            }
            // Rolü Student olan Kullanıcı Bilgilerinin Ekrana Getirilmesi
            else if (user.Role == "Student")
            {
                return Ok(new StudentUpdateResponseModel
                {
                    UserName = user.UserName,
                    TCNO = user.TCNO,
                    Gender = user.Gender,
                    StudentNo = user.StudentNo,
                    Email = user.Email,
                    Class = user.Class,
                });
            }
            // Belirli bir role uymadığı durumda hata mesajı .
            return BadRequest("Belirtilen role uyan kullanıcılar bulunamadı.");
        }

        // Kullanıcıların Güncellenmesi
        [HttpPut("UserUpdate")]
        public IActionResult UserUpdate(AppUser u)
        {
            if (u != null)
            {
                var user = _userGenericRepo.UGetById(u.Id);

                user.FirstName = u.FirstName;
                user.Surname = u.Surname;
                user.TCNO = u.TCNO;
                user.Gender = u.Gender;
                user.Email = u.Email;
                user.PhoneNumber = u.PhoneNumber;
                user.PersonalNo = u.PersonalNo;
                user.StudentNo = u.StudentNo;
                user.Title = u.Title;
                user.Class = u.Class;
                user.UserName = u.FirstName + u.Surname;

                _userGenericRepo.UUpdate(user);
                return Ok(new { Message = "Kişi başarıyla güncellendi." });
            }

            // Bir değer gelmediği zamanki hata mesajı.
            return BadRequest("Gelen user değerleri boş.");
        }
    }
}
