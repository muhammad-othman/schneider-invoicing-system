using SchneiderElectric.InvoicingSystem.Application.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchneiderElectric.InvoicingSystem.Domain;
using SchneiderElectric.InvoicingSystem.Presistence.Shared;

namespace SchneiderElectric.InvoicingSystem.Presistence.Repositories
{
    public class RateRepository : Repository<Rate>,IRateRepository
    {
        public RateRepository(IDatabaseContext database) : base(database)
        {
        }

        public Rate FindById(int id)
        {
            return _database.Set<Rate>().Find(id);
        }
    }
}
