using RestSharp.Serializers;
using System;

namespace CBApi.Models
{
    [Serializable]
    [SerializeAs(Name = "Request")]
    public class RequestSavedSearchDelete {
        public string DeveloperKey { get; set; }
        public string ExternalID { get; set; }
        public string ExternalUserID { get; set; }
        public String HostSite { get; set; }
        public String Status { get; set; }
    }
}
