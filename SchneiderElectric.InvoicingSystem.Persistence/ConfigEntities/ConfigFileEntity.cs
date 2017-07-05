using SchneiderElectric.InvoicingSystem.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchneiderElectric.InvoicingSystem.Presistence.ConfigEntities
{
    public class ConfigFileEntity:  EntityTypeConfiguration<File>
    {
        public ConfigFileEntity()
        {
            this.ToTable("Invoice.File");

            this
                .Property(e => e.Name)
                .IsUnicode(false);
        }
    }
}
