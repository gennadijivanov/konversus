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

        public DateTime ChangeTime { get; set; }

        public ClientStatus Status { get; set; }

        public int? PIN { get; set; }

        public string Ticket { get; set; }

        public QueueInfo Queue { get; set; }

        /// <summary>
        /// ���� ���������. ������ ��� ��� ���������
        /// </summary>
        public OperatorInfo User { get; set; }

        public bool IsVip
        {
            get { return PIN.HasValue; }
        }
    }
}