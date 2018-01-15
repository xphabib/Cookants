using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PosCookents.Models;

namespace PosCookents.Controllers
{
    public class UserModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View(db.UserModels.ToList());
        }

        public ActionResult Regrstration()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Regrstration([Bind(Include = "Id,Email,Password,ConfirmPassword")] UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                db.UserModels.Add(userModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userModel);
        }

        public ActionResult Login()
        {
            return View();
        }
    }
}
