using SchneiderElectric.InvoicingSystem.Application.Commands.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchneiderElectric.InvoicingSystem.Domain;
using SchneiderElectric.InvoicingSystem.Application.IRepositories;
using SchneiderElectric.InvoicingSystem.Application.Interfaces;

namespace SchneiderElectric.InvoicingSystem.Application.Commands
{
    public class ExpenseCommands : IExpenseCommands
    {
        private IExpenseRepository _ExpenseRepository;
        private IUnitOFWork _UnitOfWork;
        private IEmployeeRepository _employeeRepository;

        public ExpenseCommands(IExpenseRepository ExpenseRepository, IUnitOFWork UnitOfWork, IEmployeeRepository employeeRepository)
        {
            _ExpenseRepository = ExpenseRepository;
            _employeeRepository = employeeRepository;
            _UnitOfWork = UnitOfWork;
        }
        public bool Create(Expense expense)
        {
            try
            {
                Guid expenseId = Guid.NewGuid();
                expense.ExpenseId = expenseId;

                foreach (var overtime in expense.OverTimeExpenses)
                { 
                    overtime.OverTimeExpenseId = Guid.NewGuid();
                    overtime.ExpenseId = expenseId;
                }

                foreach (var self in expense.SelfExpenses)
                { 
                    self.SelfExpenseId = Guid.NewGuid();
                    self.ExpenseId = expenseId;
                }

                foreach (var perdiem in expense.PerdiemExpenses)
                { 
                    perdiem.PerdiemExpenseId = Guid.NewGuid();
                    perdiem.ExpenseId = expenseId;
                }

                foreach (var file in expense.Files)
                { 
                    file.FileId = Guid.NewGuid();
                    file.ExpenseId = expenseId;
                }
                _ExpenseRepository.Add(expense);
                _UnitOfWork.Save();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool Cancel(Guid expenseId, string sapId)
        {
            try { 
            if(_employeeRepository.GetEmployeeType(sapId) != EmployeeType.PA ) return false;

            var chosenExpense = _ExpenseRepository.FindById(expenseId);
            if (chosenExpense != null)
            {
                chosenExpense.ExpenseState = ExpenseState.AtPACanceled;
                _UnitOfWork.Save();
            }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Recall(Guid expenseId, string sapId)
        {
            try { 
            if (_employeeRepository.GetEmployeeType(sapId) != EmployeeType.PA) return false;

            var chosenExpense = _ExpenseRepository.FindById(expenseId);
            if (chosenExpense != null)
            {
                chosenExpense.ExpenseState = ExpenseState.AtPA;
                _UnitOfWork.Save();
            }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Edit(Expense Expense, Guid expenseId)
        {
            try{ 
            var chosenExpense=_ExpenseRepository.FindById(expenseId);
            if (chosenExpense != null)
            {
                chosenExpense.CountryList = Expense.CountryList;
                chosenExpense.CountryListID = Expense.CountryListID;
                chosenExpense.EmployeeSapId = Expense.EmployeeSapId;
                chosenExpense.EndDate = Expense.EndDate;
                chosenExpense.StartDate = Expense.StartDate;
                chosenExpense.ExpenseDescription = Expense.ExpenseDescription;
                chosenExpense.SelfExpenses = Expense.SelfExpenses;
                chosenExpense.PerdiemExpenses = Expense.PerdiemExpenses;
                chosenExpense.OverTimeExpenses = Expense.OverTimeExpenses;

                _UnitOfWork.Save();
                return true;
            }
            else
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool Submit(Expense expense, string sapId)
        {
            try { 
            var userType = _employeeRepository.GetEmployeeType(sapId);
            _ExpenseRepository.Attach(expense);

            if ( userType == EmployeeType.PA)
            {
                expense.ExpenseState = ExpenseState.AtEM;
                _UnitOfWork.Save();
                return true;
            }
            else if (userType == EmployeeType.Engineer)
            {
                expense.ExpenseState = ExpenseState.AtPA;
                _UnitOfWork.Save();
                return true;
            }
            var approved = true;
            foreach (var overtime in expense.OverTimeExpenses)
                if (overtime.Rejected)
                    approved = false;
            foreach (var self in expense.SelfExpenses)
                if (self.Rejected)
                    approved = false;
            foreach (var perdiem in expense.PerdiemExpenses)
                if (perdiem.Rejected)
                    approved = false;

            if(!approved)
            {
                expense.ExpenseState = ExpenseState.AtPARejected;
                _UnitOfWork.Save();
            }
            else if (userType == EmployeeType.EM)
            {
                expense.ExpenseState = ExpenseState.AtFinance;
                _UnitOfWork.Save();
            }
            else if (userType == EmployeeType.Finance)
            {
                expense.ExpenseState = ExpenseState.Finished;
                _UnitOfWork.Save();
            }
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        #region NewUpload

        public bool Create(Expense expense, List<Tuple<Guid, string>> tempFilesList)
        {
            try
            {
                Guid expenseId = Guid.NewGuid();
                expense.ExpenseId = expenseId;

                foreach (var overtime in expense.OverTimeExpenses)
                {
                    SetNewIdForOverTimeExpense(overtime, tempFilesList);
                }

                foreach (var self in expense.SelfExpenses)
                {
                    SetNewIdForSelfExpense(self, tempFilesList);
                }

                foreach (var perdiem in expense.PerdiemExpenses)
                {
                    SetNewIdForPerdiemExpense(perdiem, tempFilesList);
                }

                HandleAttachments(expense, tempFilesList);

                _ExpenseRepository.Add(expense);
                _UnitOfWork.Save();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        void SetNewIdForSelfExpense(SelfExpense subExpense, List<Tuple<Guid, string>> tempFilesList)
        {
            if (tempFilesList == null)
                return;

            var oldGuid = subExpense.SelfExpenseId;
            var newGuid = Guid.NewGuid();
            subExpense.SelfExpenseId = newGuid;

            // this list contains fileNames associated with the old Id
            var fileNames = tempFilesList.Where(entry => entry.Item1 == oldGuid).Select(entry => entry.Item2).ToList();
            tempFilesList.RemoveAll(entry => entry.Item1 == oldGuid);
            foreach (var fileName in fileNames)
                tempFilesList.Add(Tuple.Create(newGuid, fileName));
        }

        void SetNewIdForOverTimeExpense(OverTimeExpense subExpense, List<Tuple<Guid, string>> tempFilesList)
        {
            if (tempFilesList == null)
                return;

            var oldGuid = subExpense.OverTimeExpenseId;
            var newGuid = Guid.NewGuid();
            subExpense.OverTimeExpenseId = newGuid;

            // this list contains fileNames associated with the old Id
            var fileNames = tempFilesList.Where(entry => entry.Item1 == oldGuid).Select(entry => entry.Item2).ToList();
            tempFilesList.RemoveAll(entry => entry.Item1 == oldGuid);
            foreach (var fileName in fileNames)
                tempFilesList.Add(Tuple.Create(newGuid, fileName));
        }

        void SetNewIdForPerdiemExpense(PerdiemExpense subExpense, List<Tuple<Guid, string>> tempFilesList)
        {
            if (tempFilesList == null)
                return;

            var oldGuid = subExpense.PerdiemExpenseId;
            var newGuid = Guid.NewGuid();
            subExpense.PerdiemExpenseId = newGuid;

            // this list contains fileNames associated with the old Id
            var fileNames = tempFilesList.Where(entry => entry.Item1 == oldGuid).Select(entry => entry.Item2).ToList();
            tempFilesList.RemoveAll(entry => entry.Item1 == oldGuid);
            foreach (var fileName in fileNames)
                tempFilesList.Add(Tuple.Create(newGuid, fileName));
        }

        void HandleAttachments(Expense expense, List<Tuple<Guid, string>> tempFilesList)
        {
                foreach (var fileName in tempFilesList)
                {
                    var file = new File();
                    file.FileId = Guid.NewGuid();
                    file.ExpenseId = expense.ExpenseId;
                    file.Name = fileName.Item2;
                    expense.Files.Add(file);
                }
        }

        #endregion

    }
}
