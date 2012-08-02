using System;

namespace Conversus.Core.DomainModel
{
    public enum OperatorStatus
    {
        /// <summary>
        /// отключен
        /// </summary>
        Stop,

        /// <summary>
        /// работает
        /// </summary>
        Play,

        /// <summary>
        /// перерыв
        /// </summary>
        Pause
    }

    public interface IOperator : IEntity
    {
        string Name { get; set; }

        string Login { get; set; }

        string Password { get; set; }

        Guid QueueId { get; set; }

        string Window { get; set; }

        OperatorStatus Status { get; set; }
        
        DateTime ChangeTime { get; set; }
    }
}