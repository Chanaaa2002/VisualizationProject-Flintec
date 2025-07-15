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
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult Output()
        {
            return View();
        }
        public ActionResult Birthday()
        {
            return View();
        }
        public ActionResult Add_Announcements()
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
        [HttpGet]
        public ActionResult Get_SafetySummary(int id)
        {
            var summary = db.Safety.Find(id);
            if (summary == null)
                return HttpNotFound();

            return Json(new
            {
                Id = summary.Id,
                Date = summary.Date.ToString("yyyy-MM-dd"),
                Employee_Name = summary.Employee_Name,
                Description = summary.Description,
                Injury_status = summary.Injury_status
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_SafetySummary(Safety_Summary model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, error = "Invalid data" });

            var summary = db.Safety.Find(model.Id);
            if (summary == null)
                return Json(new { success = false, error = "Record not found" });

            summary.Date = model.Date;
            summary.Employee_Name = model.Employee_Name;
            summary.Description = model.Description;
            summary.Injury_status = model.Injury_status;
            db.SaveChanges();

            return Json(new { success = true });
        }

        
        public ActionResult Announcements()
        {
            var list = db.announcements.OrderByDescending(x => x.Date).ToList();
            return View(list);
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add_Announcements(Announcements model)
        {
            if (ModelState.IsValid)
            {
                db.announcements.Add(model);
                db.SaveChanges();
                if (Request.IsAjaxRequest())
                    return Json(new { success = true });

                TempData["Success"] = "Announcement saved!";
                return RedirectToAction("Announcements");
            }
            if (Request.IsAjaxRequest())
                return Json(new { success = false });
            return View(model);
        }
        public ActionResult Partial_AnnouncementsTable()
        {
            var list = db.announcements.OrderByDescending(x => x.Date).ToList();
            return PartialView("_AnnouncementsTable", list);
        }
    }
}