using EventManagement11.Application.Interfaces;
using EventManagement11.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventManagement11.Infrastructure.Repositories;

public class LocationRepository : ILocationRepository
{
    private readonly EventDBContext context;

    public LocationRepository(EventDBContext context)
    {
        this.context = context;
    }

    public Location? GetById(int id)
    {
        return context.Locations
            .Include(l => l.Events)
            .FirstOrDefault(l => l.Id == id);
    }

    public IReadOnlyList<Location> GetAll()
    {
        return context.Locations
            .Include(l => l.Events)
            .ToList();
    }

    public void Save(Location entity)
    {
        context.Locations.Add(entity);
        context.SaveChanges();
    }

    public void Update(Location entity)
    {
        context.Locations.Update(entity);
        context.SaveChanges();
    }

    public void Delete(Location entity)
    {
        context.Locations.Remove(entity);
        context.SaveChanges();
    }
}


