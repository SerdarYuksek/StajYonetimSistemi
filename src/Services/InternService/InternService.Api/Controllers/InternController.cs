using InternService.Api.Manager;
using InternService.Api.Model;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace InternService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InternController : ControllerBase
    {
        private CrudGenericRepository<InternInfo> igr;
        private CrudGenericRepository<InternStatus> isgr;

        [HttpGet]
        public IActionResult CreateIntern()
        {

            return Ok();
        }

        [HttpPost]
        public IActionResult CreateIntern(InternInfo i)
        {

            i.StartDate = new DateTime(i.StartDate.Year, i.StartDate.Month, i.StartDate.Day);
            i.FinishDate = new DateTime(i.FinishDate.Year, i.FinishDate.Month, i.FinishDate.Day);
            igr.INAdd(i);
            return Ok(new { Message = "Staj kaydı oluşturuldu." });

        }
        [HttpGet]
        public IActionResult ListIntern()
        {
            //var intern = igr.INGetListAll().Where(x => x.StudentNo == User.Identity.StudentNo);
            return Ok();

        }
        [HttpGet]
        public IActionResult ListInternInfo(int id)
        {
            var intern = igr.INGetListAll().Where(x => x.ID == id);
            return Ok();
        }
        [HttpGet]
        public IActionResult InternDel(InternInfo i)
        {
            var InternID = igr.INGetById(i.ID);
            igr.INDelete(InternID);
            return Ok();
        }
        [HttpGet]
        public IActionResult InternConfırm(InternStatus i)
        {
            isgr.INGetConfirm(i);
            return Ok();

        }
        [HttpGet]
        public IActionResult InternDecline(InternStatus i)
        {
            isgr.INGetDecline(i);
            return Ok();

        }
        [HttpGet]
        public IActionResult InternAccept(InternStatus i)
        {
            isgr.INGetAccept(i);
            return Ok();

        }
        [HttpGet]
        public IActionResult InterntReject(InternStatus i)
        {
            isgr.INGetReject(i);
            return Ok();

        }
        [HttpGet]
        public IActionResult ContributConfirm(InternStatus i)
        {
            isgr.INGetContributConfirm(i);
            return Ok();

        }
        [HttpGet]
        public IActionResult ContributDecline(InternStatus i)
        {
            isgr.INGetContributDecline(i);
            return Ok();

        }

    }
}
