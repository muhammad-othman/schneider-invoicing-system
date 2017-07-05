using SchneiderElectric.InvoicingSystem.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchneiderElectric.InvoicingSystem.Presistence.ConfigEntities
{
    public class ConfigRejctedCommentEntity : EntityTypeConfiguration<RejectedComment>
    {
        public ConfigRejctedCommentEntity()
        {
            this.ToTable("Invoice.RejctedComment");

            this
                .Property(e => e.Comment)
                .IsUnicode(false);
        }
    }
}
