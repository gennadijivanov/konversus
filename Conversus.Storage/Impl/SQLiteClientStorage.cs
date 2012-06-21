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

            var db = new conversusEntities();

            //TODO
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

        public void Update(ClientData data)
        {
            const string updateCommandTpl = @"UPDATE [Clients] SET [QueueId]='{1}', [Name]='{2}', [BookingTime]={3}, 
                [TakeTicket]={4}, [PerformStart]={5}, [PerformEnd]={6}, [Status]={7}, [PIN]={8}, [Ticket]='{9}'
                WHERE [Id]='{0}';";

            using (var connection = _baseManager.GetConnection())
            {
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = string.Format(updateCommandTpl, data.Id, data.QueueId, data.Name,
                                                        GetDBdatetime(data.BookingTime),
                                                        GetDBdatetime(data.TakeTicket),
                                                        GetDBdatetime(data.PerformStart),
                                                        GetDBdatetime(data.PerformEnd),
                                                        (int) data.Status,
                                                        data.PIN.HasValue ? data.PIN.Value.ToString() : "NULL",
                                                        data.Ticket);
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        public ClientData Get(Guid id)
        {
            const string selectCommandTpl = @"SELECT [Id], [QueueId], [Name], [BookingTime], [TakeTicket],
                [PerformStart], [PerformEnd], [Status], [PIN], [Ticket] FROM [Clients] WHERE [Id]='{0}';";

            ClientData result = default(ClientData);

            using (var connection = _baseManager.GetConnection())
            {
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = string.Format(selectCommandTpl, id);
                    command.CommandType = CommandType.Text;
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        result = new ClientData()
                                   {
                                       Id = reader.GetGuid(0),
                                       QueueId = reader.GetGuid(1),
                                       Name = reader.GetString(2),
                                       BookingTime = reader.IsDBNull(3) ? null : (DateTime?) reader.GetDateTime(3),
                                       TakeTicket = reader.IsDBNull(4) ? null : (DateTime?) reader.GetDateTime(4),
                                       PerformStart = reader.IsDBNull(5) ? null : (DateTime?) reader.GetDateTime(5),
                                       PerformEnd = reader.IsDBNull(6) ? null : (DateTime?) reader.GetDateTime(6),
                                       Status = (ClientStatus) reader.GetInt32(7),
                                       PIN = reader.GetInt32(8),
                                       Ticket = reader.GetString(9)
                                   };
                    }
                    reader.Close();
                }
                connection.Close();
            }

            return result;
        }

        public ICollection<ClientData> Get(IFilterParameters filter)
        {
            ClientFilterParameters f = filter != null ? filter as ClientFilterParameters : null;

            var db = new conversusEntities();

            var query = db.Clients.AsQueryable();
            
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

        public void Delete(Guid id)
        {
            var db = new conversusEntities();

            var client = db.Clients.SingleOrDefault(c => c.Id == id);
            if (client != null)
                db.Clients.DeleteObject(client);
        }

        private static string GetDBdatetime(DateTime? dateTime) 
        {
            if (!dateTime.HasValue)
                return "NULL";
            return string.Format("'{0}'", dateTime.Value.ToString("yyyy-MM-dd hh:mm:ss"));
        }
    }
}
