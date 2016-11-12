using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Models;

namespace MVC.Controllers
{
    public class SubCategoryController : Controller
    {
        private LawsuitDB DB = new LawsuitDB();

        public ActionResult Index()
        {
            return View(Utils.GetSubCategoryList(DB));
        }
        //
        // GET: /SubCategory/Add
        public ActionResult Add()
        {
            this.ViewData["Categories"] = Utils.GetCategoryList(DB);
            SubCategory subCategory = new SubCategory();
            return View(subCategory);
        }
        //
        // POST: /SubCategory/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(SubCategory subCategory)
        {
            if (!ModelState.IsValid)
            {
                this.ViewData["Categories"] = Utils.GetCategoryList(DB);
                return View(subCategory);
            }
            DB.SubCategories.Add(subCategory);
            TempData["Message"] = Utils.TrySaveChanges(DB, Defines.SaveChangesCases.Add);
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int subCategoryId = -1)
        {
            this.ViewData["Categories"] = DB.Categories.ToList();
            if (subCategoryId == -1)
                return RedirectToAction("Index");
            SubCategory subCategory = DB.SubCategories.Select(i => i).Where(i => i.SubCategoryId == subCategoryId).First();
            return View(subCategory);
        }
        //
        // POST: /SubCategory/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(SubCategory subCategory, String button)
        {
            if (button == "remove")
                return RedirectToAction("Remove", "SubCategory", new { subCategoryId = subCategory.SubCategoryId });
            if (!ModelState.IsValid)
            {
                this.ViewData["Categories"] = Utils.GetCategoryList(DB);
                return View(subCategory);
            }
            DB.Entry(subCategory).State = System.Data.EntityState.Modified;
            TempData["Message"] = Utils.TrySaveChanges(DB, Defines.SaveChangesCases.Edit);
            return RedirectToAction("Index");
        }
        //
        // GET: /SubCategory/Remove
        public ActionResult Remove(int subCategoryId = -1)
        {
            if (subCategoryId != -1)
            {
                SubCategory subCategory = DB.SubCategories.Select(i => i).Where(i => i.SubCategoryId == subCategoryId).First();
                DB.SubCategories.Remove(subCategory);
                TempData["Message"] = Utils.TrySaveChanges(DB, Defines.SaveChangesCases.Remove);
            }
            return RedirectToAction("Index");
        }
    }
}
