using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conversus.Core.DomainModel;

namespace Conversus.Core.DTO
{
    public struct ClientData : ITimestampable
    {
        public int Id;

        public string Name;

        public DateTime Deadline;

        public ClientStatus Status;

        public int PIN;

        public string Ticket;

        public long Timestamp { get; set; }
    }
}
