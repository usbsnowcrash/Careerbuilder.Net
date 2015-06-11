using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CBApi.Models {
    public class JsonWrapper<T> {
        public JsonWrapper() { }

        public string TotalResults { get; set; }
        public string ReturnedResults { get; set; }
        public List<T> Results { get; set; }
        public List<string> Errors { get; set; }
        public string Timestamp { get; set; }
        public string Status { get; set; }
    }
}
