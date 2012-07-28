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

        public UserInfo User { get; set; }

        /// <summary>
        /// ���� ���������. ������ ��� ��� ���������
        /// </summary>
        public UserInfo User { get; set; }

        public bool IsVip
        {
            get { return PIN.HasValue; }
        }
    }
}