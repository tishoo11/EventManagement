using EventManagement11.Application.Interfaces;
using EventManagement11.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventManagement11.Infrastructure.Repositories;

public class TicketRepository : ITicketRepository
{
    private readonly EventDBContext context;

    public TicketRepository(EventDBContext context)
    {
        this.context = context;
    }

    public Ticket? GetById(int id)
    {
        return context.Tickets
            .Include(t => t.TicketType)
            .FirstOrDefault(t => t.Id == id);
    }

    public IReadOnlyList<Ticket> GetAll()
    {
        return context.Tickets
            .Include(t => t.TicketType)
            .ToList();
    }

    public void Save(Ticket entity)
    {
        context.Tickets.Add(entity);
        context.SaveChanges();
    }

    public void Update(Ticket entity)
    {
        context.Tickets.Update(entity);
        context.SaveChanges();
    }

    public void Delete(Ticket entity)
    {
        context.Tickets.Remove(entity);
        context.SaveChanges();
    }
}
