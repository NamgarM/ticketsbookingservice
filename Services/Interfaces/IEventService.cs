using FirstSprintProject.Models;

namespace FirstSprintProject.Services.Interfaces;

public interface IEventService
{
    bool AddEvent(Event newEvent);
    bool DeleteEvent(int id);
    Event? GetEvent(int id);
    List<Event> GetEvents();
    bool UpdateEvent(Event eventData);
}
