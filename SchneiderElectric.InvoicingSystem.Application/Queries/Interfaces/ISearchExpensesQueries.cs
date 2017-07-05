using SchneiderElectric.InvoicingSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchneiderElectric.InvoicingSystem.Application.Queries.Interfaces
{
  public  interface ISearchExpensesQueries
    {
        List<Expense> Excute(List<Expense> Expenses, string keyword);

        List<Expense> Excute(List<Expense> Expenses, DateTime? keyword);

        List<Expense> Excute(List<Expense> Expenses, DateTime? keywordStartDate,DateTime? keywordEndDate);
    }
}
