using EventManagement11.Application.Interfaces;
using EventManagement11.Domain.Entities;

namespace EventManagement11.Application.Services;

public class TicketTypeService
{
    private readonly ITicketTypeRepository ticketTypes;

    public TicketTypeService(ITicketTypeRepository ticketTypes)
    {
        this.ticketTypes = ticketTypes;
    }

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

    public IReadOnlyList<TicketType> GetAll() => ticketTypes.GetAll();
}
