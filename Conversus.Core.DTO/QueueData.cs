using System;
using Conversus.Core.DomainModel;

namespace Conversus.Core.DTO
{
    public struct QueueData : ITimestampable
    {
        public Guid Id;
        public QueueType Type;

        public long Timestamp { get; set; }
    }
}
