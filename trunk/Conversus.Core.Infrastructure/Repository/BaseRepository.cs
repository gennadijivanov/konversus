using System;
using System.Collections.Generic;
using Conversus.Core.DomainModel;
using Conversus.Core.Infrastructure.UnitOfWork;

namespace Conversus.Core.Infrastructure.Repository
{
    public abstract class BaseRepository<TEntityData, TEntity> : IRepository, IUnitOfWorkRepository
        where TEntityData : struct
        where TEntity : class
    {
        private readonly IUnitOfWork _unitOfWork;

        protected BaseRepository()
            : this(null)
        {
        }

        protected BaseRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // abstract methods to implement in end classes
        protected abstract TEntity CreateFromData(TEntityData data);
        protected abstract TEntityData GetDataFromEntity(TEntity item);
        protected abstract TEntityData? GetData(Guid id); // must be called only from GetEntityData

        // !!! factory !!!
        protected virtual IUnitOfWork GetUnitOfWork()
        {
            return new UnitOfWork.UnitOfWork(); //UnitOfWorkFactory.Create();
        }

        protected virtual void OnAdded(IEntity item)
        {
        }

        protected virtual void OnUpdated(IEntity item)
        {
        }

        protected virtual void OnDeleted(IEntity item)
        {
        }

        #region Protected section

        protected TEntityData? GetEntityData(Guid id)
        {
            return GetData(id);
        }

        #endregion

        #region IRepository

        public void OnChange(IEntity entity)
        {
        }

        public IEntity Get(Guid id)
        {
            TEntityData? entityDataNullable = GetEntityData(id);
            return (IEntity)(entityDataNullable.HasValue ? CreateFromData(entityDataNullable.Value) : null);
        }

        public abstract ICollection<IEntity> GetCollection(IFilterParameters filter);

        public void Add(IEntity entity)
        {
            if (_unitOfWork != null)
            {
                _unitOfWork.RegisterAdded(entity, this);
            }
            else
            {
                IUnitOfWork unitOfWork = GetUnitOfWork();
                unitOfWork.RegisterAdded(entity, this);
                unitOfWork.Commit();
            }
        }

        public void Update(IEntity entity)
        {
            if (_unitOfWork != null)
            {
                _unitOfWork.RegisterChanged(entity, this);
            }
            else
            {
                IUnitOfWork unitOfWork = GetUnitOfWork();
                unitOfWork.RegisterChanged(entity, this);
                unitOfWork.Commit();
            }
        }

        public void Remove(IEntity entity)
        {
            if (_unitOfWork != null)
            {
                _unitOfWork.RegisterRemoved(entity, this);
            }
            else
            {
                IUnitOfWork unitOfWork = GetUnitOfWork();
                unitOfWork.RegisterRemoved(entity, this);
                unitOfWork.Commit();
            }
        }

        #endregion
    }
}
