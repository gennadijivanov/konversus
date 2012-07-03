using System;
using System.Collections.Generic;
using System.Linq;
using Conversus.Core.DomainModel;
using Conversus.Core.Infrastructure.Repository;
using Conversus.Storage;
using Client = Conversus.Impl.Objects.Client;

namespace Conversus.BusinessLogic.Impl
{
    public class ClientLogic : IClientLogic
    {
        private readonly IClientStorage Storage = StorageLogicFactory.Instance.Get<IClientStorage>();

        #region Implementation of IClientLogic

        public IClient CreateForCommon(string name, QueueType queueType)
        {
            IQueue queue = BusinessLogicFactory.Instance.Get<IQueueLogic>().GetOrCreateQueue(queueType);
            IClient client = new Client(Guid.NewGuid(), name, queue.Id, DateTime.MinValue, null, ClientStatus.Waiting,
                                        "");
            Storage.Create(client);
            return client;
        }

        public IClient CreateFromLotus(string name, int pin)
        {
            //TODO: get Queue type from PIN or add parameter!!!!!!!!!!
            IQueue queue = BusinessLogicFactory.Instance.Get<IQueueLogic>().GetOrCreateQueue(QueueType.Approvement);
            IClient client = new Client(Guid.NewGuid(), name, queue.Id, DateTime.MinValue, pin, ClientStatus.Registered,
                                        "");
            Storage.Create(client);
            return client;
        }

        //TODO: create ticket factory
        public string GetTicket(Guid clientId)
        {
            // must create ticket by factory, set to object and return

            var client = Storage.Get(clientId);
            if (client == null)
                throw new InvalidOperationException("Client is not found");

            if (string.IsNullOrEmpty(client.Ticket))
            {
                var queue = StorageLogicFactory.Instance.Get<IQueueStorage>().GetByClient(clientId);

                client.Ticket = (new TicketFactory()).SetTicketForClient(queue.Type, client.PIN);
                Storage.Update(client);
            }

            return client.Ticket;
        }

        public IClient GetClientByPin(int pin)
        {
            return Storage.Get(new ClientFilterParameters {PIN = pin}).SingleOrDefault();
        }

        public void ChangeStatus(Guid clientId, ClientStatus status)
        {
            var client = Storage.Get(clientId);
            if (client == null)
                throw new InvalidOperationException("Client is not found");
            ChangeStatus(client, status);
        }

        private void ChangeStatus(IClient client, ClientStatus status)
        {
            client.Status = status;
            switch (status)
            {
                case ClientStatus.Registered:
                case ClientStatus.Delayed:
                    break;
                case ClientStatus.Performing:
                    client.PerformStart = DateTime.Now;
                    break;
                case ClientStatus.Waiting:
                    client.TakeTicket = DateTime.Now;
                    break;
                case ClientStatus.Done:
                    client.PerformEnd = DateTime.Now;
                    break;
                case ClientStatus.Absent:
                    client.PerformEnd = DateTime.Now;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("status");
            }
            Storage.Update(client);
        }

        public ICollection<IClient> GetClients(QueueType queueType)
        {
            var queue =
                StorageLogicFactory.Instance.Get<IQueueStorage>().Get(new QueueFilterParameters()
                                                                          {QueueType = queueType}).Single();
            return Storage.Get(new ClientFilterParameters() {QueueId = queue.Id});
        }

        public ICollection<IClient> GetClientsQueue(QueueType queue)
        {
            return GetClients(queue)
                .Where(c => c.Status == ClientStatus.Waiting)
                .Select(c => new {IsVip = c.PIN.HasValue, Client = c})
                .OrderBy(c => c.IsVip)
                .ThenBy(c => c.Client.TakeTicket)
                .Select(c => c.Client)
                .ToList();
        }

        public IClient CallNextClient(QueueType queue)
        {
            var client = GetClientsQueue(queue).FirstOrDefault();
            if (client != null)
                ChangeStatus(client, ClientStatus.Performing);
            return client;
        }

        public IClient Get(Guid id)
        {
            return Storage.Get(id);
        }

        #endregion
    }

    public class TicketFactory
    {
        public string SetTicketForClient(QueueType queueType, int? pin)
        {
            // client registered over inet
            if (pin.HasValue)
            {
                IClient client = BusinessLogicFactory.Instance.Get<IClientLogic>().GetClientByPin(pin.Value);
            }

            return "ASS PUSHKIN";
        }
    }
}
