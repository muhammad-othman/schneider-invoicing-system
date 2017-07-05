using SchneiderElectric.InvoicingSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchneiderElectric.InvoicingSystem.Presentation.Controllers
{
    public class ApprovalController : Controller
    {
        // GET: Approval
        public ActionResult Index()
        {
            var employeeTypeAsString = Enum.GetName(typeof(EmployeeType), (int)Session["employeeType"]);
            ViewData["employeeType"] = employeeTypeAsString;
            return View("Approval");
        }
    }
}