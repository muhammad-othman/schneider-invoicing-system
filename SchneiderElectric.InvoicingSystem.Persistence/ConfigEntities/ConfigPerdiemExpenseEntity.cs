using SchneiderElectric.InvoicingSystem.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchneiderElectric.InvoicingSystem.Presistence.ConfigEntities
{
    public class ConfigPerdiemExpenseEntity : EntityTypeConfiguration<PerdiemExpense>
    {
        public ConfigPerdiemExpenseEntity()
            {
            this.
                ToTable("Invoice.PerdiemExpense");
            this
                .HasKey(e => e.PerdiemExpenseId);

            this.Property(e => e.Description)
                .IsUnicode(false).HasMaxLength(150);

            this
             .Property(e => e.Amount)
             .HasColumnType("money")
             .HasPrecision(19, 4);

        }
    }
}
