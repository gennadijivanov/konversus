using System;
using System.Collections.Generic;
using System.ServiceModel;
using Conversus.Core.DomainModel;

namespace Conversus.Service.Contract
{
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        ICollection<UserInfo> GetAllUsers();

        [OperationContract]
        UserInfo Get(Guid id);

        [OperationContract]
        void Create(string name, string login, string password, string window, QueueType queueType);

        [OperationContract]
        void Save(Guid id, string name, string login, string password, string window, QueueType queueType);

        [OperationContract]
        void Delete(Guid id);

        [OperationContract]
        UserInfo Authorize(string login, string password);
    }
}