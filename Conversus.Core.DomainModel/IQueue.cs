namespace Conversus.Core.DomainModel
{
    public enum QueueType
    {
        Approvement,
        Taking
    }

    public interface IQueue : IEntity
    {
        QueueType Type { get; set; }
    }
}
