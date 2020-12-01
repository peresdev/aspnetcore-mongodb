using System.Dynamic;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PollProject.Models;
using PollProject.Models.DTOS;
using PollProject.Repository;

namespace PollProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PollController : ControllerBase
    {
        private readonly ILogger<PollController> _logger;
        private readonly IPollRepository _pollRepository;
        private readonly IPollStatsRepository _pollStatsRepository;
        private readonly IPollOptionsRepository _pollOptionsRepository;

        public PollController(ILogger<PollController> logger, IPollRepository pollRepository,
                              IPollStatsRepository pollStatsRepository, IPollOptionsRepository pollOptionsRepository)
        {
            _logger = logger;
            _pollRepository = pollRepository;
            _pollStatsRepository = pollStatsRepository;
            _pollOptionsRepository = pollOptionsRepository;
        }

        [HttpGet("{pollId}")]
        public async Task<IActionResult> GetAsync(long pollId)
        {
            var poll = await _pollRepository.GetAsync(pollId);

            if (poll == null)
                return NotFound();

            return Ok(poll.Adapt<PollContract>());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm]PollDto pollDto)
        {
            if (pollDto == null)
                return NotFound();

            Poll poll = new Poll();
            poll.PollDescription = pollDto.PollDescription;
            poll.Options = await _pollOptionsRepository.CreateOptionsWithIdAsync(pollDto.Options);

            var pollCreate = await _pollRepository.CreateAsync(poll);

            dynamic pollResponse = new ExpandoObject();
            pollResponse.poll_id = pollCreate.PollId;

            return Ok(JsonConvert.SerializeObject(pollResponse));
        }

        [HttpPost]
        [Route("{optionId}/vote")]
        public async Task<IActionResult> VoteAsync([FromRoute]long optionId)
        {
            var optionVote = await _pollRepository.VoteAsync(optionId);

            if (optionVote == null)
                return NotFound();

            dynamic optionResponse = new ExpandoObject();
            optionResponse.option_id = optionId;

            return Ok(JsonConvert.SerializeObject(optionResponse));
        }

        [HttpGet("{pollId}/stats")]
        public async Task<IActionResult> StatsAsync(long pollId)
        {
            var pollStats = await _pollStatsRepository.GetAsync(pollId);

            if (pollStats.Votes.Count == 0)
                return NotFound();

            return Ok(pollStats);
        }
    }
}
