using SchneiderElectric.InvoicingSystem.Application.Commands.Interfaces;
using SchneiderElectric.InvoicingSystem.Application.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchneiderElectric.InvoicingSystem.Presentation.Controllers
{
    public class TestController : Controller
    {
        private IExpenseCommands _ExpenseCommands;
        private IEmployeeRepository _EmployeeRepository;

        public TestController(IExpenseCommands expenseCommands, IEmployeeRepository employeeRepository)
        {
            _ExpenseCommands = expenseCommands;
            _EmployeeRepository = employeeRepository;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string sapNumber, string password)
        {
            // password is not checked currently. anyhting works
            // set one of these names in the session to determine the type of the logged in user
            //string[] employeeTypes = new[] { "Engineer", "Admin", "EM", "Finance" };

            var employeeType = _EmployeeRepository.GetEmployeeType(sapNumber);
            // set sessions
            Session["employeeType"] = employeeType;
            Session["userSapId"] = sapNumber;

            // admin, em or finance
            if (employeeType != Domain.EmployeeType.Engineer)
            {
                return RedirectToAction("Index", "Approval");
            }
            // then it's an engineer
            else
            {
                // this controller is not yes created
                // to-do: implement it
                return RedirectToAction("Index", "Engineer");
            }
        }
    }
}