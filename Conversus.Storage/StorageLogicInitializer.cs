﻿using System;
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
                {typeof(IQueueStorage), new FakeQueueStorage()},
                {typeof(IClientStorage), new FakeClientStorage()},
            };

            StorageLogicFactory.Instance.RegisterObjects(storageTypes);
        }
    }
}
