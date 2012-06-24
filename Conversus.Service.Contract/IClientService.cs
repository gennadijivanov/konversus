using System;
using System.Collections.Generic;
using System.ServiceModel;
using Conversus.Core.DomainModel;

namespace Conversus.Service.Contract
{
    [ServiceContract]
    public interface IClientService
    {
        // for terminal

        IClient CreateForCommon(string name, QueueType queueType);

        IClient CreateFromLotus(string name, int pin);

        string GetTicket(Guid clientId);

        IClient GetClientByPin(int pin);

        // for operator

        void ChangeStatus(Guid clientId, ClientStatus status);

        ICollection<IClient> GetClients(QueueType queue);
    }
}
