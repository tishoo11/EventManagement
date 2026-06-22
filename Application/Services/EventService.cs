using EventManagement11.Application.Interfaces;
using EventManagement11.Domain.Entities;
using EventManagement11.Domain.Enums;
using EventManagement11.Infrastructure;

namespace EventManagement11.Application.Services;

public class EventService
{
    private readonly IEventRepository events;
    private EventDBContext context;

    public EventService(IEventRepository events)
    {
        this.events = events;
    }

    public EventService(EventDBContext context)
    {
        this.context = context;
    }

    public Event? GetById(int id) => events.GetById(id);

    public IReadOnlyList<Event> GetAll() => events.GetAll();

    public void Create(Event entity) => events.Save(entity);

    public void Edit(Event entity) => events.Update(entity);

    public void Delete(int id)
    {
        var entity = events.GetById(id);
        if (entity != null)
        {
            events.Delete(entity);
        }
    }

    public IReadOnlyList<Event> SearchByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Array.Empty<Event>();

        return events
            .GetAll()
            .Where(e => e.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
            .OrderBy(e => e.Date)
            .ToList();
    }

    public IReadOnlyList<Event> FilterByDate(DateTime date)
    {
        return events
            .GetAll()
            .Where(e => e.Date.Date == date.Date)
            .OrderBy(e => e.Date)
            .ToList();
    }

    public IReadOnlyList<Event> FilterByType(string type)
    {
        if (string.IsNullOrWhiteSpace(type))
            return Array.Empty<Event>();

        return events
            .GetAll()
            .Where(e => string.Equals(e.EventType, type, StringComparison.OrdinalIgnoreCase))
            .OrderBy(e => e.Date)
            .ToList();
    }

    public bool IsLocationAvailable(int locationId, DateTime date, int? ignoreEventId = null)
    {
        return !events.GetAll().Any(e =>
            e.LocationId == locationId &&
            e.Date.Date == date.Date &&
            (!ignoreEventId.HasValue || e.Id != ignoreEventId.Value));
    }

    public bool HasCapacity(int eventId)
    {
        var entity = events.GetById(eventId);
        return entity != null && entity.Tickets.Count(t => t.Status == TicketStatus.Sold) < entity.Capacity;
    }

    public IReadOnlyList<Event> UpcomingEvents()
    {
        return events
            .GetAll()
            .Where(e => e.Date.Date >= DateTime.Today)
            .OrderBy(e => e.Date)
            .ToList();
    }

    public IReadOnlyList<Event> MostVisitedEvents()
    {
        return events
            .GetAll()
            .OrderByDescending(e => e.Tickets.Count(t => t.Status == TicketStatus.Sold))
            .ThenBy(e => e.Date)
            .ToList();
    }
}
