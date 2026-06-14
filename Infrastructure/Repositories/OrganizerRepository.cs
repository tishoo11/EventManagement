using EventManagement11.Application.Interfaces;
using EventManagement11.Domain.Entities;
using EventManagement11.Infrastructure;
using Microsoft.EntityFrameworkCore;

public class OrganizerRepository : IOrganizerRepository
{
    private readonly EventDBContext context;
    public OrganizerRepository(EventDBContext context) => this.context = context;

    public Organizer GetById(int id) => context.Organizers
        .Include(o => o.Events) 
        .FirstOrDefault(o => o.Id == id);

    public IReadOnlyList<Organizer> GetAll() => context.Organizers.ToList();

    public void Save(Organizer entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        context.Organizers.Add(entity);
        context.SaveChanges();
    }

    public void Update(Organizer entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        context.Organizers.Update(entity);
        context.SaveChanges();
    }

    public void Delete(Organizer entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        context.Organizers.Remove(entity);
        context.SaveChanges();
    }
}