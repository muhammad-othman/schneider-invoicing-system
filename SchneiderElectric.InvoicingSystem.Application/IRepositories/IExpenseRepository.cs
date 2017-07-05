using SchneiderElectric.InvoicingSystem.Application.Interfaces;
using SchneiderElectric.InvoicingSystem.Domain;
using SchneiderElectric.InvoicingSystem.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SchneiderElectric.InvoicingSystem.Application.IRepositories
{
    public interface IExpenseRepository : IRepository<Expense>
    {
        List<ExpenseDTO> GetAllDTO();
        List<ExpenseDTO> FindDTOBy(Expression<Func<Expense, bool>> expression);
        Expense FindById(Guid id);
        ExpenseDTO FindDTOById(Guid id);
        void Attach(Expense expense);
    }
}
