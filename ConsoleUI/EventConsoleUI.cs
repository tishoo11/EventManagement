using EventManagement11.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("=== Event Management System ===");
            }
        }
    }
}
