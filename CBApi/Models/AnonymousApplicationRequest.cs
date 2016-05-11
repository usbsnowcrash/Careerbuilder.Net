using RestSharp.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CBApi.Models
{
    [Serializable]
    [SerializeAs(Name = "Request")]
    public class AnonymousApplicationRequest
    {
        public string DeveloperKey { get; set; }
        public string JobDID { get; set; }
        public bool Test { get; set; }
        public string SiteID { get; set; }
        public string CoBrand { get; set; }
        public string HostSite { get; set; }
        public string TNDID { get; set; }
        public Resume Resume { get; set; }
        public List<Response> Responses { get; set; }

        public void AttachResumeFile(string fileName, byte[] resumeFile)
        {
            Resume = new Resume { ResumeFileName = fileName, ResumeData = Convert.ToBase64String(resumeFile) };
        }
    }

}
