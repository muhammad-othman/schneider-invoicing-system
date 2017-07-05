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
    public class CountryRepository : Repository<CountryList>, ICountryRepository
    {
        public CountryRepository(IDatabaseContext database) : base(database)
        {
        }

        public CountryList FindById(double id)
        {
            return _database.Set<CountryList>().Find(id);
        }
    }
}
