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
                var user = new UserData
                {
                    Id = data.Id,
                    Name = data.Name,
                    Login = data.Login,
                    Password = data.Password,
                    Window = data.Window,
                    QueueId = data.QueueId
                };
                db.AddToOperators(user);
                db.SaveChanges();
            }
        }

        public void Update(IOperator data)
        {
            using (var db = GetDataContext())
            {
                var user = db.Operators.SingleOrDefault(c => c.Id == data.Id);

                if (user == null)
                    return;

                user.Login = data.Login;
                user.Name = data.Name;
                user.Password = data.Password;
                user.Window = data.Window;
                user.QueueId = data.QueueId;

                db.SaveChanges();
            }
        }

        public IOperator Get(Guid id)
        {
            using (var db = GetDataContext())
            {
                var dbCl = db.Operators.SingleOrDefault(c => c.Id == id);

                return dbCl == null
                    ? null
                    : ConvertFromData(dbCl);
            }
        }

        public ICollection<IOperator> Get(IFilterParameters filter)
        {
            UserFilterParameters f = filter != null ? filter as UserFilterParameters : null;

            using (var db = GetDataContext())
            {
                IEnumerable<UserData> query = db.Operators;

                if (f != null)
                {
                    if (!string.IsNullOrEmpty(f.Login))
                        query = query.Where(u => u.Login == f.Login);

                    if (!string.IsNullOrEmpty(f.Password))
                        query = query.Where(u => u.Password == f.Password);

                    if (f.QueueType.HasValue)
                        query = query.Where(u => u.Queues.Type == (int) f.QueueType.Value);
                }

                var list = query.ToList();

                return list.Select(ConvertFromData).ToList();
            }
        }

        public void Delete(Guid id)
        {
            using (var db = GetDataContext())
            {
                var user = db.Operators.SingleOrDefault(c => c.Id == id);
                if (user != null)
                    db.Operators.DeleteObject(user);
            }
        }

        private IOperator ConvertFromData(UserData data)
        {
            return new UserImpl(data.Id, data.Name, data.Login, data.Password, data.Window, data.QueueId);
        }
    }
}