using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conversus.Core.DomainModel;
using Conversus.Core.DTO;
using Conversus.Core.Infrastructure;
using Conversus.Core.Infrastructure.UnitOfWork;
using Conversus.Core.Impl.Objects;

namespace Conversus.Core.Impl.Repositories
{
    public class QueueRepository : BaseRepository<QueueData, IQueue>, IUnitOfWorkStorageRepository, IQueueRepository
    {
        public QueueRepository()
            : this(null)
        { 
        }

        public QueueRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        // all from storage. rename method
        protected override Dictionary<int, QueueData> FillCache()
        {
            throw new NotImplementedException();
        }

        protected override IQueue CreateFromData(QueueData data)
        {
            return new Queue(data);
        }

        protected override QueueData GetDataFromEntity(IQueue item)
        {
            return (item as Queue).GetData();
        }

        // must be overriden in cache repository
        protected override QueueData? GetData(int id, long? timestamp)
        {
            throw new NotImplementedException();
        }

        public IDisposable CreateContext()
        {
            throw new NotImplementedException();
        }

        public void PersistNewItemInStorage(object context, IEntity item)
        {
            throw new NotImplementedException();
        }

        public void PersistUpdatedItemInStorage(object context, IEntity item)
        {
            throw new NotImplementedException();
        }

        public void PersistDeletedItemInStorage(object context, IEntity item)
        {
            throw new NotImplementedException();
        }

        public IQueue Get(int id)
        {
            return Get(id, null) as IQueue;
        }
    }
}
