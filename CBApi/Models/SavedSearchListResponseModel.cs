using System;
using System.Collections.Generic;
using RestSharp.Serializers;

namespace CBApi.Models {
    [Serializable, SerializeAs(Name = "SavedJobSearches")]
    public class SavedSearchListResponseModel {

        public string Errors { get; set; }

        public List<SavedSearch> SavedSearches { get; set; }

    }

    [Serializable, SerializeAs(Name = "SavedSearch")]
    public class SavedSearch {

        public string SearchName { get; set; }

        public string HostSite { get; set; }

        public string ExternalID { get; set; }

    }
}