using System;
namespace CBApi.Models {
    [Serializable]
    public class Response {
        public string QuestionID { get; set; }
        public string ResponseText { get; set; }
    }
}
