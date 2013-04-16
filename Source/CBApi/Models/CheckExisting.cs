using System;
using RestSharp.Serializers;

namespace CBApi.Models {
    [Serializable, SerializeAs(Name = "Request")]
    public class UserCheck
    {
        public string DeveloperKey { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Test { get; set; }
    }
}
