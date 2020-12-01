using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PollProject.Models
{
	public class PollStats
	{
		[JsonPropertyName("views")]
		public long Views { get; set; }

		[JsonPropertyName("votes")]
		public List<PollVotes> Votes { get; set; } = new List<PollVotes>();
    }
}
