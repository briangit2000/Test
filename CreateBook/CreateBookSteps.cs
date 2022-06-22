using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using BookAPI.Helpers;
using System;
using System.Net;
using TechTalk.SpecFlow;

namespace BookAPI.Steps
{
    [Binding]
    public class CreateBookSteps
    {
        private static string bearerToken = Helpers.TokenGeneration.GetBearerToken();
        private RestResponse response = new RestResponse();
        private static string title = Utils.RandomString(12);
        private static string firstName = Utils.RandomString(10);
        private static string lastName = Utils.RandomString(10);
        private static string publisher = Utils.RandomString(12);

        [Given(@"I have an authenticated user")]
        public void GivenIHaveAnAuthenticatedUser()
        {

            if (bearerToken == null)
            {
                Assert.Fail("No token created");
            }

        }

        [When(@"A request is made to create a book")]
        public void WhenARequestIsMadeToCreateABook()
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
            response = (RestResponse)client.Execute(request);
            Console.WriteLine(response.Content);
        }


        [Then(@"OK is returned from POST")]
        public void ThenOKIsReturned()
        {
            if (response.StatusCode != HttpStatusCode.OK)
            {
                Assert.Fail("Status code didnt match");
            }
        }


        [Then(@"The id is in the response")]
        public void ThenTheIdIsInTheResponse()
        {
            JObject jObject = JObject.Parse(response.Content);
            string bookId = jObject.GetValue("bookId").ToString();

            if (bookId == null)
            {
                Assert.Fail("No Book Id returned");
            }
        }

        [When(@"A request is made to create a book with no title")]
        public void WhenARequestIsMadeToCreateABookNoTitle()
        {
            var client = new RestClient("http://localhost:5000/Books");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Bearer " + bearerToken);
            request.AddHeader("Content-Type", "application/json");
            var body = $@"{{

            ""title"": """",

            ""author"": {{

                ""firstName"": ""{firstName}"",

                ""lastName"": ""{lastName}"",

                ""dateOfBirth"": ""2002-06-21T21:12:19.686Z""

            }},

            ""publisher"": ""{publisher}"",

            ""releaseDate"": ""2022-06-10T21:15:19.686Z""

            }}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            response = (RestResponse)client.Execute(request);
        }


        [Then(@"Bad Request is returned")]
        public void ThenBadRequestIsReturned()
        {
            if (response.StatusCode != HttpStatusCode.BadRequest)
            {
                Assert.Fail("Status code didnt match");
            }
        }
    }
}
