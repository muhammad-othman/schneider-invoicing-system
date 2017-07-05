using SchneiderElectric.InvoicingSystem.Application.IRepositories;
using SchneiderElectric.InvoicingSystem.Domain;
using SchneiderElectric.InvoicingSystem.Presistence.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchneiderElectric.InvoicingSystem.Presistence.Repositories
{
    public class EmployeeRepository : Repository<MockEmployee>, IEmployeeRepository
    {
        public EmployeeRepository(IDatabaseContext database, IProjectRepository projectRepository, IDepartmentRepository departmentRepository) : base(database)
        {
            _projectRepository = projectRepository;
            _deparmtnetRepository = departmentRepository;
        }
        private IProjectRepository _projectRepository;
        private IDepartmentRepository _deparmtnetRepository;

        public EmployeeType GetEmployeeType(string employeeSap)
        {
            if (this.FindBy(e => e.EmployeeSapId == employeeSap)?.First()?.MockDepartment?.DepartmentName == "Finance")
                return EmployeeType.Finance;
            if (_deparmtnetRepository.FindBy(d => d.EMId == employeeSap)?.Count() > 0)
                return EmployeeType.EM;
            if (_projectRepository.FindBy(d => d.PAId == employeeSap)?.Count() > 0)
                return EmployeeType.PA;

            return EmployeeType.Engineer;
        }
        public MockEmployee FindById(string id)
        {
            return _database.Set<MockEmployee>().Find(id);
        }
    }
}
