using System.Collections.Generic;

namespace CBApi.Models.Service
{
    public abstract class TargetSite
    {
        protected string _Domain;
        protected string _HostOverride = null;
        protected bool _Secure = true;
        protected Dictionary<string, string> _AdditionalHeaders = new Dictionary<string, string>();

        public string Domain
        {
            get { return _Domain; }
        }

        public string Host {
            get { return _HostOverride; }
        }

        public bool Secure {
            get { return _Secure; }
        }

        public Dictionary<string, string> Headers {
            get { return _AdditionalHeaders; }
        }
    }
}