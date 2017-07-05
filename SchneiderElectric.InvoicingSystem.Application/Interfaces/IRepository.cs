using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SchneiderElectric.InvoicingSystem.Application.Interfaces
{
    public interface IRepository <T>
    {
        IQueryable<T> GetAll();

        void Add(T Entity);

        void Remove(T Entity);

        IEnumerable<T> FindBy(Expression<Func<T, bool>> expression);
    }
}
