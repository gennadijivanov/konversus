using Conversus.Core.DomainModel;

namespace Conversus.Core.Infrastructure.Repository
{
    public class OperatorFilterParameters : IFilterParameters
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public QueueType? QueueType { get; set; }

        public OperatorStatus? Status { get; set; }
    }
}