using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using PollProject.Models;

namespace PollProject.Repository
{
    public class PollVotesRepository : IPollVotesRepository
    {
        private readonly IMongoDatabase _database;

        public PollVotesRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<bool> IsVotedAsync(long optionId)
        {
            var matchOptions = BsonDocument.Parse(@"{ $match: { 'options.option_id': NumberLong(" + optionId + ") } }");
            var matchVotesExists = BsonDocument.Parse("{ $match: { 'options.votes': { $exists: true } } }");

            var projection = Builders<Poll>.Projection.Include("options.votes");

            var isVoted = await _database.GetCollection<Poll>("Poll").Aggregate()
                                                               .Unwind("options")
                                                               .AppendStage<Poll>(matchOptions)
                                                               .AppendStage<Poll>(matchVotesExists)
                                                               .Project(projection)
                                                               .ToListAsync();

            return isVoted.Count > 0;
        }
    }
}