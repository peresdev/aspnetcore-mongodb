using System.Text.Json.Serialization;

namespace PollProject.Models
{
    public class PollVotes
    {
        [JsonPropertyName("option_id")]
        public long OptionId { get; set; }

        [JsonPropertyName("qty")]
        public long Qty { get; set; }
    }
}
