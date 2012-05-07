using Conversus.Core.Infrastructure.UnitOfWork;
using Conversus.Impl.Repositories;
using Conversus.Core.DomainModel;

namespace Conversus.Impl.Factories
{
    public static class RepositoryFactory
    {
        private static readonly IQueueRepository QueueRepository = new QueueRepository();
        public static IQueueRepository GetQueueRepository(IUnitOfWork unitOfWork = null)
        {
            return unitOfWork == null ? QueueRepository : new QueueRepository(unitOfWork);
        }

        private static readonly IClientRepository ClientRepository = new ClientRepository();
        public static IClientRepository GetClientRepository(IUnitOfWork unitOfWork = null)
        {
            return unitOfWork == null ? ClientRepository : new ClientRepository(unitOfWork);
        }

        private static readonly IClientFactory ClientFactory = new ClientFactory();
        public static IClientFactory GetClientFactory()
        {
            return ClientFactory;
        }

        private static readonly IQueueFactory QueueFactory = new QueueFactory();
        public static IQueueFactory GetQueueFactory()
        {
            return QueueFactory;
        }
    }
}
