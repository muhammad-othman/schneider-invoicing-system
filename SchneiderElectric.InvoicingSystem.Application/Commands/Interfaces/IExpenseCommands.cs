using SchneiderElectric.InvoicingSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchneiderElectric.InvoicingSystem.Application.Commands.Interfaces
{
    public interface IExpenseCommands
    {
        bool Edit(Expense Expense, Guid expenseId);
        bool Cancel(Guid expenseId, string sapId);
        bool Recall(Guid expenseId, string sapId);
        bool Create(Expense expense);
        bool Create(Expense expense, List<Tuple<Guid, string>> tempFilesList);
        bool Submit(Expense expense, string sapId);
    }
}
