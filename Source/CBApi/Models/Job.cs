using System;

namespace CBApi.Models {
    [Serializable]
    public class Job {
        public string ApplyURL { get; set; }
        public bool ExternalApplication { get; set; }
        public string ApplicationSubmitServiceURL { get; set; }
        public DateTime BeginDate { get; set; }
        public string BlankApplicationServiceURL { get; set; }
        public string Categories { get; set; }
        public string Company { get; set; }
        public string CompanyDetailsURL { get; set; }
        public string CompanyDID { get; set; }
        public string CompanyJobSearchURL { get; set; }
        public string CompanyImageURL { get; set; }
        public string ContactInfoEmailURL { get; set; }
        public string ContactInfoFax { get; set; }
        public string ContactInfoName { get; set; }
        public string ContactInfoPhone { get; set; }
        public string DegreeRequired { get; set; }
        public string DID { get; set; }
        public string DisplayJobID { get; set; }
        public string Division { get; set; }
        public string EmploymentType { get; set; }
        public DateTime EndDate { get; set; }
        public string ExperienceRequired { get; set; }
        public string JobDescription { get; set; }
        public string JobRequirements { get; set; }
        public string JobTitle { get; set; }
        public string LocationStreet1 { get; set; }
        public string LocationStreet2 { get; set; }
        public string LocationCity { get; set; }
        public string LocationCountry { get; set; }
        public string LocationFormatted { get; set; }
        public string LocationLatitude { get; set; }
        public string LocationLongitude { get; set; }
        public string LocationMetroCity { get; set; }
        public string LocationPostalCode { get; set; }
        public string LocationState { get; set; }
        public bool ManagesOthers { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ONetCode { get; set; }
        public Money PayHigh { get; set; }
        public Money PayLow { get; set; }
        public string PayPer { get; set; }
        public string PayHighLowFormatted { get; set; }
        public Money PayCommission { get; set; }
        public Money PayBonus { get; set; }
        public string PayOther { get; set; }
        public string PrinterFriendlyURL { get; set; }
        public bool RelocationCovered { get; set; }
        public string TravelRequired { get; set; }
    }
}