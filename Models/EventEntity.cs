using FirstSprintProject.Dtos;

namespace FirstSprintProject.Models;

public class EventEntity
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartAt { get; set; }
    public DateTime EndAt { get; set; }

    public EventEntity(int id, string title, string description, DateTime startAt, DateTime endAt)
    {
        Id = id;
        Title = title;
        Description = description;
        StartAt = startAt;
        EndAt = endAt;
    }

    public EventEntity(EventDto eventDto)
    {
        Id = eventDto.Id;
        Title = eventDto.Title;
        Description = eventDto.Description;
        StartAt = eventDto.StartAt.Value;
        EndAt = eventDto.EndAt.Value;
    }
}
