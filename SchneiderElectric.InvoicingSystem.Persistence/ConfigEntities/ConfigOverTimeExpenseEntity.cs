﻿using SchneiderElectric.InvoicingSystem.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchneiderElectric.InvoicingSystem.Presistence.ConfigEntities
{
    public class ConfigOverTimeExpenseEntity : EntityTypeConfiguration<OverTimeExpense>
    {
        public ConfigOverTimeExpenseEntity()
        {
            this
                .ToTable("Invoice.OverTimeExpense");
            this
                .HasKey(e => e.OverTimeExpenseId);
            this.Property(e => e.Description)
                .IsUnicode(false).HasMaxLength(150);
            this
              .Property(e => e.Amount)
              .HasPrecision(19, 4)
              .HasColumnType("money");

            this
              .Property(e => e.RateToUS)
              .HasColumnType("money")
              .HasPrecision(19, 4);

        }
    }
}
