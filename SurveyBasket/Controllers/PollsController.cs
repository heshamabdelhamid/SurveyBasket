using Mapster;
using Microsoft.AspNetCore.Mvc;
using SurveyBasket.Contracts.Requests;
using SurveyBasket.Contracts.Responses;
using SurveyBasket.Entities;
using SurveyBasket.Mapping;
using SurveyBasket.Services;

namespace SurveyBasket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PollsController(IPollService pollService) : ControllerBase
    {
        private readonly IPollService _pollService = pollService;

        [HttpGet("")]
        public IActionResult GetAll() => Ok(_pollService.GetAll().ToResponse());
        
        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            var poll = _pollService.Get(id);

            //mapster mapping 
            //var config = new TypeAdapterConfig();
            //config.NewConfig<Poll, PollResponse>().Map(dest => dest.Notes, src => src.Description);

            //manual mapping
            return poll is null ? NotFound() : Ok(poll.ToResponse());
        }

        [HttpPost("")]
        public IActionResult Add([FromBody] CreatePollRequest request)
        {
            var poll = _pollService.Add(request.ToEntity());
            return CreatedAtAction(nameof(Get), new { id = poll.Id }, poll);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] CreatePollRequest request)
        {
            var isUpdated = _pollService.Update(id, request.ToEntity());
            return isUpdated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var isDeleted = _pollService.Delete(id);
            return isDeleted ? NoContent() : NotFound();
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            var student = new Student
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                //DateOfBirth = new DateTime(2000, 1, 1)
            };

            return Ok(student.Adapt<StudentResponse>());
        }
    }
}