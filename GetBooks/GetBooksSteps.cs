using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Net;
using TechTalk.SpecFlow;

namespace BookAPI.GetBooks
{
    [Binding]
    public class GetBooksSteps
    {
        private static string bearerToken = Helpers.TokenGeneration.GetBearerToken();
        private RestResponse response = new RestResponse();

        [When(@"A request is made to get Stephen King Book from API")]
        public void WhenARequestIsMadeToGetStephenKingBookFromAPI()
        {
            var client = new RestClient("http://localhost:5000/Books?AuthorFirstName=Stephen&AuthorLastName=King&PageNumber=1");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer " + bearerToken);
            response = (RestResponse)client.Execute(request);
        }

        [Then(@"OK is returned from GET")]
        public void ThenOKIsReturned()
        {
            if (response.StatusCode != HttpStatusCode.OK)
            {
                Assert.Fail("Status code didnt match");
            }
        }

        [Then(@"The Green Mile is returned")]
        public void ThenTheGreenMileIsReturned()
        {
            JObject jObject = JObject.Parse(response.Content);
            JArray books = (JArray)jObject["books"];
            string title = "";
            foreach (var book in books)
            {
                title = book["title"].ToString();
            }

            if (title != "The Green Mile")
            {
                Assert.Fail("No Book Id returned");
            }
        }

        [When(@"A request is made to get Stephen King Book from API with invalid page number")]
        public void WhenARequestIsMadeToGetStephenKingBookFromAPIInvalid()
        {
            var client = new RestClient("http://localhost:5000/Books?AuthorFirstName=Stephen&AuthorLastName=King&PageNumber=0");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer " + bearerToken);
            response = (RestResponse)client.Execute(request);
        }

        [Then(@"Bad Request is returned from GET")]
        public void ThenBadRequestIsReturned()
        {
            if (response.StatusCode != HttpStatusCode.BadRequest)
            {
                Assert.Fail("Status code didnt match");
            }
        }
    }
}
