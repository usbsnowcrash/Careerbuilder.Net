using RestSharp.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CBApi.Models
{
    [Serializable]
    [SerializeAs(Name = "ResponseApplication")]
    public class AnonymousApplicationResponse
    {
        public string Errors { get; set; }
        public DateTime TimeResponseSent { get; set; }
        public float TimeElapsed { get; set; }
        public string ApplicationStatus { get; set; }
    }
}
