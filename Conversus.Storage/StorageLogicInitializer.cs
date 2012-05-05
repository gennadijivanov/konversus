using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conversus.Core.Storage.Impl;

namespace Conversus.Storage
{
    public static class StorageLogicInitializer
    {
        public static void Initialize()
        {
            Dictionary<Type, object> storageTypes = new Dictionary<Type, object>
            {
                {typeof(IQueueStorage), new FakeQueueStorage()},
            };

            StorageLogicFactory.Instance.RegisterObjects(storageTypes);
        }
    }
}
