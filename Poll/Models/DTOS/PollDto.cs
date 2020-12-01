using Microsoft.AspNetCore.Mvc;

namespace PollProject.Models.DTOS
{
    public class PollDto
    {
        [FromForm(Name = "poll_description")]
        public string PollDescription { get; set; }

        [FromForm(Name = "options")]
        public string[] Options { get; set; }
    }
}
