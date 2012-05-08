using System;
using System.Collections.Generic;
using System.Linq;
using Conversus.Core.DTO;

namespace Conversus.Storage.Impl
{
    public class FakeQueueStorage : IQueueStorage
    {
        private static readonly Dictionary<Guid, QueueData> _dict = new Dictionary<Guid, QueueData>();

        public void Create(QueueData data)
        {
            _dict.Add(data.Id, data);
        }

        public void Update(QueueData data)
        {
            _dict[data.Id] = data;
        }

        public QueueData Get(Guid id)
        {
            return _dict[id];
        }

        public void Delete(Guid id)
        {
            _dict.Remove(id);
        }

        public QueueData GetByClient(Guid clientId)
        {
            return _dict.Values.FirstOrDefault();
        }
    }
}
