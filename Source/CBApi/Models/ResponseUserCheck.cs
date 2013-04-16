using System;

namespace CBApi.Models.Responses
{
    [Serializable]
    public class ResponseUserCheck
    {
        public string UserCheckStatus { get; set; }
        public string ResponseExternalID { get; set; }
    }
}