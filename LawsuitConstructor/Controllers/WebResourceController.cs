using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Models;

namespace MVC.Controllers
{
    public class WebResourceController : Controller
    {
        private LawsuitDB DB = new LawsuitDB();
        //
        // GET: /WebResource/Index
        public ActionResult Index()
        {
            var missingStaticWebResources = Utils.GetMissingStaticWebResources(DB);
            if (missingStaticWebResources.Count > 0)
                Utils.SetMissingStaticWebResourses(missingStaticWebResources, DB);
            return View(Utils.GetWebResourseList(DB));
        }
        //
        // GET: /WebResource/Add
        public ActionResult Add()
        {
            WebResource webResource = new WebResource();
            return View(webResource);
        }
        //
        // POST: /WebResource/Add
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Add(WebResource webResource)
        {
            if (!ModelState.IsValid)
                return View(webResource);
            DB.WebResources.Add(webResource);
            TempData["Message"] = Utils.TrySaveChanges(DB, Defines.SaveChangesCases.Add);
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int webResourceId = -1)
        {
            if (webResourceId == -1)
                return RedirectToAction("Index");
            WebResource webResource = DB.WebResources.Select(i => i).Where(i => i.WebResourceId == webResourceId).First();
            return View(webResource);
        }
        //
        // POST: /WebResource/Edit
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(WebResource webResource, String button)
        {
            if (button == "remove")
                return RedirectToAction("Remove", "WebResource", new { webResourceId = webResource.WebResourceId });
            if (!ModelState.IsValid)
                return View(webResource);
            DB.Entry(webResource).State = System.Data.EntityState.Modified;
            TempData["Message"] = Utils.TrySaveChanges(DB, Defines.SaveChangesCases.Edit);
            return RedirectToAction("Index");
        }
        //
        // GET: /WebResource/Delete
        public ActionResult Remove(int webResourceId = -1)
        {
            if (webResourceId != -1)
            {
                WebResource webResource = DB.WebResources.Select(i => i).Where(i => i.WebResourceId == webResourceId).First();
                DB.WebResources.Remove(webResource);
                TempData["Message"] = Utils.TrySaveChanges(DB, Defines.SaveChangesCases.Remove);
            }
            return RedirectToAction("Index");
        }
    }
}
