using System;

namespace CBApi.Models
{
    [Serializable]
    public class SavedSearchDeleteResponse
    {
        public RequestSavedSearchDelete request { get; set; }
        public String Status { get; set; }
    }
}
