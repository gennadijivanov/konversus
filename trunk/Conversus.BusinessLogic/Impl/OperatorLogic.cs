using System;
using System.Collections.Generic;
using System.Linq;
using Conversus.Core.DomainModel;
using Conversus.Core.Infrastructure;
using Conversus.Core.Infrastructure.Repository;
using Conversus.Impl;
using Conversus.Storage;
using User = Conversus.Impl.Objects.Operator;

namespace Conversus.BusinessLogic.Impl
{
    public class OperatorLogic : IOperatorLogic
    {
        private readonly IOperatorStorage Storage = StorageLogicFactory.Instance.Get<IOperatorStorage>();

        public ICollection<IOperator> GetAllUsers()
        {
            return Storage.Get(new QueueFilterParameters());
        }

        public IOperator Get(Guid id)
        {
            return Storage.Get(id);
        }

        public ICollection<IOperator> Get(OperatorFilterParameters filter)
        {
            return Storage.Get(filter);
        }

        public void Create(Guid id, string name, string login, string password, string window, QueueType queueType)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                throw new ArgumentNullException();

            if (Storage.Get(new OperatorFilterParameters() { Login = login }).Count > 0)
                throw new InvalidOperationException("User is already exists");

            password = GetMD5Hash(password);

            IQueue queue = BusinessLogicFactory.Instance.Get<IQueueLogic>().GetOrCreateQueue(queueType);
            IOperator user = new User(id, name, login, password, window, queue.Id, OperatorStatus.Stop, DateTime.Now);

            Storage.Create(user);
        }

        public void Save(Guid id, string name, string login, string password, string window, QueueType queueType)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(login))
                throw new ArgumentNullException();

            var user = Storage.Get(id);
            user.Name = name;
            user.Login = login;
            user.Window = window;
            if (!string.IsNullOrEmpty(password))
                user.Password = GetMD5Hash(password);

            IQueue queue = BusinessLogicFactory.Instance.Get<IQueueLogic>().GetOrCreateQueue(queueType);
            user.QueueId = queue.Id;

            Storage.Update(user);
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

        public IOperator Login(string login, string password)
        {
            CheckLoginedOperatorsCount();

            password = GetMD5Hash(password);
            var oper = Storage.Get(new OperatorFilterParameters { Login = login, Password = password })
                .SingleOrDefault();

            if (oper == null)
                return null;

            if (oper.Status != OperatorStatus.Stop)
                throw new Exception("Оператор уже залогинен");

            oper.Status = OperatorStatus.Play;
            Storage.Update(oper);
            return oper;
        }

        public void Logout(Guid id)
        {
            ChangeStatus(id, OperatorStatus.Stop);
        }

        public void ChangeStatus(Guid id, OperatorStatus status)
        {
            var oper = Storage.Get(id);

            if (oper == null || oper.Status == status)
                return;

            oper.Status = status;
            Storage.Update(oper);
        }

        private void CheckLoginedOperatorsCount()
        {
            List<IOperator> loginedOperators =
                Storage.Get(new OperatorFilterParameters { Status = OperatorStatus.Play }).ToList();
            loginedOperators.AddRange(Storage.Get(new OperatorFilterParameters() { Status = OperatorStatus.Pause }));

            var propLogic = BusinessLogicFactory.Instance.Get<IPropertyLogic>();
            string company = propLogic.GetProperty(Constants.Properties.Company);
            string licKey = propLogic.GetProperty(Constants.Properties.LicenseKey);

            LicenseType? license = EncryptionManager.TestLicense(licKey, company);

            if (!license.HasValue)
                throw new Exception("Администратор должен активировать лицензию");
            else if (loginedOperators.Count >= (int)license.Value)
                throw new Exception("Уже достигнуто максимально возможное число операторов");
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
            return s.ToString();
        }
    }
}