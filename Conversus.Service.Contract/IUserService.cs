using System;
using System.Collections.Generic;
using System.ServiceModel;
using Conversus.Core.DomainModel;

namespace Conversus.Service.Contract
{
    [ServiceContract]
    public interface IUserService
    {
        ICollection<UserInfo> GetAllUsers();

        UserInfo Get(Guid id);

        void Create(Guid id, string name, string login, string password, QueueType queueType);

        void Delete(Guid id);

        void SetWindow(Guid id, string window);
    }
}