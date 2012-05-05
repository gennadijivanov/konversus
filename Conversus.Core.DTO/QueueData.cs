using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conversus.Core.DomainModel;

namespace Conversus.Core.DTO
{
    public struct QueueData : ITimestampable
    {
        public int Id;
        public QueueType Type;

        public long Timestamp { get; set; }
    }
}
