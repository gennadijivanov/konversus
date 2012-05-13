using System;
using Conversus.Core.DomainModel;

namespace Conversus.Core.DTO
{
    public struct ClientData
    {
        public Guid Id;

        public string Name;

        public DateTime? BookingTime;

        public DateTime? TakeTicket;

        public DateTime? PerformStart;

        public DateTime? PerformEnd;

        public ClientStatus Status;

        public int? PIN;

        public string Ticket;
    }
}
