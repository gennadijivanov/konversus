using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conversus.Core.DTO;
using Conversus.Core.Infrastructure.Repository;
using System.Data.SQLite;
using System.Data;
using Conversus.Core.DomainModel;

namespace Conversus.Storage.Impl
{
    public class SQLiteClientStorage : IClientStorage
    {
        private readonly SQLiteBaseManager _baseManager;

        public SQLiteClientStorage()
            : this(SQLiteBaseManager.GetInstance())
        {
        }

        public SQLiteClientStorage(SQLiteBaseManager baseManager)
        {
            _baseManager = baseManager;
        }

        public void Create(ClientData data)
        {
            if (data.PIN.HasValue)
            {
                var allWithSamePin = Get(new ClientFilterParameters(){PIN = data.PIN.Value});
                if (allWithSamePin.Count > 0)
                    throw new InvalidOperationException("Client with same PIN already exists");
            }

            using (var db = new ConversusDataContext())
            {
                Clients client = new Clients()
                                     {
                                         Id = data.Id,
                                         Name = data.Name,
                                         PIN = data.PIN,
                                         QueueId = data.QueueId
                                     };
                db.AddToClients(client);
                db.SaveChanges();
            }
        }

        public void Update(ClientData data)
        {
            using (var db = new ConversusDataContext())
            {
                var dbCl = db.Clients.SingleOrDefault(c => c.Id == data.Id);

                if (dbCl == null)
                    return;

                //TODO: set field of data obj
                db.SaveChanges();
            }
        }

        public ClientData Get(Guid id)
        {
            using (var db = new ConversusDataContext())
            {
                var dbCl = db.Clients.SingleOrDefault(c => c.Id == id);

                if (dbCl == null)
                    return default(ClientData);

                //TODO: set all properties
                return  new ClientData()
                {
                    Id = dbCl.Id,
                    Name = dbCl.Name,
                    QueueId = dbCl.QueueId,
                    PIN = dbCl.PIN
                };
            }
        }

        public ICollection<ClientData> Get(IFilterParameters filter)
        {
            ClientFilterParameters f = filter != null ? filter as ClientFilterParameters : null;

            using (var db = new ConversusDataContext())
            {
                IQueryable<Clients> query = db.Clients.AsQueryable();

                if (f != null && (f.QueueId.HasValue || f.PIN.HasValue))
                {
                    if (f.QueueId.HasValue)
                        query = query.Where(c => c.QueueId == f.QueueId.Value);
                
                    if (f.PIN.HasValue)
                        query = query.Where(c => c.PIN == f.PIN.Value);
                }

                //TODO: set all properties
                return query.ToList().Select(c => new ClientData()
                                             {
                                                 Id = c.Id,
                                                 Name = c.Name,
                                                 QueueId = c.QueueId,
                                                 PIN = c.PIN
                                             }).ToList();
            }
        }

        public void Delete(Guid id)
        {
            using (var db = new ConversusDataContext())
            {
                var client = db.Clients.SingleOrDefault(c => c.Id == id);
                if (client != null)
                    db.Clients.DeleteObject(client);
            }
        }

        private static string GetDBdatetime(DateTime? dateTime) 
        {
            if (!dateTime.HasValue)
                return "NULL";
            return string.Format("'{0}'", dateTime.Value.ToString("yyyy-MM-dd hh:mm:ss"));
        }
    }
}
