using System;
using System.Collections.Generic;
using Conversus.Core.DomainModel;

namespace Conversus.BusinessLogic
{
    public interface IUserLogic
    {
        ICollection<IUser> GetAllUsers();

        IUser Get(Guid id);

        void Create(Guid id, string name, string login, string password, QueueType queueType);

        void Delete(Guid id);

        void SetWindow(Guid id, string window);
    }
}