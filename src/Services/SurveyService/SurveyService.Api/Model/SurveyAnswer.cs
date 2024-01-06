using System.ComponentModel.DataAnnotations;

namespace SurveyService.Api.Model
{
    // Survey Servicesindeki SurveyAnswer Tablosunun Entityleri
    public class SurveyAnswer
    {
        [Key]
        public int ID { get; set; }
        public int QuestionNumber { get; set; }
        public string StudentNo { get; set; }
        public string AnswerOption { get; set; }

    }
}
