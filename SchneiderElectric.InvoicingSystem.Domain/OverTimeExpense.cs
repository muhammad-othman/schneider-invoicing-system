using System;
using System.Collections.Generic;

namespace SchneiderElectric.InvoicingSystem.Domain
{
    public class OverTimeExpense
    {
        public Guid OverTimeExpenseId { get; set; }
        public decimal Amount { get; set; }
        public int NumberOfHours { get; set; }
        public decimal RateToUS { get; set; }
        public string Description { get; set; }
        public int? CurrencyID { get; set; }
        public bool Rejected { get; set; }
        public ExpenseCategory ExpenseCategory { get; set; }
        public Guid? ExpenseId { get; set; }
        public virtual Currency Currency { get; set; }
    }
}
