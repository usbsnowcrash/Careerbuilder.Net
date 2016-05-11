using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CBApi {
    [Serializable]
    public class APIException : Exception {

        public List<string> APIErrors { get; set; }
        public APIException(string message) : base(message){
            APIErrors = new List<string>();
            APIErrors.Add(message);
        }

        public APIException(string message,List<string> errors) : base(message) {
            APIErrors = errors;
        }
    }
}
