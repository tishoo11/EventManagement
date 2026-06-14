using EventManagement11.Application.Interfaces;
using EventManagement11.Domain.Entities;
using EventManagement11.Infrastructure;
using Microsoft.EntityFrameworkCore;

public class TicketTypeRepository : ITicketTypeRepository
{
    private readonly EventDBContext context;
    public TicketTypeRepository(EventDBContext context) => this.context = context;

    public TicketType GetById(int id) => context.TicketTypes
        .Include(tt => tt.Tickets) 
        .FirstOrDefault(tt => tt.Id == id);

    public IReadOnlyList<TicketType> GetAll() => context.TicketTypes.ToList();

    public void Save(TicketType entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        context.TicketTypes.Add(entity);
        context.SaveChanges();
    }

    public void Update(TicketType entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        context.TicketTypes.Update(entity);
        context.SaveChanges();
    }

    public void Delete(TicketType entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        context.TicketTypes.Remove(entity);
        context.SaveChanges();
    }
}