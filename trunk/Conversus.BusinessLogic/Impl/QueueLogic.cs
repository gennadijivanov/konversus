using System;
using System.Collections.Generic;
using System.Linq;
using Conversus.Core.DomainModel;
using Conversus.Core.Infrastructure.Repository;
using Conversus.Impl.Objects;
using Conversus.Storage;

namespace Conversus.BusinessLogic.Impl
{
    public class QueueLogic : IQueueLogic
    {
        private readonly IQueueStorage Storage = StorageLogicFactory.Instance.Get<IQueueStorage>();

        #region Implementation of IQueueLogic

        public IQueue GetOrCreateQueue(QueueType queueType)
        {
            IQueue queueData = Storage.Get(new QueueFilterParameters(){ QueueType = queueType }).SingleOrDefault();

            if (queueData == null)
            {
                queueData = new Queue(Guid.NewGuid(), queueType);
                Storage.Create(queueData);
            }

            return queueData;
        }

        public ICollection<IQueue> GetQueues()
        {
            return Storage.Get(new QueueFilterParameters());;
        }

        #endregion
    }
}