using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Net;
using TechTalk.SpecFlow;

namespace BookAPI.GetBook
{
    [Binding]
    public class GetBookSteps
    {
        private static string bearerToken = Helpers.TokenGeneration.GetBearerToken();
        private RestResponse response = new RestResponse();

        [When(@"A request is made to get Book by Id")]
        public void WhenARequestIsMadeToGetBookById()
        {
            var client = new RestClient("http://localhost:5000/Books/6");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer " + bearerToken);
            response = (RestResponse)client.Execute(request);
        }

        [When(@"A request is made to get Book by Id with Id that is not in the Database")]
        public void WhenARequestIsMadeToGetBookByIdWithIdThatIsNotInTheDatabase()
        {
            var client = new RestClient("http://localhost:5000/Books/9999");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer " + bearerToken);
            response = (RestResponse)client.Execute(request);
        }

        [Then(@"OK is returned from GET by Id")]
        public void ThenOKIsReturnedFromGETById()
        {
            if (response.StatusCode != HttpStatusCode.OK)
            {
                Assert.Fail("Status code didnt match");
            }
        }

        [Then(@"Lord of the Rings is returned")]
        public void ThenLordOfTheRingsIsReturned()
        {
            JObject jObject = JObject.Parse(response.Content);
            string title = jObject.GetValue("title").ToString();

            if (title != "Lord of the Rings")
            {
                Assert.Fail("Wrong book returned");
            }
        }

        [Then(@"Internal Server Error is returned from GET by Id")]
        public void ThenInternalServerErrorIsReturnedFromGETById()
        {
            if (response.StatusCode != HttpStatusCode.InternalServerError)
            {
                Assert.Fail("Status code didnt match");
            }
        }
    }
}
