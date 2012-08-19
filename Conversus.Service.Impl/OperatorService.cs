using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Conversus.BusinessLogic;
using Conversus.Core.DomainModel;
using Conversus.Core.Infrastructure.Repository;
using Conversus.Service.Contract;

namespace Conversus.Service.Impl
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class OperatorService : IOperatorService
    {
        private IOperatorLogic _operatorLogic;
        private IOperatorLogic OperatorLogic
        {
            get { return _operatorLogic ?? (_operatorLogic = BusinessLogicFactory.Instance.Get<IOperatorLogic>()); }
        }

        private static IQueueLogic _queueLogic;
        private static IQueueLogic QueueLogic
        {
            get { return _queueLogic ?? (_queueLogic = BusinessLogicFactory.Instance.Get<IQueueLogic>()); }
        }

        public ICollection<OperatorInfo> GetAllUsers()
        {
            return OperatorLogic.GetAllUsers().Select(ToOperatorInfo).ToList();
        }

        public OperatorInfo Get(Guid id)
        {
            return ToOperatorInfo(OperatorLogic.Get(id));
        }

        public void Create(string name, string login, string password, string window, QueueType queueType)
        {
            OperatorLogic.Create(Guid.NewGuid(), name, login, password, window, queueType);
        }

        public void Save(Guid id, string name, string login, string password, string window, QueueType queueType)
        {
            OperatorLogic.Save(id, name, login, password, window, queueType);
        }

        public void Delete(Guid id)
        {
            OperatorLogic.Delete(id);
        }

        public OperatorInfo Login(string login, string password)
        {
            return ToOperatorInfo(OperatorLogic.Login(login, password));
        }

        public void Logout(Guid id)
        {
            OperatorLogic.Logout(id);
        }

        public ICollection<OperatorInfo> GetUsersByQueue(QueueType type)
        {
            return OperatorLogic.Get(new OperatorFilterParameters {QueueType = type}).Select(ToOperatorInfo).ToList();
        }

        public void PauseMaintenance(Guid id)
        {
            OperatorLogic.ChangeStatus(id, OperatorStatus.Pause);
        }

        public void ReopenMaintenance(Guid id)
        {
            OperatorLogic.ChangeStatus(id, OperatorStatus.Play);
        }

        public static OperatorInfo ToOperatorInfo(IOperator oper)
        {
            if (oper == null)
                return null;

            var queue = QueueLogic.Get(oper.QueueId);

            return new OperatorInfo()
                       {
                           Id = oper.Id,
                           Name = oper.Name,
                           Login = oper.Login,
                           Password = oper.Password,
                           CurrentWindow = oper.Window,
                           Queue = new QueueInfo(queue.Id, queue.Type, QueueLogic.GetTitle(queue.Type)),
                           Status = oper.Status
                       };
        }
    }
}