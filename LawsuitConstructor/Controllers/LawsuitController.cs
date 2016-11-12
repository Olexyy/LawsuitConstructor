using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Models;

namespace MVC.Controllers
{
    public class LawsuitController : Controller
    {
        private LawsuitDB DB = new LawsuitDB();      
        //
        // GET: /Lawsuit/Index
        public ActionResult Index()
        {
            return View(Utils.GetLawsuitList(DB));
        }
        //
        // GET: /Lawsuit/Add
        public ActionResult Add()
        {
            this.ViewData["SubCategories"] = DB.SubCategories.ToList();
            this.ViewData["WebResources"] = Utils.GetCustomWebResourseList(DB, true);
            Lawsuit lawsuit = new Lawsuit();
            return View(lawsuit);
        }
        //
        // POST: /Lawsuit/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Lawsuit lawsuit)
        {
            if (!ModelState.IsValid)
            {
                this.ViewData["SubCategories"] = DB.SubCategories.ToList();
                this.ViewData["WebResources"] = Utils.GetCustomWebResourseList(DB, true);
                return View(lawsuit);
            }
            DB.Lawsuits.Add(lawsuit);
            TempData["Message"] = Utils.TrySaveChanges(DB, Defines.SaveChangesCases.Add);
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int lawsuitId = -1)
        {
            this.ViewData["SubCategories"] = DB.SubCategories.ToList();
            this.ViewData["WebResources"] = Utils.GetCustomWebResourseList(DB, true);
            if (lawsuitId == -1)
                return RedirectToAction("Index");
            Lawsuit lawsuit = DB.Lawsuits.Select(i => i).Where(i => i.LawsuitId == lawsuitId).First();
            return View(lawsuit);
        }
        //
        // POST: /Lawsuit/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Lawsuit lawsuit, String button)
        {
            if (button == "remove")
                return RedirectToAction("Remove", "Lawsuit", new { lawsuitId = lawsuit.LawsuitId });
            if (!ModelState.IsValid)
            {
                this.ViewData["SubCategories"] = DB.SubCategories.ToList();
                this.ViewData["WebResources"] = Utils.GetCustomWebResourseList(DB, true);
                return View(lawsuit);
            }
            DB.Entry(lawsuit).State = System.Data.EntityState.Modified;
            TempData["Message"] = Utils.TrySaveChanges(DB, Defines.SaveChangesCases.Edit);
            return RedirectToAction("Index");
        }
        //
        // GET: /Lawsuit/Delete
        public ActionResult Remove(int lawsuitId = -1)
        {
            if (lawsuitId != -1)
            {
                Lawsuit lawsuit = DB.Lawsuits.Select(i => i).Where(i => i.LawsuitId == lawsuitId).First();
                DB.Lawsuits.Remove(lawsuit);
                TempData["Message"] = Utils.TrySaveChanges(DB, Defines.SaveChangesCases.Remove);
            }
            return RedirectToAction("Index");
        }
    }
}