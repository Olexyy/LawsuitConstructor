using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Models;
using MVC;

namespace MVC.Controllers
{
    public class TextStyleController : Controller
    {
        private LawsuitDB DB = new LawsuitDB();
        //
        // GET: /TextStyle/Index
        public ActionResult Index()
        {
            return View(Utils.GetTextStylesList(DB));
        }
        //
        // GET: /TextStyle/Add
        public ActionResult Add()
        {
            TextStyle textStyle = new TextStyle();
            return View(textStyle);
        }
        //
        // POST: /TextStyle/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(TextStyle textStyle)
        {
            if (!ModelState.IsValid)
                return View(textStyle);
            DB.TextStyles.Add(textStyle);
            TempData["Message"] = Utils.TrySaveChanges(DB, Defines.SaveChangesCases.Add);
            return RedirectToAction("Index");
        }
        //
        // GET: /TextStyle/Edit
        public ActionResult Edit(int textStyleId = -1)
        {
            if (textStyleId == -1)
                return RedirectToAction("Index");
            TextStyle textStyle = DB.TextStyles.Select(i => i).Where(i => i.TextStyleId == textStyleId).First();
            return View(textStyle);
        }
        //
        // POST: /TextStyle/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(TextStyle textStyle, string button)
        {
            if (button == "remove")
                return RedirectToAction("Remove", "TextStyle", new { textStyleId = textStyle.TextStyleId });
            if (!ModelState.IsValid)
                return View(textStyle);
            DB.Entry(textStyle).State = System.Data.EntityState.Modified;
            TempData["Message"] = Utils.TrySaveChanges(DB, Defines.SaveChangesCases.Edit);
            return RedirectToAction("Index");
        }
        //
        // GET: /TextStyle/Remove
        public ActionResult Remove(int textStyleId = -1)
        {
            if (textStyleId != -1)
            {
                TextStyle textStyle = DB.TextStyles.Select(i => i).Where(i => i.TextStyleId == textStyleId).First();
                DB.TextStyles.Remove(textStyle);
                TempData["Message"] = Utils.TrySaveChanges(DB, Defines.SaveChangesCases.Remove);
                return RedirectToAction("Index");
            }
            else
                return View("Error");
        }

    }
}