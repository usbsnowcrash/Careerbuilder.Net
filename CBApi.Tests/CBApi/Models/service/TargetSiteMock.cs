using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CBApi.Models.Service;

namespace Tests.CBApi.models.service {
    public class TargetSiteMock : TargetSite {
        public TargetSiteMock(string domain) {
            _Domain = domain;
        }

        public string SetHost {
            set { _HostOverride = value; }
        }

        public bool SetSecure {
            set { _Secure = value; }
        }

        public Dictionary<string, string> SetHeaders {
            set { _AdditionalHeaders = value; }
            get {return _AdditionalHeaders; }
        }
    }
}
