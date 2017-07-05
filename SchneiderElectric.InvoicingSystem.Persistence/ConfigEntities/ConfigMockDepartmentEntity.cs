using SchneiderElectric.InvoicingSystem.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchneiderElectric.InvoicingSystem.Presistence.ConfigEntities
{
    public class ConfigMockDepartmentEntity : EntityTypeConfiguration<MockDepartment>
    {
        public ConfigMockDepartmentEntity()
        {
            this.ToTable("MockDepartment");

            this.HasKey<int>(e => e.DepartmentId);

            this.HasMany(e => e.MockEmployees)
               .WithOptional(e => e.MockDepartment)
               .HasForeignKey(e => e.DepartmentId);
            this
              .Property(e => e.DepartmentName)
              .HasMaxLength(50)
              .IsUnicode(false);
            this
              .Property(e => e.EMId)
              .HasMaxLength(5)
              .IsUnicode(false);
        }
    }
}
