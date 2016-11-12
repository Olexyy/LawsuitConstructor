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
    public class BlockController : Controller
    {
        private LawsuitDB DB = new LawsuitDB();
        //
        // GET: /Block/Index
        public ActionResult Index()
        {
            this.ViewData["Groups"] = Utils.GetGroupList(DB);
            return View(Utils.GetBlocksDictionary(DB));
        }
        //
        // GET: /Block/Add
        public ActionResult Add(int blockId = -1)
        {
            this.ViewData[Defines.FieldsTypesEnum.NoMarks.ToString()] = Utils.GetFields(Defines.FieldsTypesEnum.NoMarks, DB);
            this.ViewData["Groups"] = Utils.GetGroupList(DB);
            Block block;
            if (blockId != -1)
                block = DB.Blocks.Where(i=>i.BlockId == blockId).First();
            else
                block = new Block();
            return View(block);
        }
        //
        // POST: /Block/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Add(Block block, string button)
        {
            if (!ModelState.IsValid)
            {
                this.ViewData[Defines.FieldsTypesEnum.NoMarks.ToString()] = Utils.GetFields(Defines.FieldsTypesEnum.NoMarks, DB);
                this.ViewData["Groups"] = Utils.GetGroupList(DB);
                return View(block);
            }
            if (button == "preview")
            {
                var query = DB.Blocks.Where(i => i.BlockId == block.BlockId).ToList();
                if (query.Count > 0)
                   block = query.First();
                return View("Preview", Utils.GetBlockHtmlBody(block, DB));
            }
            DB.Blocks.Add(block);
            this.TempData["Message"] = Utils.TrySaveChanges(DB, Defines.SaveChangesCases.Add);
            if (button == "part" && TempData["Message"] as String == String.Empty)
                return RedirectToAction("Add", "Part", new { blockId = block.BlockId });
            return RedirectToAction("Index");
        }
        //
        // GET: /Block/Edit
        public ActionResult Edit(int blockId = -1)
        {
            if (blockId == -1)
                return RedirectToAction("Index");
            this.ViewData[Defines.FieldsTypesEnum.NoMarks.ToString()] = Utils.GetFields(Defines.FieldsTypesEnum.NoMarks, DB);
            this.ViewData["Groups"] = Utils.GetGroupList(DB);
            Block block = DB.Blocks.Select(i => i).Where(i => i.BlockId == blockId).First();
            return View(block); 
        }
        //
        // POST: /Block/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Block block, String button)
        {
            if (button == "remove")
                return RedirectToAction("Remove", "Block", new { blockId = block.BlockId });
            if (!ModelState.IsValid)
            {
                this.ViewData[Defines.FieldsTypesEnum.NoMarks.ToString()] = Utils.GetFields(Defines.FieldsTypesEnum.NoMarks, DB);
                this.ViewData["Groups"] = Utils.GetGroupList(DB);
                return View(block);
            }
            if (button == "preview")
            {
                var query = DB.Blocks.Where(i => i.BlockId == block.BlockId).ToList();
                if (query.Count > 0)
                    block = query.First();
                return View("Preview", Utils.GetBlockHtmlBody(block, DB));
            }
            DB.Entry(block).State = System.Data.EntityState.Modified;
            TempData["Message"] = Utils.TrySaveChanges(DB, Defines.SaveChangesCases.Add);
            if (button == "part" && TempData["Message"] as String == String.Empty)
                return RedirectToAction("Add", "Part", new { blockId = block.BlockId });
            return RedirectToAction("Index");
        }
        //
        // GET: /Block/Remove
        public ActionResult Remove(int blockId = -1)
        {
            if (blockId != -1)
            {
                Block block = DB.Blocks.Select(i => i).Where(i => i.BlockId == blockId).First();
                foreach (var part in DB.Parts.Where(i => i.BlockId == blockId))
                    DB.Parts.Remove(part);
                DB.Blocks.Remove(block);
                TempData["Message"] = Utils.TrySaveChanges(DB, Defines.SaveChangesCases.Remove);
                return RedirectToAction("Index");
            }
            else return View("Error"); // TODO: test;
        }
    }
}