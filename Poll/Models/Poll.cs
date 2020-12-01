using System.Collections.Generic;
using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace PollProject.Models
{
    public class Poll
    {
        [JsonPropertyName("poll_id")]
        public long PollId { get; set; }

        [JsonPropertyName("poll_description")]
        public string PollDescription { get; set; }

        [JsonPropertyName("views")]
        public long Views { get; set; }

        [JsonPropertyName("options")]
        public List<PollOptions> Options { get; set; }

        [JsonPropertyName("votes")]
        public PollVotes Votes { get; set; }
    }
}
