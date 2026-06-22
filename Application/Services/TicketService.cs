using EventManagement11.Application.Interfaces;
using EventManagement11.Domain.Entities;
using EventManagement11.Infrastructure;

namespace EventManagement11.Application.Services;

public class TicketService
{
    private readonly ITicketRepository tickets;
    private EventDBContext context;

    public TicketService(ITicketRepository tickets)
    {
        this.tickets = tickets;
    }

    public TicketService(EventDBContext context)
    {
        this.context = context;
    }

    public Ticket? GetById(int id) => tickets.GetById(id);

    public IReadOnlyList<Ticket> GetAll() => tickets.GetAll();

    public string GenerateCode() => Guid.NewGuid().ToString("N");

    public Ticket Create(int eventId, TicketType ticketType)
    {
        var ticket = new Ticket(eventId, ticketType, GenerateCode());
        tickets.Save(ticket);
        return ticket;
    }

    public void Cancel(int id)
    {
        var ticket = tickets.GetById(id);
        if (ticket != null)
        {
            ticket.Cancel();
            tickets.Update(ticket);
        }
    }

    public void MarkAsUsed(int id)
    {
        var ticket = tickets.GetById(id);
        if (ticket != null)
        {
            ticket.MarkAsUsed();
            tickets.Update(ticket);
        }
    }

    public bool IsValid(int id)
    {
        var ticket = tickets.GetById(id);
        return ticket?.IsValid == true;
    }
}
