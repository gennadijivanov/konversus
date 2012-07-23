using System;
using System.Collections.Generic;
using Conversus.Core.DomainModel;
using Conversus.Core.Infrastructure.Repository;

namespace Conversus.BusinessLogic
{
    public interface IUserLogic
    {
        ICollection<IUser> GetAllUsers();

        IUser Get(Guid id);

        ICollection<IUser> Get(UserFilterParameters filter);

        void Create(Guid id, string name, string login, string password, string window, QueueType queueType);

        void Save(Guid id, string name, string login, string password, string window, QueueType queueType);

        void Delete(Guid id);

        void SetWindow(Guid id, string window);

        IUser Authorize(string login, string password);
    }
}