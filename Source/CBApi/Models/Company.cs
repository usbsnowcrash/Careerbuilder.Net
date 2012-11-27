using System;

namespace CBApi.Models {
    [Serializable]
    public class Company {
        public string CompanyName { get; set; }
        public string CompanyDetailsURL { get; set; }
        public string CompanyDid { get; set; }
    }
}