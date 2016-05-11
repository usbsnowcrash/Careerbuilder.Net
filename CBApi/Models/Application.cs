using System;
using System.Collections.Generic;
using CBApi.Models.Responses;

namespace CBApi.Models {
    [Serializable]
    public class Application {
        public string DeveloperKey { get; set; }
        public string JobDID { get; set; }
        public bool Test { get; set; }
        public string SiteID { get; set; }
        public string CoBrand { get; set; }
        public Resume Resume { get; set; }
        public List<Question> Questions { get; set; }

        public void AttachResumeFile(string fileName, byte[] resumeFile) {
            Resume = new Resume { ResumeFileName = fileName, ResumeData = Convert.ToBase64String(resumeFile) };
        }
    }
}