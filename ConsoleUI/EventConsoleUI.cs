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

        private void TicketTypesMenu()
        {
            while (true)
            {
                Console.Clear();
                Header("Типове билети");
                Console.WriteLine("1. Всички");
                Console.WriteLine("2. Добавяне");
                Console.WriteLine("3. Редактиране на цена");
                Console.WriteLine("4. Изтриване");
                Console.WriteLine("0. Назад");
                Console.WriteLine();

                var choice = ReadInt("Избор: ");

                switch (choice)
                {
                    case 1:
                        PrintTicketTypes(ticketTypes.GetAll());
                        Pause();
                        break;
                    case 2:
                        CreateTicketType();
                        break;
                    case 3:
                        EditTicketType();
                        break;
                    case 4:
                        DeleteTicketType();
                        break;
                    case 0:
                        return;
                    default:
                        Message("Невалиден избор.");
                        break;
                }
            }
        }

        private void TicketsMenu()
        {
            while (true)
            {
                Console.Clear();
                Header("Билети");
                Console.WriteLine("1. Всички");
                Console.WriteLine("2. Генериране");
                Console.WriteLine("3. Отмяна");
                Console.WriteLine("4. Маркиране като използван");
                Console.WriteLine("5. Проверка за валидност");
                Console.WriteLine("0. Назад");
                Console.WriteLine();

                var choice = ReadInt("Избор: ");

                switch (choice)
                {
                    case 1:
                        PrintTickets(tickets.GetAll());
                        Pause();
                        break;
                    case 2:
                        CreateTicket();
                        break;
                    case 3:
                        CancelTicket();
                        break;
                    case 4:
                        MarkTicketAsUsed();
                        break;
                    case 5:
                        CheckTicketValidity();
                        break;
                    case 0:
                        return;
                    default:
                        Message("Невалиден избор.");
                        break;
                }
            }
        }

        private void CreateEvent()
        {
            try
            {
                Console.Clear();
                Header("Добавяне на събитие");

                var name = ReadRequired("Име: ");
                var date = ReadDateTime("Дата и час (dd.MM.yyyy HH:mm): ");
                var locationId = ReadInt("Location ID: ");
                var organizerId = ReadInt("Organizer ID: ");
                var capacity = ReadInt("Капацитет: ");
                var type = ReadRequired("Тип: ");

                var location = locations.GetById(locationId);
                if (location == null)
                {
                    Message("Локацията не е намерена.");
                    return;
                }

                if (organizers.GetById(organizerId) == null)
                {
                    Message("Организаторът не е намерен.");
                    return;
                }

                if (capacity > location.Capacity)
                {
                    Message("Капацитетът на събитието е по-голям от капацитета на локацията.");
                    return;
                }

                if (!events.IsLocationAvailable(locationId, date))
                {
                    Message("Локацията е заета за тази дата.");
                    return;
                }

                var entity = new Event(name, date, locationId, organizerId, capacity, type);
                events.Create(entity);

                Message($"Събитието е създадено. ID: {entity.Id}");
            }
            catch (Exception ex)
            {
                Message(ex.Message);
            }
        }

        private void EditEvent()
        {
            try
            {
                Console.Clear();
                Header("Редактиране на събитие");

                var id = ReadInt("Event ID: ");
                var entity = events.GetById(id);

                if (entity == null)
                {
                    Message("Събитието не е намерено.");
                    return;
                }

                Console.WriteLine($"Текущо име: {entity.Name}");
                var name = ReadOptional("Ново име (Enter за пропуск): ");

                Console.WriteLine($"Текуща дата: {entity.Date:dd.MM.yyyy HH:mm}");
                var dateText = ReadOptional("Нова дата и час (Enter за пропуск): ");

                Console.WriteLine($"Текущ тип: {entity.EventType}");
                var type = ReadOptional("Нов тип (Enter за пропуск): ");

                Console.WriteLine($"Текущ капацитет: {entity.Capacity}");
                var capacityText = ReadOptional("Нов капацитет (Enter за пропуск): ");

                var newDate = string.IsNullOrWhiteSpace(dateText) ? entity.Date : ReadDateTimeFromInput(dateText);
                var newCapacity = string.IsNullOrWhiteSpace(capacityText) ? entity.Capacity : ParseInt(capacityText);
                var soldTickets = entity.Tickets.Count(t => t.Status == TicketStatus.Sold);
                var location = locations.GetById(entity.LocationId) ?? entity.Location;

                if (location == null)
                {
                    Message("Локацията на събитието не е намерена.");
                    return;
                }

                if (!string.IsNullOrWhiteSpace(dateText) && !events.IsLocationAvailable(entity.LocationId, newDate, entity.Id))
                {
                    Message("Локацията е заета за тази дата.");
                    return;
                }

                if (newCapacity > location.Capacity)
                {
                    Message("Капацитетът е по-голям от капацитета на локацията.");
                    return;
                }

                if (!string.IsNullOrWhiteSpace(name))
                    entity.EditName(name);

                if (!string.IsNullOrWhiteSpace(dateText))
                    entity.Reschedule(newDate);

                if (!string.IsNullOrWhiteSpace(type))
                    entity.ChangeType(type);

                if (!string.IsNullOrWhiteSpace(capacityText))
                    entity.ChangeCapacity(newCapacity, soldTickets, location.Capacity);

                events.Edit(entity);
                Message("Събитието е обновено.");
            }
            catch (Exception ex)
            {
                Message(ex.Message);
            }
        }

        private void DeleteEvent()
        {
            Console.Clear();
            Header("Изтриване на събитие");
            var id = ReadInt("Event ID: ");
            events.Delete(id);
            Message("Събитието е изтрито.");
        }
    }
}
