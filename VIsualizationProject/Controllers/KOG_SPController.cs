using System;
using System.Linq;
using System.Web.Mvc;
using VIsualizationProject.Migrations;
using VIsualizationProject.Models;

namespace VIsualizationProject.Controllers
{
    public class KOG_SPController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public ActionResult Add_SafetySummary()
        {
            return View();
        }
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult Output()
        {
            return View();
        }

        public ActionResult MonthlyPP()
        {
            return View();
        }

        public ActionResult Birthday()
        {
            return View();
        }
        public ActionResult Add_Birthday()
        {
            return View();
        }
        public ActionResult Add_Announcements()
        {
            return View();
        }

        // Safety Summary
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
            var list = db.Safety.OrderByDescending(x => x.Date).Take(10).ToList();
            return View(list);
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
        public ActionResult All_Charts()
        {
            // Get all accident dates from Safety_Summary table
            var safetyDates = db.Safety.Select(s => s.Date).ToList();
            ViewBag.SafetyDates = safetyDates.Select(d => d.ToString("yyyy-MM-dd")).ToList();
            return View();
        }




        // Announcements

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
                return RedirectToAction("Announcements");
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            ViewBag.Errors = errors;
            return View(model);
        }

        public ActionResult Partial_AnnouncementsTable()
        {
            var list = db.announcements.OrderByDescending(x => x.Date).ToList();
            return PartialView("_AnnouncementsTable", list);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete_Announcement(int id)
        {
            var ann = db.announcements.Find(id);
            if (ann != null)
            {
                db.announcements.Remove(ann);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false, error = "Record not found." });
        }
        [HttpGet]
        public ActionResult Get_Announcement(int id)
        {
            var ann = db.announcements.Find(id);
            if (ann == null)
                return HttpNotFound();

            return Json(new
            {
                Id = ann.Id,
                Date = ann.Date.ToString("yyyy-MM-dd"),
                Announcement = ann.Announcement,
                Publisher = ann.Publisher,
                Piblisher_Post = ann.Publisher_Post,
                Type = ann.Type
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_Announcement(Announcements model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, error = "Invalid data" });

            var ann = db.announcements.Find(model.Id);
            if (ann == null)
                return Json(new { success = false, error = "Record not found" });

            ann.Date = model.Date;
            ann.Announcement = model.Announcement;
            ann.Publisher = model.Publisher;
            ann.Publisher_Post = model.Publisher_Post;
            ann.Type = model.Type;
            db.SaveChanges();

            return Json(new { success = true });
        }

        // POST: SP/Add_Birthday
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add_Birthday(Birthday model)
        {
            if (ModelState.IsValid)
            {
                db.birthday.Add(model);
                db.SaveChanges();
                // redirect to a confirmation or list page
                return RedirectToAction("Birthday", "KOG_SP");
            }
            // if validation failed, redisplay form with errors
            return View(model);

        }
}
    