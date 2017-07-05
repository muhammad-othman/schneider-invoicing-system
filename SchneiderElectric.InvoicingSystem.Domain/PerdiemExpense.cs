using System;
using System.Collections.Generic;

namespace SchneiderElectric.InvoicingSystem.Domain
{
    public class PerdiemExpense
    {
        public Guid PerdiemExpenseId { get; set; }
        public decimal Amount { get; set; }
        public int NumberOfDays { get; set; }
        public int? CurrencyID { get; set; }
        public string Description { get; set; }
        public int? RateId { get; set; }
        public bool Rejected { get; set; }
        public Guid? ExpenseId { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual Rate Rate { get; set; }
    }
}
