using System.Text.Json.Serialization;

namespace PollProject.Models
{
    public class PollOptions
    {
        [JsonPropertyName("option_id")]
        public long OptionId { get; set; }

        [JsonPropertyName("option_description")]
        public string OptionDescription { get; set; }
    }
}
