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

        public IClientService ClientService
        {
            get { return ClientServiceBase<IClientService>.GetClient("IClientService"); }
        }

        public IQueueService QueueService
        {
            get { return ClientServiceBase<IQueueService>.GetClient("IQueueService"); }
        }

        public IUserService UserService
        {
            get { return ClientServiceBase<IUserService>.GetClient("IUserService"); }
        }

        private ServiceHelper()
        {
        }
    }
}
