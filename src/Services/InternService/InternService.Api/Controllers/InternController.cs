using InternService.Api.Context;
using InternService.Api.Manager;
using InternService.Api.Model;
using Microsoft.AspNetCore.Mvc;


namespace InternService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InternController : ControllerBase
    {
        //Generic Classta yapılan CRUD işlemleri bir entitye tanımlayıp nesne oluşturuldu 
        private InternDbContext dbContext;
        public CrudGenericRepository<InternInfo> igr;
        public CrudGenericRepository<InternStatus> isgr;

        //User Controllerın Contructerında dbcontextimiz ve nesnelerimiz generic taraf ile bağlandı
        public InternController(InternDbContext dbContext)
        {
            this.dbContext = dbContext;
            igr = new CrudGenericRepository<InternInfo>(dbContext);
            isgr = new CrudGenericRepository<InternStatus>(dbContext);
        }

        // Staj Kaydı Oluşturma ekranına erişme metodu(get)
        [HttpGet("CreateIntern")]
        public IActionResult CreateIntern()
        {

            return Ok();
        }

        // Staj Kaydı Oluşturma ekranınında staj bilgilerinin gönderilmesi metodu(post)
        [HttpPost("CreateIntern")]
        public IActionResult CreateIntern(InternInfo i)
        {

            i.StartDate = new DateTime(i.StartDate.Year, i.StartDate.Month, i.StartDate.Day);
            i.FinishDate = new DateTime(i.FinishDate.Year, i.FinishDate.Month, i.FinishDate.Day);
            igr.INAdd(i);
            return Ok(new { Message = "Staj kaydı oluşturuldu." });

        }

        //Staj kayıtlarının öğrenci tarafından veya Personel tarafından görüntülenmesi
        [HttpGet("listIntern")]
        public IActionResult ListIntern(string? studentno)
        {
            //Staj kayıtları öğrenci tarafından erişiliyorsa
            if (studentno == null)
            {
                // var intern = igr.INGetListAll().Where(x => x.StudentNo == User.Identity.StudentNo;
                return Ok(new { Message = "Staj kayıtları Listelendi." });
            }
            // Staj kayıtları personel tarafından erişiliyorsa
            else
            {
                var intern = igr.INGetListAll().Where(x => x.StudentNo == studentno);
                return Ok(intern);
            }

        }

        //Staj kayıtlarının detaylarına erişiliyorsa
        [HttpGet("ListInternInfo")]
        public IActionResult ListInternInfo(int id)
        {
            var intern = igr.INGetListAll().Where(x => x.ID == id);
            return Ok(intern);
        }

        //Staj kaydı silinecekse
        [HttpDelete("InternDel")]
        public IActionResult InternDel(InternInfo i)
        {
            var InternID = igr.INGetById(i.ID);
            igr.INDelete(InternID);
            return Ok(new { Message = "Staj kaydı silindi." });
        }

        //Staj kaydı onaylanacaksa
        [HttpPut("InternConfırm")]
        public IActionResult InternConfırm(InternStatus i)
        {
            isgr.INGetConfirm(i);
            return Ok(new { Message = "Staj kaydı onaylandı." });

        }

        //Staj kaydı onaylaması geri çekilecekse
        [HttpPut("InternDecline")]
        public IActionResult InternDecline(InternStatus i)
        {
            isgr.INGetDecline(i);
            return Ok(new { Message = "Staj kaydının onayı geri alındı." });

        }

        //Staj kaydı Kabul edilecekse
        [HttpPut("InternAccept")]
        public IActionResult InternAccept(InternStatus i)
        {
            isgr.INGetAccept(i);
            return Ok(new { Message = "Staj kaydı kabul edildi." });

        }

        //Staj kaydı kabulü geri çekilecekse
        [HttpPut("InterntReject")]
        public IActionResult InterntReject(InternStatus i)
        {
            isgr.INGetReject(i);
            return Ok(new { Message = "Staj kaydının kabulü geri alındı." });

        }

        //Staj kaydının katkı payı onaylanacaksa
        [HttpPut("ContributConfirm")]
        public IActionResult ContributConfirm(InternStatus i)
        {
            isgr.INGetContributConfirm(i);
            return Ok(new { Message = "Staj kaydının katkı payı onayı kabul edildi." });

        }

        //Staj kaydının katkı payı onaylaması geri çekilecekse
        [HttpPut("ContributDecline")]
        public IActionResult ContributDecline(InternStatus i)
        {
            isgr.INGetContributDecline(i);
            return Ok(new { Message = "Staj kaydının katkı payı onayı geri alındı." });

        }

        [HttpGet("InternUpdate")]
        //Öğrencinin Staj kaydı bilgilerini güncellemesi(get)
        public IActionResult InternUpdate()
        {

            // var intern = igr.INGetListAll().Where(x => x.StudentNo == User.Identity.StudentNo;
            return Ok(new { Message = "Staj kaydının bilgileri ekrana getirildi." }); //bu mesaj içeriği intern olarak değişecek

        }

        [HttpPut("InternUpdate")]
        //Öğrencinin staj kaydı bilgilerinin güncellenmesi(post)
        public IActionResult InternUpdate(InternInfo i)
        {

            var x = igr.INGetById(i.ID);

            x.CompanyName = i.CompanyName;
            x.address = i.address;
            x.area = i.area;
            x.OwnerName = i.OwnerName;
            x.Mail = i.Mail;
            x.FaxNo = i.FaxNo;
            x.WebAddress = i.WebAddress;
            x.StartDate = i.StartDate;
            x.FinishDate = i.FinishDate;
            x.İnternType = i.İnternType;
            x.İnternNumber = i.İnternNumber;
            x.Holliday = i.Holliday;
            x.SaturdayInc = i.SaturdayInc;
            x.Education = i.Education;
          
            igr.INUpdate(x);
            return Ok(new { Message = "Staj Kaydı Başarıyla Güncellendi" });

        }

    }
}
