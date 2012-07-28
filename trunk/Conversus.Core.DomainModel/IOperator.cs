using System;

namespace Conversus.Core.DomainModel
{
    public interface IOperator : IEntity
    {
        string Name { get; set; }

        string Login { get; set; }

        string Password { get; set; }

        Guid QueueId { get; set; }

        string Window { get; set; }
    }
}