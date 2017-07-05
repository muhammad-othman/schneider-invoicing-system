using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchneiderElectric.InvoicingSystem.Domain
{
    public class MockDepartment
    {
        public MockDepartment()
        {
            MockEmployees = new HashSet<MockEmployee>();
        }

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }
        public string EMId { get; set; }

        public virtual MockEmployee MockEmployee { get; set; }
        public virtual ICollection<MockEmployee> MockEmployees { get; set; }

    }
}
