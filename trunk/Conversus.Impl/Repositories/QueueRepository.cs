using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conversus.Core.DomainModel;
using Conversus.Core.DTO;
using Conversus.Core.Infrastructure;
using Conversus.Core.Infrastructure.UnitOfWork;
using Conversus.Core.Impl.Objects;
using Conversus.Storage;

namespace Conversus.Core.Impl.Repositories
{
    public class QueueRepository : BaseRepository<QueueData, IQueue>, IUnitOfWorkStorageRepository, IQueueRepository
    {
        private IQueueStorage _storage;
        private IQueueStorage Storage
        {
            get { return _storage ?? (_storage = StorageLogicFactory.Instance.Get<IQueueStorage>()); }
        }

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

        protected override QueueData? GetData(int id, long? timestamp)
        {
            return Storage.Get(id);
        }

        public void PersistNewItemInStorage(IEntity item)
        {
            Storage.Create((item as Queue).GetData());
        }

        public void PersistUpdatedItemInStorage(IEntity item)
        {
            Storage.Update((item as Queue).GetData());
        }

        public void PersistDeletedItemInStorage(IEntity item)
        {
            Storage.Delete(item.Id);
        }

        public IQueue Get(int id)
        {
            return Get(id, null) as IQueue;
        }
    }
}
