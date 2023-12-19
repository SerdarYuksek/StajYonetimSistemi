using Microsoft.AspNetCore.Mvc;
using SurveyService.Api.Context;
using SurveyService.Api.Manager;
using SurveyService.Api.Model;
using X.PagedList;

namespace SurveyService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        //Generic Classta yapılan CRUD işlemleri bir entitye tanımlayıp nesne oluşturuldu 
        private SurveyDbContext dbContext;
        private CrudGenericRepository<SurveyQuestion> sigr;
        private CrudGenericRepository<SurveyAnswer> sagr;

        //User Controllerın Contructerında dbcontextimiz ve nesnelerimiz generic taraf ile bağlandı
        public SurveyController(SurveyDbContext dbContext)
        {
            this.dbContext = dbContext;
            sigr = new CrudGenericRepository<SurveyQuestion>(dbContext);
            sagr = new CrudGenericRepository<SurveyAnswer>(dbContext);
        }

        // Anket Sorusu Oluşturma ekranına erişme (get)
        [HttpGet("AddQuestion")]
        public IActionResult AddQuestion()
        {

            return Ok();
        }

        // Anket Sorusu Oluşturma ekranınında sorunun ve şıkların gönderilmesi metodu(post)
        [HttpPost("AddQuestion")]
        public IActionResult AddQuestion(SurveyQuestion s)
        {
            int lastQuestionNumber = sigr.SGetListAll().Max(x => x.QuestionNumber);
            s.QuestionNumber = lastQuestionNumber + 1;
            sigr.SAdd(s);
            return Ok(new { Message = "Anket Sorusu oluşturuldu." });

        }

        //Anket Sorusu silinecekse
        [HttpDelete("QuestionDelete")]
        public IActionResult QuestionDelete(SurveyQuestion s)
        {
            var SurveyID = sigr.SGetById(s.ID);
            sigr.SDelete(SurveyID);
            return Ok(new { Message = "Anket Sorusu silindi." });
        }

        //Anket Soru ve Şık bilgilerinin güncellemesi için id değerine göre soru içeriğinin ekrana getirilmesi
        [HttpGet("QuestionUpdate")]
        public IActionResult QuestionUpdate(int id)
        {
            var question = sigr.SGetById(id);
            return Ok(question); 

        }

        //Anket Soru ve şık bilgilerinin güncellenmesi
        [HttpPut("QuestionUpdate")]
        public IActionResult QuestionUpdate(SurveyQuestion s)
        {

            var x = sigr.SGetById(s.ID);

            x.Question = s.Question;
            x.QuestionOptionsA = s.QuestionOptionsA;
            x.QuestionOptionsB = s.QuestionOptionsB;
            x.QuestionOptionsC = s.QuestionOptionsC;
            x.QuestionOptionsD = s.QuestionOptionsD;
            x.QuestionOptionsE = s.QuestionOptionsE;

            sigr.SUpdate(x);
            return Ok(new { Message = "Anket Soru ve şıkları Başarıyla Güncellendi" });

        }

        //Anket sorularının kullanıcıya teker teker sunulması
        [HttpGet("ListSurvey")]
        public IActionResult ListSurvey( int page = 1)
        {
            var surveyList = sigr.SGetListAll().ToPagedList(page, 1);

            // Eğer sayfa numarası, toplam sayfa sayısından büyükse, anket bitmiş demektir.
            if (page > surveyList.PageCount)
            {
                return Ok(new { Message = "Teşekkürler.Anketi tamamlandınız." });
            }

            // İlgili sayfadaki soruyu alın
            var currentSurvey = surveyList.FirstOrDefault();

            return Ok(new { CurrentSurvey = currentSurvey, TotalPages = surveyList.PageCount, NextPage = page + 1 });
        }


        // Anket cevaplarının kaydedilmesi 
        [HttpPost("AnswerSave")]
        public IActionResult AnswerSave(SurveyAnswer a)
        {
            sagr.SAdd(a);
            int nextPage = sigr.SGetListAll().ToPagedList().PageNumber + 1; // Sayfa numarasını bir artırarak bir sonraki soruya geçin
            return Ok(new { NextPage = nextPage }); //yeni page numarası ApiGatewaye gönderilerek listsurveye aktarılır.
        }

    }
}
