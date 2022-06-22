using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookAPI.Helpers
{
    class ApiClient
    {
        public static RestClient client;

        public static RestResponse ApiResponse(Method verb, string url, Object body, string bearerToken)
        {
            client = new RestClient(url);

            var request = new RestRequest(verb);
            request.AddHeader("Authorization", "Bearer " + bearerToken);
            request.AddHeader("Content-Type", "multipart/form-data");
            request.AddHeader("Accept", "application/json, text/javascript");
            request.AddParameter("utf8", "✓");

            if (body != null)
            {
                var dictionary = DeserializeJson(body);
                foreach (KeyValuePair<string, string> entry in dictionary)
                {
                    request.AddParameter(entry.Key, entry.Value);
                }
            }
            RestResponse response = (RestResponse)client.Execute(request);
            return response;
        }

        private static Dictionary<string, string> DeserializeJson(Object body)
        {
            var jsonSettings = new JsonSerializerSettings();

            var json = JsonConvert.SerializeObject(body, jsonSettings);
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }
    }
}
