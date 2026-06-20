using EventManagement11.Application.Interfaces;
using EventManagement11.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventManagement11.Infrastructure.Repositories;

public class TicketTypeRepository : ITicketTypeRepository
{
    private readonly EventDBContext context;

    public TicketTypeRepository(EventDBContext context)
    {
        this.context = context;
    }

    public TicketType? GetById(int id)
    {
        return context.TicketTypes
            .Include(tt => tt.Tickets)
            .FirstOrDefault(tt => tt.Id == id);
    }

    public IReadOnlyList<TicketType> GetAll()
    {
        return context.TicketTypes
            .Include(tt => tt.Tickets)
            .ToList();
    }

    public void Save(TicketType entity)
    {
        context.TicketTypes.Add(entity);
        context.SaveChanges();
    }

    public void Update(TicketType entity)
    {
        context.TicketTypes.Update(entity);
        context.SaveChanges();
    }

    public void Delete(TicketType entity)
    {
        context.TicketTypes.Remove(entity);
        context.SaveChanges();
    }
}
