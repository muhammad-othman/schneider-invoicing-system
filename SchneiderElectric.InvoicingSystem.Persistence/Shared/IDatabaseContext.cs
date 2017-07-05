using SchneiderElectric.InvoicingSystem.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchneiderElectric.InvoicingSystem.Presistence.Shared
{
    public interface IDatabaseContext
    {
        DbEntityEntry Entry(object entity);
        IDbSet<MockEmployee> MockEmployees { get; set; }
        IDbSet<MockProject> MockProjects { get; set; }
        IDbSet<CountryList> Countries { get; set; }
        IDbSet<Currency> Currencies { get; set; }
        IDbSet<Expense> Expenses { get; set; }
        IDbSet<OverTimeExpense> OverTimeExpenses { get; set; }
        IDbSet<PerdiemExpense> PerdiemExpenses { get; set; }
        IDbSet<Rate> Rates { get; set; }
        IDbSet<RejectedComment> RejctedComments { get; set; }
        IDbSet<SelfExpense> SelfExpenses { get; set; }
       
        IDbSet<T> Set<T>() where T : class;
        void Save();
    }
}
