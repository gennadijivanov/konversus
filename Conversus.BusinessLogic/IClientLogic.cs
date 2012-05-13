using System;
using System.Collections.Generic;
using Conversus.Core.DomainModel;
using Conversus.Core.DTO;

namespace Conversus.BusinessLogic
{
    public interface IClientLogic
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
