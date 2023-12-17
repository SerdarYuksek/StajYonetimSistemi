using Microsoft.AspNetCore.Mvc;
using UserService.Api.Context;
using UserService.Api.Manager;
using UserService.Api.Model;
using X.PagedList;
using static System.Net.Mime.MediaTypeNames;

namespace UserService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private CrudGenericRepository<Student> sgr;
        private CrudGenericRepository<Personal> pgr;

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

        //Rol Bilgisine Göre Kullanıcıların Silinmesi
        public IActionResult UserDel(string role, int ıd)
        {
            // Rolü Personal Olan Kullanıcının Silinmesi
            if (role == "Personal")
            {
                var personalId = pgr.UGetById(ıd);
                pgr.UDelete(personalId);
                return Ok(new { Message = "Kişi başarıyla silindi." });
            }
            // Rolü Student Olan Kullanıcının Silinmesi
            else if (role == "Student")
            {
                var StudentId = sgr.UGetById(ıd);
                sgr.UDelete(StudentId);
                return Ok(new { Message = "Kişi başarıyla silindi." });
            }
            // Belirli bir role uymadığı durumda hata mesajı .
            return BadRequest("Belirtilen role uyan kullanıcılar bulunamadı.");

        }


        [HttpGet]
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

        [HttpPost]
        //Rol Bilgisine Göre Kullanıcıların Güncellenmesi
        public IActionResult UserUpdate(string role, Personal p, Student s)
        {
            // Rolü Personal ve id değeri ile eşleşen kullanıcının bilgilerinin güncellenmesi
            if (role == "Personal")
            {
                var x = pgr.UGetById(p.ID);

                x.FirstName = p.FirstName;
                x.Surname = p.Surname;
                x.TCNO = p.TCNO;
                x.Gender = p.Gender;
                x.Mail = p.Mail;
                x.PersonalNo = p.PersonalNo;
                x.Title = p.Title;
                
                pgr.UUpdate(x);
                return Ok(new { Message = "Kişi başarıyla Güncellendi." });
            }
            // Rolü Student ve id değeri ile eşleşen kullanıcının bilgilerinin güncellenmesi
            else if (role == "Student")
            {
                var x = sgr.UGetById(s.ID);

                x.FirstName = s.FirstName;
                x.Surname = s.Surname;
                x.TCNO = s.TCNO;
                x.Gender = s.Gender;
                x.Class = s.Class;
                x.Mail = p.Mail;
                x.StudentNo = s.StudentNo;

                sgr.UUpdate(x);
                return Ok(new { Message = "Kişi başarıyla Güncellendi." });
            }
            // Belirli bir role uymadığı durumda hata mesajı .
            return BadRequest("Belirtilen role uyan kullanıcılar bulunamadı.");
        }
    }
}
