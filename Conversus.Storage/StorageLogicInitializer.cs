using System;
using System.Collections.Generic;
using Conversus.Storage.Impl;

namespace Conversus.Storage
{
    public static class StorageLogicInitializer
    {
        public static void Initialize()
        {
            var storageTypes = new Dictionary<Type, object>
            {
                {typeof(IQueueStorage), new SQLiteQueueStorage()},
                {typeof(IClientStorage), new SQLiteClientStorage()},
            };

            StorageLogicFactory.Instance.RegisterObjects(storageTypes);
        }
    }
}
