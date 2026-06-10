using EventManagement11.Domain.Entities;

namespace EventManagement11.Application.Interfaces;
public interface IEventRepository
{
    Event GetById(int id);
    IReadOnlyList<Event> GetAll();
    void Save(Event entity);
    void Update(Event entity);
    void Delete(Event entity);
}