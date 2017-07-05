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
    public class OverTimeExpenseRepository : Repository<OverTimeExpense>, IOverTimeExpenseRepository
    {
        public OverTimeExpenseRepository(IDatabaseContext database) : base(database)
        {
        }

        public OverTimeExpense FindById(Guid id)
        {
            return _database.Set<OverTimeExpense>().Find(id);
        }
    }
}
