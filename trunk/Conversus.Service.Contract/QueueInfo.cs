using System;
using Conversus.Core.DomainModel;

namespace Conversus.Service.Contract
{
    [Serializable]
    public class QueueInfo
    {
        public Guid Id { get; set; }

        public QueueType Type { get; set; }

        public string Title { get; set; }

        public QueueInfo(Guid id, QueueType type, string title)
        {
            Id = id;
            Type = type;
            Title = title;
        }
    }
}