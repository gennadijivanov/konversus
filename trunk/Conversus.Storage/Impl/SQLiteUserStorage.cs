using System;
using System.Collections.Generic;
using System.Linq;
using Conversus.Core.DomainModel;
using Conversus.Core.Infrastructure.Repository;
using UserImpl = Conversus.Impl.Objects.User;
using UserData = Conversus.Storage.User;

namespace Conversus.Storage.Impl
{
    public class SQLiteUserStorage : IUserStorage
    {
        public void Create(IUser data)
        {
            using (var db = new ConversusDataContext())
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
                db.AddToUsers(user);
                db.SaveChanges();
            }
        }

        public void Update(IUser data)
        {
            using (var db = new ConversusDataContext())
            {
                var user = db.Users.SingleOrDefault(c => c.Id == data.Id);

                if (user == null)
                    return;

                user.Login = data.Login;
                user.Name = data.Name;
                user.Password = data.Password;
                user.Window = data.Window;
                user.QueueId = data.QueueId;

                //TODO: set field of data obj
                db.SaveChanges();
            }
        }

        public IUser Get(Guid id)
        {
            using (var db = new ConversusDataContext())
            {
                var dbCl = db.Users.SingleOrDefault(c => c.Id == id);

                return dbCl == null
                    ? null
                    : ConvertFromData(dbCl);
            }
        }

        public ICollection<IUser> Get(IFilterParameters filter)
        {
            UserFilterParameters f = filter != null ? filter as UserFilterParameters : null;

            using (var db = new ConversusDataContext())
            {
                IEnumerable<UserData> query = db.Users;

                if (f != null)
                {
                    if (!string.IsNullOrEmpty(f.Login))
                        query = query.Where(u => u.Login == f.Login);

                    if (!string.IsNullOrEmpty(f.Password))
                        query = query.Where(u => u.Password == f.Password);
                }

                var list = query.ToList();

                return list.Select(ConvertFromData).ToList();
            }
        }

        public void Delete(Guid id)
        {
            using (var db = new ConversusDataContext())
            {
                var user = db.Users.SingleOrDefault(c => c.Id == id);
                if (user != null)
                    db.Users.DeleteObject(user);
            }
        }

        private IUser ConvertFromData(UserData data)
        {
            return new UserImpl(data.Id, data.Name, data.Login, data.Password, data.Window, data.QueueId);
        }
    }
}