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

        //TODO записать в базу время, когда печатается билет
        //DateTime.Now не вижу смысла передавать
        [OperationContract]
        void ClientGettingTicket();

        // for operator

        [OperationContract]
        void ChangeStatus(Guid clientId, ClientStatus status);

        /// <summary>
        /// Сидящие в очереди с билетиком
        /// </summary>
        [OperationContract]
        ICollection<ClientInfo> GetClientsQueue(QueueType queue);

        [OperationContract]
        ClientInfo CallNextClient(QueueType queue);

        [OperationContract]
        void CallClient(Guid id);
    }
}
