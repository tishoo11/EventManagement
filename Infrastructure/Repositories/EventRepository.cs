using EventManagement11.Application.Interfaces;
using EventManagement11.Domain.Entities;
using EventManagement11.Infrastructure;
using Microsoft.EntityFrameworkCore;

public class EventRepository : IEventRepository
{
    private readonly EventDBContext context;

    public EventRepository(EventDBContext context)
    {
        this.context = context;
    }

    public Event GetById(int id)
    {
        return context.Events
            .Include(e => e.Location)
            .Include(e => e.Organizer)
            .Include(e => e.Tickets)
            .FirstOrDefault(e => e.Id == id);
    }

    public IReadOnlyList<Event> GetAll()
    {
        return context.Events.ToList();
    }

    public void Save(Event entity)
    {
        context.Events.Add(entity);
        context.SaveChanges();
    }

    public void Update(Event entity)
    {
        context.Events.Update(entity);
        context.SaveChanges();
    }

    public void Delete(Event entity)
    {
        context.Events.Remove(entity);
        context.SaveChanges();
    }
}