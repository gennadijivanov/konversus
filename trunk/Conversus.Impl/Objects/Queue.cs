using System;
using Conversus.Core.DomainModel;

namespace Conversus.Impl.Objects
{
    public class Queue : IQueue
    {
        public Guid Id { get; set; }

        public QueueType Type { get; set; }
        
        public Queue(Guid id, QueueType type)
        {
            Id = id;
            Type = type;
        }
    }
}
