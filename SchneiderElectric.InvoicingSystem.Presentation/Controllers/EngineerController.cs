using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchneiderElectric.InventorySystem.Presentation.Controllers
{
    public class EngineerController : Controller
    {
        // GET: Engineer
        public ActionResult Index()
        {
            return View("Engineer");
        }
    }
}