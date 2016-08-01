using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using POD.Entity;
using SQLite;
using Xamarin.Forms;

namespace POD.Repository
{
    public class Repository<T> where T : BaseEntity, new ()
    {
        private static readonly object _locker = new object();
        private readonly SQLiteConnection _database;

        public Repository()
        {
            _database = DependencyService.Get<ISqlLiteProvider>().GetConnection("ApplicationDatabase.db3");
            // create the tables
            //_database.CreateTable<T>();
        }

        public TableQuery<T> Query()
        {
            lock (_locker)
            {
                return _database.Table<T>();
            }
        }

        public List<T> List<TOrderByKey>(Expression<Func<T, bool>> where = null, Expression<Func<T, TOrderByKey>> orderBy = null)
        {
            lock (_locker)
            {
                var query = _database.Table<T>();

                if (where != null)
                {
                    query = query.Where(where);
                }

                if (orderBy != null)
                {
                    query = query.OrderBy(orderBy);
                }

                return query.ToList();
            }
        }

        public T GetById(int id)
        {
            lock (_locker)
            {
                return _database.Table<T>().SingleOrDefault(x => x.Id == id);
            }
        }

        public int InsertOrUpdate(T item)
        {
            lock (_locker)
            {
                if (item.Id != 0)
                {
                    _database.Update(item);
                    return item.Id;
                }

                return _database.Insert(item);
            }
        }

        public int Delete(int id)
        {
            lock (_locker)
            {
                return _database.Delete<T>(id);
            }
        }
    }
}
