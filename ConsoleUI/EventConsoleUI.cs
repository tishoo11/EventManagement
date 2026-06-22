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

                try
                {
                    int totalEvents = events.GetAll().Count();
                    int totalSoldTickets = tickets.GetAll().Count(t => t.Status == TicketStatus.Sold);
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"  [СТАТИСТИКА] Активни събития: {totalEvents} | Общо продадени билети: {totalSoldTickets}");
                    Console.WriteLine("  " + new string('-', 60));
                    Console.ResetColor();
                }
                catch { }

                Console.WriteLine("  1. Събития");
                Console.WriteLine("  2. Локации");
                Console.WriteLine("  3. Организатори");
                Console.WriteLine("  4. Типове билети");
                Console.WriteLine("  5. Билети");
                Console.WriteLine("  0. Изход");
                Console.WriteLine();

                var choice = ReadMenuChoice("Избор: ");
                switch (choice)
                {
                    case 1: EventsMenu(); break;
                    case 2: LocationsMenu(); break;
                    case 3: OrganizersMenu(); break;
                    case 4: TicketTypesMenu(); break;
                    case 5: TicketsMenu(); break;
                    case 0: return;
                    default: MessageError("Невалиден избор."); Pause(); break;
                }
            }
        }

        private void EventsMenu()
        {
            while (true)
            {
                Console.Clear();
                Header("МЕНЮ | СЪБИТИЯ");
                Console.WriteLine("  1. Всички събития");
                Console.WriteLine("  2. Добавяне");
                Console.WriteLine("  3. Редактиране");
                Console.WriteLine("  4. Изтриване");
                Console.WriteLine("  5. Търсене по име");
                Console.WriteLine("  6. Филтър по дата");
                Console.WriteLine("  7. Филтър по тип");
                Console.WriteLine("  8. Предстоящи");
                Console.WriteLine("  9. Най-посещавани");
                Console.WriteLine(" 10. Проверка за свободна локация");
                Console.WriteLine(" 11. Проверка за капацитет");
                Console.WriteLine("  0. Назад");
                Console.WriteLine();

                var choice = ReadMenuChoice("Избор: ");
                switch (choice)
                {
                    case 1: PrintEvents(events.GetAll()); Pause(); break;
                    case 2: CreateEvent(); break;
                    case 3: EditEvent(); break;
                    case 4: DeleteEvent(); break;
                    case 5: SearchEvents(); break;
                    case 6: FilterEventsByDate(); break;
                    case 7: FilterEventsByType(); break;
                    case 8: PrintEvents(events.UpcomingEvents()); Pause(); break;
                    case 9: PrintEvents(events.MostVisitedEvents()); Pause(); break;
                    case 10: CheckLocationAvailability(); break;
                    case 11: CheckEventCapacity(); break;
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
                Console.WriteLine("  1. Всички локации");
                Console.WriteLine("  2. Добавяне");
                Console.WriteLine("  3. Редактиране");
                Console.WriteLine("  4. Изтриване");
                Console.WriteLine("  5. Справка за заетост");
                Console.WriteLine("  0. Назад");
                Console.WriteLine();

                var choice = ReadMenuChoice("Избор: ");
                switch (choice)
                {
                    case 1: PrintLocations(locations.GetAll()); Pause(); break;
                    case 2: CreateLocation(); break;
                    case 3: EditLocation(); break;
                    case 4: DeleteLocation(); break;
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
                Console.WriteLine("  1. Всички организатори");
                Console.WriteLine("  2. Добавяне");
                Console.WriteLine("  3. Редактиране");
                Console.WriteLine("  4. Изтриване");
                Console.WriteLine("  0. Назад");
                Console.WriteLine();

                var choice = ReadMenuChoice("Избор: ");
                switch (choice)
                {
                    case 1: PrintOrganizers(organizers.GetAll()); Pause(); break;
                    case 2: CreateOrganizer(); break;
                    case 3: EditOrganizer(); break;
                    case 4: DeleteOrganizer(); break;
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
                Console.WriteLine("  1. Всички типове");
                Console.WriteLine("  2. Добавяне");
                Console.WriteLine("  3. Редактиране на цена");
                Console.WriteLine("  4. Изтриване");
                Console.WriteLine("  0. Назад");
                Console.WriteLine();

                var choice = ReadMenuChoice("Избор: ");
                switch (choice)
                {
                    case 1: PrintTicketTypes(ticketTypes.GetAll()); Pause(); break;
                    case 2: CreateTicketType(); break;
                    case 3: EditTicketType(); break;
                    case 4: DeleteTicketType(); break;
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
                Header("ПАСАЖ И КАСА | УПРАВЛЕНИЕ НА БИЛЕТИ");
                Console.WriteLine("  1. Списък на всички издадени билети");
                Console.WriteLine("  2. [КАСА] Продажба и издаване на билет");
                Console.WriteLine("  3. [ОТМЯНА] Анулиране/Връщане на билет");
                Console.WriteLine("  4. [ВХОД] Сканиране и маркиране като използван");
                Console.WriteLine("  5. Проверка на валидност на код");
                Console.WriteLine("  0. Назад");
                Console.WriteLine();

                var choice = ReadMenuChoice("Избор: ");
                switch (choice)
                {
                    case 1: PrintTickets(tickets.GetAll()); Pause(); break;
                    case 2: CreateTicket(); break;
                    case 3: CancelTicket(); break;
                    case 4: MarkTicketAsUsed(); break;
                    case 5: CheckTicketValidity(); break;
                    case 0: return;
                    default: MessageError("Невалиден избор."); Pause(); break;
                }
            }
        }

        private void CreateEvent()
        {
            Console.Clear();
            Header("ДОБАВЯНЕ НА СЪБИТИЕ");
            Console.WriteLine("Съвет: Въведи '0' по всяко време за отказ и връщане назад.\n");

            var name = ReadRequiredText("Име: ");
            if (name == null) return;

            var date = ReadDateTimeInput("Дата и час (DD.MM.YYYY HH:MM): ");
            if (date == null) return;

            var allLocations = locations.GetAll().ToList();
            if (!allLocations.Any()) { MessageError("Няма налични локации. Първо създайте локация."); Pause(); return; }

            Console.WriteLine("\n--- Налични локации ---");
            PrintLocations(allLocations);
            int? locationId;
            while (true)
            {
                locationId = ReadId("Изберете ID на локация: ");
                if (locationId == null) return;

                if (locations.GetById(locationId.Value) == null)
                {
                    MessageError("Локацията не е намерена.");
                    continue;
                }
                if (!events.IsLocationAvailable(locationId.Value, date.Value))
                {
                    MessageError("Локацията е заета за тази дата. Изберете друга.");
                    continue;
                }
                break;
            }

            var allOrganizers = organizers.GetAll().ToList();
            if (!allOrganizers.Any()) { MessageError("Няма налични организатори."); Pause(); return; }

            Console.WriteLine("\n--- Налични организатори ---");
            PrintOrganizers(allOrganizers);
            int? organizerId;
            while (true)
            {
                organizerId = ReadId("Изберете ID на организатор: ");
                if (organizerId == null) return;

                if (organizers.GetById(organizerId.Value) != null) break;
                MessageError("Организаторът не е намерен.");
            }

            var loc = locations.GetById(locationId.Value);
            int? capacity;
            while (true)
            {
                capacity = ReadId($"Капацитет (Макс: {loc.Capacity}): ");
                if (capacity == null) return;

                if (capacity.Value <= loc.Capacity) break;
                MessageError($"Капацитетът надвишава лимита на локацията ({loc.Capacity}).");
            }

            var allTypes = ticketTypes.GetAll().ToList();
            if (!allTypes.Any()) { MessageError("Няма налични типове билети."); Pause(); return; }

            Console.WriteLine("\n--- Налични типове събитие ---");
            PrintTicketTypes(allTypes);
            string type;
            while (true)
            {
                type = ReadRequiredText("Тип събитие (въведи името точно): ");
                if (type == null) return;

                if (allTypes.Any(t => t.Name.Equals(type, StringComparison.OrdinalIgnoreCase))) break;
                MessageError("Невалиден тип събитие. Моля, въведете точно име от списъка.");
            }

            try
            {
                var entity = new Event(name, date.Value, locationId.Value, organizerId.Value, capacity.Value, type);
                events.Create(entity);
                MessageSuccess($"Събитието '{name}' е създадено успешно! ID: {entity.Id}");
            }
            catch (Exception ex) { MessageError($"Грешка: {ex.Message}"); Pause(); }
        }

        private void EditEvent()
        {
            Console.Clear();
            Header("РЕДАКТИРАНЕ НА СЪБИТИЕ");
            var allEvents = events.GetAll().ToList();
            if (!allEvents.Any()) { MessageError("Няма налични събития."); Pause(); return; }

            PrintEvents(allEvents);
            int? id;
            Event entity;
            while (true)
            {
                id = ReadId("\nВъведете ID на събитие за редакция (0 за отказ): ");
                if (id == null) return;

                entity = events.GetById(id.Value);
                if (entity != null) break;
                MessageError("Събитието не е намерено.");
            }

            Console.WriteLine("\n--- Въведи нови данни (Enter за запазване на текущите, 0 за отказ) ---");
            var name = ReadOptionalText($"Ново име [{entity.Name}]: ");
            if (name == "0") return;

            var dateText = ReadOptionalText($"Нова дата [{entity.Date:dd.MM.yyyy HH:mm}]: ");
            if (dateText == "0") return;

            var type = ReadOptionalText($"Нов тип [{entity.EventType}]: ");
            if (type == "0") return;
            var capacityText = ReadOptionalText($"Нов капацитет [{entity.Capacity}]: ");
            if (capacityText == "0") return;
            try
            {
                var newDate = string.IsNullOrWhiteSpace(dateText) ? entity.Date : ReadDateTimeFromInput(dateText);
                var newCapacity = string.IsNullOrWhiteSpace(capacityText) ? entity.Capacity : ParseInt(capacityText);
                var soldTickets = entity.Tickets.Count(t => t.Status == TicketStatus.Sold);
                var location = locations.GetById(entity.LocationId) ?? entity.Location;

                if (!string.IsNullOrWhiteSpace(dateText) && !events.IsLocationAvailable(entity.LocationId, newDate, entity.Id))
                {
                    MessageError("Локацията е заета за тази нова дата.");
                    Pause(); return;
                }

                if (newCapacity > location.Capacity)
                {
                    MessageError($"Капацитетът е по-голям от капацитета на локацията (макс. {location.Capacity}).");
                    Pause(); return;
                }

                if (!string.IsNullOrWhiteSpace(name)) entity.EditName(name);
                if (!string.IsNullOrWhiteSpace(dateText)) entity.Reschedule(newDate);
                if (!string.IsNullOrWhiteSpace(type)) entity.ChangeType(type);
                if (!string.IsNullOrWhiteSpace(capacityText)) entity.ChangeCapacity(newCapacity, soldTickets, location.Capacity);

                events.Edit(entity);
                MessageSuccess("Събитието е обновено успешно.");
            }
            catch (Exception ex) { MessageError(ex.Message); Pause(); }
        }

        private void DeleteEvent()
        {
            Console.Clear();
            Header("ИЗТРИВАНЕ НА СЪБИТИЕ");
            var allEvents = events.GetAll().ToList();
            if (!allEvents.Any()) { MessageError("Няма налични събития."); Pause(); return; }

            PrintEvents(allEvents);
            int? id;
            while (true)
            {
                id = ReadId("\nВъведете ID за изтриване (0 за отказ): ");
                if (id == null) return;

                if (events.GetById(id.Value) != null) break;
                MessageError("Събитието не е намерено.");
            }

            try
            {
                events.Delete(id.Value);
                MessageSuccess("Събитието е изтрито успешно.");
            }
            catch (Exception ex) { MessageError(ex.Message); Pause(); }
        }

        private void SearchEvents()
        {
            Console.Clear();
            Header("ТЪРСЕНЕ НА СЪБИТИЕ");
            var text = ReadRequiredText("Въведете име за търсене (0 за отказ): ");
            if (text == null) return;

            Console.WriteLine();
            PrintEvents(events.SearchByName(text));
            Pause();
        }

        private void FilterEventsByDate()
        {
            Console.Clear();
            Header("ФИЛТЪР ПО ДАТА");
            var date = ReadDateTimeInput("Дата (dd.MM.yyyy HH:mm) (0 за отказ): ");
            if (date == null) return;

            Console.WriteLine();
            PrintEvents(events.FilterByDate(date.Value));
            Pause();
        }

        private void FilterEventsByType()
        {
            Console.Clear();
            Header("ФИЛТЪР ПО ТИП");
            var type = ReadRequiredText("Тип (0 за отказ): ");
            if (type == null) return;

            Console.WriteLine();
            PrintEvents(events.FilterByType(type));
            Pause();
        }

        private void CheckLocationAvailability()
        {
            Console.Clear();
            Header("ПРОВЕРКА НА ЛОКАЦИЯ");
            var allLocs = locations.GetAll().ToList();
            if (!allLocs.Any()) { MessageError("Няма локации."); Pause(); return; }

            PrintLocations(allLocs);
            var locId = ReadId("\nID на локация (0 за отказ): ");
            if (locId == null) return;
            if (locations.GetById(locId.Value) == null) { MessageError("Локация не е намерена."); Pause(); return; }

            var date = ReadDateTimeInput("Дата и час (dd.MM.yyyy HH:mm) (0 за отказ): ");
            if (date == null) return;

            if (events.IsLocationAvailable(locId.Value, date.Value)) MessageSuccess("Локацията е СВОБОДНА.");
            else MessageError("Локацията е ЗАЕТА за този час.");
            Pause();
        }

        private void CheckEventCapacity()
        {
            Console.Clear();
            Header("ПРОВЕРКА ЗА КАПАЦИТЕТ");
            var allEvs = events.GetAll().ToList();
            if (!allEvs.Any()) { MessageError("Няма събития."); Pause(); return; }

            PrintEvents(allEvs);
            var evId = ReadId("\nID на събитие (0 за отказ): ");
            if (evId == null) return;
            if (events.GetById(evId.Value) == null) { MessageError("Събитие не е намерено."); Pause(); return; }

            if (events.HasCapacity(evId.Value)) MessageSuccess("Има свободни места за събитието.");
            else MessageError("Капацитетът за това събитие е запълнен.");
            Pause();
        }

        private void CreateLocation()
        {
            Console.Clear();
            Header("ДОБАВЯНЕ НА ЛОКАЦИЯ");

            var name = ReadRequiredText("Име (0 за отказ): ");
            if (name == null) return;
            var address = ReadRequiredText("Адрес (0 за отказ): ");
            if (address == null) return;
            var capacity = ReadId("Капацетет (0 за отказ): ");
            if (capacity == null) return;
            try
            {
                var entity = new Location(name, address, capacity.Value);
                locations.Create(entity);
                MessageSuccess($"Локацията е създадена успешно! ID: {entity.Id}");
            }
            catch (Exception ex) { MessageError(ex.Message); Pause(); }
        }

        private void EditLocation()
        {
            Console.Clear();
            Header("РЕДАКТИРАНЕ НА ЛОКАЦИЯ");
            var allLocs = locations.GetAll().ToList();
            if (!allLocs.Any()) { MessageError("Няма локации."); Pause(); return; }

            PrintLocations(allLocs);
            int? id;
            Location entity;
            while (true)
            {
                id = ReadId("\nID на локация (0 за отказ): ");
                if (id == null) return;
                entity = locations.GetById(id.Value);
                if (entity != null) break;
                MessageError("Локацията не е намерена.");
            }

            Console.WriteLine($"\nТекущо име: {entity.Name}");
            var name = ReadRequiredText("Ново име (0 за отказ): ");
            if (name == null) return;

            Console.WriteLine($"Текущ адрес: {entity.Address}");
            var address = ReadRequiredText("Нов адрес (0 за отказ): ");
            if (address == null) return;

            Console.WriteLine($"Текущ капацитет: {entity.Capacity}");
            var capacity = ReadId("Нов капацитет (0 за отказ): ");
            if (capacity == null) return;
            try
            {
                entity.Edit(name, address, capacity.Value);
                locations.Edit(entity);
                MessageSuccess("Локацията е обновена.");
            }
            catch (Exception ex) { MessageError(ex.Message); Pause(); }
        }

        private void DeleteLocation()
        {
            Console.Clear();
            Header("ИЗТРИВАНЕ НА ЛОКАЦИЯ");
            var allLocs = locations.GetAll().ToList();
            if (!allLocs.Any()) { MessageError("Няма локации."); Pause(); return; }

            PrintLocations(allLocs);
            int? id;
            while (true)
            {
                id = ReadId("\nID на локация (0 за отказ): ");
                if (id == null) return;
                if (locations.GetById(id.Value) != null) break;
                MessageError("Не е намерена.");
            }

            try { locations.Delete(id.Value); MessageSuccess("Локацията е изтрита."); }
            catch (Exception ex) { MessageError(ex.Message); Pause(); }
        }

        private void LocationOccupancy()
        {
            Console.Clear();
            Header("ЗАЕТОСТ НА ЛОКАЦИЯ");
            var allLocs = locations.GetAll().ToList();
            if (!allLocs.Any()) { MessageError("Няма локации."); Pause(); return; }

            PrintLocations(allLocs);
            var id = ReadId("\nID на локация (0 за отказ): ");
            if (id == null) return;

            var entity = locations.GetById(id.Value);
            if (entity == null) { MessageError("Не е намерена."); Pause(); return; }

            Console.WriteLine($"\nЛокация: {entity.Name} | Капацитет: {entity.Capacity}");
            Console.WriteLine($"Общо организирани събития: {locations.GetOccupancy(id.Value)}\n");
            PrintEvents(entity.Events.OrderBy(e => e.Date));
            Pause();
        }

        private void CreateOrganizer()
        {
            Console.Clear();
            Header("ДОБАВЯНЕ НА ОРГАНИЗАТОР");
            var name = ReadRequiredText("Име (0 за отказ): ");
            if (name == null) return;
            var phone = ReadRequiredText("Телефон (0 за отказ): ");
            if (phone == null) return;
            try
            {
                var entity = new Organizer(name, phone);
                organizers.Create(entity);
                MessageSuccess($"Организаторът е създаден успешно! ID: {entity.Id}");
            }
            catch (Exception ex) { MessageError(ex.Message); Pause(); }
        }

        private void EditOrganizer()
        {
            Console.Clear();
            Header("РЕДАКТИРАНЕ НА ОРГАНИЗАТОР");
            var allOrg = organizers.GetAll().ToList();
            if (!allOrg.Any()) { MessageError("Няма организатори."); Pause(); return; }

            PrintOrganizers(allOrg);
            int? id;
            Organizer entity;
            while (true)
            {
                id = ReadId("\nID на организатор (0 за отказ): ");
                if (id == null) return;
                entity = organizers.GetById(id.Value);
                if (entity != null) break;
                MessageError("Организаторът не е намерен.");
            }

            Console.WriteLine($"\nТекущо име: {entity.Name}");
            var name = ReadRequiredText("Ново име (0 за отказ): ");
            if (name == null) return;

            Console.WriteLine($"Текущ телефон: {entity.ContactNumber}");
            var phone = ReadRequiredText("Нов телефон (0 за отказ): ");
            if (phone == null) return;
            try
            {
                entity.Edit(name, phone);
                organizers.Edit(entity);
                MessageSuccess("Организаторът е обновен.");
            }
            catch (Exception ex) { MessageError(ex.Message); Pause(); }
        }

        private void DeleteOrganizer()
        {
            Console.Clear();
            Header("ИЗТРИВАНЕ НА ОРГАНИЗАТОР");
            var allOrg = organizers.GetAll().ToList();
            if (!allOrg.Any()) { MessageError("Няма организатори."); Pause(); return; }

            PrintOrganizers(allOrg);
            int? id;
            while (true)
            {
                id = ReadId("\nID на организатор (0 за отказ): ");
                if (id == null) return;
                if (organizers.GetById(id.Value) != null) break;
                MessageError("Не е намерен.");
            }

            try { organizers.Delete(id.Value); MessageSuccess("Организаторът е изтрит."); }
            catch (Exception ex) { MessageError(ex.Message); Pause(); }
        }

        private void CreateTicketType()
        {
            Console.Clear();
            Header("ДОБАВЯНЕ НА ТИП БИЛЕТ");
            var name = ReadRequiredText("Име (0 за отказ): ");
            if (name == null) return;
            var price = ReadDecimalInput("Цена (0 за отказ): ");
            if (price == null) return;
            try
            {
                var entity = new TicketType(name, new Money(price.Value));
                ticketTypes.Create(entity);
                MessageSuccess($"Типът билет е създаден! ID: {entity.Id}");
            }
            catch (Exception ex) { MessageError(ex.Message); Pause(); }
        }

        private void EditTicketType()
        {
            Console.Clear();
            Header("РЕДАКТИРАНЕ НА ТИП БИЛЕТ");
            var allTypes = ticketTypes.GetAll().ToList();
            if (!allTypes.Any()) { MessageError("Няма типове билети."); Pause(); return; }

            PrintTicketTypes(allTypes);
            int? id;
            TicketType entity;
            while (true)
            {
                id = ReadId("\nID на тип билет (0 за отказ): ");
                if (id == null) return;
                entity = ticketTypes.GetById(id.Value);
                if (entity != null) break;
                MessageError("Не е намерен.");
            }

            Console.WriteLine($"\nТекуща цена: {entity.Price}");
            var price = ReadDecimalInput("Нова цена (0 за отказ): ");
            if (price == null) return;
            try
            {
                entity.ChangePrice(new Money(price.Value));
                ticketTypes.Edit(entity);
                MessageSuccess("Цената е обновена.");
            }
            catch (Exception ex) { MessageError(ex.Message); Pause(); }
        }

        private void DeleteTicketType()
        {
            Console.Clear();
            Header("ИЗТРИВАНЕ НА ТИП БИЛЕТ");
            var allTypes = ticketTypes.GetAll().ToList();
            if (!allTypes.Any()) { MessageError("Няма типове."); Pause(); return; }

            PrintTicketTypes(allTypes);
            int? id;
            while (true)
            {
                id = ReadId("\nID на тип (0 за отказ): ");
                if (id == null) return;
                if (ticketTypes.GetById(id.Value) != null) break;
                MessageError("Не е намерен.");
            }

            try { ticketTypes.Delete(id.Value); MessageSuccess("Типът билет е изтрит."); }
            catch (Exception ex) { MessageError(ex.Message); Pause(); }
        }

        private void CreateTicket()
        {
            Console.Clear();
            Header("КАСА | ПРОДАЖБА И ИЗДАВАНЕ НА БИЛЕТ");
            var allEvents = events.GetAll().ToList();
            if (!allEvents.Any())
            {
                MessageError("Няма регистрирани събития. Първо създайте събитие.");
                Pause();
                return;
            }

            Console.WriteLine("--- Изберете събитие за продажба ---");
            PrintEvents(allEvents);

            int? eventId = ReadId("\nВъведете ID на събитието (0 за отказ): ");
            if (eventId == null) return;

            var selectedEvent = events.GetById(eventId.Value);
            if (selectedEvent == null)
            {
                MessageError("Събитието не е намерено.");
                Pause();
                return;
            }

            int soldCount = selectedEvent.Tickets.Count(t => t.Status == TicketStatus.Sold);
            int availableSeats = selectedEvent.Capacity - soldCount;

            if (availableSeats <= 0)
            {
                MessageError($"Капацитетът за '{selectedEvent.Name}' е запълнен ({soldCount}/{selectedEvent.Capacity}). Не може да се издаде билет!");
                Pause();
                return;
            }

            var allTypes = ticketTypes.GetAll().ToList();
            if (!allTypes.Any())
            {
                MessageError("Няма дефинирани ценови категории (типове билети) в системата.");
                Pause();
                return;
            }

            Console.WriteLine($"\nИзбрано събитие: {selectedEvent.Name}");
            Console.WriteLine($"Остават свободни места: {availableSeats} от общо {selectedEvent.Capacity}");
            Console.WriteLine("\n--- Изберете ценова категория ---");
            PrintTicketTypes(allTypes);

            int? typeId = ReadId("\nВъведете ID на типа билет (0 за отказ): ");
            if (typeId == null) return;

            var type = ticketTypes.GetById(typeId.Value);
            if (type == null)
            {
                MessageError("Типът билет не е намерен.");
                Pause();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n[ПОТВЪРЖДЕНИЕ] Покупка на билет за: \"{selectedEvent.Name}\"");
            Console.WriteLine($"Категория: {type.Name} | Дължима сума: {type.Price} лв.");
            Console.ResetColor();
            Console.Write("Завършване на продажбата? (yes/no): ");
            string conf = Console.ReadLine()?.Trim().ToLower();
            if (conf != "y" && conf != "ye" && conf != "yes")
            {
                Console.WriteLine("Продажбата е отказана.");
                Pause();
                return;
            }

            try
            {
                var ticket = tickets.Create(eventId.Value, type);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n=============================================");
                Console.WriteLine("         БИЛЕТЪТ Е ГЕНЕРИРАН УСПЕШНО!        ");
                Console.WriteLine("=============================================");
                Console.WriteLine($"  СЪБИТИЕ:   {selectedEvent.Name}");
                Console.WriteLine($"  ДАТА/ЧАС:  {selectedEvent.Date:dd.MM.yyyy HH:mm}");
                Console.WriteLine($"  КАТЕГОРИЯ: {type.Name}");
                Console.WriteLine($"  ЦЕНА:      {type.Price} лв.");
                Console.WriteLine($"  УНИКАЛЕН КОД: {ticket.Code}");
                Console.WriteLine("=============================================");
                Console.ResetColor();
                Pause();
            }
            catch (Exception ex)
            {
                MessageError($"Грешка: {ex.Message}");
                Pause();
            }
        }

        private void CancelTicket()
        {
            Console.Clear();
            Header("ОТМЯНА / АНУЛИРАНЕ НА БИЛЕТ");
            var allT = tickets.GetAll().ToList();
            if (!allT.Any()) { MessageError("Няма издадени билети."); Pause(); return; }

            PrintTickets(allT);
            int? id = ReadId("\nВъведете ID на билет за отмяна (0 за отказ): ");
            if (id == null) return;

            try
            {
                tickets.Cancel(id.Value);
                MessageSuccess("Билетът е успешно АНУЛИРАН. Мястото в събитието е освободено.");
            }
            catch (Exception ex) { MessageError(ex.Message); Pause(); }
        }

        private void MarkTicketAsUsed()
        {
            Console.Clear();
            Header("ВХОДЕН КОНТРОЛ | СКАНИРАНЕ НА БИЛЕТ");
            var allT = tickets.GetAll().ToList();
            if (!allT.Any()) { MessageError("Няма издадени билети."); Pause(); return; }

            PrintTickets(allT);
            int? id = ReadId("\nВъведете ID на билет за проверка на входа (0 за отказ): ");
            if (id == null) return;

            try
            {
                tickets.MarkAsUsed(id.Value);
                MessageSuccess("Билетът беше УСПЕШНО СКАНИРАН! Посетителят може да влезе.");
            }
            catch (Exception ex) { MessageError($"[ДОСТЪП ОТКАЗАН] {ex.Message}"); Pause(); }
        }

        private void CheckTicketValidity()
        {
            Console.Clear();
            Header("ИНСПЕКЦИЯ | ПРОВЕРКА НА БИЛЕТ");
            var allT = tickets.GetAll().ToList();
            if (!allT.Any()) { MessageError("Няма налични билети."); Pause(); return; }

            PrintTickets(allT);
            int? id = ReadId("\nВъведете ID на билет за проверка (0 за отказ): ");
            if (id == null) return;

            var ticket = allT.FirstOrDefault(t => t.Id == id.Value);
            if (ticket == null)
            {
                MessageError("Билетът не съществува в базата данни.");
                Pause();
                return;
            }

            var ev = events.GetById(ticket.EventId);

            Console.WriteLine("\n--- Справка от системата ---");
            Console.WriteLine($"Код: {ticket.Code}");
            Console.WriteLine($"Събитие: {(ev != null ? ev.Name : "Неизвестно")}");

            if (tickets.IsValid(id.Value))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n[✓] БИЛЕТЪТ Е НАПЪЛНО ВАЛИДЕН И АКТИВЕН.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n[X] БИЛЕТЪТ Е НЕВАЛИДЕН. Текущ статус: {ticket.Status}");
                Console.ResetColor();
            }
            Pause();
        }

        private void PrintEvents(IEnumerable<Event> list)
        {
            var items = list.ToList();
            if (!items.Any()) { Console.WriteLine(" Няма намерени събития."); return; }

            Console.WriteLine(string.Format(" {0,-4} | {1,-20} | {2,-15} | {3,-16} | {4,-15}", "ID", "Име", "Тип", "Дата", "Заетост/Кап."));
            Console.WriteLine(new string('-', 80));
            foreach (var e in items)
            {
                var sold = e.Tickets.Count(t => t.Status == TicketStatus.Sold);
                string name = e.Name.Length > 20 ? e.Name.Substring(0, 17) + "..." : e.Name;
                string type = e.EventType.Length > 15 ? e.EventType.Substring(0, 12) + "..." : e.EventType;
                Console.WriteLine(string.Format(" #{0,-3} | {1,-20} | {2,-15} | {3:dd.MM.yyyy HH:mm} | {4}/{5}",
                    e.Id, name, type, e.Date, sold, e.Capacity));
            }
        }

        private void PrintLocations(IEnumerable<Location> list)
        {
            var items = list.ToList();
            if (!items.Any()) { Console.WriteLine(" Няма намерени локации."); return; }

            Console.WriteLine(string.Format(" {0,-4} | {1,-25} | {2,-10} | {3,-15}", "ID", "Име", "Капацитет", "Бр. Събития"));
            Console.WriteLine(new string('-', 60));
            foreach (var l in items)
            {
                string name = l.Name.Length > 25 ? l.Name.Substring(0, 22) + "..." : l.Name;
                Console.WriteLine(string.Format(" #{0,-3} | {1,-25} | {2,-10} | {3,-15}", l.Id, name, l.Capacity, l.Events.Count));
            }
        }

        private void PrintOrganizers(IEnumerable<Organizer> list)
        {
            var items = list.ToList();
            if (!items.Any()) { Console.WriteLine(" Няма намерени организатори."); return; }

            Console.WriteLine(string.Format(" {0,-4} | {1,-25} | {2,-15} | {3,-15}", "ID", "Име", "Телефон", "Бр. Събития"));
            Console.WriteLine(new string('-', 65));
            foreach (var o in items)
            {
                string name = o.Name.Length > 25 ? o.Name.Substring(0, 22) + "..." : o.Name;
                Console.WriteLine(string.Format(" #{0,-3} | {1,-25} | {2,-15} | {3,-15}", o.Id, name, o.ContactNumber, o.Events.Count));
            }
        }

        private void PrintTicketTypes(IEnumerable<TicketType> list)
        {
            var items = list.ToList();
            if (!items.Any()) { Console.WriteLine(" Няма намерени типове билети."); return; }

            Console.WriteLine(string.Format(" {0,-4} | {1,-25} | {2,-10} | {3,-15}", "ID", "Име", "Цена", "Генерирани бр."));
            Console.WriteLine(new string('-', 65));
            foreach (var tt in items)
            {
                string name = tt.Name.Length > 25 ? tt.Name.Substring(0, 22) + "..." : tt.Name;
                Console.WriteLine(string.Format(" #{0,-3} | {1,-25} | {2,-10} | {3,-15}", tt.Id, name, tt.Price, tt.Tickets.Count));
            }
        }

        private void PrintTickets(IEnumerable<Ticket> list)
        {
            var items = list.ToList();
            if (!items.Any()) { Console.WriteLine(" Няма намерени билети."); return; }

            Console.WriteLine(string.Format(" {0,-4} | {1,-10} | {2,-12} | {3,-18} | {4,-20}", "ID", "Код", "Статус", "Събитие", "Категория (Цена)"));
            Console.WriteLine(new string('-', 75));
            foreach (var t in items)
            {
                var ev = events.GetById(t.EventId);
                var tt = ticketTypes.GetById(t.TicketTypeId);

                string eventName = ev != null ? (ev.Name.Length > 18 ? ev.Name.Substring(0, 15) + "..." : ev.Name) : $"ID #{t.EventId}";
                string typeName = tt != null ? $"{tt.Name} ({tt.Price}лв)" : $"ID #{t.TicketTypeId}";

                Console.Write($" #{t.Id,-3} | {t.Code,-10} | ");

                switch (t.Status)
                {
                    case TicketStatus.Sold:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(string.Format("{0,-12}", "Продаден"));
                        break;
                    case TicketStatus.Canceled:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(string.Format("{0,-12}", "Анулиран"));
                        break;
                    case TicketStatus.Used:
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write(string.Format("{0,-12}", "Използван"));
                        break;
                    default:
                        Console.ResetColor();
                        Console.Write(string.Format("{0,-12}", t.Status));
                        break;
                }
                Console.ResetColor();

                Console.WriteLine($" | {eventName,-18} | {typeName,-20}");
            }
        }

        private static void Header(string text)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(new string('=', text.Length + 4));
            Console.WriteLine($"  {text}");
            Console.WriteLine(new string('=', text.Length + 4));
            Console.ResetColor();
            Console.WriteLine();
        }

        private static void MessageSuccess(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n[УСПЕХ] {text}");
            Console.ResetColor();
            Pause();
        }

        private static void MessageError(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ГРЕШКА] {text}");
            Console.ResetColor();
        }

        private static void Pause()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\nНатиснете Enter за продължаване...");
            Console.ResetColor();
            Console.ReadLine();
        }

        private static void SetInputColor() => Console.ForegroundColor = ConsoleColor.Yellow;

        private static int ReadMenuChoice(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                SetInputColor();
                var value = Console.ReadLine()?.Trim();
                Console.ResetColor();
                if (int.TryParse(value, out var result)) return result;
                MessageError("Въведете валидно число.");
            }
        }

        private static int? ReadId(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                SetInputColor();
                var value = Console.ReadLine()?.Trim();
                Console.ResetColor();

                if (value == "0") return null;
                if (int.TryParse(value, out var result) && result > 0) return result;
                MessageError("Въведете валидно положително число или 0 за отказ.");
            }
        }

        private static string ReadRequiredText(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                SetInputColor();
                var value = Console.ReadLine()?.Trim();
                Console.ResetColor();

                if (value == "0") return null;
                if (!string.IsNullOrWhiteSpace(value)) return value;
                MessageError("Полето е задължително.");
            }
        }

        private static string ReadOptionalText(string prompt)
        {
            Console.Write(prompt);
            SetInputColor();
            var value = Console.ReadLine()?.Trim();
            Console.ResetColor();
            return string.IsNullOrWhiteSpace(value) ? "" : value;
        }

        private static decimal? ReadDecimalInput(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                SetInputColor();
                var value = Console.ReadLine()?.Trim();
                Console.ResetColor();

                if (value == "0") return null;
                if (decimal.TryParse(value, out var result)) return result;
                MessageError("Въведете валидна сума.");
            }
        }

        private static DateTime? ReadDateTimeInput(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                SetInputColor();
                var value = Console.ReadLine()?.Trim();
                Console.ResetColor();

                if (value == "0") return null;
                if (DateTime.TryParseExact(value, new[] { "dd.MM.yyyy HH:mm", "dd.MM.yyyy" }, null, DateTimeStyles.None, out var result))
                    return result;
                MessageError("Въведете валидна дата във формат 20.06.2026 18:30");
            }
        }

        private static DateTime ReadDateTimeFromInput(string input)
        {
            if (DateTime.TryParseExact(input, new[] { "dd.MM.yyyy HH:mm", "dd.MM.yyyy" }, null, DateTimeStyles.None, out var result))
                return result;
            throw new FormatException("Невалидна дата.");
        }

        private static int ParseInt(string input)
        {
            return int.Parse(input);
        }
    }
}