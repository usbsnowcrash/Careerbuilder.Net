using RestSharp.Serializers;
using System;


namespace CBApi.Models
{
    [Serializable]
    [SerializeAs(Name = "SavedJobSearch")]
    public class SavedSearchCreateResponse
    {
        public string Errors { get; set; }
        public Search SavedSearch { get; set; }
    }

    [Serializable]
    [SerializeAs(Name="SavedSearch")]
    public class Search
    {
        public string SearchName { get; set; }
        public string HostSite { get; set; }
        public string SiteID { get; set; }
        public string Cobrand { get; set; }
        public string IsDailyEmail { get; set; }
        public string EmailDeliveryDay { get; set; }
        public Parameters SavedSearchParameters { get; set; }
        public string JobSearchUrl { get; set; }
    }

    [Serializable]
    [SerializeAs(Name="SavedSearchParameters")]
    public class Parameters
    {
        public string BooleanOperator { get; set; }
        public string Category { get; set; }
        public string EducationCode { get; set; }
        public string EmpType { get; set; }
        public string ExcludeCompanyNames { get; set; }
        public string ExcludeJobTitles { get; set; }
        public string ExcludeKeywords { get; set; }
        public bool ExcludeNational { get; set; }
        public string IndustryCodes { get; set; }
        public string JobTitle { get; set; }
        public string Keywords { get; set; }
        public string Location { get; set; }
        public string OrderBy { get; set; }
        public string OrderDirection { get; set; }
        public int PayHigh { get; set; }
        public bool PayInfoOnly { get; set; }
        public int PayLow { get; set; }
        public int PostedWithin { get; set; }
        public int Radius { get; set; }
        public bool SpecificEducation { get; set; }
        public string JCPositionLevel { get; set; }
        public string JCLocation { get; set; }
        public string JCAdvertiserflags { get; set; }
        public string JCJobNature { get; set; }
        public string JobCategory { get; set; }
        public string Company { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
