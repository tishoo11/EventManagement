using EventManagement11.Application.Interfaces;
using EventManagement11.Domain.Entities;
using EventManagement11.Infrastructure;
using Microsoft.EntityFrameworkCore;

public class LocationRepository : ILocationRepository
{
    private readonly EventDBContext context;
    public LocationRepository(EventDBContext context) => this.context = context;

    public Location GetById(int id) => context.Locations
        .Include(l => l.Events) 
        .FirstOrDefault(l => l.Id == id);

    public IReadOnlyList<Location> GetAll() => context.Locations.ToList();

    public void Save(Location entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        context.Locations.Add(entity);
        context.SaveChanges();
    }

    public void Update(Location entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        context.Locations.Update(entity);
        context.SaveChanges();
    }

    public void Delete(Location entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        context.Locations.Remove(entity);
        context.SaveChanges();
    }
}