using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement11.ConsoleUI
{
    public class EventConsoleUI
    {
        private readonly EventService service;

        public EventConsoleUI(EventService service)
        {
            this.service = service;
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
