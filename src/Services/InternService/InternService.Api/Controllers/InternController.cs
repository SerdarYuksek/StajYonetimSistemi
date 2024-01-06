using InternService.Api.Context;
using InternService.Api.Model;
using InternService.Api.Services;
using Microsoft.AspNetCore.Mvc;


namespace InternService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InternController : ControllerBase
    {
        //Generic Classta yapılan CRUD işlemleri bir entitye tanımlayıp nesne oluşturuldu 
        private InternDbContext dbContext;
        public CrudGenericRepository<InternInfo> internGenericRepo;
        public CrudGenericRepository<InternStatus> internStatusGenericRepo;

        //User Controllerın Contructerında dbcontextimiz ve nesnelerimiz generic taraf ile bağlandı
        public InternController(InternDbContext dbContext)
        {
            this.dbContext = dbContext;
            internGenericRepo = new CrudGenericRepository<InternInfo>(dbContext);
            internStatusGenericRepo = new CrudGenericRepository<InternStatus>(dbContext);
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
            internGenericRepo.INAdd(i);
            return Ok(new { Message = "Staj kaydı oluşturuldu." });

        }

        //Staj kayıtlarının öğrenci tarafından veya Personel tarafından görüntülenmesi
        [HttpGet("listIntern/{id}")]
        public IActionResult ListIntern(int id)
        {
            var internInfo = internGenericRepo.INGetById(id);
            var intenStatus = internStatusGenericRepo.INGetById(id);
            var totalInternDay = internGenericRepo.CalculateTotalInternDay(internInfo.StartDate, internInfo.FinishDate);

            return Ok(new InternListResponseModel
            {
                CompanyName = internInfo.CompanyName,
                StartDate = internInfo.StartDate,
                FinishDate = internInfo.FinishDate,
                TotalInternDay = totalInternDay,
                AcceptDay = intenStatus.AcceptDay,
                ConfirmStatus = intenStatus.ConfirmStatus,
                AcceptStatus = intenStatus.AcceptStatus,
                ContributStatus = intenStatus.ContributStatus
            });
        }

        //Staj kayıtlarının detaylarına erişiliyorsa
        [HttpGet("ListInternDetail/{id}")]
        public IActionResult ListInternDetail(int id)
        {
            var internDetail = internGenericRepo.INGetListAll().Where(x => x.ID == id);
            return Ok(new { InternList = internDetail, Message = "Staj kayıt Detayları Listelendi." });
        }

        //Staj kaydı silinecekse
        [HttpDelete("InternDel{id}")]
        public IActionResult InternDel(int id)
        {
            var acceptValue = internStatusGenericRepo.INGetById(id).AcceptStatus;
            if (acceptValue != null && acceptValue == false)
            {
                var intern = internGenericRepo.INGetById(id);
                internGenericRepo.INDelete(intern);
                return Ok(new { Message = "Staj kaydı silindi." });
            }
            else
            {
                return BadRequest(new { Message = "Stajınız onaylandığı için kaydınızı silemezsiniz." });
            }

        }

        // Staj kaydı onaylanacaksa
        [HttpPut("InternConfirm/{id}")]
        public IActionResult InternConfirm(int id)
        {
            var internship = internStatusGenericRepo.INGetById(id);

            if (internship != null)
            {
                internStatusGenericRepo.INGetConfirm(internship);
                return Ok(new { Message = "Staj kaydı onaylandı." });
            }

            return BadRequest(new { Message = "Staj kaydı bulunamadı." });
        }

        //Staj kaydı onayı geri çekilecekse
        [HttpPut("ConfirmDecline/{id}")]
        public IActionResult ConfirmDecline(int id)
        {
            var internship = internStatusGenericRepo.INGetById(id);

            if (internship != null)
            {
                internStatusGenericRepo.INGetConfirmDecline(internship);
                return Ok(new { Message = "Staj kaydı onayı geri alındı." });
            }

            return BadRequest(new { Message = "Staj kaydı bulunamadı." });
        }

        //Staj kaydı Kabul edilecekse
        [HttpPut("InternAccept/{id}")]
        public IActionResult InternAccept(int id, int acceptDays)
        {
            var internship = internStatusGenericRepo.INGetById(id);

            if (internship != null)
            {
                internStatusGenericRepo.INGetAccept(internship, acceptDays);
                return Ok(new { Message = "Staj kaydı kabul edildi." });
            }

            return BadRequest(new { Message = "Staj kaydı bulunamadı." });
        }

        //Staj kaydı kabulü geri çekilecekse
        [HttpPut("InternAcceptDecline/{id}")]
        public IActionResult InternAcceptDecline(int id)
        {
            var internship = internStatusGenericRepo.INGetById(id);

            if (internship != null)
            {
                internStatusGenericRepo.INGetAcceptDecline(internship);
                return Ok(new { Message = "Staj kaydı kabulu geri alındı." });
            }

            return BadRequest(new { Message = "Staj kaydı bulunamadı." });
        }

        //Staj kaydı kabulü reddedilecekse
        [HttpPut("InternReject/{id}")]
        public IActionResult InternReject(int id, string rejectReason)
        {
            var internship = internStatusGenericRepo.INGetById(id);

            if (internship != null)
            {
                internStatusGenericRepo.INGetReject(internship, rejectReason);
                return Ok(new { Message = "Staj Reddedildi." });
            }

            return BadRequest(new { Message = "Staj kaydı bulunamadı." });
        }

        //Staj kaydının katkı payı onaylanacaksa
        [HttpPut("ContributConfirm/{id}")]
        public IActionResult ContributConfirm(int id)
        {
            var internship = internStatusGenericRepo.INGetById(id);

            if (internship != null)
            {
                internStatusGenericRepo.INGetContributConfirm(internship);
                return Ok(new { Message = "Staj kaydı katkı payı onaylandı." });
            }

            return BadRequest(new { Message = "Staj kaydı bulunamadı." });
        }

        //Staj kaydının katkı payı onaylaması geri çekilecekse
        [HttpPut("ContributDecline/{id}")]
        public IActionResult ContributDecline(int id)
        {
            var internship = internStatusGenericRepo.INGetById(id);

            if (internship != null)
            {
                internStatusGenericRepo.INGetContributDecline(internship);
                return Ok(new { Message = "Staj kaydı katkı payı onayı geri alındı." });
            }
            return BadRequest(new { Message = "Staj kaydı bulunamadı." });
        }
    }
}
