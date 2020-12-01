using PollProject.Database.Map;

namespace PollProject.Infra.Database
{
    public static class MongoDbPersistence
    {
        public static void Configure()
        {
            PollMap.Configure();
            PollOptionsMap.Configure();
            PollStatsMap.Configure();
            PollVotesMap.Configure();
        }
    }
}
