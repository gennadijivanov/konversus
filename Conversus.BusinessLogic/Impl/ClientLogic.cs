using System;
using System.Collections.Generic;
using System.Linq;
using Conversus.Core.DomainModel;
using Conversus.Core.Infrastructure;
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

            var ticket = GetTicket(queueType, false);

            IClient client = new Client(Guid.NewGuid(), name, queue.Id, DateTime.MinValue, null,
                                        ClientStatus.Waiting, ticket);
            client.TakeTicket = DateTime.Now;
            Storage.Create(client);
            return client;
        }

        public IClient CreateFromLotus(string name, int pin, QueueType queueType, DateTime bookingTime)
        {
            IQueue queue = BusinessLogicFactory.Instance.Get<IQueueLogic>().GetOrCreateQueue(queueType);
            IClient client = new Client(Guid.NewGuid(), name, queue.Id, bookingTime, pin, ClientStatus.Registered,
                                        "");
            Storage.Create(client);
            return client;
        }

        public ICollection<IClient> Get(ClientFilterParameters filter)
        {
            return Storage.Get(filter);
        }

        public void ChangeStatus(Guid clientId, ClientStatus status)
        {
            var client = Storage.Get(clientId);
            if (client == null)
                throw new InvalidOperationException("Client is not found");
            ChangeStatus(client, status);
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
                .Where(c => c.Status == ClientStatus.Waiting || c.Status == ClientStatus.Postponed)
                .Select(c => new {IsVip = c.PIN.HasValue, Client = c})
                .OrderBy(c => c.IsVip)
                .ThenBy(c => c.Client.TakeTicket)
                .Select(c => c.Client)
                .ToList();
        }

        public IClient CallNextClient(QueueType queue)
        {
            var client = GetClientsQueue(queue).FirstOrDefault(c => c.Status == ClientStatus.Waiting);
            if (client != null)
                ChangeStatus(client, ClientStatus.Performing);
            return client;
        }

        public IClient Get(Guid id)
        {
            return Storage.Get(id);
        }

        #endregion

        private string GetTicket(QueueType queueType, bool isVip)
        {
            var clients = GetClients(queueType).Select(c => c.TakeTicket).ToList();
                
            int clientsCount = clients
                .Count(c => c.HasValue && c.Value.Date == DateTime.Today);

            string ticket = string.Format("{0} {1}",
                isVip ? Constants.VipQueueLetter : Constants.QueueTypeLetters[queueType],
                clientsCount + 1);
            return ticket;
        }

        private void ChangeStatus(IClient client, ClientStatus status)
        {
            client.Status = status;
            switch (status)
            {
                case ClientStatus.Registered:
                case ClientStatus.Postponed:
                    break;
                case ClientStatus.Performing:
                    client.PerformStart = DateTime.Now;
                    break;
                case ClientStatus.Waiting:
                    client.TakeTicket = DateTime.Now;
                    client.Ticket = GetTicket(
                        StorageLogicFactory.Instance.Get<IQueueStorage>().Get(client.QueueId).Type,
                        client.PIN.HasValue);
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
    }
}
