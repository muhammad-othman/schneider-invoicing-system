using SchneiderElectric.InvoicingSystem.Application.IRepositories;
using SchneiderElectric.InvoicingSystem.Presistence.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchneiderElectric.InvoicingSystem.Domain;

namespace SchneiderElectric.InvoicingSystem.Presistence.Repositories
{
    public class FileRepository : Repository<Domain.File>, IFileRepository
    {
        public FileRepository(IDatabaseContext database) : base(database)
        {
        }

        public Domain.File FindById(Guid id)
        {
            return _database.Set<Domain.File>().Find(id);
        }
    }
}
