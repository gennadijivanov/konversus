using System.Collections.Generic;
using System.Linq;
using Conversus.Core.DomainModel;
using Conversus.Core.Infrastructure.Repository;
using Conversus.Impl.Factories;
using Conversus.Impl.Repositories;

namespace Conversus.BusinessLogic.Impl
{
    public class QueueLogic : IQueueLogic
    {
        #region Implementation of IQueueLogic

        public IQueue GetOrCreateQueue(QueueType queueType)
        {
            var rep = RepositoryFactory.GetQueueRepository();
            IQueue queue = rep.Get(new QueueFilterParameters(){ QueueType = queueType }).SingleOrDefault();

            if (queue == null)
            {
                queue = RepositoryFactory.GetQueueFactory().CreateNewQueue(queueType);
                rep.Add(queue);
            }

            return queue;
        }

        public ICollection<IQueue> GetQueues()
        {
            return RepositoryFactory.GetQueueRepository().GetCollection().Cast<IQueue>().ToList();
        }

        #endregion
    }
}