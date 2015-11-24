using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Hydra.DataSources {
    public abstract class RestApi : IDataSource {

        public string BaseUrl { get; protected set; }
        public string ApiPath { get; protected set; }

        private HttpClient httpClient;

        public RestApi(string baseUrl, string apiPath = "/") {
            BaseUrl = baseUrl;
            ApiPath = apiPath;

            httpClient = new HttpClient() {
                BaseAddress = new Uri(BaseUrl)
            };
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Fills in the parameters of a URL and makes sure it is properly encoded. 
        /// </summary>
        /// <param name="urlFormat"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        protected string EncodeUrl(string methodUrlFormat, params object[] parameters) {
            string formattedUrl = string.Format(methodUrlFormat, parameters);
            return WebUtility.UrlEncode(formattedUrl);
        }
        
        /// <summary>
        /// Performs a GET-request to the specified resource.
        /// </summary>
        /// <typeparam name="T">The type of the deserialized response.</typeparam>
        /// <param name="relativeUrl"></param>
        /// <param name="parameters"></param>
        /// <returns>The deserialized response</returns>
        protected async Task<T> Get<T>(string encodedUrl) {
            HttpResponseMessage response = await httpClient.GetAsync(ApiPath + encodedUrl);
            response.EnsureSuccessStatusCode(); // Throw exceptions if something went wrong

            Stream stream = await response.Content.ReadAsStreamAsync();
            return Deserialize<T>(stream);
        }

        /// <summary>
        /// Deserializes a stream. By default assumes a JSON-stream and type T (implicitly) having a DataContract.
        /// Override this if you want to use a manual JSON parser, or one parsing something else than JSON.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="serializerSettings"></param>
        /// <returns></returns>
        protected T Deserialize<T>(Stream input, DataContractJsonSerializerSettings serializerSettings = null) {
            DataContractJsonSerializer serializer;
            if (serializerSettings != null) {
                serializer = new DataContractJsonSerializer(typeof(T), serializerSettings);
            } else {
                serializer = new DataContractJsonSerializer(typeof(T));
            }

            return (T) serializer.ReadObject(input);
        }

    }
}
