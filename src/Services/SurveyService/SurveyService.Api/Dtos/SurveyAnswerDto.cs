namespace SurveyService.Api.Dtos
{
    //Survey Servicesinin diğer serviceler ile arasındaki haberleşmede göndereceği SurveyAnswer entityleri
    public class SurveyAnswerDto
    {
        public int ID { get; set; }
        public int QuestionNumber { get; set; }
        public string StudentNo { get; set; }
        public object Answer { get; set; }
    }
}
