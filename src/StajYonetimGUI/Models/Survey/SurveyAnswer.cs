using System.ComponentModel.DataAnnotations;

namespace StajYonetimGUI.Models.Survey
{
    public class SurveyAnswer
    {
        [Key]
        public int ID { get; set; }
        public int QuestionNumber { get; set; }
        public string StudentNo { get; set; }
        public string AnswerOption { get; set; }
    }
}
