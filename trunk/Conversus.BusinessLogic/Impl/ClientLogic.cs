using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conversus.Core.DTO;
using Conversus.Core.DomainModel;
using Conversus.Core.Infrastructure.Repository;
using Conversus.Impl.Repositories;
using Conversus.Impl.Factories;

namespace Conversus.BusinessLogic.Impl
{
    public class ClientLogic : IClientLogic
    {
        #region Implementation of IClientLogic

        public IClient CreateForCommon(string name, QueueType queueType)
        {
            var client = RepositoryFactory.GetClientFactory().CreateNewClient(name, queueType, null);
            RepositoryFactory.GetClientRepository().Add(client);
            return client;
        }

        public IClient CreateFromLotus(string name, int pin)
        {
            //TODO: get Queue type from PIN or add parameter!!!!!!!!!!
            var client = RepositoryFactory.GetClientFactory().CreateNewClient(name, QueueType.Approvement, pin);
            RepositoryFactory.GetClientRepository().Add(client);
            return client;
        }

        //TODO: create ticket factory
        public string GetTicket(Guid clientId)
        {
            // must create ticket by factory, set to object and return
            
            var client = RepositoryFactory.GetClientRepository().Get(clientId);
            if (client.Id != clientId)
                throw new InvalidOperationException("Client is not found");
            return client.Ticket;
        }

        public IClient GetClientByPin(int pin)
        {
            return RepositoryFactory.GetClientRepository()
                .Get(new ClientFilterParameters { PIN = pin }).SingleOrDefault();
        }

        public void ChangeStatus(Guid clientId, ClientStatus status)
        {
            var client = RepositoryFactory.GetClientRepository().Get(clientId);
            //хак конечно, но пока метод Гет нулл не возвращает
            if (client.Id != clientId)
                throw new InvalidOperationException("Client is not found");
            client.ChangeStatus(status);
        }

        public ICollection<IClient> GetClients(QueueType queueType)
        {
            var queue = RepositoryFactory.GetQueueRepository()
                .Get(new QueueFilterParameters {QueueType = queueType}).Single();
            return RepositoryFactory.GetClientRepository().GetClients(queue.Id);
        }

        #endregion
    }
}
