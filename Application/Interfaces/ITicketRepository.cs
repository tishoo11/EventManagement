using EventManagement11.Domain.Entities;

namespace EventManagement11.Application.Interfaces;
public interface ITicketRepository
{
    Ticket GetById(int id);
    IReadOnlyList<Ticket> GetAll();
    void Save(Ticket ticket);
    void Update(Ticket ticket);
    void Delete(Ticket entity);
}