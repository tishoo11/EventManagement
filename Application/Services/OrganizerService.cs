using EventManagement11.Application.Interfaces;
using EventManagement11.Domain.Entities;
using EventManagement11.Infrastructure;

namespace EventManagement11.Application.Services;

public class OrganizerService
{
    private readonly IOrganizerRepository organizers;
    private EventDBContext context;

    public OrganizerService(IOrganizerRepository organizers)
    {
        this.organizers = organizers;
    }

    public OrganizerService(EventDBContext context)
    {
        this.context = context;
    }

    public Organizer? GetById(int id) => organizers.GetById(id);

    public IReadOnlyList<Organizer> GetAll() => organizers.GetAll();

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
}
