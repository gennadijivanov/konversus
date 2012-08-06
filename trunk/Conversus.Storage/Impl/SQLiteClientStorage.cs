using System;
using System.Collections.Generic;
using System.Linq;
using Conversus.Core.Infrastructure.Repository;
using Conversus.Core.DomainModel;
using ClientImpl = Conversus.Impl.Objects.Client;
using ClientData = Conversus.Storage.Clients;

namespace Conversus.Storage.Impl
{
    public class SQLiteClientStorage : SQLiteStorageBase, IClientStorage
    {
        public SQLiteClientStorage(string connectionString) : base(connectionString)
        {
        }

        public void Create(IClient data)
        {
            using (var db = GetDataContext())
            {
                var client = new ClientData
                                 {
                                     Id = data.Id,
                                     Name = data.Name,
                                     PIN = data.PIN,
                                     QueueId = data.QueueId,
                                     Status = (int) data.Status,
                                     ChangeStatusTime = data.ChangeTime,
                                     BookingTime = data.BookingTime,
                                     Ticket = data.Ticket
                                 };
                db.AddToClients(client);
                db.SaveChanges();
            }
        }

        public void Update(IClient data)
        {
            data.ChangeTime = DateTime.Now;
            Create(data);
        }

        public IClient Get(Guid id)
        {
            using (var db = GetDataContext())
            {
                var dbCl = db.Clients.OrderByDescending(c => c.ChangeStatusTime).FirstOrDefault(c => c.Id == id);

                if (dbCl == null)
                    return null;

                return ConvertFromData(dbCl);
            }
        }

        public ICollection<IClient> Get(IFilterParameters filter)
        {
            ClientFilterParameters f = filter != null ? filter as ClientFilterParameters : null;

            using (var db = GetDataContext())
            {
                IEnumerable<ClientData> query = db.Clients;

                var lookUp = query.ToLookup(c => c.Id, c => c);
                query = lookUp.Select(client => client.OrderByDescending(c => c.ChangeStatusTime).First());

                if (f != null)
                {
                    if (f.QueueId.HasValue)
                        query = query.Where(c => c.QueueId == f.QueueId.Value);

                    if (f.PIN.HasValue)
                        query = query.Where(c => c.PIN == f.PIN.Value);

                    if (!string.IsNullOrEmpty(f.Ticket))
                        query = query.Where(c => c.Ticket == f.Ticket);

                    if (f.Status.HasValue)
                        query = query.Where(c => c.Status == (int)f.Status.Value);
                }

                return query.Select(ConvertFromData).ToList();
            }
        }

        public void Delete(Guid id)
        {
            using (var db = GetDataContext())
            {
                var client = db.Clients.Where(c => c.Id == id);
                if (!client.Any())
                    return;
                
                foreach (Clients clientRecord in client)
                {
                    db.Clients.DeleteObject(clientRecord);
                }
            }
        }

        public void ChangeQueue(Guid clientId, Guid targetOperatorId, SortPriority sortPriority)
        {
            using (var db = GetDataContext())
            {
                var dbCl = db.Clients.OrderByDescending(c => c.ChangeStatusTime).FirstOrDefault(c => c.Id == clientId);

                if (dbCl == null)
                    return;

                dbCl.OperatorId = targetOperatorId;
                dbCl.QueueId = db.Operators.Single(o => o.Id == targetOperatorId).QueueId;
                dbCl.SortPriority = (int)sortPriority;
                dbCl.ChangeStatusTime = DateTime.Now;

                db.AddToClients(dbCl);
                db.SaveChanges();
            }
        }
        
        private IClient ConvertFromData(ClientData data)
        {
            var client = new ClientImpl(data.Id, data.Name, data.QueueId, data.BookingTime, data.ChangeStatusTime,
                data.PIN, (ClientStatus)data.Status, (SortPriority)data.SortPriority, data.Ticket);
            return client;
        }
    }
}
