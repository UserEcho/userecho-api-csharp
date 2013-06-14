using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace UserEcho
{
    public class Client
    {
        public const string CLIENT_VERSION = "1.0";
        private string apiUrl = "https://userecho.com/api/";
        private string apiToken;
        private RestClient client;

        public Client(string apiToken)
        {
            this.apiToken = apiToken;
            this.client = new RestClient(apiUrl);
        }
        
        public JToken Request(Method method, string path, Object parameters = null)
        {
            var request = new RestRequest(path+".json", method);
      
            request.AddHeader("Authorization", "Bearer " + this.apiToken);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("API-Client", string.Format("userecho-csharp-{0}", CLIENT_VERSION));

            if (parameters != null)
            {
                request.AddParameter("application/json", JsonConvert.SerializeObject(parameters), ParameterType.RequestBody);
            }

            var response = this.client.Execute(request);
            var content = response.Content; // raw content as string

            JToken result = null;
            
            try
            {             
                result = JArray.Parse(content);
            }

            catch {
                throw new UEAPIError(content);
            }
                      
            return result;
        }

        public class UEAPIError : Exception { public UEAPIError(string msg) : base(msg) { } }

        public JToken Get(string path) { return Request(Method.GET, path); }
        public JToken Post(string path, Object parameters) { return Request(Method.POST, path, parameters); }
        }
}