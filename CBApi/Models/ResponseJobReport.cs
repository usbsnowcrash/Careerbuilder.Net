using System;

namespace CBApi.Models.Responses
{
    [Serializable]
    public class ResponseJobReport
    {
        public DateTime TimeResponseSent { get; set; }
        public float TimeElapsed { get; set; }
        public string JobDid { get; set; }
        public int TotalApps { get; set; }
        public Bucket Buckets { get; set; }
    }
}