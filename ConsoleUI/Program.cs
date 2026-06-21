using EventManagement11.Application.Services;
using EventManagement11.Domain.Entities;
using EventManagement11.Domain.ValueObjects;
using EventManagement11.Infrastructure;
using EventManagement11.Infrastructure.Repositories;

namespace EventManagement11.ConsoleUI;

internal static class Program
{
    private static void Main()
    {
        using var context = new EventDBContext();

        var eventRepository = new EventRepository(context);
        var locationRepository = new LocationRepository(context);
        var organizerRepository = new OrganizerRepository(context);
        var ticketRepository = new TicketRepository(context);
        var ticketTypeRepository = new TicketTypeRepository(context);

        var eventService = new EventService(eventRepository);
        var locationService = new LocationService(locationRepository, eventRepository);
        var organizerService = new OrganizerService(organizerRepository);
        var ticketTypeService = new TicketTypeService(ticketTypeRepository);
        var ticketService = new TicketService(ticketRepository);

        var ui = new EventConsoleUI(
            eventService,
            locationService,
            organizerService,
            ticketTypeService,
            ticketService);
        
        ui.Run();
    }
    
}
