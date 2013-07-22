using RestSharp.Serializers;
using System;
using System.Collections.Generic;

namespace CBApi.Models
{
    [Serializable]
    [SerializeAs(Name="SavedJobSearches")]
    public class SavedSearchListResponseModel
    {
        public string Errors { get; set; }
        public Searches SavedSearches { get; set; }
    }

    [Serializable]
    [SerializeAs(Name="SavedSearches")]
    public class Searches
    {
        public List<SavedSearch> SavedSearchList { get; set; }
    }

    [Serializable]
    [SerializeAs(Name="SavedSearch")]
    public class SavedSearch
    {
        public string SearchName { get; set; }
        public string HostSite { get; set; }
        public string ExternalID { get; set; }
    }
}
