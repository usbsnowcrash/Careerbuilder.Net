using RestSharp.Serializers;
using System;

namespace CBApi.Models
{
    [Serializable]
    [SerializeAs(Name="Request")]
    public class SavedSearchUpdateRequestModel :SavedSearchCreate
    {
        //as it says in the documentation this uses all of the same params as
        //saved search create with the addition of ExternalID
        public string ExternalID { get; set; }
    }
}
