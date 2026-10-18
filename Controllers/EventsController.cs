using FirstSprintProject.Dtos;
using FirstSprintProject.Dtos.Responses;
using FirstSprintProject.Models;
using FirstSprintProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FirstSprintProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController(IEventService _eventService) : ControllerBase
{
    [HttpGet("/events")]
    public ActionResult<List<EventDto>> GetAllEvents()
    {
        var eventsDtos = _eventService.GetEvents().Select(e => new EventDto
        {
            Id = e.Id,
            Title = e.Title ?? string.Empty,
            Description = e.Description ?? string.Empty,
            StartAt = e.StartAt,
            EndAt = e.EndAt
        }).ToList();

        return new OkObjectResult(eventsDtos);
    }

    [HttpGet("/events/{id:int}")]
    public ActionResult<ApiResult<EventDto>> GetEventById(int id)
    {
        var eventToGet = _eventService.GetEvent(id);
        if (eventToGet != null)
        {
            return new ApiResult<EventDto>()
            {
                Data = new EventDto()
                {
                    Id = eventToGet.Id,
                    Title = eventToGet.Title ?? string.Empty,
                    Description = eventToGet.Description ?? string.Empty,
                    StartAt = eventToGet.StartAt,
                    EndAt = eventToGet.EndAt
                },
                Success = true,
                StatusCode = HttpStatusCode.OK,
                Message = $"You got an event data with id {id}"
            };
        }

        return Problem(detail: $"An event with id {id} doesn't exist in the databese", statusCode: StatusCodes.Status404NotFound, title: "Not Found");
    }

    [HttpPost("/events")]
    public IActionResult PostEvent([FromBody] EventDto eventDto)
    {
        var newEvent = new Event(eventDto);
        if (_eventService.AddEvent(newEvent))
            return CreatedAtAction(nameof(PostEvent), new { id = newEvent.Id }, newEvent);

        return Problem(detail: "The event is already existing", statusCode: StatusCodes.Status400BadRequest);
    }

    [HttpPut("/events/{id:int}")]
    public IActionResult PutEvent([FromRoute] int id, [FromBody] EventDto eventDto)
    {
        var newEvent = new Event(id, eventDto);
        if (_eventService.UpdateEvent(newEvent))
            return Ok($"Event with id {newEvent.Id} is updated");

        return Problem(detail: $"Event with id {newEvent.Id} is not found, therefore, not updated", statusCode: StatusCodes.Status404NotFound, title: "Not Found");
    }

    [HttpDelete("/events/{id:int}")]
    public IActionResult DeleteEvent(int id)
    {
        if (_eventService.DeleteEvent(id))
        {
            return Ok($"Event with id {id} is deleted");
        }

        return Problem(detail: $"Event with id {id} isn't found", statusCode: StatusCodes.Status404NotFound, title: "Not Found");
    }
}
