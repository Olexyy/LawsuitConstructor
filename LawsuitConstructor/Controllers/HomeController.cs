using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Models;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private LawsuitDB DB = new LawsuitDB();
        
        public ActionResult Index()
        {
            var model = Utils.GetStaticWebResourse(Defines.StaticWebResoucesEnum.HomePage, DB);
            return View(model);
        }

        public ActionResult СourtFee()
        {
            var model = Utils.GetStaticWebResourse(Defines.StaticWebResoucesEnum.СourtFee, DB);
            return View("Index", model);
        }
        public ActionResult Advices()
        {
            var model = Utils.GetStaticWebResourse(Defines.StaticWebResoucesEnum.Advices, DB);
            return View("Index", model);
        }
        public ActionResult View(int webResourceId = -1)
        {
            if (webResourceId == -1)
                View("Error");
            var model = DB.WebResources.Where(i=>i.WebResourceId == webResourceId).First();
            return View("Index", model);
        }
        public ActionResult General()
        {
            return View(Utils.GetCategoryList(DB));
        }
    }
}
