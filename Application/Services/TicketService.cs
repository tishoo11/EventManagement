using EventManagement11.Application.Interfaces;
using EventManagement11.Domain.Entities;

namespace EventManagement11.Application.Services;

public class TicketService
{
    private readonly ITicketRepository tickets;

    public TicketService(ITicketRepository tickets)
    {
        this.tickets = tickets;
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
