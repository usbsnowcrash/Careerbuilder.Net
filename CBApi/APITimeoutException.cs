using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CBApi {
    [Serializable]
    public class APITimeoutException : APIException {
        public APITimeoutException(string message)
            : base(message) {
        }
    }
}
