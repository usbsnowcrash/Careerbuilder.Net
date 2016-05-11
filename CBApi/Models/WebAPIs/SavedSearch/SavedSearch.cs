using CBApi.Models.WebAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CBApi.Models.WebAPIs.SavedSearch
{
    public class SavedSearches : WebApiBase
    {
        public List<SavedSearch> Results { get; set; }
    }

    public class SavedSearch
    {
        public string DID { get; set; }
        public string SearchName { get; set; }
        public string HostSite { get; set; }
        public string SiteID { get; set; }
        public string Cobrand { get; set; }
        public string IsDailyEmail { get; set; }
        public string EmailDeliveryDay { get; set; }
        public string JobSearchUrl { get; set; }
        public Parameters SavedSearchParameters { get; set; }
    }

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
        public string JCAdvertiserFlags { get; set; }
        public string JCJobNature { get; set; }
        public string Company { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}
