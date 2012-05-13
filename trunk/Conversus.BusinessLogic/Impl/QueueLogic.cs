using System;
using System.Collections.Generic;
using System.Linq;
using Conversus.Core.DTO;
using Conversus.Core.DomainModel;
using Conversus.Core.Infrastructure.Repository;
using Conversus.Impl.Factories;
using Conversus.Impl.Objects;
using Conversus.Impl.Repositories;

namespace Conversus.BusinessLogic.Impl
{
    public class QueueLogic : IQueueLogic
    {
        private readonly IQueueRepository _repository;

        public QueueLogic()
        {
            _repository = RepositoryFactory.GetQueueRepository();
        }

        #region Implementation of IQueueLogic

        public IQueue GetOrCreateQueue(QueueType queueType)
        {
            IQueue queue = _repository.Get(new QueueFilterParameters(){ QueueType = queueType }).SingleOrDefault();

            if (queue == null)
            {
                queue = RepositoryFactory.GetQueueFactory().CreateNewQueue(queueType);
                _repository.Add(queue);
            }

            return queue;
        }

        public ICollection<IQueue> GetQueues()
        {
            return _repository.GetCollection().Cast<IQueue>().ToList();
        }

        #endregion
    }
}