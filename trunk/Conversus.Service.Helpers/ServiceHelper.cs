using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conversus.Service.Contract;

namespace Conversus.Service.Helpers
{
    public class ServiceHelper
    {
        private static ServiceHelper _instance;

        public static ServiceHelper Instance
        {
            get { return _instance ?? (_instance = new ServiceHelper()); }
        }

        public static IClientService ClientService
        {
            get { return ClientServiceBase<IClientService>.GetClient("ClientService"); }
        }

        public static IQueueService QueueService
        {
            get { return ClientServiceBase<IQueueService>.GetClient("QueueService"); }
        }

        private ServiceHelper()
        {
        }
    }
}
