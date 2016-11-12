using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Models;

namespace MVC.Controllers
{
    public class CategoryController : Controller
    {
        private LawsuitDB DB = new LawsuitDB();

        public ActionResult Index()
        {
            return View(Utils.GetCategoryList(DB));
        }
        //
        // GET: /Category/Add
        public ActionResult Add()
        {
            Category category = new Category();
            return View(category);
        }
        //
        // POST: /Category/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Category category)
        {
            if (!ModelState.IsValid)
                return View(category);
            DB.Categories.Add(category);
            TempData["Message"] = Utils.TrySaveChanges(DB, Defines.SaveChangesCases.Add);
            return RedirectToAction("Index");  
        }
        public ActionResult Edit(int categoryId = -1)
        {
            if (categoryId == -1)
                return RedirectToAction("Index");
            Category category = DB.Categories.Select(i => i).Where(i => i.CategoryId == categoryId).First();
            return View(category);
        }
        //
        // POST: /Category/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Category category, String button)
        {
            if (button == "remove")
                return RedirectToAction("Remove", "Category", new { categoryId = category.CategoryId });
            if (!ModelState.IsValid)
                return View(category);
            DB.Entry(category).State = System.Data.EntityState.Modified;
            TempData["Message"] = Utils.TrySaveChanges(DB, Defines.SaveChangesCases.Edit);
            return RedirectToAction("Index");
        }
        //
        // GET: /Category/Delete
        public ActionResult Remove(int categoryId = -1)
        {
            if (categoryId != -1)
            {
                Category category = DB.Categories.Select(i => i).Where(i => i.CategoryId == categoryId).First();
                DB.Categories.Remove(category);
                TempData["Message"] = Utils.TrySaveChanges(DB, Defines.SaveChangesCases.Remove);
            }
            return RedirectToAction("Index");
        }
    }
}
