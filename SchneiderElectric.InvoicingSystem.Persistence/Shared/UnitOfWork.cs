using SchneiderElectric.InvoicingSystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchneiderElectric.InvoicingSystem.Presistence.Shared
{
    public class UnitOfWork : IUnitOFWork
    {
        private readonly IDatabaseContext _database;

        public UnitOfWork(IDatabaseContext database)
        {
            _database = database;

        } 

        public void Save()
        {
            _database.Save();
        }
    }
}
