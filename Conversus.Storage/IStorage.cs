using System;
using System.Collections.Generic;
using Conversus.Core.Infrastructure.Repository;

namespace Conversus.Storage
{
    public interface IStorage<TEntity> where TEntity : class
    {
        void Create(TEntity data);
        TEntity Update(TEntity data);
        TEntity Get(Guid id);
        ICollection<TEntity> Get(IFilterParameters filter);
        ICollection<TEntity> GetWithHistory(DateTime startDate, DateTime endDate);
        void Delete(Guid id);
    }
}
