using System;
using System.Collections.Generic;
using Conversus.Core.DTO;
using Conversus.Core.DomainModel;

namespace Conversus.BusinessLogic.Impl
{
    public class QueueLogic : IQueueLogic
    {
        #region Implementation of IQueueLogic

        public QueueData GetQueue(int clientId)
        {
            throw new NotImplementedException();
        }

        public QueueData GetQueue(Guid clientId)
        {
            throw new NotImplementedException();
        }

        public QueueData GetOrCreateQueue(QueueType queueType)
        {
            throw new NotImplementedException();
        }

        public ICollection<QueueData> GetQueues()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}