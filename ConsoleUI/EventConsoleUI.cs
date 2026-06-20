using EventManagement11.Application.Services;
using EventManagement11.Domain.Entities;
using EventManagement11.Domain.Enums;
using EventManagement11.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

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
                Header("ГЛАВНО МЕНЮ | EVENT MANAGEMENT SYSTEM");
                Console.WriteLine(" 1. Събития");
                Console.WriteLine(" 2. Локации");
                Console.WriteLine(" 3. Организатори");
                Console.WriteLine(" 4. Типове билети");
                Console.WriteLine(" 5. Билети");
                Console.WriteLine(" 0. Изход");
                Console.WriteLine();

                var choice = ReadInt("Избор: ");
                switch (choice)
                {
                    case 1: EventsMenu(); break;
                    case 2: LocationsMenu(); break;
                    case 3: OrganizersMenu(); break;
                    case 4: TicketTypesMenu(); break;
                    case 5: TicketsMenu(); break;
                    case 0: return;
                    default:
                        MessageError("Невалиден избор.");
                        Pause();
                        break;
                }
            }
        }

        private void EventsMenu()
        {
            while (true)
            {
                Console.Clear();
                Header("МЕНЮ | СЪБИТИЯ");
                Console.WriteLine(" 1. Всички");
                Console.WriteLine(" 2. Добавяне");
                Console.WriteLine(" 3. Редактиране");
                Console.WriteLine(" 4. Изтриване");
                Console.WriteLine(" 5. Търсене по име");
                Console.WriteLine(" 6. Филтър по дата");
                Console.WriteLine(" 7. Филтър по тип");
                Console.WriteLine(" 8. Предстоящи");
                Console.WriteLine(" 9. Най-посещавани");
                Console.WriteLine(" 10. Проверка за свободна локация");
                Console.WriteLine(" 11. Проверка за капацитет");
                Console.WriteLine(" 0. Назад");
                Console.WriteLine();

                var choice = ReadInt("Избор: ");
                switch (choice)
                {
                    case 1: PrintEvents(events.GetAll()); Pause(); break;
                    case 2: CreateEvent(); Pause(); break;
                    case 3: EditEvent(); Pause(); break;
                    case 4: DeleteEvent(); Pause(); break;
                    case 5: SearchEvents(); break;
                    case 6: FilterEventsByDate(); break;
                    case 7: FilterEventsByType(); break;
                    case 8: PrintEvents(events.UpcomingEvents()); Pause(); break;
                    case 9: PrintEvents(events.MostVisitedEvents()); Pause(); break;
                    case 10: CheckLocationAvailability(); Pause(); break;
                    case 11: CheckEventCapacity(); Pause(); break;
                    case 0: return;
                    default: MessageError("Невалиден избор."); Pause(); break;
                }
            }
        }

        private void LocationsMenu()
        {
            while (true)
            {
                Console.Clear();
                Header("МЕНЮ | ЛОКАЦИИ");
                Console.WriteLine(" 1. Всички");
                Console.WriteLine(" 2. Добавяне");
                Console.WriteLine(" 3. Редактиране");
                Console.WriteLine(" 4. Изтриване");
                Console.WriteLine(" 5. Справка за заетост");
                Console.WriteLine(" 0. Назад");
                Console.WriteLine();

                var choice = ReadInt("Избор: ");
                switch (choice)
                {
                    case 1: PrintLocations(locations.GetAll()); Pause(); break;
                    case 2: CreateLocation(); Pause(); break;
                    case 3: EditLocation(); Pause(); break;
                    case 4: DeleteLocation(); Pause(); break;
                    case 5: LocationOccupancy(); break;
                    case 0: return;
                    default: MessageError("Невалиден избор."); Pause(); break;
                }
            }
        }

        private void OrganizersMenu()
        {
            while (true)
            {
                Console.Clear();
                Header("МЕНЮ | ОРГАНИЗАТОРИ");
                Console.WriteLine(" 1. Всички");
                Console.WriteLine(" 2. Добавяне");
                Console.WriteLine(" 3. Редактиране");
                Console.WriteLine(" 4. Изтриване");
                Console.WriteLine(" 0. Назад");
                Console.WriteLine();

                var choice = ReadInt("Избор: ");
                switch (choice)
                {
                    case 1: PrintOrganizers(organizers.GetAll()); Pause(); break;
                    case 2: CreateOrganizer(); Pause(); break;
                    case 3: EditOrganizer(); Pause(); break;
                    case 4: DeleteOrganizer(); Pause(); break;
                    case 0: return;
                    default: MessageError("Невалиден избор."); Pause(); break;
                }
            }
        }

        private void TicketTypesMenu()
        {
            while (true)
            {
                Console.Clear();
                Header("МЕНЮ | ТИПОВЕ БИЛЕТИ");
                Console.WriteLine(" 1. Всички");
                Console.WriteLine(" 2. Добавяне");
                Console.WriteLine(" 3. Редактиране на цена");
                Console.WriteLine(" 4. Изтриване");
                Console.WriteLine(" 0. Назад");
                Console.WriteLine();

                var choice = ReadInt("Избор: ");
                switch (choice)
                {
                    case 1: PrintTicketTypes(ticketTypes.GetAll()); Pause(); break;
                    case 2: CreateTicketType(); Pause(); break;
                    case 3: EditTicketType(); Pause(); break;
                    case 4: DeleteTicketType(); Pause(); break;
                    case 0: return;
                    default: MessageError("Невалиден избор."); Pause(); break;
                }
            }
        }

        private void TicketsMenu()
        {
            while (true)
            {
                Console.Clear();
                Header("МЕНЮ | БИЛЕТИ");
                Console.WriteLine(" 1. Всички");
                Console.WriteLine(" 2. Генериране");
                Console.WriteLine(" 3. Отмяна");
                Console.WriteLine(" 4. Маркиране като използван");
                Console.WriteLine(" 5. Проверка за валидност");
                Console.WriteLine(" 0. Назад");
                Console.WriteLine();

                var choice = ReadInt("Избор: ");
                switch (choice)
                {
                    case 1: PrintTickets(tickets.GetAll()); Pause(); break;
                    case 2: CreateTicket(); Pause(); break;
                    case 3: CancelTicket(); Pause(); break;
                    case 4: MarkTicketAsUsed(); Pause(); break;
                    case 5: CheckTicketValidity(); Pause(); break;
                    case 0: return;
                    default: MessageError("Невалиден избор."); Pause(); break;
                }
            }
        }

        private void CreateEvent()
        {
            try
            {
                Console.Clear();
                Header("ДОБАВЯНЕ НА СЪБИТИЕ");

                var name = ReadRequired("Име: ");
                DateTime date;
                int locationId;

                while (true)
                {
                    date = ReadDateTime("Дата и час (DD.MM.YYYY HH:MM): ");

                    var allLocations = locations.GetAll().ToList();
                    if (allLocations.Count == 0)
                    {
                        MessageError("Няма налични локации. Първо създайте локация.");
                        return;
                    }

                    while (true)
                    {
                        Console.WriteLine("\nНалични локации:");
                        PrintLocations(allLocations);
                        locationId = ReadInt("Изберете ID на локация: ");

                        if (locations.GetById(locationId) != null)
                        {
                            break;
                        }

                        MessageError("Локацията не е намерена. Моля, опитайте отново.");
                    }

                    if (events.IsLocationAvailable(locationId, date))
                    {
                        break;
                    }

                    MessageError("Локацията е заета за тази дата. Трябва да изберете друга дата или локация.");
                }

                var location = locations.GetById(locationId);

                int organizerId;
                var allOrganizers = organizers.GetAll().ToList();
                if (allOrganizers.Count == 0)
                {
                    MessageError("Няма налични организатори. Първо създайте организатор.");
                    return;
                }

                while (true)
                {
                    Console.WriteLine("\nНалични организатори:");
                    PrintOrganizers(allOrganizers);
                    organizerId = ReadInt("Изберете ID на организатор: ");

                    if (organizers.GetById(organizerId) != null)
                    {
                        break;
                    }

                    MessageError("Организаторът не е намерен. Моля, опитайте отново.");
                }

                int capacity;
                while (true)
                {
                    capacity = ReadInt("Капацитет: ");

                    if (capacity <= location.Capacity)
                    {
                        break;
                    }

                    MessageError($"Капацитетът на събитието е по-голям от капацитета на локацията (макс. {location.Capacity}). Опитайте отново.");
                }

                string type;
                var allTypes = ticketTypes.GetAll().ToList();
                if (allTypes.Count == 0)
                {
                    MessageError("Няма налични типове билети. Първо създайте тип билет.");
                    return;
                }

                while (true)
                {
                    Console.WriteLine("\nНалични типове събитие:");
                    PrintTicketTypes(allTypes);
                    type = ReadRequired("Тип: ");

                    bool isValidType = false;
                    foreach (var ticketType in allTypes)
                    {
                        if (ticketType.Name.Equals(type, StringComparison.OrdinalIgnoreCase))
                        {
                            isValidType = true;
                            break;
                        }
                    }

                    if (isValidType)
                    {
                        break;
                    }

                    MessageError("Невалиден тип събитие. Моля, въведете точно име от списъка.");
                }

                var entity = new Event(name, date, locationId, organizerId, capacity, type);
                events.Create(entity);

                MessageSuccess($"Събитието е създадено успешно! ID: {entity.Id}");
            }
            catch (Exception ex)
            {
                MessageError($"Грешка: {ex.Message}");
            }
        }

        private void EditEvent()
        {
            try
            {
                Console.Clear();
                Header("РЕДАКТИРАНЕ НА СЪБИТИЕ");

                var allEvents = events.GetAll().ToList();
                if (allEvents.Count == 0)
                {
                    MessageError("Няма налични събития.");
                    return;
                }

                int id;
                Event entity;
                while (true)
                {
                    Console.WriteLine("\nНалични събития:");
                    PrintEvents(allEvents);
                    id = ReadInt("Въведете ID на събитие: ");
                    entity = events.GetById(id);

                    if (entity != null)
                    {
                        break;
                    }
                    MessageError("Събитието не е намерено. Моля, опитайте отново.");
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
                    MessageError("Локацията на събитието не е намерена.");
                    return;
                }

                if (!string.IsNullOrWhiteSpace(dateText) && !events.IsLocationAvailable(entity.LocationId, newDate, entity.Id))
                {
                    MessageError("Локацията е заета за тази дата.");
                    return;
                }

                if (newCapacity > location.Capacity)
                {
                    MessageError($"Капацитетът е по-голям от капацитета на локацията (макс. {location.Capacity}).");
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
                MessageSuccess("Събитието е обновено успешно.");
            }
            catch (Exception ex)
            {
                MessageError(ex.Message);
            }
        }

        private void DeleteEvent()
        {
            Console.Clear();
            Header("ИЗТРИВАНЕ НА СЪБИТИЕ");

            var allEvents = events.GetAll().ToList();
            if (allEvents.Count == 0)
            {
                MessageError("Няма налични събития.");
                return;
            }

            int id;
            while (true)
            {
                Console.WriteLine("\nНалични събития:");
                PrintEvents(allEvents);
                id = ReadInt("Въведете ID на събитие: ");

                if (events.GetById(id) != null)
                {
                    break;
                }
                MessageError("Събитието не е намерено. Моля, опитайте отново.");
            }

            try
            {
                events.Delete(id);
                MessageSuccess("Събитието е изтрито.");
            }
            catch (Exception ex)
            {
                MessageError(ex.Message);
            }
        }

        private void SearchEvents()
        {
            Console.Clear();
            Header("ТЪРСЕНЕ НА СЪБИТИЕ");
            var text = ReadRequired("Въведете име за търсене: ");
            PrintEvents(events.SearchByName(text));
            Pause();
        }

        private void FilterEventsByDate()
        {
            Console.Clear();
            Header("ФИЛТЪР ПО ДАТА");
            var date = ReadDateTime("Дата (dd.MM.yyyy HH:mm): ");
            PrintEvents(events.FilterByDate(date));
            Pause();
        }

        private void FilterEventsByType()
        {
            Console.Clear();
            Header("ФИЛТЪР ПО ТИП");
            var type = ReadRequired("Тип: ");
            PrintEvents(events.FilterByType(type));
            Pause();
        }

        private void CheckLocationAvailability()
        {
            Console.Clear();
            Header("ПРОВЕРКА НА ЛОКАЦИЯ");

            var allLocations = locations.GetAll().ToList();
            if (allLocations.Count == 0)
            {
                MessageError("Няма налични локации.");
                return;
            }

            int locationId;
            while (true)
            {
                Console.WriteLine("\nНалични локации:");
                PrintLocations(allLocations);
                locationId = ReadInt("Въведете ID на локация: ");

                if (locations.GetById(locationId) != null)
                {
                    break;
                }
                MessageError("Локацията не е намерена. Моля, опитайте отново.");
            }

            var date = ReadDateTime("Дата и час (dd.MM.yyyy HH:mm): ");
            var available = events.IsLocationAvailable(locationId, date);

            if (available)
                MessageSuccess("Локацията е свободна.");
            else
                MessageError("Локацията е заета за този час.");
        }

        private void CheckEventCapacity()
        {
            Console.Clear();
            Header("ПРОВЕРКА ЗА КАПАЦИТЕТ");

            var allEvents = events.GetAll().ToList();
            if (allEvents.Count == 0)
            {
                MessageError("Няма налични събития.");
                return;
            }

            int eventId;
            while (true)
            {
                Console.WriteLine("\nНалични събития:");
                PrintEvents(allEvents);
                eventId = ReadInt("Въведете ID на събитие: ");

                if (events.GetById(eventId) != null)
                {
                    break;
                }
                MessageError("Събитието не е намерено. Моля, опитайте отново.");
            }

            var available = events.HasCapacity(eventId);
            if (available)
                MessageSuccess("Има свободни места за събитието.");
            else
                MessageError("Капацитетът за това събитие е запълнен.");
        }

        private void CreateLocation()
        {
            try
            {
                Console.Clear();
                Header("ДОБАВЯНЕ НА ЛОКАЦИЯ");

                var name = ReadRequired("Име: ");
                var address = ReadRequired("Адрес: ");
                var capacity = ReadInt("Капацитет: ");

                var entity = new Location(name, address, capacity);
                locations.Create(entity);

                MessageSuccess($"Локацията е създадена успешно! ID: {entity.Id}");
            }
            catch (Exception ex)
            {
                MessageError(ex.Message);
            }
        }

        private void EditLocation()
        {
            try
            {
                Console.Clear();
                Header("РЕДАКТИРАНЕ НА ЛОКАЦИЯ");

                var allLocations = locations.GetAll().ToList();
                if (allLocations.Count == 0)
                {
                    MessageError("Няма налични локации.");
                    return;
                }

                int id;
                Location entity;
                while (true)
                {
                    Console.WriteLine("\nНалични локации:");
                    PrintLocations(allLocations);
                    id = ReadInt("Въведете ID на локация: ");
                    entity = locations.GetById(id);

                    if (entity != null)
                    {
                        break;
                    }
                    MessageError("Локацията не е намерена. Моля, опитайте отново.");
                }

                Console.WriteLine($"Текущо име: {entity.Name}");
                var name = ReadRequired("Ново име: ");

                Console.WriteLine($"Текущ адрес: {entity.Address}");
                var address = ReadRequired("Нов адрес: ");

                Console.WriteLine($"Текущ капацитет: {entity.Capacity}");
                var capacity = ReadInt("Нов капацитет: ");

                entity.Edit(name, address, capacity);
                locations.Edit(entity);

                MessageSuccess("Локацията е обновена.");
            }
            catch (Exception ex)
            {
                MessageError(ex.Message);
            }
        }

        private void DeleteLocation()
        {
            Console.Clear();
            Header("ИЗТРИВАНЕ НА ЛОКАЦИЯ");

            var allLocations = locations.GetAll().ToList();
            if (allLocations.Count == 0)
            {
                MessageError("Няма налични локации.");
                return;
            }

            int id;
            while (true)
            {
                Console.WriteLine("\nНалични локации:");
                PrintLocations(allLocations);
                id = ReadInt("Въведете ID на локация: ");

                if (locations.GetById(id) != null)
                {
                    break;
                }
                MessageError("Локацията не е намерена. Моля, опитайте отново.");
            }

            try
            {
                locations.Delete(id);
                MessageSuccess("Локацията е изтрита.");
            }
            catch (Exception ex)
            {
                MessageError(ex.Message);
            }
        }

        private void LocationOccupancy()
        {
            Console.Clear();
            Header("ЗАЕТОСТ НА ЛОКАЦИЯ");

            var allLocations = locations.GetAll().ToList();
            if (allLocations.Count == 0)
            {
                MessageError("Няма налични локации.");
                Pause();
                return;
            }

            int id;
            Location entity;
            while (true)
            {
                Console.WriteLine("\nНалични локации:");
                PrintLocations(allLocations);
                id = ReadInt("Въведете ID на локация: ");
                entity = locations.GetById(id);

                if (entity != null)
                {
                    break;
                }
                MessageError("Локацията не е намерена. Моля, опитайте отново.");
            }

            Console.WriteLine($"Локация: {entity.Name}");
            Console.WriteLine($"Брой събития: {locations.GetOccupancy(id)}\n");

            PrintEvents(entity.Events.OrderBy(e => e.Date));
            Pause();
        }

        private void CreateOrganizer()
        {
            try
            {
                Console.Clear();
                Header("ДОБАВЯНЕ НА ОРГАНИЗАТОР");

                var name = ReadRequired("Име: ");
                var phone = ReadRequired("Телефон: ");

                var entity = new Organizer(name, phone);
                organizers.Create(entity);

                MessageSuccess($"Организаторът е създаден успешно! ID: {entity.Id}");
            }
            catch (Exception ex)
            {
                MessageError(ex.Message);
            }
        }

        private void EditOrganizer()
        {
            try
            {
                Console.Clear();
                Header("РЕДАКТИРАНЕ НА ОРГАНИЗАТОР");

                var allOrganizers = organizers.GetAll().ToList();
                if (allOrganizers.Count == 0)
                {
                    MessageError("Няма налични организатори.");
                    return;
                }

                int id;
                Organizer entity;
                while (true)
                {
                    Console.WriteLine("\nНалични организатори:");
                    PrintOrganizers(allOrganizers);
                    id = ReadInt("Въведете ID на организатор: ");
                    entity = organizers.GetById(id);

                    if (entity != null)
                    {
                        break;
                    }
                    MessageError("Организаторът не е намерен. Моля, опитайте отново.");
                }

                Console.WriteLine($"Текущо име: {entity.Name}");
                var name = ReadRequired("Ново име: ");

                Console.WriteLine($"Текущ телефон: {entity.ContactNumber}");
                var phone = ReadRequired("Нов телефон: ");

                entity.Edit(name, phone);
                organizers.Edit(entity);

                MessageSuccess("Организаторът е обновен.");
            }
            catch (Exception ex)
            {
                MessageError(ex.Message);
            }
        }

        private void DeleteOrganizer()
        {
            Console.Clear();
            Header("ИЗТРИВАНЕ НА ОРГАНИЗАТОР");

            var allOrganizers = organizers.GetAll().ToList();
            if (allOrganizers.Count == 0)
            {
                MessageError("Няма налични организатори.");
                return;
            }

            int id;
            while (true)
            {
                Console.WriteLine("\nНалични организатори:");
                PrintOrganizers(allOrganizers);
                id = ReadInt("Въведете ID на организатор: ");

                if (organizers.GetById(id) != null)
                {
                    break;
                }
                MessageError("Организаторът не е намерен. Моля, опитайте отново.");
            }

            try
            {
                organizers.Delete(id);
                MessageSuccess("Организаторът е изтрит.");
            }
            catch (Exception ex)
            {
                MessageError(ex.Message);
            }
        }

        private void CreateTicketType()
        {
            try
            {
                Console.Clear();
                Header("ДОБАВЯНЕ НА ТИП БИЛЕТ");

                var name = ReadRequired("Име: ");
                var price = ReadDecimal("Цена: ");

                var entity = new TicketType(name, new Money(price));
                ticketTypes.Create(entity);

                MessageSuccess($"Типът е създаден успешно! ID: {entity.Id}");
            }
            catch (Exception ex)
            {
                MessageError(ex.Message);
            }
        }

        private void EditTicketType()
        {
            try
            {
                Console.Clear();
                Header("РЕДАКТИРАНЕ НА ТИП БИЛЕТ");

                var allTypes = ticketTypes.GetAll().ToList();
                if (allTypes.Count == 0)
                {
                    MessageError("Няма налични типове билети.");
                    return;
                }

                int id;
                TicketType entity;
                while (true)
                {
                    Console.WriteLine("\nНалични типове билети:");
                    PrintTicketTypes(allTypes);
                    id = ReadInt("Въведете ID на тип билет: ");
                    entity = ticketTypes.GetById(id);

                    if (entity != null)
                    {
                        break;
                    }
                    MessageError("Типът билет не е намерен. Моля, опитайте отново.");
                }

                Console.WriteLine($"Текуща цена: {entity.Price}");
                var price = ReadDecimal("Нова цена: ");

                entity.ChangePrice(new Money(price));
                ticketTypes.Edit(entity);

                MessageSuccess("Цената е обновена.");
            }
            catch (Exception ex)
            {
                MessageError(ex.Message);
            }
        }

        private void DeleteTicketType()
        {
            Console.Clear();
            Header("ИЗТРИВАНЕ НА ТИП БИЛЕТ");

            var allTypes = ticketTypes.GetAll().ToList();
            if (allTypes.Count == 0)
            {
                MessageError("Няма налични типове билети.");
                return;
            }

            int id;
            while (true)
            {
                Console.WriteLine("\nНалични типове билети:");
                PrintTicketTypes(allTypes);
                id = ReadInt("Въведете ID на тип билет: ");

                if (ticketTypes.GetById(id) != null)
                {
                    break;
                }
                MessageError("Типът билет не е намерен. Моля, опитайте отново.");
            }

            try
            {
                ticketTypes.Delete(id);
                MessageSuccess("Типът билет е изтрит.");
            }
            catch (Exception ex)
            {
                MessageError(ex.Message);
            }
        }

        private void CreateTicket()
        {
            try
            {
                Console.Clear();
                Header("ГЕНЕРИРАНЕ НА БИЛЕТ");

                var allEvents = events.GetAll().ToList();
                if (allEvents.Count == 0)
                {
                    MessageError("Няма налични събития.");
                    return;
                }

                int eventId;
                Event eventEntity;
                while (true)
                {
                    Console.WriteLine("\nНалични събития:");
                    PrintEvents(allEvents);
                    eventId = ReadInt("Въведете ID на събитие: ");
                    eventEntity = events.GetById(eventId);

                    if (eventEntity != null) break;
                    MessageError("Събитието не е намерено. Моля, опитайте отново.");
                }

                if (!events.HasCapacity(eventId))
                {
                    MessageError("Няма свободен капацитет за това събитие.");
                    return;
                }

                var allTypes = ticketTypes.GetAll().ToList();
                if (allTypes.Count == 0)
                {
                    MessageError("Няма налични типове билети.");
                    return;
                }

                int ticketTypeId;
                TicketType ticketType;
                while (true)
                {
                    Console.WriteLine("\nНалични типове билети:");
                    PrintTicketTypes(allTypes);
                    ticketTypeId = ReadInt("Въведете ID на тип билет: ");
                    ticketType = ticketTypes.GetById(ticketTypeId);

                    if (ticketType != null) break;
                    MessageError("Типът билет не е намерен. Моля, опитайте отново.");
                }

                var ticket = tickets.Create(eventId, ticketType);
                MessageSuccess($"Билетът е създаден успешно! Код: {ticket.Code}");
            }
            catch (Exception ex)
            {
                MessageError(ex.Message);
            }
        }

        private void CancelTicket()
        {
            Console.Clear();
            Header("ОТМЯНА НА БИЛЕТ");

            var allTickets = tickets.GetAll().ToList();
            if (allTickets.Count == 0)
            {
                MessageError("Няма налични билети.");
                return;
            }

            int id;
            while (true)
            {
                Console.WriteLine("\nНалични билети:");
                PrintTickets(allTickets);
                id = ReadInt("Въведете ID на билет: ");

                if (allTickets.Any(t => t.Id == id))
                {
                    break;
                }
                MessageError("Билетът не е намерен. Моля, опитайте отново.");
            }

            try
            {
                tickets.Cancel(id);
                MessageSuccess("Билетът е отменен.");
            }
            catch (Exception ex)
            {
                MessageError(ex.Message);
            }
        }

        private void MarkTicketAsUsed()
        {
            Console.Clear();
            Header("МАРКИРАНЕ НА БИЛЕТ КАТО ИЗПОЛЗВАН");

            var allTickets = tickets.GetAll().ToList();
            if (allTickets.Count == 0)
            {
                MessageError("Няма налични билети.");
                return;
            }

            int id;
            while (true)
            {
                Console.WriteLine("\nНалични билети:");
                PrintTickets(allTickets);
                id = ReadInt("Въведете ID на билет: ");

                if (allTickets.Any(t => t.Id == id))
                {
                    break;
                }
                MessageError("Билетът не е намерен. Моля, опитайте отново.");
            }

            try
            {
                tickets.MarkAsUsed(id);
                MessageSuccess("Билетът е маркиран като използван.");
            }
            catch (Exception ex)
            {
                MessageError(ex.Message);
            }
        }

        private void CheckTicketValidity()
        {
            Console.Clear();
            Header("ПРОВЕРКА ЗА ВАЛИДНОСТ НА БИЛЕТ");

            var allTickets = tickets.GetAll().ToList();
            if (allTickets.Count == 0)
            {
                MessageError("Няма налични билети.");
                return;
            }

            int id;
            while (true)
            {
                Console.WriteLine("\nНалични билети:");
                PrintTickets(allTickets);
                id = ReadInt("Въведете ID на билет: ");

                if (allTickets.Any(t => t.Id == id))
                {
                    break;
                }
                MessageError("Билетът не е намерен. Моля, опитайте отново.");
            }

            if (tickets.IsValid(id))
                MessageSuccess("Билетът е ВАЛИДЕН.");
            else
                MessageError("Билетът е НЕВАЛИДЕН.");
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

        private static void Header(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=================================");
            Console.WriteLine(text);
            Console.WriteLine("=================================");
            Console.ResetColor();
            Console.WriteLine();
        }

        private static void MessageSuccess(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        private static void MessageError(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        private static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Натисни Enter...");
            Console.ReadLine();
        }

        private static string ReadRequired(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var value = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(value))
                    return value.Trim();

                MessageError("Полето е задължително.");
            }
        }

        private static string ReadOptional(string prompt)
        {
            Console.Write(prompt);
            var value = Console.ReadLine();
            return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
        }

        private static int ReadInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var value = Console.ReadLine();
                if (int.TryParse(value, out var result))
                    return result;

                MessageError("Въведи валидно цяло число.");
            }
        }

        private static decimal ReadDecimal(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var value = Console.ReadLine();
                if (decimal.TryParse(value, NumberStyles.Number, CultureInfo.CurrentCulture, out var result) ||
                    decimal.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out result))
                    return result;

                MessageError("Въведи валидна сума.");
            }
        }

        private static DateTime ReadDateTime(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var value = Console.ReadLine();

                if (DateTime.TryParseExact(value, new[] { "dd.MM.yyyy HH:mm", "dd.MM.yyyy" }, CultureInfo.CurrentCulture, DateTimeStyles.None, out var result) ||
                    DateTime.TryParseExact(value, new[] { "dd.MM.yyyy HH:mm", "dd.MM.yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                    return result;

                MessageError("Въведи валидна дата. Пример: 20.06.2026 18:30");
            }
        }

        private static DateTime ReadDateTimeFromInput(string input)
        {
            if (DateTime.TryParseExact(input, new[] { "dd.MM.yyyy HH:mm", "dd.MM.yyyy" }, CultureInfo.CurrentCulture, DateTimeStyles.None, out var result) ||
                DateTime.TryParseExact(input, new[] { "dd.MM.yyyy HH:mm", "dd.MM.yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                return result;

            throw new FormatException("Невалидна дата.");
        }

        private static int ParseInt(string input)
        {
            return int.Parse(input, CultureInfo.InvariantCulture);
        }
    }
}