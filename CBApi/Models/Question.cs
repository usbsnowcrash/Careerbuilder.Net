using System;
namespace CBApi.Models.Responses {
    [Serializable]
    public class Question {
        public string QuestionID { get; set; }
        public string QuestionType { get; set; }
        public bool IsRequired { get; set; }
        public string ExpectedResponseFormat { get; set; }
        public string QuestionText { get; set; }
        public string ResponseText { get; set; }
    }
}