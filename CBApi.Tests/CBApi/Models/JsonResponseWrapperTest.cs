using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CBApi.Models;
using NUnit.Framework;
using RestSharp;
using RestSharp.Deserializers;

namespace Tests.com.careerbuilder.CBApi.models {

    [TestFixture]
    public class JsonResponseWrapperTest {

        [Test]
        public void DeserializationWorks_WhenPassedRightJSON_Response() {
            var jsonPath = Path.Combine(Environment.CurrentDirectory, "JsonResponseWrapper.json");
            var streamReader = new StreamReader(jsonPath);
            var json = new JsonDeserializer();

            var output = json.Deserialize<JsonWrapper<Name>>(new RestResponse() { Content = streamReader.ReadToEnd() });
            Assert.AreEqual(output.TotalResults, "1");
            Assert.AreEqual(output.ReturnedResults, "1");
            Assert.AreEqual(output.Results[0].Language, "Greek");
            Assert.AreEqual(output.Results[0].Value, "GR");
            CollectionAssert.AreEqual(output.Errors, new List<string>());
            Assert.AreEqual(output.Timestamp, "2015-06-10T14:56:37.3319476-04:00");
            Assert.AreEqual(output.Status, "Success");
        }
    }
}
