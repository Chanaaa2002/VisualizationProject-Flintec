using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VIsualizationProject.Models;
using System.Globalization;
using Newtonsoft.Json;

namespace VIsualizationProject.Controllers
{
    public class QualityController : Controller
    {
        private ProductionVisualization db = new ProductionVisualization();

      
        public ActionResult AddOGQC()
        {
            return View();
        }
        


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOGQC(OGQC model)
        {
            if (ModelState.IsValid)
            {
                db.OGQCqu.Add(model);
                db.SaveChanges();
                return RedirectToAction("OGQC"); 
            }
            return View(model);
        }     
        public ActionResult OGQC()
        {
            var items = db.OGQCqu.OrderByDescending(x => x.Detected_Date).ToList();
            return View(items);
        }


        public ActionResult OGQC_Chart()
        {
           
            var ogqcData = db.OGQCqu.ToList();

           
            var months = ogqcData
                .Select(x => x.Detected_Date.ToString("yyyy-MM"))
                .Distinct()
                .OrderBy(x => x)
                .ToList();

            var lines = ogqcData
                .Select(x => x.Production_Line)
                .Distinct()
                .OrderBy(x => x)
                .ToArray();

           
            var summary = new Dictionary<string, Dictionary<string, int>>();
            foreach (var line in lines)
            {
                summary[line] = new Dictionary<string, int>();
                foreach (var month in months)
                {
                    var total = ogqcData
                        .Where(x => x.Production_Line == line && x.Detected_Date.ToString("yyyy-MM") == month)
                        .Sum(x => int.TryParse(x.Defect_Qty, out int qty) ? qty : 0);
                    summary[line][month] = total;
                }
            }

            // 4. Pass to ViewBag
            ViewBag.Months = months;
            ViewBag.Lines = lines;
            ViewBag.Summary = summary;

            return View();
        }
    }
}