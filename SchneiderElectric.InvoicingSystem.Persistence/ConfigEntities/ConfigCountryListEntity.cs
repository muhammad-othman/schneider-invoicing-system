using SchneiderElectric.InvoicingSystem.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchneiderElectric.InvoicingSystem.Presistence.ConfigEntities
{
    class ConfigCountryListEntity : EntityTypeConfiguration<CountryList>
    {
        public ConfigCountryListEntity()
        {
            this.ToTable("CountryList");

            this.HasKey<double>(e => e.ID);
                this.HasMany(e => e.Currencies)
               .WithOptional(e => e.CountryList)
               .HasForeignKey(e => e.CountryId);
            this.Property(e => e.City).HasMaxLength(255).IsUnicode(false);
            this.Property(e => e.Code).HasMaxLength(255).IsUnicode(false);
            this.Property(e => e.Country).HasMaxLength(255).IsUnicode(false);
            this.Property(e => e.Region).HasMaxLength(50).IsUnicode(false);
        }
    }
}
