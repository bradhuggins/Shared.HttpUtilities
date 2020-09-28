#region Using Statements
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace Shared.HttpUtilities.Tests
{
    [TestClass]
    public class JsonHelperTests
    {
        private readonly Models.SampleObject _mockdata = new Models.SampleObject()
        {
            Id = 1,
            Name = "Test"
        };

        [TestMethod]
        public void JsonSerializerSettingsTest_Get()
        {
            // Arrange

            // Act
            var actual = JsonHelper.JsonSerializerSettings;

            // Assert
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void JsonSerializerSettingsTest_GetSet()
        {
            // Arrange
            JsonHelper.JsonSerializerSettings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };

            // Act
            var actual = JsonHelper.JsonSerializerSettings;

            // Assert
            Assert.IsNotNull(actual);
        }


        [TestMethod]
        public void SerializeContentTest()
        {
            // Arrange

            // Act
            var actual = JsonHelper.SerializeContent(_mockdata);

            // Assert
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void SerializeContent2Test()
        {
            // Arrange

            // Act
            var actual = JsonHelper.SerializeContent(_mockdata,
                new JsonSerializerSettings()
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore
                });

            // Assert
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void SerializeTest()
        {
            // Arrange

            // Act
            var actual = JsonHelper.Serialize(_mockdata);

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Length > 0);
        }

        [TestMethod]
        public void Serialize2Test()
        {
            // Arrange

            // Act
            var actual = JsonHelper.Serialize(_mockdata,
                new JsonSerializerSettings()
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore
                });

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Length > 0);
        }


        [TestMethod]
        public async Task DeserializeTest()
        {
            // Arrange
            string json = "{\"id\":1,\"name\":\"Test\"}";

            // Act
            Models.SampleObject actual = await JsonHelper.Deserialize<Models.SampleObject>(json);

            // Assert
            Assert.IsNotNull(actual);

        }

        [TestMethod]
        public async Task DeserializeHttpResponseMessageTest()
        {
            // Arrange
            HttpResponseMessage response = new HttpResponseMessage();            
            response.Content = new StringContent("{\"id\":1,\"name\":\"Test\"}", Encoding.UTF8, "application/json");

            // Act
            string actual = await JsonHelper.Deserialize(response);

            // Assert
            Assert.IsNotNull(actual);

        }
        
         [TestMethod]
        public async Task DeserializeResponseTest()
        {
            // Arrange
            HttpResponseMessage response = new HttpResponseMessage();
            response.Content = new StringContent("{\"id\":1,\"name\":\"Test\"}", Encoding.UTF8, "application/json");

            // Act
            Models.SampleObject actual = await JsonHelper.DeserializeResponse<Models.SampleObject>(response);

            // Assert
            Assert.IsNotNull(actual);

        }


    }
}
