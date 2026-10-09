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
        var eventsDtos = _eventService.GetEvents().Select(e => new EventDto{
            Id = e.Id,
            Title = e.Title,
            Description = e.Description,
            StartAt = e.StartAt,
            EndAt = e.EndAt
        }).ToList();

        return new OkObjectResult(eventsDtos);
    }

    [HttpGet("/events/{id:int}")]
    public ApiBaseResult GetEventById(int id)
    {
        var eventToGet = _eventService.GetEvent(id);
        if (eventToGet != null)
        {
            return new ApiResult<EventDto>()
            { 
                Data = new EventDto()
                { 
                    Id = eventToGet.Id,
                    Title = eventToGet.Title,
                    Description = eventToGet.Description,
                    StartAt = eventToGet.StartAt,
                    EndAt = eventToGet.EndAt
                },
                Success = true,
                StatusCode = HttpStatusCode.OK,
                Message = $"You got an event data with id {id}"
            };
        }

        return new()
        { 
            Success = false,
            StatusCode= HttpStatusCode.NotFound,
            Message = $"An avent with id {id} doesn't exist in the databese"
        };
    }

    [HttpPost("/events")]
    public IActionResult PostEvent([FromBody] EventDto eventDto)
    {
        var newEvent = new EventEntity(eventDto);
        if (_eventService.AddEvent(newEvent))
            return CreatedAtAction(nameof(_eventService.AddEvent), new { id = newEvent.Id }, newEvent);

        return BadRequest("The event is already existing");
    }

    [HttpPut("/events/{id:int}")]
    public IActionResult PutEvent([FromBody] EventDto eventDto) 
    {
        var newEvent = new EventEntity(eventDto);
        if (_eventService.UpdateEvent(newEvent))
            return Ok($"Event with id {newEvent.Id} is updated");

        return NotFound($"Event with id {newEvent.Id} is not found, therefore, not updated");
    }

    [HttpDelete("/events/{id:int}")]
    public IActionResult DeleteEvent(int id)
    {
        if (_eventService.DeleteEvent(id))
        {
            return Ok($"Event with id {id} is deleted"); 
        }

        return NotFound($"Event with id {id} isn't found");
    }
}
