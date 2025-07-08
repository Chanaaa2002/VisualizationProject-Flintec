using System;
using System.Linq;
using System.Web.Mvc;
using VIsualizationProject.Models;

namespace VIsualizationProject.Controllers
{
    public class SPController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // Shows the add form (GET)
        [HttpGet]
        public ActionResult Add_SafetySummary()
        {
            return View();
        }

        // Handles form post (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add_SafetySummary(Safety_Summary model)
        {
            if (ModelState.IsValid)
            {
                db.Safety.Add(model);
                db.SaveChanges();
                TempData["Success"] = "Safety Summary saved!";
                return RedirectToAction("Safety_Summary");
            }
            return View(model);
        }

           
        public ActionResult Safety_Summary()
        {
            var list = db.Safety.OrderByDescending(x => x.Date).ToList();
            return View(list);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete_SafetySummary(int id)
        {
            var summary = db.Safety.Find(id);
            if (summary != null)
            {
                db.Safety.Remove(summary);
                db.SaveChanges();
                TempData["Success"] = "Safety Summary deleted!";
            }
            return RedirectToAction("Safety_Summary");
        }
        


    }
}