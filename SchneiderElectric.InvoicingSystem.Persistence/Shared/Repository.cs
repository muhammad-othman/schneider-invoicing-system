using SchneiderElectric.InvoicingSystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SchneiderElectric.InvoicingSystem.Presistence.Shared
{
  public   class Repository<T> : IRepository<T> 
        where T : class
    {
        protected readonly IDatabaseContext _database;
           
           public  Repository(IDatabaseContext database)
            {
            _database = database;
            }
        public virtual void Add(T Entity)
        {
            _database.Set<T>().Add(Entity);
        }

        public virtual IQueryable<T> GetAll()
        {
            return _database.Set<T>();
        }

        public virtual void Remove(T Entity)
        {
            _database.Set<T>().Remove(Entity);
        }
        public virtual IEnumerable<T> FindBy(Expression<Func<T, bool>> expression)
        {
            return _database.Set<T>().Where(expression);
        }
    }
}
