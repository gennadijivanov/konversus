using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;
using System.IO;

namespace Conversus.Storage.Impl
{
    public class SQLiteBaseManager: IDisposable
    {
        public const string DefaultBaseName = "conversus.db3";
        private string _baseName;
        private SQLiteConnection _connection;

        private static SQLiteBaseManager _instance;

        public static SQLiteBaseManager GetInstance(string baseName = DefaultBaseName) 
        {
            return _instance ?? (_instance = new SQLiteBaseManager(baseName));
        }

        public SQLiteConnection Connection
        {
            get { return _connection ?? (_connection = new SQLiteConnection("Data Source = " + _baseName)); }
        }

        private SQLiteBaseManager(string baseName)
        {
            _baseName = baseName;
            CreateDatabase();
        }

        private void CreateDatabase()
        {
            if (File.Exists(_baseName))
            {
                Connection.Open();
                return;
            }

            SQLiteConnection.CreateFile(_baseName);

            Connection.Open();

            using (var command = new SQLiteCommand(Connection))
            {
                command.CommandType = CommandType.Text;

                command.CommandText = @"CREATE TABLE [Queues] (
                    [Id] UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
                    [Type] int NOT NULL
                );";
                command.ExecuteNonQuery();

                command.CommandText = @"CREATE TABLE [Clients] (
                    [Id] UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
                    [QueueId] UNIQUEIDENTIFIER NOT NULL,
                    [Name] NVARCHAR(500) NOT NULL,
                    [BookingTime] DATETIME NULL,
                    [TakeTicket] DATETIME NULL,
                    [PerformStart] DATETIME NULL,
                    [PerformEnd] DATETIME NULL,
                    [Status] INT NOT NULL,
                    [PIN] INT NULL,
                    [Ticket] NVARCHAR(500) NULL
                );";
                command.ExecuteNonQuery();
            }
        }

        public void Dispose()
        {
            Connection.Close();
        }
    }
}
