using System;
using System.Collections.Generic;
using Conversus.Core.DomainModel;

namespace Conversus.BusinessLogic
{
    public interface IUserLogic
    {
        ICollection<IUser> GetAllUsers();

        IUser Get(Guid id);

        void Create(Guid id, string name, string login, string password, string window, QueueType queueType);

        void Save(Guid id, string name, string login, string password, string window);

        void Delete(Guid id);

        void SetWindow(Guid id, string window);

        bool Authorize(string login, string password);
    }
}