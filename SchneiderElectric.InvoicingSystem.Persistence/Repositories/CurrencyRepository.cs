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
    public class CurrencyRepository : Repository<Currency>, ICurrencyRepository
    {
        public CurrencyRepository(IDatabaseContext database) : base(database)
        {
        }

        public Currency FindById(int id)
        {
            return _database.Set<Currency>().Find(id);
        }
    }
}
