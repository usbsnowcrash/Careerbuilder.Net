using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CBApi.Models.WebAPIs
{
    public class WebApiBase
    {
        public string Errors { get; set; }
        public int ReturnedResults { get; set; }
        public string Status { get; set; }
        public string Timestamp { get; set; }
        public string TotalResults { get; set; }
    }
}
