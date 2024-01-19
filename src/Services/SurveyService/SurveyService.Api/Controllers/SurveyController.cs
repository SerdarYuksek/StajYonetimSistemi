using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Persistence.Context;
using Persistence.Repository;
using X.PagedList;

namespace SurveyService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        //Generic Classta yapılan CRUD işlemleri bir entitye tanımlayıp nesne oluşturuldu 
        private SurveyDbContext dbContext;
        private CrudGenericRepository<SurveyQuestion> surveyQuestionGenericRepo;
        private CrudGenericRepository<SurveyAnswer> surveyAnswerGenericRepo;

        //User Controllerın Contructerında dbcontextimiz ve nesnelerimiz generic taraf ile bağlandı
        public SurveyController(SurveyDbContext dbContext)
        {
            this.dbContext = dbContext;
            surveyQuestionGenericRepo = new CrudGenericRepository<SurveyQuestion>(dbContext);
            surveyAnswerGenericRepo = new CrudGenericRepository<SurveyAnswer>(dbContext);
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
            var allQuestions = surveyQuestionGenericRepo.SGetListAll();

            // Veritabanında soru varsa eğer son numarayı atıyoruz
            if (allQuestions.Any())
            {
                var lastQuestionNumber = allQuestions.Max(x => x.QuestionNumber);
                s.QuestionNumber = lastQuestionNumber + 1;
            }
            else
            {
                // Eğer hiç soru yoksa, soru numarasını 1 olarak ayarlayın.
                s.QuestionNumber = 1;
            }

            surveyQuestionGenericRepo.SAdd(s);
            return Ok(new { Message = "Anket Sorusu oluşturuldu." });
        }


        //Anket Sorusu silinecekse
        [HttpDelete("QuestionDelete{id}")]
        public IActionResult QuestionDelete(int id)
        {
            if (id != null)
            {
                var question = surveyQuestionGenericRepo.SGetById(id);
                surveyQuestionGenericRepo.SDelete(question);
                return Ok(new { Message = "Anket Sorusu silindi." });
            }
            else
            {
                return BadRequest("İd değeri geçersiz");
            }

        }

        //Anket Soru ve Şık bilgilerinin güncellemesi için id değerine göre soru içeriğinin ekrana getirilmesi
        [HttpGet("QuestionUpdate{id}")]
        public IActionResult QuestionUpdate(int? id)
        {
            if (id != null)
            {
                var question = surveyQuestionGenericRepo.SGetById(id);
                return Ok(question);
            }
            else
            {
                return BadRequest("İd değeri geçersiz");
            }
        }

        //Anket Soru ve şık bilgilerinin güncellenmesi
        [HttpPut("QuestionUpdate")]
        public IActionResult QuestionUpdate(SurveyQuestion s)
        {

            var x = surveyQuestionGenericRepo.SGetById(s.ID);

            var existQuestionNumber = surveyQuestionGenericRepo.SGetListAll().FirstOrDefault(x => x.QuestionNumber == s.QuestionNumber);

            if (existQuestionNumber.QuestionNumber != null && existQuestionNumber.QuestionNumber != s.QuestionNumber)
            {
                x.QuestionNumber = s.QuestionNumber;
                x.Question = s.Question;
                x.QuestionOptionsA = s.QuestionOptionsA;
                x.QuestionOptionsB = s.QuestionOptionsB;
                x.QuestionOptionsC = s.QuestionOptionsC;
                x.QuestionOptionsD = s.QuestionOptionsD;
                x.QuestionOptionsE = s.QuestionOptionsE;

                surveyQuestionGenericRepo.SUpdate(x);
                return Ok(new { Message = "Anket Soru ve şıkları Başarıyla Güncellendi" });
            }
            else
            {
                return BadRequest("Farklı bir soru numarası giriniz");
            }
        }

        //Anket sorularının kullanıcıya teker teker sunulması
        [HttpGet("ListSurvey")]
        public IActionResult ListSurvey(int page)
        {
            var surveyList = surveyQuestionGenericRepo.SGetListAll().ToPagedList(page, 1);

            // Eğer sayfa numarası, toplam sayfa sayısından büyükse, anket bitmiş demektir.
            if (page > surveyList.PageCount)
            {
                return Ok(new { Message = "Teşekkürler.Anketi tamamlandınız." });
            }

            // İlgili sayfadaki soruyu alın
            var currentSurvey = surveyList.FirstOrDefault();

            return Ok(new { CurrentSurvey = currentSurvey });
        }


        // Anket cevaplarının kaydedilmesi 
        [HttpPost("AnswerSave")]
        public IActionResult AnswerSave(SurveyAnswer a)
        {
            surveyAnswerGenericRepo.SAdd(a);
            return Ok();
        }

    }
}
