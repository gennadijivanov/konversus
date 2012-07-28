using System;
using System.Collections.Generic;
using Conversus.Core.DomainModel;
using Conversus.Core.Infrastructure.Repository;

namespace Conversus.BusinessLogic
{
    public interface IOperatorLogic
    {
        ICollection<IOperator> GetAllUsers();

        IOperator Get(Guid id);

        ICollection<IOperator> Get(UserFilterParameters filter);

        void Create(Guid id, string name, string login, string password, string window, QueueType queueType);

        void Save(Guid id, string name, string login, string password, string window, QueueType queueType);

        void Delete(Guid id);

        void SetWindow(Guid id, string window);

        IOperator Authorize(string login, string password);
    }
}