using System;
using System.Collections.Specialized;
using System.Reflection;

namespace CBApi.Models {
    [Serializable]
    public class ApplyLink {
        public string JobDID { get; set; }
        public string SiteID { get; set; }
        public string HostSite { get; set; }
        public string JApply { get; set; }
        public string ApplicationEmail { get; set; }
        public string TrackingID { get; set; }
        public string Cobrand { get; set; }

        public ApplyLink() : this(new NameValueCollection()) { }

        public ApplyLink(NameValueCollection args) {
            foreach (string arg in args.Keys) {
                this.GetType().GetProperty(arg).SetValue(this, args[arg], null);
            }
            foreach (PropertyInfo property in this.GetType().GetProperties()) {
                if (property.GetValue(this, null) == null) {
                    property.SetValue(this, String.Empty, null);
                }
            }
        }
    }
}
