using System;

namespace CBApi.Models.CheckExisting {
    [Serializable]
    public class Request
    {
        public string DeveloperKey { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Test { get; set; }
    }

    [Serializable]
    public class ResponseUserCheck
    {
        public string UserCheckStatus { get; set; }
        public string ResponseExternalID { get; set; }
    }
}
