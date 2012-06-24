using System;
using System.Collections.Generic;
using System.Linq;
using Conversus.Core.Infrastructure.Repository;
using Conversus.Core.DomainModel;
using Conversus.Impl.Objects;

namespace Conversus.Storage.Impl
{
    public class SQLiteQueueStorage : IQueueStorage
    {
        public void Create(IQueue data)
        {
            //var allWithSamePin = Get(new QueueFilterParameters() { QueueType = data.Type });
            //if (allWithSamePin.Count > 0)
            //    throw new InvalidOperationException("Client with same PIN already exists");

            using (var db = new ConversusDataContext())
            {
                var client = new Queues()
                                 {
                                     Id = data.Id,
                                     Type = (int) data.Type
                                 };
                db.AddToQueues(client);
                db.SaveChanges();
            }
        }

        public void Update(IQueue data)
        {
            using (var db = new ConversusDataContext())
            {
                var dbCl = db.Queues.SingleOrDefault(c => c.Id == data.Id);

                if (dbCl == null)
                    return;

                //TODO: set field of data obj
                db.SaveChanges();
            }
        }

        public IQueue Get(Guid id)
        {
            using (var db = new ConversusDataContext())
            {
                var dbCl = db.Queues.SingleOrDefault(c => c.Id == id);

                return dbCl == null
                    ? null
                    : ConvertFromData(dbCl);
            }
        }

        public IQueue GetByClient(Guid clientId)
        {
            return Get(new QueueFilterParameters() { ClientId = clientId }).SingleOrDefault();
        }

        public ICollection<IQueue> Get(IFilterParameters filter)
        {
            QueueFilterParameters f = filter != null ? filter as QueueFilterParameters : null;

            using (var db = new ConversusDataContext())
            {
                IEnumerable<Queues> query = db.Queues;

                if (f != null && (f.ClientId.HasValue || f.QueueType.HasValue))
                {
                    if (f.ClientId.HasValue)
                        query = query.Where(q => q.Clients.Any(c => c.Id == f.ClientId.Value));

                    if (f.QueueType.HasValue)
                        query = query.Where(q => q.Type == (int)f.QueueType.Value);
                }

                var list = query.ToList();

                return list.Select(ConvertFromData).ToList();
            }

        }

        public void Delete(Guid id)
        {
            using (var db = new ConversusDataContext())
            {
                var queue = db.Queues.SingleOrDefault(c => c.Id == id);
                if (queue != null)
                    db.Queues.DeleteObject(queue);
            }
        }

        private IQueue ConvertFromData(Queues data)
        {
            return new Queue(data.Id, (QueueType)data.Type);
        }
    }
}
