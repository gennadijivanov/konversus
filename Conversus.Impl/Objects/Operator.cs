using System;
using Conversus.Core.DomainModel;

namespace Conversus.Impl.Objects
{
    public class Operator : IOperator
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public Guid QueueId { get; set; }

        public string Window { get; set; }

        public Operator(Guid id, string name, string login, string password, string window, Guid queueId)
        {
            Id = id;
            Name = name;
            Login = login;
            Password = password;
            QueueId = queueId;
            Window = window;
        }
    }
}