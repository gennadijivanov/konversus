using System;
using System.Collections.Generic;
using System.Linq;
using Conversus.Core.DTO;
using Conversus.Core.DomainModel;
using Conversus.Core.Infrastructure.Repository;
using Conversus.Core.Infrastructure.UnitOfWork;
using Conversus.Impl.Objects;
using Conversus.Storage;

namespace Conversus.Impl.Repositories
{
    public class ClientRepository : BaseRepository<ClientData, IClient>, IUnitOfWorkStorageRepository, IClientRepository
    {
        private IClientStorage _storage;
        private IClientStorage Storage
        {
            get { return _storage ?? (_storage = StorageLogicFactory.Instance.Get<IClientStorage>()); }
        }

        public ClientRepository()
            : this(null)
        {
        }

        public ClientRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override IClient CreateFromData(ClientData data)
        {
            return new Client(data);
        }

        protected override ClientData GetDataFromEntity(IClient item)
        {
            return (item as Client).GetData();
        }

        protected override ClientData? GetData(Guid id)
        {
            return Storage.Get(id);
        }

        public override ICollection<IEntity> GetCollection(IFilterParameters filter)
        {
            throw new NotImplementedException();
        }

        public IDisposable CreateContext()
        {
            return Storage.CreateContext();
        }

        public void PersistNewItemInStorage(object context, IEntity item)
        {
            Storage.Create(context, (item as Client).GetData());
        }

        public void PersistUpdatedItemInStorage(object context, IEntity item)
        {
            Storage.Update(context, (item as Client).GetData());
        }

        public void PersistDeletedItemInStorage(object context, IEntity item)
        {
            Storage.Delete(context, item.Id);
        }

        public new IClient Get(Guid id)
        {
            return base.Get(id) as IClient;
        }

        public ICollection<IClient> Get(IFilterParameters filter)
        {
            var data = Storage.Get(filter);
            return data.Select(CreateFromData).ToList();
        }

        public ICollection<IClient> GetClients(Guid queueId)
        {
            return Storage.Get(new ClientFilterParameters(){ QueueId = queueId}).Select(CreateFromData).ToList();
        }
    }
}