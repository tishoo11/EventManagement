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

        private void SearchEvents()
        {
            Console.Clear();
            Header("Търсене по име");
            var text = ReadRequired("Текст: ");
            PrintEvents(events.SearchByName(text));
            Pause();
        }

        private void FilterEventsByDate()
        {
            Console.Clear();
            Header("Филтър по дата");
            var date = ReadDateTime("Дата и час (dd.MM.yyyy HH:mm): ");
            PrintEvents(events.FilterByDate(date));
            Pause();
        }

        private void FilterEventsByType()
        {
            Console.Clear();
            Header("Филтър по тип");
            var type = ReadRequired("Тип: ");
            PrintEvents(events.FilterByType(type));
            Pause();
        }

        private void CheckLocationAvailability()
        {
            Console.Clear();
            Header("Проверка за свободна локация");
            var locationId = ReadInt("Location ID: ");
            var date = ReadDateTime("Дата и час (dd.MM.yyyy HH:mm): ");
            var available = events.IsLocationAvailable(locationId, date);
            Message(available ? "Локацията е свободна." : "Локацията е заета.");
        }

        private void CheckEventCapacity()
        {
            Console.Clear();
            Header("Проверка за капацитет");
            var eventId = ReadInt("Event ID: ");
            var available = events.HasCapacity(eventId);
            Message(available ? "Има свободни места." : "Капацитетът е запълнен.");
        }

        private void CreateLocation()
        {
            try
            {
                Console.Clear();
                Header("Добавяне на локация");

                var name = ReadRequired("Име: ");
                var address = ReadRequired("Адрес: ");
                var capacity = ReadInt("Капацитет: ");

                var entity = new Location(name, address, capacity);
                locations.Create(entity);

                Message($"Локацията е създадена. ID: {entity.Id}");
            }
            catch (Exception ex)
            {
                Message(ex.Message);
            }
        }

        private void EditLocation()
        {
            try
            {
                Console.Clear();
                Header("Редактиране на локация");

                var id = ReadInt("Location ID: ");
                var entity = locations.GetById(id);

                if (entity == null)
                {
                    Message("Локацията не е намерена.");
                    return;
                }

                Console.WriteLine($"Текущо име: {entity.Name}");
                var name = ReadRequired("Ново име: ");
                Console.WriteLine($"Текущ адрес: {entity.Address}");
                var address = ReadRequired("Нов адрес: ");
                Console.WriteLine($"Текущ капацитет: {entity.Capacity}");
                var capacity = ReadInt("Нов капацитет: ");

                entity.Edit(name, address, capacity);
                locations.Edit(entity);

                Message("Локацията е обновена.");
            }
            catch (Exception ex)
            {
                Message(ex.Message);
            }
        }

        private void DeleteLocation()
        {
            Console.Clear();
            Header("Изтриване на локация");
            var id = ReadInt("Location ID: ");
            locations.Delete(id);
            Message("Локацията е изтрита.");
        }

        private void LocationOccupancy()
        {
            Console.Clear();
            Header("Справка за заетост на локация");
            var id = ReadInt("Location ID: ");
            var entity = locations.GetById(id);

            if (entity == null)
            {
                Message("Локацията не е намерена.");
                return;
            }

            Console.WriteLine($"Локация: {entity.Name}");
            Console.WriteLine($"Събития: {locations.GetOccupancy(id)}");
            Console.WriteLine();

            PrintEvents(entity.Events.OrderBy(e => e.Date));
            Pause();
        }

        private void CreateOrganizer()
        {
            try
            {
                Console.Clear();
                Header("Добавяне на организатор");

                var name = ReadRequired("Име: ");
                var phone = ReadRequired("Телефон: ");

                var entity = new Organizer(name, phone);
                organizers.Create(entity);

                Message($"Организаторът е създаден. ID: {entity.Id}");
            }
            catch (Exception ex)
            {
                Message(ex.Message);
            }
        }

        private void EditOrganizer()
        {
            try
            {
                Console.Clear();
                Header("Редактиране на организатор");

                var id = ReadInt("Organizer ID: ");
                var entity = organizers.GetById(id);

                if (entity == null)
                {
                    Message("Организаторът не е намерен.");
                    return;
                }

                Console.WriteLine($"Текущо име: {entity.Name}");
                var name = ReadRequired("Ново име: ");
                Console.WriteLine($"Текущ телефон: {entity.ContactNumber}");
                var phone = ReadRequired("Нов телефон: ");

                entity.Edit(name, phone);
                organizers.Edit(entity);

                Message("Организаторът е обновен.");
            }
            catch (Exception ex)
            {
                Message(ex.Message);
            }
        }

        private void DeleteOrganizer()
        {
            Console.Clear();
            Header("Изтриване на организатор");
            var id = ReadInt("Organizer ID: ");
            organizers.Delete(id);
            Message("Организаторът е изтрит.");
        }

        private void CreateTicketType()
        {
            try
            {
                Console.Clear();
                Header("Добавяне на тип билет");

                var name = ReadRequired("Име: ");
                var price = ReadDecimal("Цена: ");

                var entity = new TicketType(name, new Money(price));
                ticketTypes.Create(entity);

                Message($"Типът е създаден. ID: {entity.Id}");
            }
            catch (Exception ex)
            {
                Message(ex.Message);
            }
        }

        private void EditTicketType()
        {
            try
            {
                Console.Clear();
                Header("Редактиране на цена");

                var id = ReadInt("TicketType ID: ");
                var entity = ticketTypes.GetById(id);

                if (entity == null)
                {
                    Message("Типът билет не е намерен.");
                    return;
                }

                Console.WriteLine($"Текуща цена: {entity.Price}");
                var price = ReadDecimal("Нова цена: ");

                entity.ChangePrice(new Money(price));
                ticketTypes.Edit(entity);

                Message("Цената е обновена.");
            }
            catch (Exception ex)
            {
                Message(ex.Message);
            }
        }

        private void DeleteTicketType()
        {
            Console.Clear();
            Header("Изтриване на тип билет");
            var id = ReadInt("TicketType ID: ");
            ticketTypes.Delete(id);
            Message("Типът билет е изтрит.");
        }

        private void CreateTicket()
        {
            try
            {
                Console.Clear();
                Header("Генериране на билет");

                var eventId = ReadInt("Event ID: ");
                var ticketTypeId = ReadInt("TicketType ID: ");

                var eventEntity = events.GetById(eventId);
                if (eventEntity == null)
                {
                    Message("Събитието не е намерено.");
                    return;
                }

                if (!events.HasCapacity(eventId))
                {
                    Message("Няма свободен капацитет.");
                    return;
                }

                var ticketType = ticketTypes.GetById(ticketTypeId);
                if (ticketType == null)
                {
                    Message("Типът билет не е намерен.");
                    return;
                }

                var ticket = tickets.Create(eventId, ticketType);
                Message($"Билетът е създаден. Код: {ticket.Code}");
            }
            catch (Exception ex)
            {
                Message(ex.Message);
            }
        }

        private void CancelTicket()
        {
            Console.Clear();
            Header("Отмяна на билет");
            var id = ReadInt("Ticket ID: ");
            tickets.Cancel(id);
            Message("Билетът е отменен.");
        }

        private void MarkTicketAsUsed()
        {
            Console.Clear();
            Header("Маркиране като използван");
            var id = ReadInt("Ticket ID: ");
            tickets.MarkAsUsed(id);
            Message("Билетът е маркиран като използван.");
        }

        private void CheckTicketValidity()
        {
            Console.Clear();
            Header("Валидност на билет");
            var id = ReadInt("Ticket ID: ");
            Message(tickets.IsValid(id) ? "Билетът е валиден." : "Билетът не е валиден.");
        }

        private void PrintEvents(IEnumerable<Event> list)
        {
            var items = list.ToList();
            if (items.Count == 0)
            {
                Console.WriteLine("Няма записи.");
                return;
            }

            foreach (var e in items)
            {
                var soldTickets = e.Tickets.Count(t => t.Status == TicketStatus.Sold);
                Console.WriteLine($"#{e.Id} | {e.Name} | {e.EventType} | {e.Date:dd.MM.yyyy HH:mm} | Локация: {(e.Location?.Name ?? e.LocationId.ToString())} | Организатор: {(e.Organizer?.Name ?? e.OrganizerId.ToString())} | {soldTickets}/{e.Capacity}");
            }
        }

        private void PrintLocations(IEnumerable<Location> list)
        {
            var items = list.ToList();
            if (items.Count == 0)
            {
                Console.WriteLine("Няма записи.");
                return;
            }

            foreach (var l in items)
            {
                Console.WriteLine($"#{l.Id} | {l.Name} | {l.Address} | Капацитет: {l.Capacity} | Събития: {l.Events.Count}");
            }
        }

        private void PrintOrganizers(IEnumerable<Organizer> list)
        {
            var items = list.ToList();
            if (items.Count == 0)
            {
                Console.WriteLine("Няма записи.");
                return;
            }

            foreach (var o in items)
            {
                Console.WriteLine($"#{o.Id} | {o.Name} | {o.ContactNumber} | Събития: {o.Events.Count}");
            }
        }

        private void PrintTicketTypes(IEnumerable<TicketType> list)
        {
            var items = list.ToList();
            if (items.Count == 0)
            {
                Console.WriteLine("Няма записи.");
                return;
            }

            foreach (var tt in items)
            {
                Console.WriteLine($"#{tt.Id} | {tt.Name} | Цена: {tt.Price} | Билети: {tt.Tickets.Count}");
            }
        }

        private void PrintTickets(IEnumerable<Ticket> list)
        {
            var items = list.ToList();
            if (items.Count == 0)
            {
                Console.WriteLine("Няма записи.");
                return;
            }

            foreach (var t in items)
            {
                Console.WriteLine($"#{t.Id} | Код: {t.Code} | Статус: {t.Status} | Цена: {t.Price} | EventID: {t.EventId} | TypeID: {t.TicketTypeId}");
            }
        }
    }
}
