using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using PollProject.Models;

namespace PollProject.Repository
{
    public class PollOptionsRepository : IPollOptionsRepository
    {
        private readonly IMongoDatabase _database;

        public PollOptionsRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<long> QuantityByOptionIdAsync(long optionId)
        {
            var projection = Builders<Poll>.Projection.Include("options.votes.qty");

            var match = new BsonDocument { { "$match", new BsonDocument { { "options.option_id", optionId } } } };

            var quantity = await _database.GetCollection<Poll>("Poll").Aggregate()
                                                                .Unwind("options")
                                                                .AppendStage<Poll>(match)
                                                                .Project(projection)
                                                                .ReplaceRoot<Poll>("$options.votes")
                                                                .As<PollVotes>()
                                                                .FirstOrDefaultAsync();

            return quantity.Qty;
        }

        public List<PollOptions> MountOptions(List<PollOptions> pollOptions, long maxOptionId)
        {
            var options = new List<PollOptions>();
            var i = 0;

            foreach (var option in pollOptions)
            {
                options.Add(
                    new PollOptions
                    {
                        OptionId = maxOptionId + i,
                        OptionDescription = option.OptionDescription
                    }
                );
                i++;
            }

            return options;
        }

        public async Task<List<PollOptions>> CreateOptionsWithIdAsync(string[] options)
        {
            if(options[0] != null)
            {
                if (options[0].Contains(","))
                {
                    var options2 = options[0].Split(',').Reverse().ToArray();
                    options = options2;
                }
            }

            var pollOptions = new List<PollOptions>();
            var sequenceOptionId = await SequenceOptionIdAsync();

            for (int i = 0; i < options.Count(); i++)
            {
                var pollOption = new PollOptions()
                {
                    OptionId = sequenceOptionId + i,
                    OptionDescription = options[i]
                };

                pollOptions.Add(pollOption);
            }

            return pollOptions;
        }

        public async Task<long> SequenceOptionIdAsync()
        {
            var sequenceOptionId = await _database.GetCollection<Poll>("Poll").Find(it => it.Options.Any(it => it.OptionId != 0))
                                                                        .SortByDescending(it => it.Options)
                                                                        .Limit(1).ToListAsync();

            return sequenceOptionId.Max(it => it.Options.Max(it => it.OptionId)) + 1;
        }
    }
}