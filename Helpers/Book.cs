using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookAPI.Helpers
{
    public class Book
    {
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
        public string Author { get; set; }
        public string AuthorId { get; set; }
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        [JsonProperty("dateOfBirth")]
        public string DateOfBirth { get; set; }
        [JsonProperty("publisher")]
        public string Publisher { get; set; }
        [JsonProperty("releaseDate")]
        public string ReleaseDate { get; set; }

        //const
        public Book()
        {

        }
    }
}
