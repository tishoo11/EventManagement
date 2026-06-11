using EventManagement11.Domain.Enums;
using EventManagement11.Domain.ValueObjects;

namespace EventManagement11.Domain.Entities
{
    public class Ticket
    {

        public int Id { get; private set; }
        public int EventId { get; private set; }
        public int TicketTypeId { get; private set; }
        public TicketType TicketType { get; private set; }
        public string Code { get; private set; }
        public Money Price { get; private set; }
        public DateTime SoldAt { get; private set; }
        public TicketStatus Status { get; private set; }
        public bool IsValid
        {
            get
            {
                return Status == TicketStatus.Sold;
            }
        }

        protected   Ticket()
        {
        }

        public Ticket(int eventId, TicketType ticketType, string code)
        {
            if (eventId <= 0)
                throw new ArgumentException("Моля, изберете събитие.");


            if (ticketType == null)
                throw new ArgumentException("Моля, изберете тип билет.");


            if (ticketType.Id <= 0)
                throw new ArgumentException("Избраният тип билет не е валиден.");

            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("Кодът на билета е задължителен.");


            if (ticketType.Price == null)
                throw new ArgumentException("Избраният тип билет няма зададена цена.");

            if (code.Trim().Length > 30)
                throw new ArgumentException("Кодът на билета не може да съдържа повече от 30 символа.");

            EventId = eventId;
            TicketTypeId = ticketType.Id; //цената се взема автоматично от TicketType
            Code = code.Trim();

            // цената се копира в Ticket, за да остане същата дори ако по-късно цената на типа билет бъде променена.
            Price = new Money(ticketType.Price.Amount);

            SoldAt = DateTime.Now;
            Status = TicketStatus.Sold;
        }

        public void Cancel()
        {
            if (Status == TicketStatus.Cancelled)
                throw new InvalidOperationException("Билетът вече е отменен.");

            if (Status == TicketStatus.Used)
                throw new InvalidOperationException("Използван билет не може да бъде отменен.");

            Status = TicketStatus.Cancelled;
        }

        public void MarkAsUsed()
        {
            if (Status == TicketStatus.Cancelled)
                throw new InvalidOperationException("Отменен билет не може да бъде използван.");


            if (Status == TicketStatus.Used)
                throw new InvalidOperationException("Билетът вече е използван.");

            Status = TicketStatus.Used;
        }

    }
}
