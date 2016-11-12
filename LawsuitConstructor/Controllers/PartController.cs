using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC;
using MVC.Models;

namespace MVC.Controllers
{
    public class PartController : Controller
    {
        private LawsuitDB DB = new LawsuitDB();
        //
        // GET: /Part/Add
        public ActionResult Add(int blockId)
        {
            this.ViewData[Defines.FieldsTypesEnum.NoMarks.ToString()] = Utils.GetFields(Defines.FieldsTypesEnum.NoMarks, DB);
            this.ViewData[Defines.FieldsTypesEnum.WithMarks.ToString()] = Utils.GetFields(Defines.FieldsTypesEnum.WithMarks, DB);
            this.ViewData["TextStyles"] = Utils.GetTextStylesList(DB);
            Part part = new Part();
            part.BlockId = blockId;
            return View(part);
        }
        //
        // POST: /Part/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Add(Part part, string button)
        {
            if (!ModelState.IsValid)
            {
                this.ViewData[Defines.FieldsTypesEnum.NoMarks.ToString()] = Utils.GetFields(Defines.FieldsTypesEnum.NoMarks, DB);
                this.ViewData[Defines.FieldsTypesEnum.WithMarks.ToString()] = Utils.GetFields(Defines.FieldsTypesEnum.WithMarks, DB);
                this.ViewData["TextStyles"] = Utils.GetTextStylesList(DB);
                return View(part);
            }
            if (button == "preview")
                return View("Preview", Utils.GetPartHtmlBody(part, DB));
            DB.Parts.Add(part);
            TempData["Message"] = Utils.TrySaveChanges(DB, Defines.SaveChangesCases.Add);
            return RedirectToAction("Edit", "Block", new { blockId = part.BlockId });
        }
        //
        // GET: /Part/Edit
        public ActionResult Edit(int partId = -1)
        {
            if (partId == -1)
                return View("Error"); // TODO: test
            this.ViewData[Defines.FieldsTypesEnum.NoMarks.ToString()] = Utils.GetFields(Defines.FieldsTypesEnum.NoMarks, DB);
            this.ViewData[Defines.FieldsTypesEnum.WithMarks.ToString()] = Utils.GetFields(Defines.FieldsTypesEnum.WithMarks, DB);
            this.ViewData["TextStyles"] = Utils.GetTextStylesList(DB);
            Part part = DB.Parts.Where(i => i.PartId == partId).First();
            return View(part);
        }
        //
        // POST: /Part/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Part part, String button)
        {
            if (button == "remove")
                return RedirectToAction("Remove", "Part", new { partId = part.PartId });
            if (!ModelState.IsValid)
            {
                this.ViewData[Defines.FieldsTypesEnum.NoMarks.ToString()] = Utils.GetFields(Defines.FieldsTypesEnum.NoMarks, DB);
                this.ViewData[Defines.FieldsTypesEnum.WithMarks.ToString()] = Utils.GetFields(Defines.FieldsTypesEnum.WithMarks, DB);
                this.ViewData["TextStyles"] = Utils.GetTextStylesList(DB);
                return View(part);
            }
            if (button == "preview")
                return View("Preview", Utils.GetPartHtmlBody(part, DB));
            DB.Entry(part).State = System.Data.EntityState.Modified;
            TempData["Message"] = Utils.TrySaveChanges(DB, Defines.SaveChangesCases.Edit);
            return RedirectToAction("Edit", "Block", new { blockId = part.BlockId });
        }
        //
        // GET: /Lawsuit/Remove
        public ActionResult Remove(int partId = -1)
        {
            if (partId != -1)
            {
                Part part = DB.Parts.Where(i => i.PartId == partId).First();
                DB.Parts.Remove(part);
                TempData["Message"] = Utils.TrySaveChanges(DB, Defines.SaveChangesCases.Remove);
                return RedirectToAction ("Edit", "Block", new { blockId = part.BlockId });
            }
            else
                return View("Error"); // TODO: test
        }
    }
}