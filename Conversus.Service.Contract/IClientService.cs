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

        [OperationContract]
        ClientInfo CreateForCommon(string name, QueueType queueType);

        [OperationContract]
        ClientInfo CreateFromLotus(string name, int pin);

        [OperationContract]
        string GetTicket(Guid clientId);

        [OperationContract]
        ClientInfo GetClientByPin(int pin);

        // for operator

        [OperationContract]
        void ChangeStatus(Guid clientId, ClientStatus status);

        [OperationContract]
        ICollection<ClientInfo> GetClients(QueueType queue);
    }
}
