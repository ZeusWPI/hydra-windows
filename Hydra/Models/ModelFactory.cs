using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Hydra.Models {
    /// <summary>
    /// A ModelFactory hides any backend specifics (e.g. downloading, caching, ...).
    /// </summary>
    public abstract class ModelFactory {
        
        protected const string zeusApiUrl = "https://zeus.ugent.be/hydra/api/";

        public async Task<object> FromRestApi(Type modelType, string baseUri, string relativeUri, DataContractJsonSerializerSettings serializerSettings = null) {
            HttpContent jsonFile = await downloadJsonFile(baseUri, relativeUri);
            Stream stream = await jsonFile.ReadAsStreamAsync();

            DataContractJsonSerializer serializer;
            if (serializerSettings != null) {
                serializer = new DataContractJsonSerializer(modelType, serializerSettings);
            } else {
                serializer = new DataContractJsonSerializer(modelType);
            }
            
            return serializer.ReadObject(stream);
        }

        protected async Task<HttpContent> downloadJsonFile(string baseUri, string relativeUri) {
            HttpClient client = new HttpClient() {
                BaseAddress = new Uri(baseUri)
            };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(relativeUri);
            response.EnsureSuccessStatusCode();

            return response.Content;
        }
    }
}
