using System;
using System.Linq;
using Conversus.Core.DomainModel;
using Conversus.Core.DTO;
using Conversus.Core.Infrastructure.Repository;
using Conversus.Impl.Factories;

namespace Conversus.Impl.Objects
{
    public class Client : IClient
    {
        private ClientData _data;

        public string Name
        {
            get { return _data.Name; }
            set { _data.Name = value; }
        }

        public DateTime? BookingTime
        {
            get { return _data.BookingTime; }
            set { _data.BookingTime = value; }
        }

        public DateTime? TakeTicket
        {
            get { return _data.TakeTicket; }
            set { _data.TakeTicket = value; }
        }

        public DateTime? PerformStart
        {
            get { return _data.PerformStart; }
            set { _data.PerformStart = value; }
        }

        public DateTime? PerformEnd
        {
            get { return _data.PerformEnd; }
            set { _data.PerformEnd = value; }
        }

        public ClientStatus Status
        {
            get { return _data.Status; }
            set { _data.Status = value; }
        }

        public int? PIN
        {
            get { return _data.PIN; }
            set { _data.PIN = value; }
        }

        public string Ticket
        {
            get { return _data.Ticket; }
            set { _data.Ticket = value; }
        }

        public Guid Id
        {
            get { return _data.Id; }
        }

        public Client(ClientData data)
        {
            _data = data;
        }

        public Client(string name, DateTime deadline, int pin, ClientStatus status, string ticket)
            : this(new ClientData
            {
                Id = Guid.NewGuid(),
                Name = name,
                BookingTime = deadline,
                PIN = pin,
                Status = status,
                Ticket = ticket
            })
        {
        }

        internal ClientData GetData()
        {
            return _data;
        }

        public IQueue GetQueue()
        {
            return RepositoryFactory.GetQueueRepository()
                .Get(new QueueFilterParameters() {ClientId = _data.Id}).Single();
        }

        public void ChangeStatus(ClientStatus newStatus)
        {
            _data.Status = newStatus;
            switch (newStatus)
            {
                case ClientStatus.Performing:
                    _data.PerformStart = DateTime.Now;
                    break;
                case ClientStatus.Waiting:
                    _data.TakeTicket = DateTime.Now;
                    break;
                case ClientStatus.Done:
                case ClientStatus.Absent:
                    _data.PerformEnd = DateTime.Now;
                    break;
            }
            RepositoryFactory.GetClientRepository().Update(this);
        }

        public string CreateTicket()
        {
            var queueType = GetQueue().Type;
            string queueCode = PIN.HasValue
                                   ? "C"
                                   : (queueType == QueueType.Approvement ? "A" : "B");

            string ticket =queueCode + " " + Id.ToString();

            _data.Ticket = ticket;

            return ticket;
        }
    }
}
