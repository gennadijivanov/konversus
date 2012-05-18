using System;
using System.Linq;
using System.Collections.Generic;
using Conversus.Core.DTO;
using Conversus.Core.Infrastructure.Repository;
using ServiceStack.Redis;

namespace Conversus.Storage.Impl
{
    public class RedisQueueStorage : IQueueStorage
    {
        readonly RedisClient _redisClient = new RedisClient("localhost");

        #region Implementation of IStorage<QueueData>

        public void Create(QueueData data)
        {
            using (var queues = _redisClient.GetTypedClient<QueueData>())
            {
                queues.Store(data);
            }
        }

        public void Update(QueueData data)
        {
            using (var queues = _redisClient.GetTypedClient<QueueData>())
            {
                queues.Store(data);
            }
        }

        public QueueData Get(Guid id)
        {
            using (var queues = _redisClient.GetTypedClient<QueueData>())
            {
                return queues.GetAll().SingleOrDefault(q => q.Id == id);
            }
        }

        public ICollection<QueueData> Get(IFilterParameters filter)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            using (var queues = _redisClient.GetTypedClient<QueueData>())
            {
                queues.DeleteById(id.ToString());
            }
        }

        public QueueData GetByClient(Guid clientId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}