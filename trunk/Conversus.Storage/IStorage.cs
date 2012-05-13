using System;
using System.Collections.Generic;
using Conversus.Core.Infrastructure.Repository;

namespace Conversus.Storage
{
    public interface IStorage<TEntityData> where TEntityData : struct
    {
        void Create(TEntityData data);
        void Update(TEntityData data);
        TEntityData Get(Guid id);
        ICollection<TEntityData> Get(IFilterParameters filter);
        void Delete(Guid id);
    }
}
