﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conversus.Core.DomainModel;

namespace Conversus.Core.Infrastructure
{
    public interface IRepository
    {
        long MaxTimestamp { get; }
        IEntity Get(int id, long? timestamp);
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