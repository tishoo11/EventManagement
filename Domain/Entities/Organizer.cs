using System;
using System.Collections.Generic;

namespace Event_Management_System.Domain.Entities
{
    public class Organizer
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string ContactNumber { get; private set; }

        public virtual ICollection<Event> Events { get; private set; }

        protected Organizer()
        {
        }

        public Organizer(string name, string phone)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Моля, въведете име на организатора.");

            if (string.IsNullOrWhiteSpace(phone))
                throw new ArgumentException( "Моля, въведете телефон на организатора.");

            if (phone.Trim().Length > 15)
                throw new ArgumentException("Телефонът на организатора не може да съдържа повече от 15 символа.");

            if (name.Trim().Length > 50)
                throw new ArgumentException("Името на организатора не може да съдържа повече от 50 символа.");

            Name = name.Trim();
            ContactNumber = phone.Trim();

            Events = new List<Event>();
        }
    }

}
