using Conversus.Core.DomainModel;
using Conversus.Core.Impl.Objects;

namespace Conversus.Impl.Factories
{
    public class QueueFactory : IQueueFactory
    {
        #region Implementation of IQueueFactory

        public IQueue CreateNewQueue(QueueType queueType)
        {
            return new Queue(queueType);
        }

        #endregion
    }
}