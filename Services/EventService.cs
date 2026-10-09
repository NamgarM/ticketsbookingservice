using FirstSprintProject.Models;
using FirstSprintProject.Services.Interfaces;

namespace FirstSprintProject.Services;

public class EventService : IEventService
{
    private List<EventEntity> _events = new()
    {
        new EventEntity(6, "Halloween", "", new(2026, 10, 31, 0, 0, 0), new(2026, 11, 1, 0, 0, 0)),
        new EventEntity(31, "Christmas", "", new(2026, 12, 24, 0, 0, 0), new(2026, 12, 26, 0, 0, 0)),
        new EventEntity(1, "New Year", "", new(2026, 12, 31, 0, 0, 0), new(2027, 1, 1, 0, 0, 0))
    };

    public List<EventEntity> GetEvents()
    {
        return _events;
    }

    public EventEntity? GetEvent(int id)
    {
        return _events.FirstOrDefault(e => e.Id == id) ?? null;
    }

    public bool AddEvent(EventEntity eventData)
    {
        if (_events.FirstOrDefault(e => e.Id == eventData.Id) != null)
            return false;

        _events.Add(eventData);
        return true;
    }

    public bool DeleteEvent(int id)
    {   
        return _events.RemoveAll(e => e.Id == id) > 0;  
    }

    public bool UpdateEvent(EventEntity eventData)
    {
        var eventToUpdate = _events.FirstOrDefault(e => e.Id == eventData.Id);

        if (eventToUpdate == null)
            return false;

        var index = _events.IndexOf(eventToUpdate);
        _events[index] = eventData;
        return true;
    }
}
