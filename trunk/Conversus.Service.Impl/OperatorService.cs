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
        private IOperatorLogic _userLogic;
        private IOperatorLogic UserLogic
        {
            get { return _userLogic ?? (_userLogic = BusinessLogicFactory.Instance.Get<IOperatorLogic>()); }
        }

        private static IQueueLogic _queueLogic;
        private static IQueueLogic QueueLogic
        {
            get { return _queueLogic ?? (_queueLogic = BusinessLogicFactory.Instance.Get<IQueueLogic>()); }
        }

        public ICollection<OperatorInfo> GetAllUsers()
        {
            return UserLogic.GetAllUsers().Select(ToUserInfo).ToList();
        }

        public OperatorInfo Get(Guid id)
        {
            return ToUserInfo(UserLogic.Get(id));
        }

        public void Create(string name, string login, string password, string window, QueueType queueType)
        {
            UserLogic.Create(Guid.NewGuid(), name, login, password, window, queueType);
        }

        public void Save(Guid id, string name, string login, string password, string window, QueueType queueType)
        {
            UserLogic.Save(id, name, login, password, window, queueType);
        }

        public void Delete(Guid id)
        {
            UserLogic.Delete(id);
        }

        public OperatorInfo Authorize(string login, string password)
        {
            return ToUserInfo(UserLogic.Authorize(login, password));
        }

        public ICollection<OperatorInfo> GetUsersByQueue(QueueType type)
        {
            return UserLogic.Get(new UserFilterParameters() {QueueType = type}).Select(ToUserInfo).ToList();
        }

        public void PauseMaintenance(Guid id)
        {
            //TODO NOT EMPLEMENTED
            throw new NotImplementedException();
        }

        public void ReopenMaintenance(Guid id)
        {
            //TODO NOT EMPLEMENTED
            throw new NotImplementedException();
        }

        public static OperatorInfo ToUserInfo(IOperator user)
        {
            if (user == null)
                return null;

            var queue = QueueLogic.Get(user.QueueId);

            return new OperatorInfo()
                       {
                           Id = user.Id,
                           Name = user.Name,
                           Login = user.Login,
                           Password = user.Password,
                           CurrentWindow = user.Window,
                           Queue = new QueueInfo(queue.Id, queue.Type, QueueLogic.GetTitle(queue.Type))
                       };
        }
    }
}