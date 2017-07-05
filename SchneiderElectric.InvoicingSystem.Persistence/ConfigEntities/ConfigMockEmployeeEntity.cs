using SchneiderElectric.InvoicingSystem.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchneiderElectric.InvoicingSystem.Presistence.ConfigEntities
{
    public class ConfigMockEmployeeEntity :EntityTypeConfiguration<MockEmployee>
    {
        public ConfigMockEmployeeEntity()
        {
            this.ToTable("MockEmployee");
            this.HasKey<string>(e => e.EmployeeSapId);
            this.HasMany(e => e.MockDepartments)
                .WithOptional(e => e.MockEmployee)
                .HasForeignKey(e => e.EMId);
            

                this.HasMany(e => e.MockProjects)
                .WithOptional(e => e.MockEmployee)
                .HasForeignKey(e => e.PAId);

            this
              .Property(e => e.EmployeeSapId)
              .HasMaxLength(5)
              .IsFixedLength()
              .IsUnicode(false);

            this
                .Property(e => e.EmployeeName)
                .IsUnicode(false);

            this.HasMany(e => e.Expenses)
                 .WithOptional(e => e.MockEmployee);
        }
    }
}
