using EventManagement11.Domain.Entities;

namespace EventManagement11.Application.Interfaces;
public interface IOrganizerRepository
{
    Organizer GetById(int id);
    IReadOnlyList<Organizer> GetAll();
    void Save(Organizer organizer);
    void Update(Organizer organizer);
    void Delete(Organizer entity);
}