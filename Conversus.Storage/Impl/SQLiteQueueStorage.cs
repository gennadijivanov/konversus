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
            const string createCommandTpl = @"INSERT INTO [Queues]([Id], [Type]) VALUES('{0}', {1});";

            using (var connection = _baseManager.GetConnection())
            {
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = string.Format(createCommandTpl, data.Id, (int)data.Type);
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public void Update(QueueData data)
        {
            const string updateCommandTpl = @"UPDATE [Queues] SET [Type]='{1}' WHERE [Id]='{0}';";

            using (var connection = _baseManager.GetConnection())
            {
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = string.Format(updateCommandTpl, data.Id, (int)data.Type);
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public QueueData Get(Guid id)
        {
            const string selectCommandTpl = @"SELECT [Id], [Type] FROM [Queues] WHERE [Id]='{0}';";

            QueueData result = default(QueueData);

            using (var connection = _baseManager.GetConnection())
            {
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = string.Format(selectCommandTpl, id);
                    command.CommandType = CommandType.Text;
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        result = new QueueData()
                        {
                            Id = reader.GetGuid(0),
                            Type = (QueueType)reader.GetInt32(1)
                        };
                    }
                    reader.Close();
                }
                connection.Close();
            }
            return result;
        }

        public QueueData GetByClient(Guid clientId)
        {
            const string selectCommandTpl =
                @"SELECT [q].[Id], [q].[Type] FROM [Queues] [q]
                INNER JOIN [Clients] [c] ON [q].[Id]=[c].[QueueId]
                WHERE [c].[Id]='{0}';";

            QueueData result = default(QueueData);

            using (var connection = _baseManager.GetConnection())
            {
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = string.Format(selectCommandTpl, clientId);
                    command.CommandType = CommandType.Text;
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        result = new QueueData()
                        {
                            Id = reader.GetGuid(0),
                            Type = (QueueType) reader.GetInt32(1)
                        };
                    }
                    reader.Close();
                }
                connection.Close();
            }
            return result;
        }

        public ICollection<QueueData> Get(IFilterParameters filter)
        {
            List<QueueData> result = new List<QueueData>();
            QueueFilterParameters f = filter != null ? filter as QueueFilterParameters : null;

            string selectCommand = @"SELECT [q].[Id], [q].[Type] FROM [Queues] [q] {0} {1}";
            string joins = "";
            string where = "";

            if (f != null && (f.ClientId.HasValue || f.QueueType.HasValue))
            {
                where = "WHERE";

                if (f.ClientId.HasValue)
                {
                    joins = " INNER JOIN [Clients] [c] ON [q].[Id]=[c].[QueueId]";
                    where += string.Format(" [c].[Id]='{0}'", f.ClientId.Value);
                }

                if (f.QueueType.HasValue)
                {
                    where += string.Format(" [q].[Type]={0}", (int)f.QueueType.Value);
                }
            }

            selectCommand = string.Format(selectCommand, joins, where);

            using (var connection = _baseManager.GetConnection())
            {
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = selectCommand;
                    command.CommandType = CommandType.Text;
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new QueueData()
                                       {
                                           Id = reader.GetGuid(0),
                                           Type = (QueueType) reader.GetInt32(1)
                                       });
                    }
                    reader.Close();
                }
                connection.Close();
            }
            return result;
        }

        public void Delete(Guid id)
        {
            const string deleteCommandTpl = @"DELETE FROM [Queues] WHERE [Id]='{0}';";

            using (var connection = _baseManager.GetConnection())
            {
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = string.Format(deleteCommandTpl, id);
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
    }
}
