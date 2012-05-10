using System;
using System.Collections.Generic;
using Conversus.Core.DomainModel;
using Conversus.Core.DTO;

namespace Conversus.BusinessLogic
{
    public interface IClientLogic
    {
        // for terminal

        ClientData CreateForCommon(string name, QueueType queueType);

        ClientData CreateFromLotus(string name, int pin);

        string GetTicket(Guid clientId);

        ClientData GetClientByPin(int pin);

        // for operator

        void ChangeStatus(Guid clientId, ClientStatus status);

        ICollection<ClientData> GetClients(QueueType queue);
    }
}
