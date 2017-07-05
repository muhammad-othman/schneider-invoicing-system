using SchneiderElectric.InvoicingSystem.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchneiderElectric.InvoicingSystem.Presistence.ConfigEntities
{
   public  class ConfigMockProjectEntity : EntityTypeConfiguration<MockProject>
    {
        public ConfigMockProjectEntity()
        {
            this.ToTable("MockProject");

            this.HasKey<string>(e => e.ProjectSapId);

            this
              .Property(e => e.ProjectSapId)
              .HasMaxLength(10)
              .IsUnicode(false);
            this
              .Property(e => e.PAId)
              .HasMaxLength(5)
              .IsUnicode(false);

            this
                .Property(e => e.ProjectName)
                .IsUnicode(false);

            this
                .HasMany(e => e.Expenses)
                .WithOptional(e => e.MockProject)
                .WillCascadeOnDelete();

        }
    }
   
}
