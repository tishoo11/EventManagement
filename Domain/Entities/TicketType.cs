using EventManagement11.Domain.ValueObjects;

namespace EventManagement11.Domain.Entities
{
    public class TicketType
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public Money Price { get; private set; }
        public virtual ICollection<Ticket> Tickets { get; private set; } = new List<Ticket>();

        protected TicketType()
        {
        }

        public TicketType(string name, Money price)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Моля, въведете име на типа билет.");

            if (price == null)
                throw new ArgumentException(" Моля, въведете цена на билета.");

            if (name.Trim().Length > 30)
                throw new ArgumentException("Името на типа билет не може да съдържа повече от 30 символа.");

            Name = name.Trim();
            Price = price;
            Tickets = new List<Ticket>();
        }

        public void ChangePrice(Money newPrice)
        {
            if (newPrice == null)
                throw new ArgumentException("Моля, въведете нова цена на билета.");

            Price = newPrice;
        }
    }

}
