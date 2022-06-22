using RestSharp;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookAPI.Helpers
{
    class TokenGeneration
    {
        private static string userName = Utils.RandomString(10);
        private static string email = Utils.RandomString(10) + "@mailinator.com";
        private static string password = Utils.RandomString(8);

        private static void CreateUser(string userName, string email, string password)
        {
            var client = new RestClient("http://localhost:5000/Authentication/create-user");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var body = $@"{{
    
            ""userName"": ""{userName}"",
          
            ""emailAddress"": ""{email}"",
           
            ""password"": ""{password}""
         
            }}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            client.Execute(request);
        }

        public static string GetBearerToken()
        {
            CreateUser(userName, email, password);

            var client = new RestClient("http://localhost:5000/Authentication/login");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var body = $@"{{

            ""emailAddress"": ""{email}"",

            ""password"": ""{password}""

            }}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            JObject jObject = JObject.Parse(response.Content);
            string bearerToken = jObject.GetValue("token").ToString();
            return bearerToken;
        }
    }
}
