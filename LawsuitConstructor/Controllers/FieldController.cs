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
    public class FieldController : Controller
    {
        private LawsuitDB DB = new LawsuitDB();
        //
        // GET: /Field/Index
        public ActionResult Index()
        {
            return View(Utils.GetFieldsList(DB));
        }
        //
        // GET: /Field/Add
        public ActionResult Add()
        {
            Field field = new Field();
            return View(field);
        }
        //
        // POST: /Field/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Field field)
        {
            if (!ModelState.IsValid)
                return View(field);
            DB.Fields.Add(field);
            TempData["Message"] = Utils.TrySaveChanges(DB, Defines.SaveChangesCases.Add);
            return RedirectToAction("Index");
        }
        //
        // GET: /Field/Edit
        public ActionResult Edit(int fieldId = -1)
        {
            if (fieldId == -1)
                return RedirectToAction("Index");
            Field field = DB.Fields.Select(i => i).Where(i => i.FieldId == fieldId).First();
            return View(field);
        }
        //
        // POST: /Field/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Field field, String button)
        {
            if (button == "remove")
                return RedirectToAction("Remove", "Field", new { fieldId = field.FieldId });
            if (!ModelState.IsValid)
                return View(field);
            DB.Entry(field).State = System.Data.EntityState.Modified;
            TempData["Message"] = Utils.TrySaveChanges(DB, Defines.SaveChangesCases.Edit);
            return RedirectToAction("Index");
        }
        //
        // GET: /Field/Remove
        public ActionResult Remove(int fieldId = -1)
        {
            if (fieldId != -1)
            {
                Field field = DB.Fields.Select(i => i).Where(i => i.FieldId == fieldId).First();
                if (Validators.IsMarkerInUse(field.FieldMarker, DB))
                {
                    TempData["Message"] = "Маркер викоритовується";
                    return RedirectToAction("Index");
                }
                DB.Fields.Remove(field);
                TempData["Message"] = Utils.TrySaveChanges(DB, Defines.SaveChangesCases.Remove);
            }
            return RedirectToAction("Index");
        }
    }
}