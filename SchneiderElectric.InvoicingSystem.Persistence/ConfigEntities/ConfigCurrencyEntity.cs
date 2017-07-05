using SchneiderElectric.InvoicingSystem.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchneiderElectric.InvoicingSystem.Presistence.ConfigEntities
{
    public class ConfigCurrencyEntity : EntityTypeConfiguration<Currency>
    {
        public ConfigCurrencyEntity()
        {
              
            this.ToTable("Currencies");

            this.Property(e => e.CurrencyName)
                .IsUnicode(false);

            this.Property(e => e.CurrencySymbol)
                .IsUnicode(false);

            this.Property(e => e.RateToUS)
                .HasPrecision(10, 4);

               this.Property(e => e.CurrencyName)
                .HasMaxLength(50)
                .IsUnicode(false);
            this
              .Property(e => e.CurrencySymbol)
              .HasMaxLength(50)
              .IsUnicode(false);
         
        }
    }
}
