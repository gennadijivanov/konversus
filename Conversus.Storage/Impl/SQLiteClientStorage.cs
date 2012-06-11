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
        private SQLiteBaseManager _baseManager;

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
            const string createCommandTpl = @"INSERT INTO [Clients]([Id], [QueueId], [Name], [BookingTime], [TakeTicket], 
                [PerformStart], [PerformEnd], [Status], [PIN], [Ticket])
                VALUES('{0}', '{1}', '{2}', {3}, {4}, {5}, {6}, {7}, {8}, '{9}');";

            using(var connection = _baseManager.Connection)
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = string.Format(createCommandTpl, data.Id, data.QueueId, data.Name,
                    GetDBdatetime(data.BookingTime),
                    GetDBdatetime(data.TakeTicket),
                    GetDBdatetime(data.PerformStart),
                    GetDBdatetime(data.PerformEnd),
                    (int)data.Status,
                    data.PIN.HasValue ? data.PIN.Value.ToString() : "NULL",
                    data.Ticket);
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();
            }
        }

        public void Update(ClientData data)
        {
            const string updateCommandTpl = @"UPDATE [Clients] SET [QueueId]='{1}', [Name]='{2}', [BookingTime]={3}, 
                [TakeTicket]={4}, [PerformStart]={5}, [PerformEnd]={6}, [Status]={7}, [PIN]={8}, [Ticket]='{9}'
                WHERE [Id]='{0}';";

            using (var connection = _baseManager.Connection)
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = string.Format(updateCommandTpl, data.Id, data.QueueId, data.Name,
                    GetDBdatetime(data.BookingTime),
                    GetDBdatetime(data.TakeTicket),
                    GetDBdatetime(data.PerformStart),
                    GetDBdatetime(data.PerformEnd), 
                    (int)data.Status,
                    data.PIN.HasValue ? data.PIN.Value.ToString() : "NULL",
                    data.Ticket);
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();
            }
        }

        public ClientData Get(Guid id)
        {
            const string selectCommandTpl = @"SELECT [Id], [QueueId], [Name], [BookingTime], [TakeTicket],
                [PerformStart], [PerformEnd], [Status], [PIN], [Ticket] FROM [Clients] WHERE [Id]='{0}';";

            using (var connection = _baseManager.Connection)
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = string.Format(selectCommandTpl, id);
                command.CommandType = CommandType.Text;
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return new ClientData()
                    {
                        Id = reader.GetGuid(0),
                        QueueId = reader.GetGuid(1),
                        Name = reader.GetString(2),
                        BookingTime = reader.IsDBNull(3) ? null : (DateTime?)reader.GetDateTime(3),
                        TakeTicket = reader.IsDBNull(4) ? null : (DateTime?)reader.GetDateTime(4),
                        PerformStart = reader.IsDBNull(5) ? null : (DateTime?)reader.GetDateTime(5),
                        PerformEnd = reader.IsDBNull(6) ? null : (DateTime?)reader.GetDateTime(6),
                        Status = (ClientStatus)reader.GetInt32(7),
                        PIN = reader.GetInt32(8),
                        Ticket = reader.GetString(9)
                    };
                }
                return new ClientData();
            }
        }

        public ICollection<ClientData> Get(IFilterParameters filter)
        {
            List<ClientData> result = new List<ClientData>();
            ClientFilterParameters f = filter != null ? filter as ClientFilterParameters : null;

            string selectCommand = @"SELECT [c].[Id], [c].[QueueId], [c].[Name], [c].[BookingTime], [c].[TakeTicket],
                [c].[PerformStart], [c].[PerformEnd], [c].[Status], [c].[PIN], [c].[Ticket] FROM [Clients] [c] {0};";
            string where = string.Empty;

            if (f != null && (f.QueueId.HasValue || f.PIN.HasValue))
            {
                where = "WHERE ";
                if (f.QueueId.HasValue)
                    where += string.Format("[c].[QueueId]='{0}' ", f.QueueId.Value);
                
                if (f.PIN.HasValue)
                    where += string.Format("[c].[PIN]='{0}' ", f.PIN.Value);
            }

            selectCommand = string.Format(selectCommand, where);

            using (var connection = _baseManager.Connection)
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = selectCommand;
                command.CommandType = CommandType.Text;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new ClientData()
                    {
                        Id = reader.GetGuid(0),
                        QueueId = reader.GetGuid(1),
                        Name = reader.GetString(2),
                        BookingTime = reader.IsDBNull(3) ? null : (DateTime?)reader.GetDateTime(3),
                        TakeTicket = reader.IsDBNull(4) ? null : (DateTime?)reader.GetDateTime(4),
                        PerformStart = reader.IsDBNull(5) ? null : (DateTime?)reader.GetDateTime(5),
                        PerformEnd = reader.IsDBNull(6) ? null : (DateTime?)reader.GetDateTime(6),
                        Status = (ClientStatus)reader.GetInt32(7),
                        PIN = reader.GetInt32(8),
                        Ticket = reader.GetString(9)
                    });
                }
                return result;
            }
        }

        public void Delete(Guid id)
        {
            const string deleteCommandTpl = @"DELETE FROM [Clients] WHERE [Id]='{0}';";

            using (var connection = _baseManager.Connection)
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = string.Format(deleteCommandTpl, id);
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();
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
