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
        ClientInfo CreateFromLotus(string name, int pin, QueueType queueType, DateTime bookingTime);

        [OperationContract]
        ClientInfo GetClientByPin(int pin);

        // for operator

        [OperationContract]
        void ChangeStatus(Guid clientId, ClientStatus status);

        /// <summary>
        /// Сидящие в очереди с билетиком (в т.ч. отложенные)
        /// </summary>
        [OperationContract]
        ICollection<ClientInfo> GetClientsQueue(QueueType queue);

        [OperationContract]
        ClientInfo CallNextClient(QueueType queue);

        [OperationContract]
        void CallClient(Guid id);

        [OperationContract]
        void Postpone(Guid id);

        [OperationContract]
        ClientInfo CallClientByTicket(string ticket);

        [OperationContract]
        ClientInfo ChangeQueue(Guid clientId, string name, QueueType queueType);
    }
}
