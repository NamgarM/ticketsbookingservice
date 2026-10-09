using FirstSprintProject.Models;

namespace FirstSprintProject.Services.Interfaces;

public interface IEventService
{
    bool AddEvent(EventEntity newEvent);
    bool DeleteEvent(int id);
    EventEntity? GetEvent(int id);
    List<EventEntity> GetEvents();
    bool UpdateEventData(EventEntity eventData);
}
