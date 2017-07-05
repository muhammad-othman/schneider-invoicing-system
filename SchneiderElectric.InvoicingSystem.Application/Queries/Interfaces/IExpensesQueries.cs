using SchneiderElectric.InvoicingSystem.Domain;
using SchneiderElectric.InvoicingSystem.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchneiderElectric.InvoicingSystem.Application.Queries.Interfaces
{
    public interface IExpensesQueries
    {
        Expense GetById(Guid expenseId);
        ExpenseDTO GetDTOById(Guid expenseId);
        List<ExpenseDTO> GetAll();
        List<ExpenseDTO> GetByProject(string projectSap);
        List<ExpenseDTO> GetByDepartment(int departmentId);
        List<ExpenseDTO> GetActiveExpenses(string employeeSap);
        List<ExpenseDTO> GetAllAvailableExpenses(string employeeSap);
        List<ExpenseDTO> GetCanceled(string employeeSap);
        List<ExpenseDTO> GetRejected(string employeeSap);
        List<RejectedComment> GetRejctedComments(Guid expenseId);

    }
}
