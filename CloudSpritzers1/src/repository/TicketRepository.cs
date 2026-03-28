using CloudSpritzers.src.model.ticket;
using System;
using System.Collections.Generic;


namespace CloudSpritzers.src.repository
{
    public class TicketRepository : IRepository<int, Ticket>
    {
        private Dictionary<int, Ticket> Tickets = new Dictionary<int, Ticket>();
        public int Add(Ticket elem)
        {
            if (elem == null)
                throw new ArgumentNullException();

            if (Tickets.ContainsKey(elem.TicketId))
                throw new ArgumentException("Ticket with the same ID already exists.");

            Tickets[elem.TicketId] = elem;
            return elem.TicketId;
        }

        public void DeleteById(int id)
        {
            if (!Tickets.ContainsKey(id))
                throw new KeyNotFoundException("Ticket with ID " + id + " not found.");

            Tickets.Remove(id);
        }

        public IEnumerable<Ticket> GetAll()
        {
            return Tickets.Values;
        }

        public Ticket GetById(int id)
        {
            Ticket ticket;

            if (Tickets.TryGetValue(id, out ticket))
                return ticket;

            throw new KeyNotFoundException("Ticket with ID " + id + " not found.");
        }

        public void UpdateById(int id, Ticket elem)
        {
            if (elem == null)
                throw new ArgumentNullException();

            if (!Tickets.ContainsKey(id))
                throw new KeyNotFoundException("Ticket with ID " + id + " not found.");

            if (id != elem.TicketId)
            {
                throw new ArgumentException("ID mismatch between key and ticket.");
            }
            Tickets[id] = elem;
        }
    }
}
