using System;
using System.Collections.Generic;
using Conversus.Core.DomainModel;
using Conversus.Service.Contract;

namespace Conversus.Service.Impl
{
    public class UserService : IUserService
    {
        public ICollection<UserInfo> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public UserInfo Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Create(Guid id, string name, string login, string password, QueueType queueType)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public void SetWindow(Guid id, string window)
        {
            throw new NotImplementedException();
        }
    }
}