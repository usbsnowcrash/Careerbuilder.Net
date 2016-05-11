using System;
using System.IO;
using System.Xml.Linq;
using CBApi.Models;
using NUnit.Framework;
using RestSharp;
using RestSharp.Deserializers;

namespace Tests.com.careerbuilder.CBApi.models
{
    [TestFixture]
    public class RecommendJobResultTest
    {
        [Test]
        public void VerifyCorrectXml_DeserializesAppropriately() {
            var xmlpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testdata","RecommendJobResult.xml");
            var doc = XDocument.Load(xmlpath);

            var xml = new XmlDeserializer();
            var output = xml.Deserialize<RecommendJobResult>(new RestResponse() { Content = doc.ToString() });

            Assert.IsNotNull(output);
            Assert.AreEqual("JHT3D8649TV74ZNK0GJ", output.JobDID);
            Assert.AreEqual("CJ123_COMPANY_DID", output.Company.CompanyDID);
            Assert.AreEqual("www.qualitystaffing.com", output.Company.CompanyDetailsURL);
            Assert.AreEqual(1, output.Relevancy);
            Assert.AreEqual("47-2211.00", output.ONet);
            Assert.AreEqual("Sheet Metal Workers", output.ONetFriendlyTitle);
            Assert.AreEqual("http://www.careerbuilder.com/jobseeker/jobs/recommendedjobs.aspx?ipath=JELO&job_did=JHT3D8649TV74ZNK0GJ", output.SimilarJobsURL);
            Assert.AreEqual("http://api.careerbuilder.com/v1/job?DID=JHT3D8649TV74ZNK0GJ&DeveloperKey=XYZ_DEVKEY", output.JobServiceURL);
            Assert.AreEqual("http://api.careerbuilder.com/v1/joblink?DID=JHT3D8649TV74ZNK0GJ&DeveloperKey=XYZ_DEVKEY", output.JobDetailsURL);
            Assert.AreEqual("Georgetown", output.Location.City);
            Assert.AreEqual("DE", output.Location.State);
            Assert.AreEqual("Fabrication - Assembly - Sheet Metal", output.Title);
            Assert.AreEqual(true, output.CanBeQuickApplied);
            Assert.AreEqual("CX", output.MatcherType);
            Assert.AreEqual("Not Specified", output.EducationRequired);
            Assert.AreEqual("Full-Time", output.EmploymentType);
            Assert.AreEqual("$20k - $30k/year", output.Pay);
            Assert.AreEqual("CanBulkApply", output.ApplyRequirements[0]);
        }
    }
}
