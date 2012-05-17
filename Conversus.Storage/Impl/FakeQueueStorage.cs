﻿using System;
using System.Collections.Generic;
using System.Linq;
using Conversus.Core.DTO;
using Conversus.Core.Infrastructure.Repository;

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

        public ICollection<QueueData> Get(IFilterParameters filter)
        {
            var query = _dict.Values.AsQueryable();
            if (filter != null && filter is QueueFilterParameters)
            {
                var f = (QueueFilterParameters)filter;
                //TODO: !!!!!!!!!!!!!!
                //if (f.ClientId.HasValue)
                //    query = query.Where(q => q.Id == _clientStorage.Get(f.ClientId.Value).);
                if (f.QueueType.HasValue)
                    query = query.Where(q => q.Type == f.QueueType.Value);
            }
            return query.ToList();
        }

        public void Delete(Guid id)
        {
            _dict.Remove(id);
        }

        public QueueData GetByClient(Guid clientId)
        {
            return Get(new QueueFilterParameters(){ ClientId = clientId }).FirstOrDefault();
        }
    }
}
