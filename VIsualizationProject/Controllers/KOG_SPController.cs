using Microsoft.IdentityModel.Tokens;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using VIsualizationProject.Models;


namespace VIsualizationProject.Controllers
{
    public class KOG_SPController : Controller
    {
        private ProductionVisualization db = new ProductionVisualization();

        [HttpGet]
       
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
        public ActionResult Attendance()
        {
            return View();
        } 
        public ActionResult SP_OGQCchart()
        {
            return View();
        }
        public ActionResult five_s()
        {
            return View();
        }

        // Announcements
        public ActionResult Announcements()
        {
            var pl = (Session["PL"] ?? "SP").ToString();
            var loc = (Session["Location"] ?? "KOG").ToString();

            using (var db = new ProductionVisualization())
            {
                var announcements = db.Announcements
                    .Where(a => a.Production_Line == pl && a.Location == loc)
                    .ToList();

                return View(announcements);
            }
        }
        [HttpGet]
        public ActionResult Add_Announcements()
        {
            var pl = (Session["PL"] ?? "SP").ToString();
            var loc = (Session["Location"] ?? "KOG").ToString();

            ViewBag.ProductionLine = pl;
            ViewBag.Location = loc;

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add_Announcements(Announcement announcement)
        {
            // Set from session for security
            announcement.Production_Line = (Session["PL"] ?? "SP").ToString();
            announcement.Location = (Session["Location"] ?? "KOG").ToString();

            if (ModelState.IsValid)
            {
                db.Announcements.Add(announcement);
                db.SaveChanges();
                return RedirectToAction("Announcements");
            }

            // If error, preserve values for the form
            ViewBag.ProductionLine = announcement.Production_Line;
            ViewBag.Location = announcement.Location;
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
        [HttpGet]
        public ActionResult Add_SafetySummary()
        {
            // Get session values for Production Line and Location
            var pl = (Session["PL"] ?? "SP").ToString();
            var loc = (Session["Location"] ?? "KOG").ToString();

            ViewBag.ProductionLine = pl;
            ViewBag.Location = loc;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add_SafetySummary(Models.Safety_Summary model)
        {
            // Always set these from session for security
            model.Production_Line = (Session["PL"] ?? "SP").ToString();
            model.Location = (Session["Location"] ?? "KOG").ToString();

            if (ModelState.IsValid)
            {
                db.Safety.Add(model);
                db.SaveChanges();
                TempData["Success"] = "Safety Summary saved!";
                return RedirectToAction("Safety_Summary");
            }
            // Preserve values for redisplay in case of error
            ViewBag.ProductionLine = model.Production_Line;
            ViewBag.Location = model.Location;
            return View(model);
        }

        public ActionResult Safety_Summary()
        {
            // Filter list by session-based Production Line and Location
            var pl = (Session["PL"] ?? "SP").ToString();
            var loc = (Session["Location"] ?? "KOG").ToString();

            var list = db.Safety
                .Where(x => x.Production_Line == pl && x.Location == loc)
                .OrderByDescending(x => x.Date)
                .Take(10)
                .ToList();

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
                Injury_status = summary.Injury_status,
                Production_Line = summary.Production_Line,
                Location = summary.Location
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
            // Optionally update these as well:
            // summary.Production_Line = (Session["PL"] ?? "SP").ToString();
            // summary.Location = (Session["Location"] ?? "KOG").ToString();

            db.SaveChanges();

            return Json(new { success = true });
        }
       
        

        // All Charts
        public ActionResult All_Charts()
        {
            var today = DateTime.Today;

            // Announcements for the model
            var announcements = db.Announcements
                .Where(a => a.Start_Date <= today && a.End_Date >= today)
                .OrderByDescending(a => a.Date)
                .ToList();

            // Safety Dates for calendar
            var safetyDates = db.Safety.Select(s => s.Date).ToList();
            ViewBag.SafetyDates = safetyDates.Select(d => d.ToString("yyyy-MM-dd")).ToList();

            var pl = (Session["PL"] ?? "SP").ToString();
            var loc = (Session["Location"] ?? "KOG").ToString();
            var birthdaysToday = db.Birthdays
                .Where(b => b.Date.Month == today.Month
                            && b.Date.Day == today.Day
                            && b.Production_Line == pl
                            && b.Location == loc)
                .ToList();
            ViewBag.BirthdaysToday = birthdaysToday;

            // Get latest layout for current production line and location
            var latestLayout = db.LayOut
                .Where(l => l.Production_Line == pl && l.Location == loc)
                .OrderByDescending(l => l.Date)
                .FirstOrDefault();
            ViewBag.LatestLayout = latestLayout;

            // Pass announcements as model
            return View(announcements);
        }



        // Birthday
        [HttpGet]
        public ActionResult Add_Birthday()
        {
            var pl = (Session["PL"] ?? "SP").ToString();
            var loc = (Session["Location"] ?? "KOG").ToString();

            ViewBag.ProductionLine = pl;
            ViewBag.Location = loc;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add_Birthday(Models.Birthdays model, HttpPostedFileBase LayoutImage)
        {
            try
            {
                // Always set these from session to ensure security
                model.Production_Line = (Session["PL"] ?? "SP").ToString();
                model.Location = (Session["Location"] ?? "KOG").ToString();

                // Handle file upload
                if (LayoutImage != null && LayoutImage.ContentLength > 0)
                {
                    using (var reader = new System.IO.BinaryReader(LayoutImage.InputStream))
                    {
                        model.Photo = reader.ReadBytes(LayoutImage.ContentLength);
                    }
                }
                else
                {
                    ModelState.AddModelError("Photo", "Photo is required.");
                }

                if (ModelState.IsValid)
                {
                    db.Birthdays.Add(model);
                    db.SaveChanges();
                    TempData["success"] = "";
                    return RedirectToAction("Birthday");
                }

                // If validation fails, repopulate the ViewBag so fields aren't blank
                ViewBag.ProductionLine = model.Production_Line;
                ViewBag.Location = model.Location;
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error saving to database: " + ex.Message);
                ViewBag.ProductionLine = model.Production_Line;
                ViewBag.Location = model.Location;
                return View(model);
            }
        }
        public ActionResult Birthday()
        {
            string pl = (Session["PL"] ?? "SP").ToString();
            string loc = (Session["Location"] ?? "KOG").ToString();

            var today = DateTime.Today;
            var birthdays = db.Birthdays
                .Where(b => b.Date.Month == today.Month
                            && b.Date.Day == today.Day
                            && b.Production_Line == pl
                            && b.Location == loc)
                .OrderBy(b => b.Date)
                .ToList();

            return View(birthdays);
        }
        public ActionResult GetBirthdayPhoto(int id)
        {
            var b = db.Birthdays.Find(id);
            if (b != null && b.Photo != null)
            {
                return File(b.Photo, "image/jpeg"); // adjust to image/png if needed
            }
            return null;
        }



        // Five_S
        public ActionResult GetFiveSAuditData()
        {
            using (var flintecDb = new FlintecFiveS())
            {
                var list = flintecDb.five_S
                    .OrderByDescending(x => x.Date)
                    .ToList()
                    .Select(item => new
                    {
                        item.Date,
                        Sort = item.S1_1 + item.S1_2 + item.S1_3 + item.S1_4 + item.S1_5 + item.S1_6 + item.S1_7,
                        Straighten = item.S2_1 + item.S2_2 + item.S2_3 + item.S2_4 + item.S2_5 + item.S2_6 + item.S2_7 + item.S2_8 + item.S2_9 + item.S2_10 + item.S2_11 + item.S2_12 + item.S2_13 + item.S2_14,
                        Shine = item.S3_1 + item.S3_2 + item.S3_3 + item.S3_4 + item.S3_5 + item.S3_6 + item.S3_7 + item.S3_8,
                        Standardize = item.S4_1 + item.S4_2 + item.S4_3 + item.S4_4 + item.S4_5 + item.S4_6 + item.S4_7 + item.S4_8 + item.S4_9 + item.S4_10,
                        Sustain = item.S5_1 + item.S5_2 + item.S5_3 + item.S5_4 + item.S5_5 + item.S5_6 + item.S5_7,
                    })
                    .ToList()
                    .Select(x => new
                    {
                        x.Date,
                        x.Sort,
                        x.Straighten,
                        x.Shine,
                        x.Standardize,
                        x.Sustain,
                        Score = x.Sort + x.Straighten + x.Shine + x.Standardize + x.Sustain
                    });

                return Json(list, JsonRequestBehavior.AllowGet);
            }
        }



        // OGQC
        public ActionResult SP_OGQC()
        {
            // Only retrieve SP production line data
            var spData = db.OGQCqu
                .Where(x => x.Production_Line == "SP")
                .OrderByDescending(x => x.Detected_Date)
                .ToList();

            return View(spData);
        }
        public ActionResult GetOGQCForSP()
        {
            var spData = db.OGQCqu
                .Where(x => x.Production_Line == "SP")
                .OrderByDescending(x => x.Detected_Date)
                .Select(x => new {
                    x.Detected_Date,
                    x.Product,
                    x.Lot_Size,
                    x.Defect_Qty,
                    x.Production_Line
                }).ToList();

            return Json(spData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSPQtyByYear(int year)
        {
            // Group by month for the given year
            var spData = db.OGQCqu
                .Where(x => x.Production_Line == "SP" && x.Detected_Date.Year == year)
                .AsEnumerable()
                .GroupBy(x => x.Detected_Date.Month)
                .Select(g => new
                {
                    Month = g.Key, // 1-12
                    DefectQty = g.Sum(x => int.TryParse(x.Defect_Qty, out int qty) ? qty : 0)
                })
                .ToList();

            // Ensure all months are present
            var result = Enumerable.Range(1, 12)
                .Select(m => new {
                    Month = m,
                    DefectQty = spData.FirstOrDefault(x => x.Month == m)?.DefectQty ?? 0
                }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        // Layout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LayOut(Models.Layout model, HttpPostedFileBase LayoutImage)
        {
            // 1. Set Production_Line and Location from Session (if needed)
            model.Production_Line = (Session["PL"] ?? "SP").ToString();
            model.Location = (Session["Location"] ?? "KOG").ToString();

            // 2. Handle the image upload
            if (LayoutImage != null && LayoutImage.ContentLength > 0)
            {
                using (var binaryReader = new System.IO.BinaryReader(LayoutImage.InputStream))
                {
                    model.LayoutImageData = binaryReader.ReadBytes(LayoutImage.ContentLength);
                }
            }
            else
            {
                ModelState.AddModelError("LayoutImage", "Layout image is required.");
                ViewBag.ProductionLine = model.Production_Line;
                ViewBag.Location = model.Location;
                return View(model);
            }

            // 3. Save to database if valid
            if (ModelState.IsValid)
            {
                try
                {
                    // Ensure the model has all required data
                    if (model.Date == default(DateTime))
                    {
                        model.Date = DateTime.Today;
                    }
                    
                    db.LayOut.Add(model);
                    db.SaveChanges();
                    TempData["success"] = "Layout saved successfully!";
                    return RedirectToAction("LayOut");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error saving to database: " + ex.Message);
                    ViewBag.ProductionLine = model.Production_Line;
                    ViewBag.Location = model.Location;
                    return View(model);
                }
            }
            else
            {
                // Log validation errors for debugging - collect errors first to avoid collection modification
                var validationErrors = ModelState.Values.SelectMany(v => v.Errors).ToList();
                foreach (var error in validationErrors)
                {
                    ModelState.AddModelError("", "Validation Error: " + error.ErrorMessage);
                }
            }

            // 4. If not valid, redisplay form
            ViewBag.ProductionLine = model.Production_Line;
            ViewBag.Location = model.Location;
            return View(model);
        }

        [HttpGet]
        public ActionResult LayOut()
        {
            var pl = (Session["PL"] ?? "SP").ToString();
            var loc = (Session["Location"] ?? "KOG").ToString();

            ViewBag.ProductionLine = pl;
            ViewBag.Location = loc;

            // Get existing layouts for display
            var layouts = db.LayOut
                .Where(l => l.Production_Line == pl && l.Location == loc)
                .OrderByDescending(l => l.Date)
                .ToList();
            ViewBag.LayOut = layouts;

            return View();
        }

        public ActionResult GetLayoutImage(int id)
        {
            var layout = db.LayOut.Find(id);
            if (layout != null && layout.LayoutImageData != null)
            {
                // You can adjust the content type if your images are not JPEG
                return File(layout.LayoutImageData, "image/jpeg");
            }
            return HttpNotFound();
        }

        // Get latest layout data as JSON for AJAX updates
        public ActionResult GetLatestLayout()
        {
            var pl = (Session["PL"] ?? "SP").ToString();
            var loc = (Session["Location"] ?? "KOG").ToString();

            // Get the most recent layout for the current production line and location
            var latestLayout = db.LayOut
                .Where(l => l.Production_Line == pl && l.Location == loc)
                .OrderByDescending(l => l.Date)
                .FirstOrDefault();

            // Log for debugging
            System.Diagnostics.Debug.WriteLine($"GetLatestLayout called - PL: {pl}, Location: {loc}, Layouts found: {db.LayOut.Count()}, Filtered layouts: {db.LayOut.Where(l => l.Production_Line == pl && l.Location == loc).Count()}");

            if (latestLayout != null)
            {
                var result = new
                {
                    Id = latestLayout.Id,
                    Production_Line = latestLayout.Production_Line,
                    Location = latestLayout.Location,
                    Date = latestLayout.Date.ToString("dd, MMMM, yyyy"),
                    ImageUrl = Url.Action("GetLayoutImage", "KOG_SP", new { id = latestLayout.Id }),
                    Timestamp = DateTime.Now.Ticks, // Add timestamp for debugging
                    DebugInfo = $"Total layouts in DB: {db.LayOut.Count()}, Current PL: {pl}, Current Location: {loc}"
                };
                
                System.Diagnostics.Debug.WriteLine($"Returning layout data: {Newtonsoft.Json.JsonConvert.SerializeObject(result)}");
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            System.Diagnostics.Debug.WriteLine($"No layout found for PL: {pl}, Location: {loc}");
            return Json(null, JsonRequestBehavior.AllowGet);
        }

    }
}
    