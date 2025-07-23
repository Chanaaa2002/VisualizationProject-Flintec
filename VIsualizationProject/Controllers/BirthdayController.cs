//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using VIsualizationProject.Models;

//namespace VIsualizationProject.Controllers
//{
//    public class BirthdayController : Controller
//    {
//        private ApplicationDbContext db = new ApplicationDbContext();


//        [HttpGet]
//        public ActionResult Add_Birthday()
//        {
//            return View();
//        }


//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Add_Birthday(Birthday model)
//        {
//            if (ModelState.IsValid)
//            {
//                db.birthday.Add(model);
//                db.SaveChanges();
//                TempData["Success"] = "Birthday saved!";
//                return RedirectToAction("Birthday"); // or wherever your summary/list page is
//            }
//            return View(model);



//        }
//}