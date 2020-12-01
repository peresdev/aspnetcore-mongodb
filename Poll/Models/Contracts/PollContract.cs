using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PollProject.Models
{
    public class PollContract
    {
        [JsonPropertyName("poll_id")]
        public long PollId { get; set; }

        [JsonPropertyName("poll_description")]
        public string PollDescription { get; set; }

        [JsonPropertyName("options")]
        public List<PollOptions> Options { get; set; } = new List<PollOptions>();
    }
}
