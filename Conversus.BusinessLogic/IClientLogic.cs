using System;
using System.Collections.Generic;
using Conversus.Core.DomainModel;
using Conversus.Core.Infrastructure.Repository;

namespace Conversus.BusinessLogic
{
    public interface IClientLogic
    {
        // for terminal

        IClient CreateForCommon(string name, QueueType queueType);

        IClient CreateFromLotus(string name, int pin, QueueType queueType, DateTime bookingTime);

        ICollection<IClient> Get(ClientFilterParameters filter);

        // for operator

        void ChangeStatus(Guid clientId, ClientStatus status);

        void SetAllRegisteredAsAbsent();

        ICollection<IClient> GetClients(QueueType queue);

        /// <summary>
        /// ждущие в очереди
        /// </summary>
        ICollection<IClient> GetClientsQueue(QueueType queue);

        IClient CallNextClient(QueueType queue, Guid operatorId);

        IClient CallClient(Guid clientId, Guid operatorId);

        IClient Get(Guid id);

        void ChangeQueue(Guid clientId, Guid targetOperatorId, SortPriority sortPriority);
    }
}
