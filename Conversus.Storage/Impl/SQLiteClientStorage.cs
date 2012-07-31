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
            if (data.PIN.HasValue)
            {
                var allWithSamePin = Get(new ClientFilterParameters {PIN = data.PIN.Value});
                if (allWithSamePin.Count > 0)
                    throw new InvalidOperationException("Client with same PIN already exists");
            }

            using (var db = GetDataContext())
            {
                var client = new ClientData
                                 {
                                     Id = data.Id,
                                     Name = data.Name,
                                     PIN = data.PIN,
                                     QueueId = data.QueueId,
                                     Status = (int) data.Status,
                                     ChangeStatusTime = DateTime.Now,
                                     BookingTime = data.BookingTime,
                                     Ticket = data.Ticket
                                 };
                db.AddToClients(client);
                db.SaveChanges();
            }
        }

        public void Update(IClient data)
        {
            using (var db = GetDataContext())
            {
                var dbCl = db.Clients.SingleOrDefault(c => c.Id == data.Id);

                if (dbCl == null)
                    return;

                dbCl.BookingTime = data.BookingTime;
                dbCl.Name = data.Name;
                dbCl.PIN = data.PIN;
                dbCl.QueueId = data.QueueId;
                dbCl.Status = (int)data.Status;
                dbCl.Ticket = data.Ticket;
                dbCl.ChangeStatusTime = DateTime.Now;
                dbCl.SortPriority = (int)data.SortPriority;

                db.AddToClients(dbCl);
                db.SaveChanges();
            }
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
                IQueryable<ClientData> query = db.Clients.AsQueryable();

                if (f != null)
                {
                    if (f.QueueId.HasValue)
                        query = query.Where(c => c.QueueId == f.QueueId.Value);

                    if (f.PIN.HasValue)
                        query = query.Where(c => c.PIN == f.PIN.Value);

                    if (!string.IsNullOrEmpty(f.Ticket))
                        query = query.Where(c => c.Ticket == f.Ticket);
                }

                var lookUp = query.ToLookup(c => c.Id, c => c);
                return lookUp.Select(client => ConvertFromData(client.OrderByDescending(c => c.ChangeStatusTime).First())).ToList();
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

        public IClient ChangeQueue(Guid clientId, Guid targetOperatorId, SortPriority sortPriority)
        {
            using (var db = GetDataContext())
            {
                var dbCl = db.Clients.OrderByDescending(c => c.ChangeStatusTime).FirstOrDefault(c => c.Id == clientId);

                if (dbCl == null)
                    return null;

                dbCl.OperatorId = targetOperatorId;
                dbCl.QueueId = db.Operators.Single(o => o.Id == targetOperatorId).QueueId;
                dbCl.SortPriority = (int)sortPriority;
                dbCl.ChangeStatusTime = DateTime.Now;

                db.AddToClients(dbCl);
                db.SaveChanges();

                return ConvertFromData(dbCl);
            }
        }
        
        private IClient ConvertFromData(ClientData data)
        {
            var client = new ClientImpl(data.Id, data.Name, data.QueueId, data.BookingTime, 
                data.PIN, (ClientStatus)data.Status, (SortPriority)data.SortPriority, data.Ticket);
            return client;
        }
    }
}
