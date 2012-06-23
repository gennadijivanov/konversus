using System;
using System.Collections.Generic;
using Conversus.Core.Infrastructure.Repository;

namespace Conversus.Storage
{
    public interface IStorage<TEntityData> where TEntityData : struct
    {
        IDisposable CreateContext();

        void Create(object context, TEntityData data);
        void Update(object context, TEntityData data);
        TEntityData Get(Guid id);
        ICollection<TEntityData> Get(IFilterParameters filter);
        void Delete(object context, Guid id);
    }
}
