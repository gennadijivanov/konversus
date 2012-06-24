using System;
using System.Collections.Generic;
using Conversus.BusinessLogic;
using Conversus.Core.DomainModel;
using Conversus.Service.Contract;

namespace Conversus.Service.Impl
{
    public class ClientService : IClientService
    {
        private IClientLogic _clientLogic;
        private IClientLogic ClientLogic
        {
            get { return _clientLogic ?? (_clientLogic = BusinessLogicFactory.Instance.Get<IClientLogic>()); }
        }

        #region Implementation of IClientService

        public IClient CreateForCommon(string name, QueueType queueType)
        {
            return ClientLogic.CreateForCommon(name, queueType);
        }

        public IClient CreateFromLotus(string name, int pin)
        {
            return ClientLogic.CreateFromLotus(name, pin);
        }

        public string GetTicket(Guid clientId)
        {
            return ClientLogic.GetTicket(clientId);
        }

        public IClient GetClientByPin(int pin)
        {
            return ClientLogic.GetClientByPin(pin);
        }

        public void ChangeStatus(Guid clientId, ClientStatus status)
        {
            ClientLogic.ChangeStatus(clientId, status);
        }

        public ICollection<IClient> GetClients(QueueType queue)
        {
            return ClientLogic.GetClients(queue);
        }

        #endregion

    }
}
