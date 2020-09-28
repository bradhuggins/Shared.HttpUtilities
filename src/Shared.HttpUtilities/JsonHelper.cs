#region Using Statements
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace Shared.HttpUtilities
{
    public class JsonHelper
    {
        private static JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.None,
            //DateFormatstring = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'ffffffzzz"
        };

        public static JsonSerializerSettings JsonSerializerSettings
        {
            get { return _jsonSerializerSettings; }
            set { _jsonSerializerSettings = value; }
        }


        public static HttpContent SerializeContent<T>(T item)
        {
            return SerializeContent(item, JsonSerializerSettings);
        }

        public static HttpContent SerializeContent<T>(T item, JsonSerializerSettings settings)
        {
            HttpContent toReturn;
            string stringJson = JsonConvert.SerializeObject(item, settings);
            toReturn = new StringContent(stringJson, Encoding.UTF8, "application/json");
            return toReturn;
        }

        public static string Serialize<T>(T item)
        {
            return Serialize(item, JsonSerializerSettings);
        }

        public static string Serialize<T>(T item, JsonSerializerSettings settings)
        {
            return JsonConvert.SerializeObject(item, settings);
        }

        public static async Task<string> Deserialize(HttpResponseMessage response)
        {
            string toReturn = await response.Content.ReadAsStringAsync();
            return toReturn;
        }
        public static async Task<T> DeserializeResponse<T>(HttpResponseMessage response)
        {
            return await DeserializeResponse<T>(response, new JsonSerializerSettings());
        }
        public static async Task<T> DeserializeResponse<T>(HttpResponseMessage response, JsonSerializerSettings settings)
        {
            string responsestring = await Deserialize(response);
            return JsonConvert.DeserializeObject<T>(responsestring, settings);
        }

    }

}
