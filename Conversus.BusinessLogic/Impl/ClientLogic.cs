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

        public IClient CreateForCommon(string name, QueueType queueType)
        {
            throw new NotImplementedException();
        }

        public IClient CreateFromLotus(string name, int pin)
        {
            throw new NotImplementedException();
        }

        public string GetTicket(Guid clientId)
        {
            return "42";
        }

        public IClient GetClientByPin(int pin)
        {
            throw new NotImplementedException();
        }

        public void ChangeStatus(Guid clientId, ClientStatus status)
        {
            throw new NotImplementedException();
        }

        public ICollection<IClient> GetClients(QueueType queue)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
