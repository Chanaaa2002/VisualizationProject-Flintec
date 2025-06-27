using VIsualizationProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SearchApplication.Controllers
{
    public class AccController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Login()
        {
            ViewBag.ShowNavbar = false;
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var user = _context.Users.SingleOrDefault(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                Session["Username"] = user.Username;
                Session["UserRole"] = user.Role;

                return RedirectToAction("Home", "SP");
            }

            ViewBag.ErrorMessage = "Invalid username or password";
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Acc");
        }
    }
}