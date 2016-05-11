using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CBApi.Framework;
using Moq;
using RestSharp;
using CBApi;

namespace Tests.CBApi.framework {
    [TestFixture]
    public class ErrorParserTests {
        [Test]
        public void CheckForErrors_DoesNothing_WhenPassedNullResponse() {
            try {
                ErrorParser.CheckForErrors(null);
                Assert.IsTrue(true);
            } catch (Exception) {
                Assert.Fail();
            }
        }

        [Test]
        public void CheckForErrors_DoesNothing_WhenNoErrorsArePresent() {
            //Mock crap
            var response = new Mock<IRestResponse>();
            response.Setup(x => x.Content).Returns("");
            response.Setup(x => x.ResponseStatus).Returns(ResponseStatus.Completed);

            try {
                ErrorParser.CheckForErrors(response.Object);
                Assert.IsTrue(true);
            } catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void CheckForErrors_ThrowsGenericException_WhenThereWasAConnectionError_WithNoErrorMessage() {
            //Mock crap
            var response = new Mock<IRestResponse>();
            response.Setup(x => x.Content).Returns("");
            response.Setup(x => x.ResponseStatus).Returns(ResponseStatus.Aborted);

            try {
                ErrorParser.CheckForErrors(response.Object);
                Assert.Fail();
            } catch (APIException ex) {
                Assert.AreEqual("An unknown error occured while making the API call", ex.Message);
                Assert.AreEqual(1, ex.APIErrors.Count);
            } 
        }

        [Test]
        public void CheckForErrors_ThrowsAPIException_WhenThereWasAConnectionError() {
            //Mock crap
            var response = new Mock<IRestResponse>();
            response.Setup(x => x.Content).Returns("");
            response.Setup(x => x.ResponseStatus).Returns(ResponseStatus.None);
            response.Setup(x => x.ErrorMessage).Returns("I AM A BANANA");

            try {
                ErrorParser.CheckForErrors(response.Object);
                Assert.Fail();
            } catch (APIException ex) {
                Assert.AreEqual("I AM A BANANA", ex.Message);
                Assert.AreEqual(1, ex.APIErrors.Count);
            } 
        }

        [Test]
        public void CheckForErrors_ThrowsAPITimeoutException_WhenThereWasATimeout() {
            //Mock crap
            var response = new Mock<IRestResponse>();
            response.Setup(x => x.Content).Returns("");
            response.Setup(x => x.ResponseStatus).Returns(ResponseStatus.TimedOut);
            response.Setup(x => x.ErrorMessage).Returns("I AM A BANANA");

            try {
                ErrorParser.CheckForErrors(response.Object);
                Assert.Fail();
            } catch (APITimeoutException ex) {
                Assert.AreEqual("I AM A BANANA", ex.Message);
                Assert.AreEqual(1, ex.APIErrors.Count);
            } 
        }

        [Test]
        public void CheckForErrors_ThrowsAPIException_WhenThereWasAnErrorNode() {
            //Mock crap
            var response = new Mock<IRestResponse>();
            response.Setup(x => x.Content).Returns("<ResponseCategories><Errors><Error>DeveloperKey is invalid or does not have permission to perform Categories requests.</Error></Errors><CountryCode>US</CountryCode><TimeResponseSent>10/26/2012 10:29:08 PM</TimeResponseSent><Categories/></ResponseCategories>");
            response.Setup(x => x.ResponseStatus).Returns(ResponseStatus.Completed);
            response.Setup(x => x.ErrorMessage).Returns("");

            try {
                ErrorParser.CheckForErrors(response.Object);
                Assert.Fail();
            } catch (APIException ex) {
                Assert.AreEqual("DeveloperKey is invalid or does not have permission to perform Categories requests.", ex.Message);
                Assert.AreEqual(1, ex.APIErrors.Count);
            }
        }

        [Test]
        public void CheckForErrors_ThrowsAPIExceptionWithMultipleMessages_WhenThereAreErrorNodes() {
            //Mock crap
            var response = new Mock<IRestResponse>();
            response.Setup(x => x.Content).Returns("<ResponseCategories><Errors><Error>DeveloperKey is invalid or does not have permission to perform Categories requests.</Error><Error>You smell funny</Error></Errors><CountryCode>US</CountryCode><TimeResponseSent>10/26/2012 10:29:08 PM</TimeResponseSent><Categories/></ResponseCategories>");
            response.Setup(x => x.ResponseStatus).Returns(ResponseStatus.Completed);
            response.Setup(x => x.ErrorMessage).Returns("");

            try {
                ErrorParser.CheckForErrors(response.Object);
                Assert.Fail();
            } catch (APIException ex) {
                Assert.AreEqual("DeveloperKey is invalid or does not have permission to perform Categories requests.", ex.Message);
                Assert.AreEqual(2, ex.APIErrors.Count);
            } 
        }

        [Test]
        public void CheckForErrors_ThrowsAPIExceptionWithMultipleMessages_WhenThereAreAllKindsOfErrors() {
            //Mock crap
            var response = new Mock<IRestResponse>();
            response.Setup(x => x.Content).Returns("<ResponseCategories><Errors><Error>DeveloperKey is invalid or does not have permission to perform Categories requests.</Error><Error>You smell funny</Error></Errors><CountryCode>US</CountryCode><TimeResponseSent>10/26/2012 10:29:08 PM</TimeResponseSent><Categories/></ResponseCategories>");
            response.Setup(x => x.ResponseStatus).Returns(ResponseStatus.TimedOut);
            response.Setup(x => x.ErrorMessage).Returns("I am on Fire, thanks now I am dead");

            try {
                ErrorParser.CheckForErrors(response.Object);
                Assert.Fail();
            } catch (APIException ex) {
                Assert.AreEqual("DeveloperKey is invalid or does not have permission to perform Categories requests.", ex.Message);
                Assert.AreEqual(2, ex.APIErrors.Count);
            }
        }

        [Test]
        public void CheckForErrors_ThrowsAPIException_WhenThereWasAnErrorNode_AndNamespaces() {
            //Mock crap
            var response = new Mock<IRestResponse>();
            response.Setup(x => x.Content).Returns("<ResponseWrapperOfanyType xmlns:i=" + '"' + "http://www.w3.org/2001/XMLSchema-instance" + '"' + " xmlns=" + '"' + "http://schemas.datacontract.org/2004/07/ConsumerWebApi.Models.Responses" + '"' +
                "><Errors xmlns:d2p1=" + '"' + "http://schemas.microsoft.com/2003/10/Serialization/Arrays" + '"' + "><d2p1:string>The Developer key you have provided does not have permission to preform this request.</d2p1:string></Errors>" +
                "<Results xmlns:d2p1=" + '"' + "http://schemas.microsoft.com/2003/10/Serialization/Arrays" + '"' + " /><ReturnedResults>0</ReturnedResults><Status>Error</Status><Timestamp>2013-11-25T10:00:10.1547889-05:00</Timestamp>" +
                "<TotalResults>0</TotalResults></ResponseWrapperOfanyType>");
            response.Setup(x => x.ResponseStatus).Returns(ResponseStatus.Error);
            response.Setup(x => x.ErrorMessage).Returns("I am on Fire, thanks now I am dead");

            try {
                ErrorParser.CheckForErrors(response.Object);
                Assert.Fail();
            } catch (APIException ex) {
                Assert.AreEqual("The Developer key you have provided does not have permission to preform this request.", ex.Message);
                Assert.AreEqual(1, ex.APIErrors.Count);
            }
        }

        [Test]
        public void CheckForErrors_ThrowsAPIExceptionWithMultipleMessages_WhenThereAreAllKindsOfErrors_AndNamespaces() {
            //Mock crap
            var response = new Mock<IRestResponse>();
            response.Setup(x => x.Content).Returns("<ResponseWrapperOfanyType xmlns:i=" + '"' + "http://www.w3.org/2001/XMLSchema-instance" + '"' + " xmlns=" + '"' + "http://schemas.datacontract.org/2004/07/ConsumerWebApi.Models.Responses" + '"' +
                "><Errors xmlns:d2p1=" + '"' + "http://schemas.microsoft.com/2003/10/Serialization/Arrays" + '"' + "><d2p1:string>The Developer key you have provided does not have permission to preform this request.</d2p1:string>" +
                "<d2p1:string>You smell funny.</d2p1:string></Errors>" + "<Results xmlns:d2p1=" + '"' + "http://schemas.microsoft.com/2003/10/Serialization/Arrays" + '"' + " /><ReturnedResults>0</ReturnedResults><Status>Error</Status>" +
                "<Timestamp>2013-11-25T10:00:10.1547889-05:00</Timestamp><TotalResults>0</TotalResults></ResponseWrapperOfanyType>");
            response.Setup(x => x.ResponseStatus).Returns(ResponseStatus.Error);
            response.Setup(x => x.ErrorMessage).Returns("I am on Fire, thanks now I am dead");

            try {
                ErrorParser.CheckForErrors(response.Object);
                Assert.Fail();
            } catch (APIException ex) {
                Assert.AreEqual("The Developer key you have provided does not have permission to preform this request.", ex.Message);
                Assert.AreEqual(2, ex.APIErrors.Count);
            }
        }
    }
}
