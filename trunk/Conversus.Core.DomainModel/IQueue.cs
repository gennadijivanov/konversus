using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conversus.Core.DomainModel
{
    public enum QueueType
    {
        Type1,
        Type2
    }

    public interface IQueue : IEntity
    {
        QueueType Type { get; set; }

        ICollection<IClient> GetClients();
    }
}
