using RestSharp.Serializers;
using System;


namespace CBApi.Models
{
    [Serializable]
    [SerializeAs(Name = "Request")]
    public class SavedSearchCreate
    {
        public string HostSite { get; set; }
        public string SearchName { get; set; }
        public string Cobrand { get; set; }
        public string SiteID { get; set; }
        public SearchParameters SavedSearchParameters { get; set; }
        public string IsDailyEmail { get; set; }
        public string ExternalUserID { get; set; }
        public string DeveloperKey { get; set; }
    }

    [Serializable]
    [SerializeAs(Name="SearchParameters")]
    public class SearchParameters
    {
        public string BooleanOperator { get; set; }
        public string Category { get; set; }
        public string EducationCode { get; set; }
        public bool SpecificEducation { get; set; }
        public string EmpType { get; set; }
        public string ExcludeCompanyNames { get; set; }
        public string ExcludeJobTitles { get; set; }
        public string ExcludeKeywords { get; set; }
        public bool ExcludeNational { get; set; }
        public string IndustryCodes { get; set; }
        public string JobTitle { get; set; }
        public string Keywords { get; set; }
        public string OrderBy { get; set; }
        public string OrderDirection { get; set; }
        public int Radius { get; set; }
        public int PayHigh { get; set; }
        public int PayLow { get; set; }
        public int PostedWithin { get; set; }
        public bool PayInfoOnly { get; set; }
        public string Location { get; set; }
        public string JobCategory { get; set; }
        public string Company { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
