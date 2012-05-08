using System;
using Conversus.Core.DomainModel;
using Conversus.Core.DTO;
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

        public DateTime Deadline
        {
            get { return _data.Deadline; }
            set { _data.Deadline = value; }
        }

        public ClientStatus Status
        {
            get { return _data.Status; }
            set { _data.Status = value; }
        }

        public int PIN
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
                Deadline = deadline,
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
            return RepositoryFactory.GetQueueRepository().GetByClient(_data.Id);
        }
    }
}
