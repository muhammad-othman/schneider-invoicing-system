using SchneiderElectric.InvoicingSystem.Application.Queries.Interfaces;
using SchneiderElectric.InvoicingSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SchneiderElectric.InvoicingSystem.Presentation.Controllers
{
    public class ValuesController : ApiController
    {
        IValueElementsQueries _queries;
        public ValuesController(IValueElementsQueries queries)
        {
            _queries = queries;
        }

        [HttpGet]
        [Route("api/values/getExpenseCategories")]
        public List<object> ExpenseCategories()
        {
            return _queries.GetExpenseCategories();
        }

        [HttpGet]
        [Route("api/values/getCountries")]
        public List<CountryList> Countries()
        {
            return _queries.GetCountries();
        }

        [HttpGet]
        [Route("api/values/getCurrencies")]
        public List<Currency> Currencies()
        {
            return _queries.GetCurrencies();
        }

        [HttpGet]
        [Route("api/values/getExpenseStates")]
        public List<object> ExpenseStates()
        {
            return _queries.GetExpenseStates();
        }

        [HttpGet]
        [Route("api/values/getExpenseTypes")]
        public List<object> ExpenseTypes()
        {
            return _queries.GetExpenseTypes();
        }

        [HttpGet]
        [Route("api/values/getRates")]
        public List<Rate> Rates()
        {
            return _queries.GetRates();
        }
    }
}
