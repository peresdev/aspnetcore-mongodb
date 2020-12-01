using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using PollProject.Models;

namespace PollProject.Repository
{
    public class PollStatsRepository : IPollStatsRepository
    {
        private readonly ILogger<PollStatsRepository> _logger;
        private readonly IMongoDatabase _database;

        public PollStatsRepository(ILogger<PollStatsRepository> logger, IMongoDatabase database)
        {
            _logger = logger;
            _database = database;
        }

        public async Task<PollStats> GetAsync(long pollId)
        {
            var projection = BsonDocument.Parse(@"{'_id' : 0, 'views' : 1, 'options' : 1}");
            var projection2 = BsonDocument.Parse(@"{'options.option_description' : 0}");

            var pollStatsBson = await _database.GetCollection<Poll>("Poll").Aggregate()
                                                                 .Match(it => it.PollId == pollId)
                                                                 .Project(projection)
                                                                 .Project(projection2)
                                                                 .ToListAsync();

            _logger.LogInformation(pollStatsBson.ToString());

            return MountStats(pollStatsBson);
        }

        public PollStats MountStats(List<BsonDocument> pollStatsBson)
        {
            var pollStats = new PollStats();

            foreach (var pollStat in pollStatsBson)
            {
                var views = pollStat.AsBsonDocument.Contains("views") ? (long)pollStat.GetValue("views") : 0;
                pollStats.Views = views;

                var options = pollStat.GetValue("options").AsBsonArray.ToList();

                foreach (var option in options)
                {
                    var pollVotesStructure = new PollVotes()
                    {
                        OptionId = option["option_id"].AsInt64,
                        Qty = option.AsBsonDocument.Contains("votes") ? option["votes"]["qty"].AsInt64 : 0
                    };

                    pollStats.Votes.Add(pollVotesStructure);
                }
            }

            return pollStats;
        }
    }
}