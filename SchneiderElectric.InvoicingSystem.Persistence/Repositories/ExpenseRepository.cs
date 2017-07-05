using AutoMapper;
using SchneiderElectric.InvoicingSystem.Application.IRepositories;
using SchneiderElectric.InvoicingSystem.Domain;
using SchneiderElectric.InvoicingSystem.Domain.DTO;
using SchneiderElectric.InvoicingSystem.Presistence.Shared;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SchneiderElectric.InvoicingSystem.Presistence.Repositories
{
    public class ExpenseRepository : Repository<Expense>, IExpenseRepository
    {
        
        public ExpenseRepository(IDatabaseContext database) : base(database)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Expense, ExpenseDTO>().
                ForMember(dto => dto.ProjectName,e=>e.MapFrom(x=>x.MockProject.ProjectName))
                .ForMember(dto => dto.EmployeeName, e => e.MapFrom(x => x.MockEmployee.EmployeeName));
            });
        }
        public virtual List<ExpenseDTO> GetAllDTO()
        {
            return _database.Set<Expense>().ProjectToList<ExpenseDTO>();
        }
        public virtual List<ExpenseDTO> FindDTOBy(Expression<Func<Expense, bool>> expression)
        {
            return _database.Set<Expense>().Where(expression).ProjectToList<ExpenseDTO>();
        }
        public void Attach(Expense expense)
        {
            var local = _database.Expenses.Local.Where(e => e.ExpenseId == expense.ExpenseId).FirstOrDefault();
            if (local != null)
                _database.Entry(local).State = EntityState.Detached;
                expense.MockProject = null;
                expense.MockEmployee = null;
            foreach (var overtime in expense.OverTimeExpenses)
            {
                overtime.Currency = null;
                _database.Entry(overtime).State = EntityState.Modified;

            }
            foreach (var self in expense.SelfExpenses)
            {
                self.Currency = null;
                _database.Entry(self).State = EntityState.Modified;
            }
            foreach (var perdiem in expense.PerdiemExpenses)
            {
                perdiem.Currency = null;
                _database.Entry(perdiem).State = EntityState.Modified;
            }

            foreach (var comment in expense.RejectedComments)
            {
                // get the comments that are new only
                var _comment = _database.RejctedComments.Where(e => e.RejectedCommentId == comment.RejectedCommentId).FirstOrDefault();
                if (_comment == null)
                {
                    _database.Entry(comment).State = EntityState.Added;
                }
            }
            expense.RejectedComments = null;

            _database.Entry(expense).State = EntityState.Modified;

        }
        public Expense FindById(Guid id)
        {
            return _database.Set<Expense>().Find(id);
        }

        public ExpenseDTO FindDTOById(Guid id)
        {
            return Mapper.Map<Expense, ExpenseDTO>(this.FindById(id));
        }
    }
}
