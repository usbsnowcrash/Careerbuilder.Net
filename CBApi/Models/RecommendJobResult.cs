using System;
using System.Collections.Generic;

namespace CBApi.Models {
    [Serializable]
    public class RecommendJobResult {
        public string JobDID { get; set; }
        public string Title { get; set; }
        public string HostSite { get; set; }
        public double Relevancy { get; set; }
        public Location Location { get; set; }
        public Company Company { get; set; }
        public string JobDetailsURL { get; set; }
        public string JobServiceURL { get; set; }
        public string SimilarJobsURL { get; set; }
        public DateTime PostingDate { get; set; }
        public bool CanBeQuickApplied { get; set; }
        public string MatcherType { get; set; }
        public string EducationRequired { get; set; }
        public string EmploymentType { get; set; }
        public string ExperienceRequired { get; set; }
        public string Pay { get; set; }
        public int JobLevel { get; set; }
        public List<string> ApplyRequirements { get; set; }
        public string ONetFriendlyTitle { get; set; }
        public string ONet { get; set; }
        
    }
}