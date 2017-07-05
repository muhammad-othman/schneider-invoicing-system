using SchneiderElectric.InvoicingSystem.Application.IRepositories;
using SchneiderElectric.InvoicingSystem.Domain;
using SchneiderElectric.InvoicingSystem.Presistence.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchneiderElectric.InvoicingSystem.Presistence.Repositories
{
    public class SelfExpenseRepository : Repository<SelfExpense>, ISelfExpenseRepository
    {
        public SelfExpenseRepository(IDatabaseContext database) : base(database)
        {
        }

        public SelfExpense FindById(Guid id)
        {
            return _database.Set<SelfExpense>().Find(id);
        }
    }
}
