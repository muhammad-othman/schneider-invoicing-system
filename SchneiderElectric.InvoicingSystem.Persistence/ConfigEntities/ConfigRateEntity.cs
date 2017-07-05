using SchneiderElectric.InvoicingSystem.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchneiderElectric.InvoicingSystem.Presistence.ConfigEntities
{
   public class ConfigRateEntity : EntityTypeConfiguration<Rate>
    {
        public ConfigRateEntity()
        {
            this.ToTable("Invoice.Rate");

            this
                .Property(e => e.RateType)
                .HasMaxLength(100)
                .IsUnicode(false);
        }
    }
}
