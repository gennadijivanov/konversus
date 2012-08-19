using System;
using System.Collections.Generic;
using System.Linq;
using Conversus.Core.DomainModel;
using Conversus.Core.Infrastructure.Repository;

namespace Conversus.Storage.Impl
{
    public class SQLitePropertyStorage : SQLiteStorageBase, IPropertyStorage
    {
        public SQLitePropertyStorage(string connectionString) : base(connectionString)
        {
        }

        public void Create(IProperty data)
        {
            throw new NotImplementedException();
        }

        public IProperty Update(IProperty data)
        {
            throw new NotImplementedException();
        }

        public IProperty Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public ICollection<IProperty> Get(IFilterParameters filter)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public string GetProperty(string key)
        {
            using (var db = GetDataContext())
            {
                var prop = db.Properties.SingleOrDefault(p => p.Name == key);
                return prop != null ? prop.Value : null;
            }
        }

        public void SetProperty(string key, string value)
        {
            using (var db = GetDataContext())
            {
                var prop = db.Properties.SingleOrDefault(p => p.Name == key);

                if (prop != null)
                    prop.Value = value;
                else
                    db.AddToProperties(new Properties() {Name = key, Value = value});

                db.SaveChanges();
            }
        }
    }
}