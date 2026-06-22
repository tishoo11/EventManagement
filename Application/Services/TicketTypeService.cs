using EventManagement11.Application.Interfaces;
using EventManagement11.Domain.Entities;
using EventManagement11.Infrastructure;

namespace EventManagement11.Application.Services;

public class TicketTypeService
{
    private readonly ITicketTypeRepository ticketTypes;
    private EventDBContext context;

    public TicketTypeService(ITicketTypeRepository ticketTypes)
    {
        this.ticketTypes = ticketTypes;
    }

    public TicketTypeService(EventDBContext context)
    {
        this.context = context;
    }

    public TicketType? GetById(int id) => ticketTypes.GetById(id);

    public IReadOnlyList<TicketType> GetAll() => ticketTypes.GetAll();

    public void Create(TicketType entity) => ticketTypes.Save(entity);

    public void Edit(TicketType entity) => ticketTypes.Update(entity);

    public void Delete(int id)
    {
        var entity = ticketTypes.GetById(id);
        if (entity != null)
        {
            ticketTypes.Delete(entity);
        }
    }
}
