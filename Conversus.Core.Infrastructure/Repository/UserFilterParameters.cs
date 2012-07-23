using Conversus.Core.DomainModel;

namespace Conversus.Core.Infrastructure.Repository
{
    public class UserFilterParameters : IFilterParameters
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public QueueType? QueueType { get; set; }
    }
}