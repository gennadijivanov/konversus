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

        public SQLiteClientStorage(SQLiteBaseManager baseManager)
        {
            _baseManager = baseManager;
        }

        public void Create(ClientData data)
        {
            const string createCommandTpl = @"INSERT INTO [Clients]([Id], [QueueId], [Name], [BookingTime], [TakeTicket], 
                [PerformStart], [PerformEnd], [Status], [PIN], [Ticket])
                VALUES('{0}', '{1}', '{2}', {3}, {4}, {5}, {6}, {7}, {8}, '{9}');";

            using (var command = new SQLiteCommand(_baseManager.Connection))
            {
                command.CommandText = string.Format(createCommandTpl, data.Id, data.QueueId, data.Name,
                    GetDBdatetime(data.BookingTime),
                    GetDBdatetime(data.TakeTicket),
                    GetDBdatetime(data.PerformStart),
                    GetDBdatetime(data.PerformEnd),
                    (int)data.Status, data.PIN, data.Ticket);
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();
            }
        }

        public void Update(ClientData data)
        {
            const string updateCommandTpl = @"UPDATE [Clients] SET [QueueId]='{1}', [Name]='{2}', [BookingTime]='{3}', 
                [TakeTicket]='{4}', [PerformStart]='{5}', [PerformEnd]='{6}', [Status]={7}, [PIN]={8}, [Ticket]={9} 
                WHERE [Id]='{0}';";

            using (var command = new SQLiteCommand(_baseManager.Connection))
            {
                command.CommandText = string.Format(updateCommandTpl, data.Id, data.QueueId, data.Name, data.BookingTime,
                    data.TakeTicket, data.PerformStart, data.PerformEnd, (int)data.Status, data.PIN,  data.Ticket);
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();
            }
        }

        public ClientData Get(Guid id)
        {
            const string selectCommandTpl = @"SELECT [Id], [QueueId], [Name], [BookingTime], [TakeTicket],
                [PerformStart], [PerformEnd], [Status], [PIN], [Ticket] FROM [Clients] WHERE [Id]='{0}';";

            using (var command = new SQLiteCommand(_baseManager.Connection))
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

        //public ICollection<QueueData> Get(IFilterParameters filter)
        //{
        //    List<QueueData> result = new List<QueueData>();
        //    QueueFilterParameters f = filter as QueueFilterParameters;

        //    string selectCommand = @"SELECT [q].[Id], [q].[Type] FROM [Queues] [q] {0} {1}";
        //    string joins = "";
        //    string where = "";

        //    if (f.ClientId.HasValue || f.QueueType.HasValue)
        //    {
        //        where = "WHERE";

        //        if (f.ClientId.HasValue)
        //        {
        //            joins = " INNER JOIN [Clients] [c] ON [q].[Id]=[c].[QueueId]";
        //            where += string.Format(" [c].[Id]='{0}'", f.ClientId.Value);
        //        }

        //        if (f.QueueType.HasValue)
        //        {
        //            where += string.Format(" [q].[Type]={0}", (int)f.QueueType.Value);
        //        }
        //    }

        //    selectCommand = string.Format(selectCommand, joins, where);

        //    using (var command = new SQLiteCommand(_baseManager.Connection))
        //    {
        //        command.CommandText = selectCommand;
        //        command.CommandType = CommandType.Text;
        //        var reader = command.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            result.Add(new QueueData()
        //            {
        //                Id = reader.GetGuid(0),
        //                Type = (QueueType)reader.GetInt32(1)
        //            });
        //        }
        //        return result;
        //    }
        //}

        public void Delete(Guid id)
        {
            const string deleteCommandTpl = @"DELETE FROM [Clients] WHERE [Id]='{0}';";

            using (var command = new SQLiteCommand(_baseManager.Connection))
            {
                command.CommandText = string.Format(deleteCommandTpl, id);
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();
            }
        }


        public ICollection<ClientData> Get(IFilterParameters filter)
        {
            throw new NotImplementedException();
        }

        private static string GetDBdatetime(DateTime? dateTime) 
        {
            if (!dateTime.HasValue)
                return "NULL";
            return string.Format("'{0}'", dateTime.Value.ToString("yyyy-MM-dd hh:mm:ss"));
        }
    }
}
