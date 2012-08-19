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
                {typeof(IOperatorStorage), new SQLiteOperatorStorage(connectionString)},
                {typeof(IPropertyStorage), new SQLitePropertyStorage(connectionString)},
            };

            StorageLogicFactory.Instance.RegisterObjects(storageTypes);
        }
    }
}
