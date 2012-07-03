using System;
using System.Collections.Generic;
using System.Linq;
using Conversus.Core.Infrastructure.Repository;
using Conversus.Core.DomainModel;
using ClientImpl = Conversus.Impl.Objects.Client;
using ClientData = Conversus.Storage.Client;

namespace Conversus.Storage.Impl
{
    public class SQLiteClientStorage : IClientStorage
    {
        public void Create(IClient data)
        {
            if (data.PIN.HasValue)
            {
                var allWithSamePin = Get(new ClientFilterParameters() {PIN = data.PIN.Value});
                if (allWithSamePin.Count > 0)
                    throw new InvalidOperationException("Client with same PIN already exists");
            }

            using (var db = new ConversusDataContext())
            {
                var client = new ClientData()
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
            using (var db = new ConversusDataContext())
            {
                var dbCl = db.Clients.SingleOrDefault(c => c.Id == data.Id);

                if (dbCl == null)
                    return;

                //TODO: set field of data obj
                db.SaveChanges();
            }
        }

        public IClient Get(Guid id)
        {
            using (var db = new ConversusDataContext())
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

            using (var db = new ConversusDataContext())
            {
                IQueryable<ClientData> query = db.Clients.AsQueryable();

                if (f != null && (f.QueueId.HasValue || f.PIN.HasValue))
                {
                    if (f.QueueId.HasValue)
                        query = query.Where(c => c.QueueId == f.QueueId.Value);

                    if (f.PIN.HasValue)
                        query = query.Where(c => c.PIN == f.PIN.Value);
                }

                //TODO: set all properties
                return query.ToList().Select(ConvertFromData).ToList();
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

        //TODO: set all properties
        private IClient ConvertFromData(ClientData data)
        {
            return new ClientImpl(data.Id, data.Name, data.QueueId, data.BookingTime, data.PIN, (ClientStatus)data.Status,
                              data.Ticket);
        }
    }
}
