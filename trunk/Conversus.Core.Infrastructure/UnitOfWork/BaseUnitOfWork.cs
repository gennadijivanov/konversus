﻿using System.Collections.Generic;
using Conversus.Core.DomainModel;
using Conversus.Core.Infrastructure.Repository;

namespace Conversus.Core.Infrastructure.UnitOfWork
{
    public enum TransactionType
    {
        Insert,
        Update,
        Delete
    }

    public abstract class BaseUnitOfWork : IUnitOfWork
    {
        protected struct Operation
        {
            public IEntity Entity { get; set; }
            public IUnitOfWorkRepository Repository { get; set; }
            public TransactionType Type { get; set; }
        }

        protected List<Operation> _operations;

        protected BaseUnitOfWork()
        {
            _operations = new List<Operation>();
        }

        public void RegisterAdded(IEntity entity, IUnitOfWorkRepository repository)
        {
            _operations.Add(new Operation { Entity = entity, Repository = repository, Type = TransactionType.Insert });
        }

        public void RegisterChanged(IEntity entity, IUnitOfWorkRepository repository)
        {
            _operations.Add(new Operation { Entity = entity, Repository = repository, Type = TransactionType.Update });
        }

        public void RegisterRemoved(IEntity entity, IUnitOfWorkRepository repository)
        {
            _operations.Add(new Operation { Entity = entity, Repository = repository, Type = TransactionType.Delete });
        }

        public abstract void Commit();
    }
}
