using EventManagement11.Domain.Entities;

namespace EventManagement11.Application.Interfaces;
public interface ITicketTypeRepository
{
    TicketType GetById(int id);
    IReadOnlyList<TicketType> GetAll();
    void Save(TicketType ticketType);
    void Update(TicketType ticketType);
}