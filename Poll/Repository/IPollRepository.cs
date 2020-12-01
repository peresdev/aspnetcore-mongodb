using System.Threading.Tasks;
using PollProject.Models;

namespace PollProject.Repository
{
    public interface IPollRepository
    {
        Task<Poll> GetAsync(long pollId);
        Task<Poll> CreateAsync(Poll poll);
        Task UpdateViewsAsync(long views, long pollId);
        Task<Poll> VoteAsync(long optionId);
        Task<long> SequencePollIdAsync();
    }
}