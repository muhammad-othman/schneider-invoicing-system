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
    public class CommentRepository : Repository<RejectedComment>, ICommentRepository
    {
        public CommentRepository(IDatabaseContext database) : base(database)
        {
        }

        public RejectedComment FindById(int id)
        {
            return _database.Set<RejectedComment>().Find(id);
        }
    }
}
