using SchneiderElectric.InvoicingSystem.Application.IRepositories;
using SchneiderElectric.InvoicingSystem.Application.Queries.Interfaces;
using SchneiderElectric.InvoicingSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchneiderElectric.InvoicingSystem.Domain.DTO;

namespace SchneiderElectric.InvoicingSystem.Application.Queries
{
    public class ExpensesQueries: IExpensesQueries
    {
        private IExpenseRepository _expenseRepository;
        private ICommentRepository _commentRepository;
        private IProjectRepository _projectRepository;
        private IDepartmentRepository _deparmtnetRepository;
        private IEmployeeRepository _employeeRepository;
   

        public ExpensesQueries(IExpenseRepository expenseRepository, ICommentRepository commentRepository,
            IProjectRepository projectRepository,IDepartmentRepository departmentRepository, IEmployeeRepository employeeRepository)
        {
            _expenseRepository = expenseRepository;
            _commentRepository = commentRepository;
            _projectRepository = projectRepository;
            _deparmtnetRepository = departmentRepository;
            _employeeRepository = employeeRepository;
        }


        public Expense GetById(Guid expenseId)
        {
           return _expenseRepository.FindById(expenseId);
        }
        public List<ExpenseDTO> GetAll()
        {
            return _expenseRepository.GetAllDTO().ToList();
        }
        public List<ExpenseDTO> GetByProject(string projectSap)
        {
            return _expenseRepository.FindDTOBy(p=>p.ProjectSapId == projectSap
                                               && p.ExpenseState != ExpenseState.AtEmployeeSaved).ToList();
        }
        public List<ExpenseDTO> GetByProjectActive(string projectSap)
        {
            return _expenseRepository.FindDTOBy(p => p.ProjectSapId == projectSap
                                               && p.ExpenseState == ExpenseState.AtPA).ToList();
        }

        public List<ExpenseDTO> GetByDepartment(int departmentId)
        {
            return _expenseRepository.FindDTOBy(p => p.MockEmployee.DepartmentId == departmentId 
                                               && p.ExpenseState != ExpenseState.AtEmployeeSaved).ToList();
        }
        public List<ExpenseDTO> GetByDepartmentActive(int departmentId)
        {
            return _expenseRepository.FindDTOBy(p => p.MockEmployee.DepartmentId == departmentId
                                               && p.ExpenseState == ExpenseState.AtEM).ToList();
        }

        public List<ExpenseDTO> GetActiveExpenses(string employeeSap)
        {
            List<ExpenseDTO> lst = new List<ExpenseDTO>();

            var type = _employeeRepository.GetEmployeeType(employeeSap);

            if (type == EmployeeType.Finance)
                return _expenseRepository.FindDTOBy(e => e.ExpenseState == ExpenseState.AtFinance).ToList();
            if (type == EmployeeType.EM)
            {
                foreach (var department in _deparmtnetRepository.FindBy(d => d.EMId == employeeSap))
                    lst.AddRange(GetByDepartmentActive(department.DepartmentId));
                return lst;
            }
            else if (type == EmployeeType.PA)
            { 
                foreach (var project in _projectRepository.FindBy(d => d.PAId == employeeSap))
                    lst.AddRange(GetByProjectActive(project.ProjectSapId));
                return lst;
            }

            return _expenseRepository.FindDTOBy(p => p.EmployeeSapId == employeeSap && p.ExpenseState== ExpenseState.AtEmployeeSaved).ToList();
        }

        public List<ExpenseDTO> GetAllAvailableExpenses(string employeeSap)
        {
            List<ExpenseDTO> lst = new List<ExpenseDTO>();

            var type = _employeeRepository.GetEmployeeType(employeeSap);
            if (type == EmployeeType.Finance)
                return _expenseRepository.FindDTOBy(p =>   p.ExpenseState != ExpenseState.AtEmployeeSaved
                                                  && p.ExpenseState != ExpenseState.AtPACanceled).ToList();

            if (type == EmployeeType.EM)
            { 
                foreach (var department in _deparmtnetRepository.FindBy(d => d.EMId == employeeSap))
                    lst.AddRange(GetByDepartment(department.DepartmentId));
                return lst;
            }

            else if (type == EmployeeType.PA)
            { 
                foreach (var project in _projectRepository.FindBy(d => d.PAId == employeeSap))
                    lst.AddRange(GetByProject(project.ProjectSapId));
                return lst;
            }

            return _expenseRepository.FindDTOBy(p => p.EmployeeSapId == employeeSap).ToList();
        }
        public List<RejectedComment> GetRejctedComments(Guid expenseId)
        {
            return _commentRepository.FindBy(r => r.ExpenseId == expenseId).ToList();
        }

        public List<ExpenseDTO> GetCanceled(string employeeSap)
        {
            List<ExpenseDTO> lst = new List<ExpenseDTO>();

            var type = _employeeRepository.GetEmployeeType(employeeSap);
             if (type == EmployeeType.PA) { 
                foreach (var project in _projectRepository.FindBy(d => d.PAId == employeeSap))
                    lst.AddRange(GetByProject(project.ProjectSapId).Where(e => e.ExpenseState == ExpenseState.AtPACanceled));
                return lst;
            }
            return null;
        }

        public List<ExpenseDTO> GetRejected(string employeeSap)
        {
            List<ExpenseDTO> lst = new List<ExpenseDTO>();
            var type = _employeeRepository.GetEmployeeType(employeeSap);
            if (type == EmployeeType.PA)
                foreach (var project in _projectRepository.FindBy(d => d.PAId == employeeSap))
                    lst.AddRange(GetByProject(project.ProjectSapId).Where(e => e.ExpenseState == ExpenseState.AtPARejected));

            return lst;
        }


        public ExpenseDTO GetDTOById(Guid expenseId)
        {
            return _expenseRepository.FindDTOById(expenseId);
        }
    }
}
