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
        public IDisposable CreateContext()
        {
            return new ConversusDataContext();
        }

        public void Create(object context, ClientData data)
        {
            if (data.PIN.HasValue)
            {
                var allWithSamePin = Get(new ClientFilterParameters() { PIN = data.PIN.Value });
                if (allWithSamePin.Count > 0)
                    throw new InvalidOperationException("Client with same PIN already exists");
            }

            var db = (ConversusDataContext)context;

            Clients client = new Clients()
                                 {
                                     Id = data.Id,
                                     Name = data.Name,
                                     PIN = data.PIN,
                                     QueueId = data.QueueId,
                                     Status = (int)data.Status
                                 };
            db.AddToClients(client);
            db.SaveChanges();
        }

        public void Update(object context, ClientData data)
        {
            var db = (ConversusDataContext)context;

            var dbCl = db.Clients.SingleOrDefault(c => c.Id == data.Id);

            if (dbCl == null)
                return;

            //TODO: set field of data obj
            db.SaveChanges();
        }

        public ClientData Get(Guid id)
        {
            using (var db = new ConversusDataContext())
            {
                var dbCl = db.Clients.SingleOrDefault(c => c.Id == id);

                if (dbCl == null)
                    return default(ClientData);

                //TODO: set all properties
                return new ClientData()
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

        public void Delete(object context, Guid id)
        {
            var db = (ConversusDataContext)context;

            var client = db.Clients.SingleOrDefault(c => c.Id == id);
            if (client != null)
                db.Clients.DeleteObject(client);
        }
    }
}
