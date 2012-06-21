using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;
using System.IO;

namespace Conversus.Storage.Impl
{
    public class SQLiteBaseManager
    {
        public const string DefaultBaseName = "conversus.db3";
        private readonly string _baseName;

        private static SQLiteBaseManager _instance;

        public static SQLiteBaseManager GetInstance(string baseName = DefaultBaseName) 
        {
            return _instance ?? (_instance = new SQLiteBaseManager(baseName));
        }

        public SQLiteConnection GetConnection()
        {
            SQLiteFactory factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
            SQLiteConnection conn = (SQLiteConnection)factory.CreateConnection();

            conn.ConnectionString = "Data Source = " + _baseName;
            //var conn = new SQLiteConnection("Data Source = " + _baseName);
            conn.Open();
            return conn;
        }

        private SQLiteBaseManager(string baseName)
        {
            _baseName = baseName;
            CreateDatabase();
        }

        private void CreateDatabase()
        {
            if (File.Exists(_baseName))
                return;

            SQLiteConnection.CreateFile(_baseName);

            

            using (var connection = GetConnection())
            {
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandType = CommandType.Text;

                    command.CommandText =
                        @"CREATE TABLE [Queues] (
                    [Id] UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
                    [Type] int NOT NULL
                );";
                    command.ExecuteNonQuery();

                    command.CommandText =
                        @"CREATE TABLE [Clients] (
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

                connection.Close();
            }
        }
    }
}
