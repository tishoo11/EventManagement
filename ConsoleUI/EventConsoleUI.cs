using EventManagement11.Application.Services;
using EventManagement11.Domain.Entities;
using EventManagement11.Domain.Enums;
using EventManagement11.Domain.ValueObjects;
using System.Globalization;

namespace EventManagement11.ConsoleUI
{
    public class EventConsoleUI
    {
        private readonly EventService events;
        private readonly LocationService locations;
        private readonly OrganizerService organizers;
        private readonly TicketTypeService ticketTypes;
        private readonly TicketService tickets;

        public EventConsoleUI(
            EventService events,
            LocationService locations,
            OrganizerService organizers,
            TicketTypeService ticketTypes,
            TicketService tickets)
        {
            this.events = events;
            this.locations = locations;
            this.organizers = organizers;
            this.ticketTypes = ticketTypes;
            this.tickets = tickets;
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Header("Event Management System");
                Console.WriteLine("1. Събития");
                Console.WriteLine("2. Локации");
                Console.WriteLine("3. Организатори");
                Console.WriteLine("4. Типове билети");
                Console.WriteLine("5. Билети");
                Console.WriteLine("0. Изход");
                Console.WriteLine();

                var choice = ReadInt("Избор: ");

                switch (choice)
                {
                    case 1:
                        EventsMenu();
                        break;
                    case 2:
                        LocationsMenu();
                        break;
                    case 3:
                        OrganizersMenu();
                        break;
                    case 4:
                        TicketTypesMenu();
                        break;
                    case 5:
                        TicketsMenu();
                        break;
                    case 0:
                        return;
                    default:
                        Message("Невалиден избор.");
                        break;
                }
            }
        }
    }
}
