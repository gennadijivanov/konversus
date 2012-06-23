﻿using System;
using System.Collections.Generic;
using Conversus.Core.DomainModel;

namespace Conversus.Core.Infrastructure.Repository
{
    public interface IRepository
    {
        IEntity Get(Guid id);
        ICollection<IEntity> GetCollection(IFilterParameters filter = null);
        void Add(IEntity entity);
        void Update(IEntity entity);
        void Remove(IEntity entity);
    }

    public interface IUnitOfWorkRepository
    {
        void OnChange(IEntity entity);
    }

    public interface IUnitOfWorkStorageRepository : IUnitOfWorkRepository
    {
        IDisposable CreateContext();

        void PersistNewItemInStorage(object context, IEntity item);
        void PersistUpdatedItemInStorage(object context, IEntity item);
        void PersistDeletedItemInStorage(object context, IEntity item);
    }
}
