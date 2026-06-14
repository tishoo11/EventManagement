using EventManagement11.Application.Interfaces;
using EventManagement11.Domain.Entities;
using EventManagement11.Infrastructure;
using Microsoft.EntityFrameworkCore;

public class TicketRepository : ITicketRepository
{
    private readonly EventDBContext context;
    public TicketRepository(EventDBContext context) => this.context = context;

    public Ticket GetById(int id) => context.Tickets
        .Include(t => t.TicketType)
        .Include(t => t.EventId)
        .FirstOrDefault(t => t.Id == id);

    public IReadOnlyList<Ticket> GetAll() => context.Tickets.ToList();

    public void Save(Ticket entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        context.Tickets.Add(entity);
        context.SaveChanges();
    }

    public void Update(Ticket entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        context.Tickets.Update(entity);
        context.SaveChanges();
    }

    public void Delete(Ticket entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        context.Tickets.Remove(entity);
        context.SaveChanges();
    }
}