using FirstSprintProject.Dtos;

namespace FirstSprintProject.Models;

public class Event
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime StartAt { get; set; }
    public DateTime EndAt { get; set; }

    public Event(int id, string title, string description, DateTime startAt, DateTime endAt)
    {
        Id = id;
        Title = title;
        Description = description;
        StartAt = startAt;
        EndAt = endAt;
    }

    public Event(int id, EventDto eventDto)
    {
        if (eventDto.StartAt.HasValue && eventDto.EndAt.HasValue)
        {
            Id = id;
            Title = eventDto.Title;
            Description = eventDto.Description;
            StartAt = eventDto.StartAt.Value;
            EndAt = eventDto.EndAt.Value;
        }
    }

    public Event(EventDto eventDto)
    {
        if (eventDto.StartAt.HasValue && eventDto.EndAt.HasValue)
        {
            Id = eventDto.Id;
            Title = eventDto.Title;
            Description = eventDto.Description;
            StartAt = eventDto.StartAt.Value;
            EndAt = eventDto.EndAt.Value;
        }
    }
}
