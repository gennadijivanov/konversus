using System;
using Conversus.Core.DomainModel;

namespace Conversus.Impl.Objects
{
    public class Client : IClient
    {
        public string Name { get; set; }

        public DateTime BookingTime { get; set; }

        public DateTime? TakeTicket { get; set; }

        public DateTime? PerformStart { get; set; }

        public DateTime? PerformEnd { get; set; }

        public ClientStatus Status { get; set; }

        public int? PIN { get; set; }

        public string Ticket { get; set; }

        public Guid QueueId { get; set; }

        public Guid Id { get; set; }

        public Client(Guid id, string name, Guid queueId, DateTime bookingTime, int? pin, ClientStatus status, string ticket)
        {
            Id = id;
            QueueId = queueId;
            Name = name;
            BookingTime = bookingTime;
            PIN = pin;
            Status = status;
            Ticket = ticket;
        }
    }
}
