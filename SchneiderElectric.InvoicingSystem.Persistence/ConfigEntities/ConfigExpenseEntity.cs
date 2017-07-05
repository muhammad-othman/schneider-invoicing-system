using SchneiderElectric.InvoicingSystem.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchneiderElectric.InvoicingSystem.Presistence.ConfigEntities
{
   public class ConfigExpenseEntity: EntityTypeConfiguration<Expense>
    {
        public ConfigExpenseEntity()
        {
            this.ToTable("Invoice.Expense");

            this.HasKey<Guid>(s => s.ExpenseId);

            this.Property(e => e.ExpenseDescription)
                .IsUnicode(false).HasMaxLength(150);

            this 
               .Property(e => e.EmployeeSapId).
                HasMaxLength(5)
                .IsFixedLength()
                .IsUnicode(false);

            this.Property(e => e.ProjectSapId)
                .HasMaxLength(10)
                .IsUnicode(false);

            this
                .Property(e => e.StartDate)
                .HasColumnType("date");

            this
               .Property(e => e.EndDate)
               .HasColumnType("date");
        }
    }
}
