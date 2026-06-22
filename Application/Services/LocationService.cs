using EventManagement11.Application.Interfaces;
using EventManagement11.Domain.Entities;
using EventManagement11.Infrastructure;

namespace EventManagement11.Application.Services;

public class LocationService
{
    private readonly ILocationRepository locations;
    private readonly IEventRepository events;
    private EventDBContext context;

    public LocationService(ILocationRepository locations, IEventRepository events)
    {
        this.locations = locations;
        this.events = events;
    }

    public LocationService(EventDBContext context)
    {
        this.context = context;
    }

    public Location? GetById(int id) => locations.GetById(id);

    public IReadOnlyList<Location> GetAll() => locations.GetAll();

    public void Create(Location entity) => locations.Save(entity);

    public void Edit(Location entity) => locations.Update(entity);

    public void Delete(int id)
    {
        var entity = locations.GetById(id);
        if (entity != null)
        {
            locations.Delete(entity);
        }
    }

    public bool IsLocationAvailable(int locationId, DateTime date, int? ignoreEventId = null)
    {
        return !events.GetAll().Any(e =>
            e.LocationId == locationId &&
            e.Date.Date == date.Date &&
            (!ignoreEventId.HasValue || e.Id != ignoreEventId.Value));
    }

    public int GetOccupancy(int locationId)
    {
        return events.GetAll().Count(e => e.LocationId == locationId);
    }
}
