using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using PollProject.Models;

namespace PollProject.Repository
{
    public interface IPollStatsRepository
    {
        Task<PollStats> GetAsync(long pollId);
        PollStats MountStats(List<BsonDocument> pollStatsBson);
    }
}