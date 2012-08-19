using System;
using System.Collections.Generic;
using System.Linq;
using Conversus.Core.DomainModel;
using Conversus.Core.Infrastructure.Repository;
using UserImpl = Conversus.Impl.Objects.Operator;
using UserData = Conversus.Storage.Operators;

namespace Conversus.Storage.Impl
{
    public class SQLiteOperatorStorage : SQLiteStorageBase, IOperatorStorage
    {
        public SQLiteOperatorStorage(string connectionString) : base(connectionString)
        {
        }

        public void Create(IOperator data)
        {
            using (var db = GetDataContext())
            {
                var user = ToOperatorData(data);

                db.AddToOperators(user);
                db.SaveChanges();
            }
        }

        private Operators ToOperatorData(IOperator data)
        {
            var user = new UserData
                           {
                               Id = data.Id,
                               Name = data.Name,
                               Login = data.Login,
                               Password = data.Password,
                               Window = data.Window,
                               QueueId = data.QueueId,
                               ChangeStatusTime = data.ChangeTime,
                               Status = (int) data.Status
                           };
            return user;
        }

        public IOperator Update(IOperator data)
        {
            data.ChangeTime = DateTime.Now;
            Create(data);
            return data;
        }

        public IOperator Get(Guid id)
        {
            using (var db = GetDataContext())
            {
                var dbCl = db.Operators.OrderByDescending(c => c.ChangeStatusTime).FirstOrDefault(c => c.Id == id);

                return dbCl == null
                    ? null
                    : ConvertFromData(dbCl);
            }
        }

        public ICollection<IOperator> Get(IFilterParameters filter)
        {
            OperatorFilterParameters f = filter != null ? filter as OperatorFilterParameters : null;

            using (var db = GetDataContext())
            {
                IEnumerable<UserData> query = db.Operators;

                var lookUp = query.ToLookup(c => c.Id, c => c);
                query = lookUp.Select(client => client.OrderByDescending(c => c.ChangeStatusTime).First());

                if (f != null)
                {
                    if (!string.IsNullOrEmpty(f.Login))
                        query = query.Where(u => u.Login == f.Login);

                    if (!string.IsNullOrEmpty(f.Password))
                        query = query.Where(u => u.Password == f.Password);

                    if (f.QueueType.HasValue)
                        query = query.Where(u => u.Queues.Type == (int) f.QueueType.Value);

                    if (f.Status.HasValue)
                        query = query.Where(u => u.Status == (int) f.Status.Value);
                }

                var list = query.ToList();

                return list.Select(ConvertFromData).ToList();
            }
        }

        public void Delete(Guid id)
        {
            using (var db = GetDataContext())
            {
                var oper = db.Operators.Where(c => c.Id == id);
                if (!oper.Any())
                    return;

                foreach (var clientRecord in oper)
                {
                    db.Operators.DeleteObject(clientRecord);
                }
            }
        }

        private IOperator ConvertFromData(UserData data)
        {
            return new UserImpl(data.Id, data.Name, data.Login, data.Password, data.Window, 
                data.QueueId, (OperatorStatus)data.Status, data.ChangeStatusTime);
        }
    }
}