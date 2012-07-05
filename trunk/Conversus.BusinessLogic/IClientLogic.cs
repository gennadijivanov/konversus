﻿using System;
using System.Collections.Generic;
using Conversus.Core.DomainModel;
using Conversus.Core.Infrastructure.Repository;

namespace Conversus.BusinessLogic
{
    public interface IClientLogic
    {
        // for terminal

        IClient CreateForCommon(string name, QueueType queueType);

        IClient CreateFromLotus(string name, int pin);

        ICollection<IClient> Get(ClientFilterParameters filter);

        // for operator

        void ChangeStatus(Guid clientId, ClientStatus status);

        ICollection<IClient> GetClients(QueueType queue);

        /// <summary>
        /// ждущие в очереди
        /// </summary>
        ICollection<IClient> GetClientsQueue(QueueType queue);

        IClient CallNextClient(QueueType queue);

        IClient Get(Guid id);
    }
}
