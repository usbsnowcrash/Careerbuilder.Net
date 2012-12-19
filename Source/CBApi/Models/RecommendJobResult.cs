using System;

namespace CBApi.Models {
    [Serializable]
    public class RecommendJobResult {
        public string JobDID { get; set; }
        public string Title { get; set; }
        public double Relevancy { get; set; }
        public Location Location { get; set; }
        public Company Company { get; set; }
        public string JobDetailsURL { get; set; }
        public string JobServiceURL { get; set; }
        public string SimilarJobsURL { get; set; }
        public bool CanBeQuickApplied { get; set; }
        public string ONet { get; set; }
        public string ONetFriendlyTitle { get; set; }
        public DateTime PostingDate { get; set; }
    }
}