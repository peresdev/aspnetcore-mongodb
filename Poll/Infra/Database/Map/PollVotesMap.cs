using MongoDB.Bson.Serialization;
using PollProject.Models;

namespace PollProject.Database.Map
{
	public class PollVotesMap
	{
		public static void Configure()
		{
			if (!BsonClassMap.IsClassMapRegistered(typeof(PollVotesMap)))
			{
				BsonClassMap.RegisterClassMap<PollVotes>(cm =>
				{
					cm.MapMember(c => c.Qty).SetElementName("qty");
					cm.SetIgnoreExtraElements(true);
				});
			}

		}
	}
}