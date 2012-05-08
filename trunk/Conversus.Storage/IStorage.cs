using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conversus.Storage
{
    public interface IStorage<TEntityData> where TEntityData : struct
    {
        void Create(TEntityData data);
        void Update(TEntityData data);
        TEntityData Get(Guid id);
        void Delete(Guid id);
    }
}
