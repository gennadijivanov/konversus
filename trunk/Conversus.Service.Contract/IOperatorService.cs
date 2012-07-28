using System;
using System.Collections.Generic;
using System.ServiceModel;
using Conversus.Core.DomainModel;

namespace Conversus.Service.Contract
{
    [ServiceContract]
    public interface IOperatorService
    {
        [OperationContract]
        ICollection<OperatorInfo> GetAllUsers();

        [OperationContract]
        OperatorInfo Get(Guid id);

        [OperationContract]
        void Create(string name, string login, string password, string window, QueueType queueType);

        [OperationContract]
        void Save(Guid id, string name, string login, string password, string window, QueueType queueType);

        [OperationContract]
        void Delete(Guid id);

        [OperationContract]
        OperatorInfo Authorize(string login, string password);

        [OperationContract]
        ICollection<OperatorInfo> GetUsersByQueue(QueueType type);

        [OperationContract]
        void PauseMaintenance(Guid id);

        [OperationContract]
        void ReopenMaintenance(Guid id);
    }
}