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

        private void EventsMenu()
        {
            while (true)
            {
                Console.Clear();
                Header("Събития");
                Console.WriteLine("1. Всички");
                Console.WriteLine("2. Добавяне");
                Console.WriteLine("3. Редактиране");
                Console.WriteLine("4. Изтриване");
                Console.WriteLine("5. Търсене по име");
                Console.WriteLine("6. Филтър по дата");
                Console.WriteLine("7. Филтър по тип");
                Console.WriteLine("8. Предстоящи");
                Console.WriteLine("9. Най-посещавани");
                Console.WriteLine("10. Проверка за свободна локация");
                Console.WriteLine("11. Проверка за капацитет");
                Console.WriteLine("0. Назад");
                Console.WriteLine();

                var choice = ReadInt("Избор: ");
                switch (choice)
                {
                    case 1:
                        PrintEvents(events.GetAll());
                        Pause();
                        break;
                    case 2:
                        CreateEvent();
                        break;
                    case 3:
                        EditEvent();
                        break;
                    case 4:
                        DeleteEvent();
                        break;
                    case 5:
                        SearchEvents();
                        break;
                    case 6:
                        FilterEventsByDate();
                        break;
                    case 7:
                        FilterEventsByType();
                        break;
                    case 8:
                        PrintEvents(events.UpcomingEvents());
                        Pause();
                        break;
                    case 9:
                        PrintEvents(events.MostVisitedEvents());
                        Pause();
                        break;
                    case 10:
                        CheckLocationAvailability();
                        break;
                    case 11:
                        CheckEventCapacity();
                        break;
                    case 0:
                        return;
                    default:
                        Message("Невалиден избор.");
                        break;
                }
            }
        }
        private void LocationsMenu()
        {
            while (true)
            {
                Console.Clear();
                Header("Локации");
                Console.WriteLine("1. Всички");
                Console.WriteLine("2. Добавяне");
                Console.WriteLine("3. Редактиране");
                Console.WriteLine("4. Изтриване");
                Console.WriteLine("5. Справка за заетост");
                Console.WriteLine("0. Назад");
                Console.WriteLine();

                var choice = ReadInt("Избор: ");

                switch (choice)
                {
                    case 1:
                        PrintLocations(locations.GetAll());
                        Pause();
                        break;
                    case 2:
                        CreateLocation();
                        break;
                    case 3:
                        EditLocation();
                        break;
                    case 4:
                        DeleteLocation();
                        break;
                    case 5:
                        LocationOccupancy();
                        break;
                    case 0:
                        return;
                    default:
                        Message("Невалиден избор.");
                        break;
                }
            }
        }

        private void OrganizersMenu()
        {
            while (true)
            {
                Console.Clear();
                Header("Организатори");
                Console.WriteLine("1. Всички");
                Console.WriteLine("2. Добавяне");
                Console.WriteLine("3. Редактиране");
                Console.WriteLine("4. Изтриване");
                Console.WriteLine("0. Назад");
                Console.WriteLine();

                var choice = ReadInt("Избор: ");

                switch (choice)
                {
                    case 1:
                        PrintOrganizers(organizers.GetAll());
                        Pause();
                        break;
                    case 2:
                        CreateOrganizer();
                        break;
                    case 3:
                        EditOrganizer();
                        break;
                    case 4:
                        DeleteOrganizer();
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
