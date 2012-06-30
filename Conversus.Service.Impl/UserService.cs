using System;
using System.Collections.Generic;
using System.Linq;
using Conversus.BusinessLogic;
using Conversus.Core.DomainModel;
using Conversus.Service.Contract;

namespace Conversus.Service.Impl
{
    public class UserService : IUserService
    {
        private IUserLogic _userLogic;
        private IUserLogic UserLogic
        {
            get { return _userLogic ?? (_userLogic = BusinessLogicFactory.Instance.Get<IUserLogic>()); }
        }

        public ICollection<UserInfo> GetAllUsers()
        {
            return UserLogic.GetAllUsers().Select(ToUserInfo).ToList();
        }

        public UserInfo Get(Guid id)
        {
            return ToUserInfo(UserLogic.Get(id));
        }

        public void Create(string name, string login, string password, QueueType queueType)
        {
            UserLogic.Create(Guid.NewGuid(), name, login, password, queueType);
        }

        public void Delete(Guid id)
        {
            UserLogic.Delete(id);
        }

        public void SetWindow(Guid id, string window)
        {
            UserLogic.SetWindow(id, window);
        }

        public bool Authorize(string login, string password)
        {
            return UserLogic.Authorize(login, password);
        }

        public UserInfo ToUserInfo(IUser user)
        {
            if (user == null)
                return null;

            var queue = BusinessLogicFactory.Instance.Get<IQueueLogic>().Get(user.QueueId);

            return new UserInfo()
                       {
                           Id = user.Id,
                           Name = user.Name,
                           Login = user.Login,
                           Password = user.Password,
                           CurrentWindow = user.Window,
                           Queue = new QueueInfo() { Id = queue.Id, Type = queue.Type }
                       };
        }
    }
}