using NUnit.Framework;
using System;
using RestSharp;
using RestSharp.Deserializers;
using System.Xml.Linq;
using System.IO;
using CBApi.Models;

namespace Tests.com.careerbuilder.CBApi.models.service
{
    [TestFixture]
    public class AnonymousApplicationTest
    {
        [Test]
        public void DeserializationWorks_WhenPassedRightXML_Response()
        {
            var xmlpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testdata","AnonymousApplicationResponseData.xml");
            var doc = XDocument.Load(xmlpath);

            var xml = new XmlDeserializer();
            var output = xml.Deserialize<AnonymousApplicationResponse>(new RestResponse() { Content = doc.ToString() });
            Assert.IsNotNull(output, "no deserialization worked");
            Assert.AreEqual("Complete", output.ApplicationStatus);
            Assert.AreEqual(4, output.TimeElapsed);
            DateTime time = new DateTime(2011, 1, 11, 13, 11, 11);
            Assert.AreEqual(time, output.TimeResponseSent);
            Assert.IsTrue(string.IsNullOrEmpty(output.Errors));
        }

        [Test]
        public void DeserializationWorks_WhenPassedRightXML_Request()
        {
            var xmlpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testdata", "AnonymousApplicationRequestData.xml");
            var doc = XDocument.Load(xmlpath);

            var xml = new XmlDeserializer();
            var output = xml.Deserialize<AnonymousApplicationRequest>(new RestResponse() { Content = doc.ToString() });

            Assert.IsNotNull(output, "no serialization happened");
            Assert.AreEqual(false, output.Test);
            Assert.AreEqual("DevKey", output.DeveloperKey);
            Assert.AreEqual("US", output.SiteID);
            Assert.AreEqual("JobDID", output.JobDID);
            Assert.AreEqual("TNDID", output.TNDID);
            Assert.IsNotNull(output.Responses, "responses did not deserialize right");
            Assert.IsTrue(output.Responses.Count > 0, "no single response in responses");
            Assert.AreEqual("ApplicantName", output.Responses[0].QuestionID);
            Assert.AreEqual("Freedom", output.Responses[0].ResponseText);
            Assert.AreEqual("ApplicantEmail", output.Responses[1].QuestionID);
            Assert.AreEqual("FreedomTacosCB@Live.com", output.Responses[1].ResponseText);
            Assert.AreEqual("Resume", output.Responses[2].QuestionID);
            Assert.AreEqual("no", output.Responses[2].ResponseText);
            Assert.AreEqual("1189225", output.Responses[3].QuestionID);
            Assert.AreEqual("2486710", output.Responses[3].ResponseText);
            Assert.AreEqual("1193248", output.Responses[4].QuestionID);
            Assert.AreEqual("2493730", output.Responses[4].ResponseText);
            Assert.AreEqual("1475425", output.Responses[5].QuestionID);
            Assert.AreEqual("350000", output.Responses[5].ResponseText);
            Assert.AreEqual("1475428", output.Responses[6].QuestionID);
            Assert.AreEqual("999999", output.Responses[6].ResponseText);
            Assert.AreEqual("1616871", output.Responses[7].QuestionID);
            Assert.AreEqual("3261961", output.Responses[7].ResponseText);
            Assert.AreEqual("1616873", output.Responses[8].QuestionID);
            Assert.AreEqual("explosions", output.Responses[8].ResponseText);
        }
    }
}
