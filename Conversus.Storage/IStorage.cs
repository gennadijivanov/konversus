using System;
using System.Collections.Generic;
using Conversus.Core.Infrastructure.Repository;

namespace Conversus.Storage
{
    public interface IStorage<TEntity> where TEntity : class
    {
        void Create(TEntity data);
        void Update(TEntity data);
        TEntity Get(Guid id);
        ICollection<TEntity> Get(IFilterParameters filter);
        void Delete(Guid id);
    }
}
