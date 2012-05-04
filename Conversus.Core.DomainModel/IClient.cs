using System;

namespace Conversus.Core.DomainModel
{
    public enum ClientStatus
    {
        Performing,
        Waiting,
        Registered
    }

    public interface IClient : IEntity
    {
        string Name { get; set; }

        DateTime Deadline { get; set; }

        ClientStatus Status { get; set; }

        int PIN { get; set; }

        string Ticket { get; set; }

        IQueue GetQueue();
    }
}
