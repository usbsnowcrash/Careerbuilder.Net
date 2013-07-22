using RestSharp.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CBApi.Models
{
    [Serializable]
    [SerializeAs(Name = "SavedJobSearch")]
    public class SavedSearchRetrieveResponseModel : SavedSearchCreateResponse
    {
        //added this class for simplicity of naming.  all of the data elements are the same as SavedSearchCreateResponse
        //In reality you could call SavedSearchCreateResponse, but this is easier to understand why it is being called
    }
}
