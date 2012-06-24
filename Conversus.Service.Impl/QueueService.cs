using System.Linq;
using System.Collections.Generic;
using Conversus.BusinessLogic;
using Conversus.Core.DomainModel;
using Conversus.Service.Contract;

namespace Conversus.Service.Impl
{
    public class QueueService : IQueueService
    {
        private IQueueLogic _queueLogic;
        private IQueueLogic QueueLogic
        {
            get { return _queueLogic ?? (_queueLogic = BusinessLogicFactory.Instance.Get<IQueueLogic>()); }
        }

        #region Implementation of IQueueService

        public IQueue GetOrCreateQueue(QueueType queueType)
        {
            return QueueLogic.GetOrCreateQueue(queueType);
        }

        public ICollection<IQueue> GetQueues()
        {
            return QueueLogic.GetQueues();
        }

        #endregion
    }
}