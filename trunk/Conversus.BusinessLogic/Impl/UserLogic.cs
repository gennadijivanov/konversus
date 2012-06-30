using System;
using System.Collections.Generic;
using Conversus.Core.DomainModel;
using Conversus.Core.Infrastructure.Repository;
using Conversus.Impl.Objects;
using Conversus.Storage;
using User = Conversus.Impl.Objects.User;

namespace Conversus.BusinessLogic.Impl
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserStorage Storage = StorageLogicFactory.Instance.Get<IUserStorage>();

        public ICollection<IUser> GetAllUsers()
        {
            //TODO: filters
            return Storage.Get(new QueueFilterParameters());
        }

        public IUser Get(Guid id)
        {
            return Storage.Get(id);
        }

        public void Create(Guid id, string name, string login, string password, QueueType queueType)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                throw new ArgumentNullException();

            if (Storage.Get(new UserFilterParameters() { Login = login }).Count > 0)
                throw new InvalidOperationException("User is already exists");

            password = GetMD5Hash(password);

            IQueue queue = BusinessLogicFactory.Instance.Get<IQueueLogic>().GetOrCreateQueue(queueType);
            IUser user = new User(id, name,login,password, queue.Id);

            Storage.Create(user);
        }

        public void Delete(Guid id)
        {
            Storage.Delete(id);
        }

        public void SetWindow(Guid id, string window)
        {
            var user = Storage.Get(id);
            user.Window = window;
            Storage.Update(user);
        }

        public bool Authorize(string login, string password)
        {
            password = GetMD5Hash(password);
            return Storage.Get(new UserFilterParameters() {Login = login, Password = password}).Count > 0;
        }

        private string GetMD5Hash(string input)
        {
            var x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(input);
            bs = x.ComputeHash(bs);
            var s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            string password = s.ToString();
            return password;
        }
    }
}