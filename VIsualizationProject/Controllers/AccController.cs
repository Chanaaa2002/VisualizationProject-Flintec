using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VIsualizationProject.Models;

namespace VIsualizationProject.Controllers
{
    public class AccController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.ShowNavbar = false;
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserVisual lm)
        {
            try
            {
                var user = db.UserVisuals
                    .FirstOrDefault(u => u.Username == lm.Username && u.Password == lm.Password);

                if (user != null)
                {
                    Session["username"] = user.Username;
                    Session["Role"] = user.Role;
                    Session["PL"] = user.PL;
                    Session["Location"] = user.Location;

                    string pl = user.PL?.ToUpper();
                    string location = user.Location?.ToUpper();

                    if (location == "KOG")
                    {
                        switch (pl)
                        {
                            case "SP": return RedirectToAction("Dashboard", "KOG_SP");
                            case "RS1": return RedirectToAction("Dashboard", "KOG_RS1");
                            case "RS2": return RedirectToAction("Dashboard", "KOG_RS2");
                            case "PB": return RedirectToAction("Dashboard", "KOG_PB");
                            case "MD1": return RedirectToAction("Dashboard", "KOG_MD1");
                            case "GAGING": return RedirectToAction("Dashboard", "KOG_GAGING");
                        }
                    }
                    else if (location == "KTY")
                    {
                        switch (pl)
                        {
                            case "MD2": return RedirectToAction("Dashboard", "KTY_MD2");
                            case "SM": return RedirectToAction("Dashboard", "KTY_SM");
                            case "MS1": return RedirectToAction("Dashboard", "KTY_MS1");
                            case "MS2": return RedirectToAction("Dashboard", "KTY_MS2");
                            case "WELDED": return RedirectToAction("Dashboard", "KTY_WELDED");
                            case "POTTED": return RedirectToAction("Dashboard", "KTY_POTTED");
                            case "ROCKER": return RedirectToAction("Dashboard", "KTY_ROCKER");
                            case "GAGING": return RedirectToAction("Dashboard", "KTY_GAGING");
                        }
                    }

                    TempData["errorMsg"] = "<script>alert('Access not configured for this production line');</script>";
                    return View();
                }
                else
                {
                    TempData["errorMsg"] = "<script>alert('Invalid username or password');</script>";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["errorMsg"] = $"<script>alert('Login error: {ex.Message}');</script>";
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login", "Acc");
        }

        public ActionResult SessionCheck()
        {
            string message = string.Empty;
            if (Session["username"] == null)
            {
                message = "Session expired. Please login again.";
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }
    }
}