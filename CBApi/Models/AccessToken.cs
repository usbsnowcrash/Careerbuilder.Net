using System;
using System.Collections.Generic;
using CBApi.Models.Responses;

namespace CBApi.Models {
    [Serializable]
    public class AccessToken {
        public string Code { get; set; }
        public DateTime Expires { get; set; }
        public string Redirect_Uri { get; set; }
        public string Client_ID { get; set; }
        public string Access_Token { get; set; }
        public double Expires_In { get; set; }
        public string Refresh_Token { get; set; }
        public string User_Full_Name { get; set; }
        public string User_Email_Address { get; set; }
    }
}