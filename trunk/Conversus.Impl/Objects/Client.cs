using System;
using Conversus.Core.DomainModel;

namespace Conversus.Impl.Objects
{
    public class Client : IClient
    {
        public string Name { get; set; }

        public DateTime BookingTime { get; set; }

        public DateTime ChangeTime { get; set; }

        public ClientStatus Status { get; set; }

        public int? PIN { get; set; }

        public string Ticket { get; set; }

        public Guid QueueId { get; set; }

        public Guid? OperatorId { get; set; }

        public SortPriority SortPriority { get; set; }

        public Guid Id { get; set; }

        public Client(Guid id, string name, Guid queueId, DateTime bookingTime, int? pin, ClientStatus status, SortPriority sortPriority, string ticket)
        {
            Id = id;
            QueueId = queueId;
            Name = name;
            BookingTime = bookingTime;
            PIN = pin;
            Status = status;
            Ticket = ticket;
            SortPriority = sortPriority;
        }
    }
}
