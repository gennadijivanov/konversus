using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conversus.Storage;

namespace Conversus.Core.Storage.Impl
{
    public class FakeQueueStorage : IQueueStorage
    {
        private static Dictionary<int, DTO.QueueData> _list = new Dictionary<int, DTO.QueueData>();

        public void Create(DTO.QueueData data)
        {
            _list.Add(data.Id, data);
        }

        public void Update(DTO.QueueData data)
        {
            _list[data.Id] = data;
        }

        public DTO.QueueData Get(int id)
        {
            return _list[id];
        }

        public void Delete(int id)
        {
            _list.Remove(id);
        }
    }
}
