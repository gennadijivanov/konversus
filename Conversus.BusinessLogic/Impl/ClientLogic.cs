using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conversus.Core.DTO;
using Conversus.Core.DomainModel;
using Conversus.Impl.Repositories;
using Conversus.Impl.Factories;

namespace Conversus.BusinessLogic.Impl
{
    public class ClientLogic : IClientLogic
    {
        #region Implementation of IClientLogic

        public void CreateClient(string name, QueueType queueType, int? pin)
        {
            var rep = RepositoryFactory.GetClientRepository();
            var client = RepositoryFactory.GetClientFactory().CreateNewClient(name, queueType, pin);
            rep.Add(client);
        }

        public void SetTicket(int id)
        {
            throw new NotImplementedException();
        }

        public ClientData GetClient(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<ClientData> GetClients(int? queueId)
        {
            throw new NotImplementedException();
        }

        public void ChangeStatus(int clientId, ClientStatus newStatus)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
