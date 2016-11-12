using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Models;

namespace MVC.Controllers
{
    public class AdminController : Controller
    {
        private LawsuitDB DB = new LawsuitDB();
       
        public ActionResult Admin()
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("Login", "AccountController");
            return View();
        }

        public ActionResult General()
        {
            return View(Utils.GetCategoryList(DB));
        }
    }
}
