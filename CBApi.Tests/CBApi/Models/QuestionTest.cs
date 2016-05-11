using CBApi.Models;
using NUnit.Framework;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.IO;
using System.Xml.Linq;

namespace Tests.CBApi.models {
    [TestFixture]
    public class QuestionTest {
        [Test]
        public void DeserializationWorks_WhenPassedRightXML() {
            var xmlpath = Path.Combine(Environment.CurrentDirectory, "ResponseBlankApplication.xml");
            var doc = XDocument.Load(xmlpath);

            var xml = new XmlDeserializer();
            var output = xml.Deserialize<BlankApplication>(new RestResponse() { Content = doc.ToString() });
            Assert.IsNotNull(output.Questions);
            Assert.AreEqual(4,output.Questions.Count);

            Assert.AreEqual("ApplicantName", output.Questions[0].QuestionID);
            Assert.AreEqual("Basic", output.Questions[0].QuestionType);
            Assert.AreEqual(true, output.Questions[0].IsRequired);
            Assert.AreEqual("Text 50", output.Questions[0].ExpectedResponseFormat);
            Assert.AreEqual("Your name", output.Questions[0].QuestionText);

            Assert.AreEqual("ApplicantEmail", output.Questions[1].QuestionID);
            Assert.AreEqual("Basic", output.Questions[1].QuestionType);
            Assert.AreEqual(true, output.Questions[1].IsRequired);
            Assert.AreEqual("Text 50", output.Questions[1].ExpectedResponseFormat);
            Assert.AreEqual("Your email", output.Questions[1].QuestionText);


            Assert.AreEqual("Resume", output.Questions[2].QuestionID);
            Assert.AreEqual("Basic", output.Questions[2].QuestionType);
            Assert.AreEqual(true, output.Questions[2].IsRequired);
            Assert.AreEqual("Text 5000", output.Questions[2].ExpectedResponseFormat);
            Assert.AreEqual("Your resume", output.Questions[2].QuestionText);

            Assert.AreEqual("CoverLetter", output.Questions[3].QuestionID);
            Assert.AreEqual("Basic", output.Questions[3].QuestionType);
            Assert.AreEqual(false, output.Questions[3].IsRequired);
            Assert.AreEqual("Text 5000", output.Questions[3].ExpectedResponseFormat);
            Assert.AreEqual("Your cover letter", output.Questions[3].QuestionText);
        }
    }
}
