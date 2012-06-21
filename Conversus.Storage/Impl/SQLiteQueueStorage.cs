using System;
using System.IO;
using System.Data;
using System.Data.SQLite;
using System.Data.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conversus.Core.DTO;
using Conversus.Core.Infrastructure.Repository;
using System.Data.Common;
using Conversus.Core.DomainModel;

namespace Conversus.Storage.Impl
{
    public class SQLiteQueueStorage : IQueueStorage
    {
        private readonly SQLiteBaseManager _baseManager;

        public SQLiteQueueStorage()
            : this(SQLiteBaseManager.GetInstance())
        {
        }

        public SQLiteQueueStorage(SQLiteBaseManager baseManager)
        {
            _baseManager = baseManager;
        }

        public void Create(QueueData data)
        {
            
                var allWithSamePin = Get(new QueueFilterParameters() { QueueType = data.Type });
                if (allWithSamePin.Count > 0)
                    throw new InvalidOperationException("Client with same PIN already exists");
         
            using (var db = new ConversusDataContext())
            {
                var client = new Queues()
                {
                    Id = data.Id,
                    Type = (int)data.Type
                };
                db.AddToQueues(client);
                db.SaveChanges();
            }
        }

        public void Update(QueueData data)
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

        public QueueData Get(Guid id)
        {
            using (var db = new ConversusDataContext())
            {
                var dbCl = db.Queues.SingleOrDefault(c => c.Id == id);

                if (dbCl == null)
                    return default(QueueData);

                //TODO: set all properties
                return new QueueData()
                {
                    Id = dbCl.Id,
                    Type = (QueueType)dbCl.Type
                };
            }
        }

        public QueueData GetByClient(Guid clientId)
        {
            return Get(new QueueFilterParameters() {ClientId = clientId}).SingleOrDefault();
        }

        public ICollection<QueueData> Get(IFilterParameters filter)
        {
            QueueFilterParameters f = filter != null ? filter as QueueFilterParameters : null;

            using (var db = new ConversusDataContext())
            {
                IQueryable<Queues> query = db.Queues.AsQueryable();

                if (f != null && (f.ClientId.HasValue || f.QueueType.HasValue))
                {
                    if (f.ClientId.HasValue)
                        query = query.Where(q => q.Clients.Any(c => c.Id == f.ClientId.Value));
                
                    if (f.QueueType.HasValue)
                        query = query.Where(q => q.Type == (int)f.QueueType.Value);
                }

                var list = query.ToList();

                //TODO: set all properties
                return list.Select(c => new QueueData()
                                             {
                                                 Id = c.Id,
                                                 Type = (QueueType)c.Type,
                                             }).ToList();
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
    }
}
