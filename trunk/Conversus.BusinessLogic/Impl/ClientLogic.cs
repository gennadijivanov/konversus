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
        private IClientRepository _clientRepository;
        private IClientRepository ClientRepository { get { return _clientRepository ?? (_clientRepository = RepositoryFactory.GetClientRepository()); } }

        #region Implementation of IClientLogic

        public IClient CreateForCommon(string name, QueueType queueType)
        {
            BusinessLogicFactory.Instance.Get<IQueueLogic>().GetOrCreateQueue(queueType);
            var client = RepositoryFactory.GetClientFactory().CreateNewClient(name, queueType, null);
            ClientRepository.Add(client);
            return client;
        }

        public IClient CreateFromLotus(string name, int pin)
        {
            //TODO: get Queue type from PIN or add parameter!!!!!!!!!!
            BusinessLogicFactory.Instance.Get<IQueueLogic>().GetOrCreateQueue(QueueType.Approvement);
            var client = RepositoryFactory.GetClientFactory().CreateNewClient(name, QueueType.Approvement, pin);
            client.Ticket = (new TicketFactory()).SetTicketForClient(QueueType.Approvement, pin);
            ClientRepository.Add(client);
            return client;
        }

        //TODO: create ticket factory
        public string GetTicket(Guid clientId)
        {
            // must create ticket by factory, set to object and return

            var client = ClientRepository.Get(clientId);
            if (client.Id != clientId)
                throw new InvalidOperationException("Client is not found");

            if (string.IsNullOrEmpty(client.Ticket))
            {
                client.Ticket = (new TicketFactory()).SetTicketForClient(client.GetQueue().Type, client.PIN);
                ClientRepository.Update(client);
            }

            return client.Ticket;
        }

        public IClient GetClientByPin(int pin)
        {
            return ClientRepository.Get(new ClientFilterParameters { PIN = pin }).SingleOrDefault();
        }

        public void ChangeStatus(Guid clientId, ClientStatus status)
        {
            var client = ClientRepository.Get(clientId);
            //хак конечно, но пока метод Гет нулл не возвращает
            if (client.Id != clientId)
                throw new InvalidOperationException("Client is not found");
            client.ChangeStatus(status);
        }

        public ICollection<IClient> GetClients(QueueType queueType)
        {
            var queue = RepositoryFactory.GetQueueRepository()
                .Get(new QueueFilterParameters {QueueType = queueType}).Single();
            return ClientRepository.GetClients(queue.Id);
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
