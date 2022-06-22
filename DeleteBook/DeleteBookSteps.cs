using BookAPI.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Net;
using TechTalk.SpecFlow;

namespace BookAPI.DeleteBook
{
    [Binding]
    public class DeleteBookSteps
    {

        private static string bearerToken = Helpers.TokenGeneration.GetBearerToken();
        private RestResponse response = new RestResponse();
        public int statusCode;
        private int bookId;
        private static string title = Utils.RandomString(12);
        private static string firstName = Utils.RandomString(10);
        private static string lastName = Utils.RandomString(10);
        private static string publisher = Utils.RandomString(12);

        [Given(@"I have created a book")]
        public void GivenIHaveCreatedABook()
        {
            var client = new RestClient("http://localhost:5000/Books");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Bearer " + bearerToken);
            request.AddHeader("Content-Type", "application/json");
            var body = $@"{{

            ""title"": ""{title}"",

            ""author"": {{

                ""firstName"": ""{firstName}"",

                ""lastName"": ""{lastName}"",

                ""dateOfBirth"": ""2002-06-21T21:12:19.686Z""

            }},

            ""publisher"": ""{publisher}"",

            ""releaseDate"": ""2022-06-21T21:12:19.686Z""

            }}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            RestResponse response = (RestResponse)client.Execute(request);
            JObject jObject = JObject.Parse(response.Content);
            string id = jObject.GetValue("bookId").ToString();
            bookId = Int32.Parse(id);
        }
        
        [When(@"A request is made to delete a book")]
        public void WhenARequestIsMadeToDeleteABook()
        {
            var client = new RestClient("http://localhost:5000/Books?BookId=" + bookId);
            client.Timeout = -1;
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("Authorization", "Bearer " + bearerToken);
            response = (RestResponse)client.Execute(request);
        }
        
        [When(@"A request is made to delete Book by Id with no Id in url")]
        public void WhenARequestIsMadeToDeleteBookByIdWithNoIdInUrl()
        {
            var client = new RestClient("http://localhost:5000/Books?BookId=");
            client.Timeout = -1;
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("Authorization", "Bearer " + bearerToken);
            response = (RestResponse)client.Execute(request);
            Console.WriteLine(response.Content);
        }
        
        [Then(@"OK is returned from DELETE")]
        public void ThenOKIsReturnedFromDELETE()
        {
            if (response.StatusCode != HttpStatusCode.OK)
            {
                Assert.Fail("Status code didnt match");
            }
        }
        
        [Then(@"Bad Request is returned from DELETE")]
        public void ThenBadRequestIsReturnedFromDELETE()
        {
            if (response.StatusCode != HttpStatusCode.BadRequest)
            {
                Assert.Fail("Status code didnt match");
            }
        }
    }
}
