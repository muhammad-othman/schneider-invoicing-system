﻿using SchneiderElectric.InvoicingSystem.Application.Interfaces;
using SchneiderElectric.InvoicingSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchneiderElectric.InvoicingSystem.Application.IRepositories
{
    public interface IPerdiemExpenseRepository : IRepository<PerdiemExpense>
    {
        PerdiemExpense FindById(Guid id);
    }
}
