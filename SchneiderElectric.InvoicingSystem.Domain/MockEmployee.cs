using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchneiderElectric.InvoicingSystem.Domain
{
    public class MockEmployee
    {
        public MockEmployee()
        {
            MockDepartments = new HashSet<MockDepartment>();
            Expenses = new HashSet<Expense>();
            MockProjects = new HashSet<MockProject>();
        }
        public string EmployeeSapId { get; set; }
        public string EmployeeName { get; set; }
        public int? DepartmentId { get; set; }
        public virtual ICollection<MockDepartment> MockDepartments { get; set; }
        public virtual MockDepartment MockDepartment { get; set; }
        public virtual ICollection<Expense> Expenses { get; set; }
        public virtual ICollection<MockProject> MockProjects { get; set; }
    }
}
