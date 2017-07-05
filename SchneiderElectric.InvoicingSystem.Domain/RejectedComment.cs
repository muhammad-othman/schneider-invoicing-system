using System;

namespace SchneiderElectric.InvoicingSystem.Domain
{
    public class RejectedComment
    {
        public int RejectedCommentId { get; set; }
        public Guid? ExpenseId { get; set; }
        public DateTime DateRejected { get; set; }
        public string Comment { get; set; }
        public ExpenseState ExpenseState { get; set; }
    }
}
