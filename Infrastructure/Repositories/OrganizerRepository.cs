using EventManagement11.Application.Interfaces;
using EventManagement11.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventManagement11.Infrastructure.Repositories;

public class OrganizerRepository : IOrganizerRepository
{
    private readonly EventDBContext context;

    public OrganizerRepository(EventDBContext context)
    {
        this.context = context;
    }

    public Organizer? GetById(int id)
    {
        return context.Organizers
            .Include(o => o.Events)
            .FirstOrDefault(o => o.Id == id);
    }

    public IReadOnlyList<Organizer> GetAll()
    {
        return context.Organizers
            .Include(o => o.Events)
            .ToList();
    }

    public void Save(Organizer entity)
    {
        context.Organizers.Add(entity);
        context.SaveChanges();
    }

    public void Update(Organizer entity)
    {
        context.Organizers.Update(entity);
        context.SaveChanges();
    }

    public void Delete(Organizer entity)
    {
        context.Organizers.Remove(entity);
        context.SaveChanges();
    }
}
