using PollProject.Models;
using MongoDB.Driver;
using System.Linq;
using System.Threading.Tasks;

namespace PollProject.Repository
{
    public class PollRepository : IPollRepository
    {
        private readonly IMongoDatabase _database;
        
        public PollRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<Poll> GetAsync(long pollId)
        {
            var poll = await _database.GetCollection<Poll>("Poll").Find(it => it.PollId == pollId).FirstOrDefaultAsync();

            if(poll != null)
            {
                var views = poll.Views;
                await UpdateViewsAsync(views, poll.PollId);
            }

            return poll;
        }

        public async Task UpdateViewsAsync(long views, long pollId)
        {
            var viewsCheck = ((views > 0) ? (views + 1) : 1);

            var filter = Builders<Poll>.Filter.Eq("poll_id", pollId);
            var update = Builders<Poll>.Update.Set("views", viewsCheck);

            await _database.GetCollection<Poll>("Poll").UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true });
        }

        public async Task<Poll> CreateAsync(Poll poll)
        {
            IPollOptionsRepository pollOptionsRepository = new PollOptionsRepository(_database);
            var options = pollOptionsRepository.MountOptions(poll.Options, await pollOptionsRepository.SequenceOptionIdAsync());

            poll.PollId = await SequencePollIdAsync();
            poll.PollDescription = poll.PollDescription;
            poll.Options = options;

            await _database.GetCollection<Poll>("Poll").InsertOneAsync(poll);

            return poll;
        }

        public async Task<Poll> VoteAsync(long optionId)
        {
            IPollVotesRepository pollVotesRepository = new PollVotesRepository(_database);
            IPollOptionsRepository pollOptionsRepository = new PollOptionsRepository(_database);

            PollVotes pollVotesUpdate = new PollVotes();
            pollVotesUpdate.Qty = await pollVotesRepository.IsVotedAsync(optionId) ? await pollOptionsRepository.QuantityByOptionIdAsync(optionId) + 1 : 1;

            var filter = Builders<Poll>.Filter.Eq("options.option_id", optionId);
            var update = Builders<Poll>.Update.Set("options.$.votes", pollVotesUpdate);

            var documentBefore = await _database.GetCollection<Poll>("Poll").FindOneAndUpdateAsync(filter, update, new FindOneAndUpdateOptions<Poll> { ReturnDocument = ReturnDocument.Before });
 
            return documentBefore;
        }

        public async Task<long> SequencePollIdAsync()
        {
            var sequencePollId = await _database.GetCollection<Poll>("Poll").Find(it => it.PollId != 0)
                                                                      .Project(it => it.PollId)
                                                                      .SortByDescending(it => it.PollId)
                                                                      .Limit(1) 
                                                                      .FirstOrDefaultAsync();

            return sequencePollId + 1;
        }
    }
}