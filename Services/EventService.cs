using FirstSprintProject.Models;
using FirstSprintProject.Services.Interfaces;
using System.Collections.Concurrent;

namespace FirstSprintProject.Services;

public class EventService : IEventService
{
    private List<Event> _events = new()
    {
        new Event(6, "Halloween", "", new(2026, 10, 31, 0, 0, 0), new(2026, 11, 1, 0, 0, 0)),
        new Event(31, "Christmas", "", new(2026, 12, 24, 0, 0, 0), new(2026, 12, 26, 0, 0, 0)),
        new Event(1, "New Year", "", new(2026, 12, 31, 0, 0, 0), new(2027, 1, 1, 0, 0, 0))
    };

    public List<Event> GetEvents()
    {
        return _events;
    }

    public Event? GetEvent(int id)
    {
        return _events.FirstOrDefault(e => e.Id == id) ?? null;
    }

    // ⚠️ ВНИМАНИЕ: Потенциальное состояние гонки (Race Condition). 
    // Два параллельных запроса могут пройти проверку и добавить дубликат. 
    // Для Sprint-1 допустимо, так как это in-memory демо. 
    // В продакшене уникальность должна гарантироваться БД или блокировками.
    public bool AddEvent(Event eventData)
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

    public bool UpdateEvent(Event eventData)
    {
        var eventToUpdate = _events.FirstOrDefault(e => e.Id == eventData.Id);

        if (eventToUpdate == null)
            return false;

        var index = _events.IndexOf(eventToUpdate);
        _events[index] = eventData;
        return true;
    }
}
