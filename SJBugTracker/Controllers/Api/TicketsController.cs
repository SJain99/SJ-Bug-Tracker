using AutoMapper;
using SJBugTracker.Dtos;
using SJBugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

namespace SJBugTracker.Controllers.Api
{
    public class TicketsController : ApiController
    {
        private ApplicationDbContext _context;

        public TicketsController()
        {
            _context = new ApplicationDbContext();
        }
        public IEnumerable<TicketDto> GetTickets()
        {
            return _context.Tickets.Include(t => t.Project)
                .Include(t => t.TicketType)
                .Include(t => t.TicketPriority)
                .Include(t => t.TicketStatus)
                .ToList()
                .Select(Mapper.Map<Ticket, TicketDto>);
        }

        public TicketDto GetTicket(int id)
        {
            var ticket = _context.Tickets.Include(t => t.Project)
                .Include(t => t.TicketType)
                .Include(t => t.TicketPriority)
                .Include(t => t.TicketStatus)
                .SingleOrDefault(t => t.Id == id);

            if (ticket == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return Mapper.Map<TicketDto>(ticket);
        }

        [HttpPost]
        public IHttpActionResult CreateTicket(TicketDto ticketDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var ticket = Mapper.Map<Ticket>(ticketDto);

            _context.Tickets.Add(ticket);
            _context.SaveChanges();

            ticketDto.Id = ticket.Id;

            return Created(new Uri(Request.RequestUri + "/" + ticket.Id), ticketDto);
        }

        [HttpPut]
        public void UpdateTicket(int id, TicketDto ticketDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var ticketInDb = _context.Tickets.SingleOrDefault(t => t.Id == id);

            if (ticketInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(ticketDto, ticketInDb);

            _context.SaveChanges();
        }

        [HttpDelete]
        public void DeleteTicket(int id)
        {
            var ticketInDb = _context.Tickets.SingleOrDefault(t => t.Id == id);

            if (ticketInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Tickets.Remove(ticketInDb);
            _context.SaveChanges();
        }
    }
}
