using MongoDB.Bson.Serialization;
using PollProject.Models;

namespace PollProject.Database.Map
{
	public class PollStatsMap
	{
		public static void Configure()
		{
			if (!BsonClassMap.IsClassMapRegistered(typeof(PollStatsMap)))
			{
				BsonClassMap.RegisterClassMap<PollStats>(cm =>
				{
					cm.MapMember(c => c.Views).SetElementName("views");
					cm.MapMember(c => c.Votes).SetElementName("votes");
					cm.SetIgnoreExtraElements(true);
				});
			}

		}
	}
}