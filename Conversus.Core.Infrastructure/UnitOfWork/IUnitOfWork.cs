﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conversus.Core.DomainModel;

namespace Conversus.Core.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        void RegisterAdded(IEntity entity, IUnitOfWorkRepository repository);
        void RegisterChanged(IEntity entity, IUnitOfWorkRepository repository);
        void RegisterRemoved(IEntity entity, IUnitOfWorkRepository repository);

        void Commit();
    }
}
