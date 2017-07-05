using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchneiderElectric.InvoicingSystem.Domain
{
   public  class File
    {
        public Guid FileId { get; set; }

        public string Name { get; set; }

        public Guid ExpenseId { get; set; }

    }
}
