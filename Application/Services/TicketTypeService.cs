using EventManagement11.Domain.Entities;
using EventManagement11.Domain.ValueObjects;
using EventManagement11.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventManagement11.Application.Services;

public class TicketTypeService
{
    private readonly ITicketTypeRepository _ticketTypes;
    private readonly ITicketRepository _tickets;

}
