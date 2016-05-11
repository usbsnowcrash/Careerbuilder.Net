using System;
using System.Collections.Generic;

namespace CBApi.Models {
    [Serializable]
    public class RequestApplication {
        public string DeveloperKey { get; set; }
        public string JobDID { get; set; }
        public string SiteID { get; set; }
        public string CoBrand { get; set; }
        public bool Test { get; set; }
        public Resume Resume { get; set; }
        public List<Response> Responses { get; set; }
    }
}
