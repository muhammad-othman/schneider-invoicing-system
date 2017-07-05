using SchneiderElectric.InvoicingSystem.Application.IRepositories;
using SchneiderElectric.InvoicingSystem.Application.Queries.Interfaces;
using SchneiderElectric.InvoicingSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchneiderElectric.InvoicingSystem.Application.Queries
{
    public class ValueElementsQueries : IValueElementsQueries
    {
        private ICountryRepository _countryRepository;
        private IRateRepository    _rateRepository;
        private ICurrencyRepository _currencyRepository;
        private IProjectRepository _projectRepository;

        public ValueElementsQueries(ICountryRepository countryRepository,
                                     IRateRepository rateRepository,
                                     ICurrencyRepository currencyRepository,
                                     IProjectRepository projectRepository)

        {
            _countryRepository= countryRepository;
            _rateRepository= rateRepository;
            _projectRepository= projectRepository;
            _currencyRepository= currencyRepository;
        }

        public List<CountryList> GetCountries()
        {
            return _countryRepository.GetAll().ToList();
        }

        public List<Currency> GetCurrencies()
        {
            return _currencyRepository.GetAll().ToList();
        }

        public List<object> GetExpenseCategories()
        {
            List<object> list = new List<object>();

            foreach (ExpenseCategory val in Enum.GetValues(typeof(ExpenseCategory)))
            {
                list.Add(new { ExpenseCategoryId = (int)val, ExpenseCategoryType = Enum.GetName(typeof(ExpenseCategory), val) });
            }
            return list;
        }

        public List<object> GetExpenseStates()
        {
            List<object> list = new List<object>();

            foreach (ExpenseState val in Enum.GetValues(typeof(ExpenseState)))
            {
                list.Add(new { ExpenseStateId = (int)val, ExpenseStateType = Enum.GetName(typeof(ExpenseState), val) });
            }
            return list;
        }

        public List<object> GetExpenseTypes()
        {
            List<object> list = new List<object>();

            foreach (ExpenseType val in Enum.GetValues(typeof(ExpenseType)))
            {
               list.Add(new { ExpenseTypeId = (int)val, ExpenseTypeName = Enum.GetName(typeof(ExpenseType), val) });
            }
            return list;
        }

        public List<Rate> GetRates()
        {
            return _rateRepository.GetAll().ToList();
        }

        public List<MockProject> GetProjects()
        {
            return _projectRepository.GetAll().ToList();

        }
    }
 }