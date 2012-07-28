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
                                     BookingTime = data.BookingTime,
                                     TakeTicket = data.TakeTicket,
                                     Ticket = data.Ticket,
                                     PerformStart = data.PerformStart,
                                     PerformEnd = data.PerformEnd
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
                dbCl.PerformEnd = data.PerformEnd;
                dbCl.PerformStart = data.PerformStart;
                dbCl.QueueId = data.QueueId;
                dbCl.Status = (int)data.Status;
                dbCl.TakeTicket = data.TakeTicket;
                dbCl.Ticket = data.Ticket;

                db.SaveChanges();
            }
        }

        public IClient Get(Guid id)
        {
            using (var db = GetDataContext())
            {
                var dbCl = db.Clients.SingleOrDefault(c => c.Id == id);

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

                return query.ToList().Select(ConvertFromData).ToList();
            }
        }

        public void Delete(Guid id)
        {
            using (var db = GetDataContext())
            {
                var client = db.Clients.SingleOrDefault(c => c.Id == id);
                if (client != null)
                    db.Clients.DeleteObject(client);
            }
        }

        private IClient ConvertFromData(ClientData data)
        {
            var client = new ClientImpl(data.Id, data.Name, data.QueueId, data.BookingTime, data.PIN, (ClientStatus)data.Status,
                data.Ticket);
            client.TakeTicket = data.TakeTicket;
            return client;
        }
    }
}
