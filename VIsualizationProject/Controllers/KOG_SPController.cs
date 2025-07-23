using System;
using System.Data.Entity;
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
        
        
        
        // Announcements
        public ActionResult Announcements()
        {
            using (var db = new ApplicationDbContext())
            {
                var announcements = db.Announcements.ToList();
                return View(announcements);
            }   
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add_Announcements(Announcement announcement)
        {
            if (ModelState.IsValid)
            {
                db.Announcements.Add(announcement);
                db.SaveChanges();
                // Redirect where you want, e.g., list page
                return RedirectToAction("Announcements");
            }
            return View(announcement);
        }

        public ActionResult Get_Announcement(int id)
        {
            var ann = db.Announcements.Find(id);
            if (ann == null)
                return HttpNotFound();
            return Json(new
            {
                ann.Id,
                Date = ann.Date.ToString("yyyy-MM-dd"),
                Announcement_name = ann.Announcement_name,
                Publisher = ann.Publisher,
                Publisher_Post = ann.Publisher_Post,
                Type = ann.Type,
                Start_Date = ann.Start_Date.ToString("yyyy-MM-dd"),  
                End_Date = ann.End_Date.ToString("yyyy-MM-dd")        
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update_Announcement(Announcement model)
        {
            if (ModelState.IsValid)
            {
                var ann = db.Announcements.Find(model.Id);
                if (ann == null)
                    return HttpNotFound();
                ann.Date = model.Date;
                ann.Announcement_name = model.Announcement_name;
                ann.Publisher = model.Publisher;
                ann.Publisher_Post = model.Publisher_Post;
                ann.Type = model.Type;
                ann.Start_Date = model.Start_Date;    
                ann.End_Date = model.End_Date;        
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false, error = "Validation failed." });
        }



        // Safety Summary
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add_SafetySummary(Models.Safety_Summary model)
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
        public ActionResult Edit_SafetySummary(Models.Safety_Summary model)
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


        //All Chart

        //public ActionResult All_Charts()
        //{
        //    // Get all accident dates from Safety_Summary table
        //    var safetyDates = db.Safety.Select(s => s.Date).ToList();
        //    ViewBag.SafetyDates = safetyDates.Select(d => d.ToString("yyyy-MM-dd")).ToList();
        //    return View();
        //}
        public ActionResult All_Charts()
        {
            var today = DateTime.Today;
            var announcements = db.Announcements
                .Where(a => a.Start_Date <= today && a.End_Date >= today)
                .OrderByDescending(a => a.Date)
                .ToList();

            var safetyDates = db.Safety.Select(s => s.Date).ToList();
            ViewBag.SafetyDates = safetyDates.Select(d => d.ToString("yyyy-MM-dd")).ToList();
            return View(announcements); // Pass announcements as model
        }



        // Birthday
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add_Birthday(Models.Birthday model)
        {
            if (ModelState.IsValid)
            {
                db.Birthdays.Add(model);
                db.SaveChanges();
                // Optionally redirect to a success page or list
                return RedirectToAction("Birthday");
            }
            return View(model);
        }
        public ActionResult Birthday()
        {
            using (var db = new ApplicationDbContext())
            {
                var birthday = db.Birthdays.ToList();
                return View(birthday);
            }
        }


    }
}
    