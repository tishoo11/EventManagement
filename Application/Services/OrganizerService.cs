using EventManagement11.Domain.Entities;
using EventManagement11.Application.Interfaces;
namespace EventManagement11.Application.Services;

public class OrganizerService
{
    private readonly IOrganizerRepository organizers;

    public OrganizerService(IOrganizerRepository organizers)
    {
        this.organizers = organizers;
    }

    public void Create(Organizer entity) => organizers.Save(entity);

    public void Edit(Organizer entity) => organizers.Update(entity);

    public void Delete(int id)
    {
        var entity = organizers.GetById(id);
        if (entity != null)
        {
            organizers.Delete(entity);
        }
    }

    public IReadOnlyList<Organizer> GetAll() => organizers.GetAll();
}
