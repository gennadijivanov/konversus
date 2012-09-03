using System.Linq;
using System.Collections.Generic;
using System.ServiceModel;
using Conversus.BusinessLogic;
using Conversus.Core.DomainModel;
using Conversus.Service.Contract;

namespace Conversus.Service.Impl
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class QueueService : IQueueService
    {
        private IQueueLogic _queueLogic;
        private IQueueLogic QueueLogic
        {
            get { return _queueLogic ?? (_queueLogic = BusinessLogicFactory.Instance.Get<IQueueLogic>()); }
        }

        #region Implementation of IQueueService

        public QueueInfo GetOrCreateQueue(QueueType queueType)
        {
            return ToQueueInfo(QueueLogic.GetOrCreateQueue(queueType));
        }

        public ICollection<QueueInfo> GetQueues()
        {
            return QueueLogic.GetQueues().Select(ToQueueInfo).ToList();
        }

        #endregion

        private QueueInfo ToQueueInfo(IQueue queue)
        {
            return queue == null 
                ? null
                : new QueueInfo(queue.Id, queue.Type, QueueLogic.GetTitle(queue.Type));
        }
    }
}