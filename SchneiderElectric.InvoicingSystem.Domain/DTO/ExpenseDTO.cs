using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchneiderElectric.InvoicingSystem.Domain.DTO
{
    public class ExpenseDTO
    {
        public ExpenseDTO()
        {
            SelfExpenses = new HashSet<SelfExpense>();
            OverTimeExpenses = new HashSet<OverTimeExpense>();
            PerdiemExpenses = new HashSet<PerdiemExpense>();
            RejectedComments = new HashSet<RejectedComment>();
            Files = new HashSet<File>();
        }

        public Guid ExpenseId { get; set; }

        public string ExpenseDescription { get; set; }

        public string EmployeeSapId { get; set; }
        public string ProjectSapId { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public double? CountryListID { get; set; }
        public ExpenseState ExpenseState { get; set; }
        public virtual CountryList CountryList { get; set; }
        public string EmployeeName { get; set; }
        public string ProjectName { get; set; }

        public virtual ICollection<SelfExpense> SelfExpenses { get; set; }

        public virtual ICollection<OverTimeExpense> OverTimeExpenses { get; set; }

        public virtual ICollection<PerdiemExpense> PerdiemExpenses { get; set; }

        public virtual ICollection<RejectedComment> RejectedComments { get; set; }

        public virtual ICollection<File> Files { get; set; }
    }
}
