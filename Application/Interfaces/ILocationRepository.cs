using EventManagement11.Domain.Entities;

namespace EventManagement11.Application.Interfaces;
public interface ILocationRepository
{
    Location GetById(int id);
    IReadOnlyList<Location> GetAll();
    void Save(Location location);
    void Update(Location location);
    void Delete(Location entity);
}