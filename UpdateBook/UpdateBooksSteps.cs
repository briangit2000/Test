using BookAPI.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using System.Net;
using TechTalk.SpecFlow;

namespace BookAPI.UpdateBook
{
    [Binding]
    public class UpdateBooksSteps
    {
        private static string bearerToken = Helpers.TokenGeneration.GetBearerToken();
        private RestResponse response = new RestResponse();
        public int statusCode;
        private static readonly int id = 7;
        private static string title = Utils.RandomString(12);
        private static string publisher = Utils.RandomString(12);
        private static string releaseDate = Utils.RandomString(12);

        [When(@"A request is made to update book title and publisher")]
        public void WhenARequestIsMadeToUpdateBookTitleAndPublisher()
        {
            var client = new RestClient("http://localhost:5000/Books/");
            client.Timeout = -1;
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Authorization", "Bearer " + bearerToken);
            request.AddHeader("Content-Type", "application/json");
            var body = $@"{{

              ""bookToUpdate"": {{

                ""id"": {id},

                ""title"": ""{title}"",

                ""publisher"": ""{publisher}""

            }}

            }}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            response = (RestResponse)client.Execute(request);
        }

        [When(@"A request is made to update book title and publisher with invalid id")]
        public void WhenARequestIsMadeToUpdateBookTitleAndPublisherWithInvalidId()
        {
            var client = new RestClient("http://localhost:5000/Books/");
            client.Timeout = -1;
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Authorization", "Bearer " + bearerToken);
            request.AddHeader("Content-Type", "application/json");
            var body = $@"{{
            
              ""bookToUpdate"": {{
            
                ""id"": {id},
            
                ""title"": ""{title}"",
            
                ""publisher"": ""{publisher}"",
            
                ""releaseDate"": ""{releaseDate}""
            
              }}
            
            }}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            response = (RestResponse)client.Execute(request);
        }

        [Then(@"OK is returned from UPDATE")]
        public void ThenOKIsReturnedFromUPDATE()
        {
            if (response.StatusCode != HttpStatusCode.OK)
            {
                Assert.Fail("Status code didnt match");
            }
        }

        [Then(@"Bad Request is returned from UPDATE")]
        public void ThenBadRequestIsReturnedFromUPDATE()
        {
            if (response.StatusCode != HttpStatusCode.BadRequest)
            {
                Assert.Fail("Status code didnt match");
            }
        }
    }
}
