using System;
using Conversus.Core.DomainModel;

namespace Conversus.Service.Contract
{
    [Serializable]
    public class ClientInfo
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime BookingTime { get; set; }

        public DateTime? TakeTicket { get; set; }

        public DateTime? PerformStart { get; set; }

        public DateTime? PerformEnd { get; set; }

        public ClientStatus Status { get; set; }

        public int? PIN { get; set; }

        public string Ticket { get; set; }

        public QueueInfo Queue { get; set; }

        /// <summary>
        /// Окно оператора. Только для уже вызванных
        /// </summary>
        public string Window { get; set; }
    }
}