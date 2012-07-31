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
                                        ClientStatus.Waiting, SortPriority.Common, ticket);
            Storage.Create(client);
            return client;
        }

        public IClient CreateFromLotus(string name, int pin, QueueType queueType, DateTime bookingTime)
        {
            IQueue queue = BusinessLogicFactory.Instance.Get<IQueueLogic>().GetOrCreateQueue(queueType);
            IClient client = new Client(Guid.NewGuid(), name, queue.Id, bookingTime, pin, ClientStatus.Registered,
                                        SortPriority.Vip, "");
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
                StorageLogicFactory.Instance.Get<IQueueStorage>().Get(new QueueFilterParameters {QueueType = queueType})
                    .Single();
            return Storage.Get(new ClientFilterParameters {QueueId = queue.Id});
        }

        public ICollection<IClient> GetClientsQueue(QueueType queue)
        {
            return GetClients(queue)
                .Where(c => c.Status == ClientStatus.Waiting || c.Status == ClientStatus.Postponed)
                .OrderByDescending(c => c.SortPriority)
                .ThenByDescending(c => c.ChangeTime)
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

        public void ChangeQueue(Guid clientId, Guid targetOperatorId, SortPriority sortPriority)
        {
            Storage.ChangeQueue(clientId, targetOperatorId, sortPriority);
        }

        #endregion

        private string GetTicket(QueueType queueType, bool isVip)
        {
            var clients = GetClients(queueType);
                
            int clientsCount = clients
                //TODO: date!!!
                .Count(c => c.Status != ClientStatus.Registered && c.BookingTime.Date == DateTime.Today);

            string ticket = string.Format("{0} {1}",
                isVip ? Constants.VipQueueLetter : Constants.QueueTypeLetters[queueType],
                clientsCount + 1);
            return ticket;
        }

        private void ChangeStatus(IClient client, ClientStatus status)
        {
            client.Status = status;
            if (status == ClientStatus.Waiting)
            {
                client.Ticket = GetTicket(
                    StorageLogicFactory.Instance.Get<IQueueStorage>().Get(client.QueueId).Type,
                    client.PIN.HasValue);
            }
            Storage.Update(client);
        }
    }
}
