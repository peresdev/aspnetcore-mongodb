using System.Threading.Tasks;

namespace PollProject.Repository
{
    public interface IPollVotesRepository
    {
        Task<bool> IsVotedAsync(long optionId);
    }
}