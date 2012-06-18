using System.Linq;
using System.Collections.Generic;
using Conversus.BusinessLogic;
using Conversus.Core.DTO;
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

        public QueueData GetOrCreateQueue(QueueType queueType)
        {
            return GetData(QueueLogic.GetOrCreateQueue(queueType));
        }

        public ICollection<QueueData> GetQueues()
        {
            return QueueLogic.GetQueues().Select(GetData).ToList();
        }

        #endregion

        private QueueData GetData(IQueue queue)
        {
            if (queue == null)
                return default(QueueData);
            return new QueueData()
            {
                Id = queue.Id,
                Type = queue.Type
            };
        }
    }
}