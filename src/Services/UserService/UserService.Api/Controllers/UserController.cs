using Microsoft.AspNetCore.Mvc;
using UserService.Api.Context;
using UserService.Api.Manager;
using UserService.Api.Model;
using X.PagedList;


namespace UserService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //Generic Classta yapılan CRUD işlemleri bir entitye tanımlayıp nesne oluşturuldu 
        private UserIdentityDbContext dbContext;
        private CrudGenericRepository<Student> sgr;
        private CrudGenericRepository<Personal> pgr;

        //User Controllerın Contructerında dbcontextimiz ve nesnelerimiz generic taraf ile bağlandı
        public UserController(UserIdentityDbContext dbContext)
        {
            this.dbContext = dbContext;
            sgr = new CrudGenericRepository<Student>(dbContext);
            pgr = new CrudGenericRepository<Personal>(dbContext);
        }

        [HttpGet("UserList")]
        //Rol Bilgisine Göre Kullanıcıların Listelenmesi
        public IActionResult UserList(string role, int page = 1)
        {
            // Rolü Personal Olan Kullanıcıların Listelenmesi
            if (role == "Personal")
            {
                var personal = pgr.UGetListAll().ToPagedList(page, 5);
                return Ok(personal);
            }
            // Rolü Student Olan Kullanıcıların Listelenmesi
            else if (role == "Student")
            {
                var students = sgr.UGetListAll().ToPagedList(page, 5);
                return Ok(students);
            }

            // Belirli bir role uymadığı durumda hata mesajı .
            return BadRequest("Belirtilen role uyan kullanıcılar bulunamadı.");

        }

        [HttpDelete("UserDel")]
        //Rol Bilgisine Göre Kullanıcıların Silinmesi
        public IActionResult UserDel(string role, int id)
        {
            // Rolü Personal Olan Kullanıcının Silinmesi
            if (role == "Personal")
            {
                var personalId = pgr.UGetById(id);
                pgr.UDelete(personalId);
                return Ok(new { Message = "Kişi başarıyla silindi." });
            }
            // Rolü Student Olan Kullanıcının Silinmesi
            else if (role == "Student")
            {
                var StudentId = sgr.UGetById(id);
                sgr.UDelete(StudentId);
                return Ok(new { Message = "Kişi başarıyla silindi." });
            }
            // Belirli bir role uymadığı durumda hata mesajı .
            return BadRequest("Belirtilen role uyan kullanıcılar bulunamadı.");

        }

        [HttpGet("UserUpdate")]
        //Rol Bilgisine Göre Kullanıcıların Güncellenmesi
        public IActionResult UserUpdate(string role, int id)
        {
            // Rolü Personal ve id değeri ile eşleşen Kullanıcının Bilgilerin Ekrana Getirilmesi
            if (role == "Personal")
            {
                var personal = pgr.UGetById(id);
                return Ok(personal);
            }
            // Rolü Student ve id değeri ile eşleşen Kullanıcının Bilgilerin Ekrana Getirilmesi
            else if (role == "Student")
            {
                var student = sgr.UGetById(id);
                return Ok(student);
            }
            // Belirli bir role uymadığı durumda hata mesajı .
            return BadRequest("Belirtilen role uyan kullanıcılar bulunamadı.");
        }

        [HttpPut("UserUpdate")]
        //Rol Bilgisine Göre Kullanıcıların Güncellenmesi
        public IActionResult UserUpdate(string role, AppUser u)
        {
            // Rolü Personal ve id değeri ile eşleşen kullanıcının bilgilerinin güncellenmesi
            if (role == "Personal")
            {
                var x = pgr.UGetById(u.PersonalData.Id);

                x.FirstName = u.PersonalData.FirstName;
                x.Surname = u.PersonalData.Surname;
                x.TCNO = u.PersonalData.TCNO;
                x.Gender = u.PersonalData.Gender;
                x.Email = u.PersonalData.Email;
                x.PhoneNumber = u.PhoneNumber;
                x.PersonalNo = u.PersonalData.PersonalNo;
                x.Title = u.PersonalData.Title;

                pgr.UUpdate(x);
                return Ok(new { Message = "Kişi başarıyla Güncellendi." });
            }
            // Rolü Student ve id değeri ile eşleşen kullanıcının bilgilerinin güncellenmesi
            else if (role == "Student")
            {
                var x = sgr.UGetById(u.StudentData.Id);

                x.FirstName = u.StudentData.FirstName;
                x.Surname = u.StudentData.Surname;
                x.TCNO = u.StudentData.TCNO;
                x.Gender = u.StudentData.Gender;
                x.Class = u.StudentData.Class;
                x.Email = u.StudentData.Email;
                x.PhoneNumber = u.PhoneNumber;
                x.StudentNo = u.StudentData.StudentNo;

                sgr.UUpdate(x);
                return Ok(new { Message = "Kişi başarıyla Güncellendi." });
            }
            // Belirli bir role uymadığı durumda hata mesajı .
            return BadRequest("Belirtilen role uyan kullanıcılar bulunamadı.");
        }
    }
}
