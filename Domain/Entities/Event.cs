using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_Management_System.Domain.Entities
{
    public class Event
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public DateTime Date { get; private set; }
        public int Capacity { get; private set; } //Продадени билети ≤ Капацитет на събитието ≤ Капацитет на локацията
        public string EventType { get; private set; }

        //public string LocationName { get; private set; } 

        // Външен ключ към Location
        public int LocationId { get; private set; }

        // Navigation property
        public virtual Location Location { get; private set; }

        // Външен ключ към Location
        public int OrganizerId { get; private set; }

        // Navigation property
        public Organizer Organizer { get; private set; }

        public ICollection<Ticket> Tickets { get; private set; } = new List<Ticket>();

        //Празният конструктор е нужен за Migration и EntityFramework
        
        protected Event()
        {
           
        }

        public Event(string name, DateTime date, int locationId, int organizerId, int capacity, string eventType)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Моля, въведете име на събитието.");
            

            if (name.Trim().Length > 120)
                throw new ArgumentException("Името на събитието не може да съдържа повече от 120 символа.");
           

            if (date == default(DateTime))
                throw new ArgumentException("Моля, въведете дата на събитието.");
            

            if (date.Date < DateTime.Today)
                throw new ArgumentException("Датата на събитието не може да бъде в миналото.");
        
            if (locationId<=0)
                throw new ArgumentException("Моля, въведете локация на събитието.");

            if (capacity <= 0)
                throw new ArgumentException( "Капацитетът на събитието трябва да бъде по-голям от 0.");
            

            if (string.IsNullOrWhiteSpace(eventType))
                throw new ArgumentException("Моля, въведете тип на събитието.");
            

            if (eventType.Trim().Length > 80)
                throw new ArgumentException( "Типът на събитието не може да съдържа повече от 80 символа.");
            

            Name = name.Trim();
            Date = date.Date;
            LocationId = locationId;
            OrganizerId = organizerId;
            Capacity = capacity;
            EventType = eventType.Trim();

            Tickets = new List<Ticket>();
        }

        // set-овете на свойствата са private, за да не могат обектите да се променят отвън.
        // Трябва да имплементирате методите за контролирана промяна на вече създадените обекти от клас Event
        // Помислете какви валидицаии са нужни.
        public void EditName( string name)
        {
            // TODO
            // Капацитетът на събитието не може да бъде по-голям от капацитета на локацията.

            Name = name.Trim();
            
        }
        
        public void Reschedule(DateTime newDate)
        {
            //TODO
        }
        public void ChangeLocation(string newLocationName)
        {
             //TODO
        }
        public void ChangeCapacity(int newCapacity, int soldTickets)
        {
            if (newCapacity < soldTickets)
            {
                throw new InvalidOperationException(
                    "Капацитетът не може да бъде по-малък от броя на продадените билети.");
            }

            if (newCapacity > Location.Capacity)
            {
                throw new InvalidOperationException(
                    "Капацитетът на събитието не може да бъде по-голям от капацитета на локацията.");
            }
            //TODO
        }
        public void ChangeType(string newEventType)
        {
            //TODO
        }
        public int GetAvailableCapacity(int soldTickets)
        {
            //TODO
            return Capacity - soldTickets;
        }
        public bool HasAvailableCapacity(int soldTickets)
        {
            // TODO
            return GetAvailableCapacity(soldTickets) > 0;
        }
    }
}
