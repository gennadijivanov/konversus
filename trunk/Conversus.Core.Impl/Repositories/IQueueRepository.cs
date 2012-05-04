using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conversus.Core.Infrastructure;
using Conversus.Core.DomainModel;

namespace Conversus.Core.Impl.Repositories
{
    public interface IQueueRepository : IRepository
    {
        IQueue Get(int id);
    }
}
