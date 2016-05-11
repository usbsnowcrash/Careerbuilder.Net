using CBApi.Models;
using NUnit.Framework;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.IO;
using System.Xml.Linq;

namespace Tests.CBApi.models {
    [TestFixture]
    public class ApplicationRequirementsTest {
        [Test]
        public void DeserializationWorks_WhenPassedRightXML() {
            var xmlpath = Path.Combine(Environment.CurrentDirectory, "ResponseBlankApplication.xml");
            var doc = XDocument.Load(xmlpath);

            var xml = new XmlDeserializer();
            var output = xml.Deserialize<BlankApplication>(new RestResponse() { Content = doc.ToString() });
            Assert.IsNotNull(output.Requirements);
            Assert.AreEqual("2 Year Degree", output.Requirements.DegreeRequired.Trim());
            Assert.AreEqual("Not Specified", output.Requirements.TravelRequired.Trim());
            Assert.AreEqual("At least 5 year(s)", output.Requirements.ExperienceRequired.Trim());
            Assert.IsTrue(output.Requirements.RequirementsText.Contains("nstallation tech, field rep, HVAC, mechanic, installer, repairman, auto technician, service specialist, millwright, millright"));
        }
    }
}
