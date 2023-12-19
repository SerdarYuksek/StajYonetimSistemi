﻿using System.ComponentModel.DataAnnotations;

namespace SurveyService.Api.Model
{
    //Survey servicesindeki SurveyQuestion tablosunun entityleri
    public class SurveyQuestion
    {
        [Key]
        public int ID { get; set; }
        public int QuestionNumber { get; set; }
        public string Question { get; set; }
        public string QuestionOptionsA { get; set; }
        public string QuestionOptionsB { get; set; }
        public string QuestionOptionsC { get; set; }
        public string QuestionOptionsD { get; set; }
        public string QuestionOptionsE { get; set; }
    }
}
