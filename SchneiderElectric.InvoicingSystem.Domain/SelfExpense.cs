using System;
using System.Collections.Generic;

namespace SchneiderElectric.InvoicingSystem.Domain
{
    public class SelfExpense
    {
        public Guid SelfExpenseId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public int? CurrencyID { get; set; }
        public bool ByAmex { get; set; }
        public ExpenseType ExpenseType { get; set; }
        public decimal RateToUS { get; set; }
        public bool Rejected { get; set; }
        public Guid? ExpenseId { get; set; }
        public virtual Currency Currency { get; set; }
    }
}
