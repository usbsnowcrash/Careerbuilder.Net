using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Reflection;

namespace CBApi.Models {
    [Serializable]
    public class ApplyLink {
        public string DeveloperKey { get; set; }
        public string JobDID { get; set; }
        public string SiteID { get; set; }
        public string HostSite { get; set; }
        public string JApply { get; set; }
        public string ApplicationEmail { get; set; }
        public string TrackingID { get; set; }
        public string Cobrand { get; set; }

        public ApplyLink(NameValueCollection args) {
            foreach (string arg in args.Keys) {
                var x = this.GetType().GetProperties();
                this.GetType().GetProperty(arg).SetValue(this, args[arg], null);
	        }
        }
    }
}
