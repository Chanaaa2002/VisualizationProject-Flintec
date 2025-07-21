using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VIsualizationProject.Controllers
{
    public class MonthlyPPController : Controller
    {
        // GET: MonthlyPP
        public ActionResult Index()
        {
            return View("SP/MonthlyPP");
        }
    }
}