using System;
using System.Collections.Generic;
using System.Linq;
using Conversus.Core.DomainModel;
using Conversus.Core.DTO;
using Conversus.Core.Infrastructure.Repository;
using Conversus.Core.Infrastructure.UnitOfWork;
using Conversus.Impl.Objects;
using Conversus.Storage;

namespace Conversus.Impl.Repositories
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

        protected override IQueue CreateFromData(QueueData data)
        {
            return new Queue(data);
        }

        protected override QueueData GetDataFromEntity(IQueue item)
        {
            return (item as Queue).GetData();
        }

        protected override QueueData? GetData(Guid id)
        {
            return Storage.Get(id);
        }

        public override ICollection<IEntity> GetCollection(IFilterParameters filter)
        {
            return Storage.Get(filter).Select(CreateFromData).Cast<IEntity>().ToList();
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

        public new IQueue Get(Guid id)
        {
            return base.Get(id) as IQueue;
        }

        public ICollection<IQueue> Get(IFilterParameters filter)
        {
            throw new NotImplementedException();
        }

        public IQueue GetByClient(Guid clientId)
        {
            return CreateFromData(Storage.GetByClient(clientId));
        }
    }
}
