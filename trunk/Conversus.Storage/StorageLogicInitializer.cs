using System;
using System.Collections.Generic;
using Conversus.Storage.Impl;

namespace Conversus.Storage
{
    public static class StorageLogicInitializer
    {
        public static void Initialize(string connectionString)
        {
            var storageTypes = new Dictionary<Type, object>
            {
                {typeof(IQueueStorage), new SQLiteQueueStorage(connectionString)},
                {typeof(IClientStorage), new SQLiteClientStorage(connectionString)},
                {typeof(IUserStorage), new SQLiteUserStorage(connectionString)},
            };

            StorageLogicFactory.Instance.RegisterObjects(storageTypes);
        }
    }
}
