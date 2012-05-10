﻿using Conversus.Core.Infrastructure;

namespace Conversus.Storage
{
    public class StorageLogicFactory : LogicFactory
    {
        private static LogicFactory _instance;

        private StorageLogicFactory()
        {
        }

        public static LogicFactory Instance
        {
            get { return _instance ?? (_instance = new StorageLogicFactory()); }
        }
    }
}
