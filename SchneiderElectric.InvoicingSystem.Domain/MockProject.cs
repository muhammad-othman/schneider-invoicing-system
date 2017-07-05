using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchneiderElectric.InvoicingSystem.Domain
{
    public class MockProject
    {
        public MockProject()
        {
            Expenses = new HashSet<Expense>();
        }
        public string ProjectSapId { get; set; }
        public string ProjectName { get; set; }
        public string PAId { get; set; }
        public virtual MockEmployee MockEmployee { get; set; }
        public virtual ICollection<Expense> Expenses { get; set; }
    }
}
