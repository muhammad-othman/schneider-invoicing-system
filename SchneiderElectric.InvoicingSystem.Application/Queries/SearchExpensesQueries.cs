using SchneiderElectric.InvoicingSystem.Application.Queries.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchneiderElectric.InvoicingSystem.Domain;
//using System.Data.Entity.Core.Objects;

namespace SchneiderElectric.InvoicingSystem.Application.Queries
{
    public class SearchExpensesQueries : ISearchExpensesQueries
    {
        

        public List<Expense> Excute(List<Expense> Expenses, string keyword)
        {
            var results = (from e in Expenses
                           where e.ExpenseDescription.Contains(keyword)
                           select e).ToList();
            return results;
            
        }

        public List<Expense> Excute(List<Expense> Expenses, DateTime? keyword)
        {
            return Expenses.Where(p => p.StartDate == keyword || p.EndDate == keyword).ToList();
        }

        public List<Expense> Excute(List<Expense> Expenses, DateTime? keywordStartDate, DateTime? keywordEndDate)
        {
            return Expenses.Where(p => p.StartDate >= keywordStartDate || p.EndDate <= keywordEndDate).ToList();
        }
    }
}
