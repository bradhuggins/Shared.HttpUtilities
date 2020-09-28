#region Using Statements
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
#endregion

namespace Shared.HttpUtilities.Tests
{
    [TestClass]
    public class RestClientTests
    {
        private readonly string _baseUrl = "http://echoapi.cloudapp.net/api";
        private readonly Models.SampleObject _mockdata = new Models.SampleObject()
        {
            Id = 1,
            Name = "Test"
        };

        [TestMethod]
        public void HasError_True_Test()
        {
            // Arrange
            RestClient target = new RestClient();
            target.ErrorMessage = "error";

            // Act
            var actual = target.HasError;

            // Assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void HasError_False_Test()
        {
            // Arrange
            RestClient target = new RestClient();

            // Act
            var actual = target.HasError;

            // Assert
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public async Task SendAsyncTest_Get()
        {
            //Arrange
            RestClient target = new RestClient();

            //Act
            var actual = await target.SendAsync(HttpMethod.Get, _baseUrl + "?param1=sample&param2=1");

            //Assert
            Assert.IsNotNull(actual);
            Assert.IsFalse(target.HasError);

        }

        [TestMethod]
        public async Task SendAsyncTest_Post()
        {
            //Arrange
            RestClient target = new RestClient();
            HttpContent content = JsonHelper.SerializeContent(_mockdata);

            //Act
            var actual = await target.SendAsync(HttpMethod.Post, _baseUrl, content);

            //Assert
            Assert.IsNotNull(actual);
            Assert.IsFalse(target.HasError);
        }

        [TestMethod]
        public async Task SendAsyncTest_Put()
        {
            //Arrange
            RestClient target = new RestClient();
            HttpContent content = JsonHelper.SerializeContent(_mockdata);

            //Act
            var actual = await target.SendAsync(HttpMethod.Put, _baseUrl+ "/1", content);

            //Assert
            Assert.IsNotNull(actual);
            Assert.IsFalse(target.HasError);
        }

        [TestMethod]
        public async Task SendAsyncTest_Delete()
        {
            //Arrange
            RestClient target = new RestClient();

            //Act
            var actual = await target.SendAsync(HttpMethod.Delete, _baseUrl + "/1");

            //Assert
            Assert.IsNotNull(actual);
            Assert.IsFalse(target.HasError);
        }

        [TestMethod]
        public void AddHeadersTest()
        {
            //Arrange
            RestClient target = new RestClient();
            target.Headers = new Dictionary<string, string>();
            target.Headers.Add("Accept", "*/*");
            target.Headers.Add("User-Agent", "SharedHttpClientProxy/1.0");

            HttpRequestMessage request = new HttpRequestMessage();

            Type t = typeof(RestClient);
            MethodInfo methodInfo = t.GetMethod("AddHeaders", BindingFlags.NonPublic | BindingFlags.Instance);
            object[] parameters = { request };

            //Act
            var actual = (HttpRequestMessage)methodInfo.Invoke(target, parameters);

            //Assert
            Assert.IsNotNull(actual);
            Assert.IsFalse(target.HasError);
        }

    }
}
