namespace EventManagement11.Domain.Entities
{
    public class Location
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public string Address { get; private set; }

        public int Capacity { get; private set; }

        public virtual ICollection<Event> Events { get; private set; }

        protected Location()
        {
            Events = new List<Event>();
        }

        public Location(
            string name,
            string address,
            int capacity)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(
                    "Моля, въведете име на локацията.");
            }

            if (string.IsNullOrWhiteSpace(address))
            {
                throw new ArgumentException(
                    "Моля, въведете адрес на локацията.");
            }

            if (capacity <= 0)
            {
                throw new ArgumentException(
                    "Капацитетът на локацията трябва да бъде по-голям от 0.");
            }

            if (name.Trim().Length > 50)
            {
                throw new ArgumentException(
                    "Името на локацията не може да съдържа повече от 50 символа.");
            }

            if (address.Trim().Length > 100)
            {
                throw new ArgumentException(
                    "Адресът на локацията не може да съдържа повече от 100 символа.");
            }

            Name = name.Trim();
            Address = address.Trim();
            Capacity = capacity;
            Events = new List<Event>();
        }

        public void Edit(
            string name,
            string address,
            int capacity)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(
                    "Моля, въведете име на локацията.");
            }

            if (string.IsNullOrWhiteSpace(address))
            {
                throw new ArgumentException(
                    "Моля, въведете адрес на локацията.");
            }

            if (capacity <= 0)
            {
                throw new ArgumentException(
                    "Капацитетът на локацията трябва да бъде по-голям от 0.");
            }

            if (name.Trim().Length > 50)
            {
                throw new ArgumentException(
                    "Името на локацията не може да съдържа повече от 50 символа.");
            }

            if (address.Trim().Length > 100)
            {
                throw new ArgumentException(
                    "Адресът на локацията не може да съдържа повече от 100 символа.");
            }

            Name = name.Trim();
            Address = address.Trim();
            Capacity = capacity;
        }
    }
    }
