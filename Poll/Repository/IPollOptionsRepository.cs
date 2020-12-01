using System.Collections.Generic;
using System.Threading.Tasks;
using PollProject.Models;

namespace PollProject.Repository
{
    public interface IPollOptionsRepository
    {
        List<PollOptions> MountOptions(List<PollOptions> pollOptions, long maxOptionId);
        Task<List<PollOptions>> CreateOptionsWithIdAsync(string[] options);
        Task<long> SequenceOptionIdAsync();
        Task<long> QuantityByOptionIdAsync(long optionId);
    }
}