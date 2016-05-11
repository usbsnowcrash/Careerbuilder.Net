using System.Collections.Generic;
using CBApi.Models.Responses;
using System;

namespace CBApi.Models {
    [Serializable]
    public class BlankApplication : Application {
        public string ApplicationSubmitServiceURL { get; set; }
        public string ApplyURL { get; set; }
        public ApplicationRequirements Requirements { get; set; }
        public string JobTitle { get; set; }
        public int TotalQuestions { get; set; }
        public int TotalRequiredQuestions { get; set; }
    }
}