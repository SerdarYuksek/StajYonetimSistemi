namespace SurveyService.Api.Dtos
{
    //Survey Servicesinin diğer serviceler ile arasındaki haberleşmede göndereceği SurveyQuestion entityleri
    public class SurveyQuestionDto
    {
        public int ID { get; set; }
        public int QuestionNumber { get; set; }
        public string UserChoice { get; set; }
    }
}
