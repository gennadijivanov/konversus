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
            throw new NotImplementedException();
        }

        public IClient CreateFromLotus(string name, int pin)
        {
            throw new NotImplementedException();
        }

        //TODO: create ticket factory
        public string GetTicket(Guid clientId)
        {
            // must create ticket by factory, set to object and return
            
            var client = RepositoryFactory.GetClientRepository().Get(clientId);
            return client.Ticket;
        }

        public IClient GetClientByPin(int pin)
        {
            throw new NotImplementedException();
        }

        public void ChangeStatus(Guid clientId, ClientStatus status)
        {
            var client = RepositoryFactory.GetClientRepository().Get(clientId);
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
