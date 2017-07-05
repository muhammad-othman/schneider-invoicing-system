using SchneiderElectric.InvoicingSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchneiderElectric.InvoicingSystem.Application.Queries.Interfaces
{
    public interface IValueElementsQueries
    {
        List<CountryList> GetCountries();
        List<MockProject> GetProjects();
        List<Currency> GetCurrencies();
        List<object> GetExpenseCategories();
        List<object> GetExpenseStates();
        List<object> GetExpenseTypes();
        List<Rate> GetRates();
    }
}
