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
    public class LawsuitBlockController : Controller
    {
        private LawsuitDB DB = new LawsuitDB();      
        //
        // GET: /Lawsuit/Index
        public ActionResult Index(int lawsuitId)
        {
            if (lawsuitId == -1)
                return RedirectToAction("Index" , "Lawsuit");
            LawsuitWithBlockListData data = new LawsuitWithBlockListData();
            data.Lawsuit = DB.Lawsuits.Select(i => i).Where(i => i.LawsuitId == lawsuitId).First();
            data.Blocks = Utils.GetBlocksDictionary(DB);
            this.ViewData["Groups"] = Utils.GetGroupList(DB);
            return View(data);
        }
        /*[HttpPost]
        public JsonResult All()
        {
            return Json(DB.Blocks.ToList());
        }*/
        [HttpPost]
        public JsonResult Selected(int lawsuitId)
        {
            return Json(Utils.GetLawsuitBlocksJson(lawsuitId, DB));
        }

        [HttpPost]
        public void Action(LawsuitBlockData data)
        {
            if (data.Type == LawsuitBlockData.Types.Add)
            {
                LawsuitBlock pair = new LawsuitBlock();
                pair.LawsuitId = data.LawsuitId;
                pair.BlockId = data.BlockId;
                if (DB.LawsuitBlocks.Where(i => i.LawsuitId == pair.LawsuitId).ToList().Count > 0)
                    pair.LawsuitBlockWeight = DB.LawsuitBlocks.Where(i => i.LawsuitId == pair.LawsuitId).Select(i => i.LawsuitBlockWeight).Max() + 1;
                else
                    pair.LawsuitBlockWeight = 0;
                DB.LawsuitBlocks.Add(pair);
                Utils.TrySaveChanges(DB, Defines.SaveChangesCases.Add);
            }
            else if (data.Type == LawsuitBlockData.Types.ChangeWeight) 
            {
                int lawsuitId = data.LawsuitId;
                int source = data.BlockId;
                int target = data.TargetBlockId;
                LawsuitBlock sourceItem = null;
                int targetWeight = 0;
                List<LawsuitBlock> list = DB.LawsuitBlocks.Select(i => i).Where(i => i.LawsuitId == lawsuitId).ToList();
                list.Sort(Utils.BlockComparer);
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].BlockId == target)
                    {
                        targetWeight = list[i].LawsuitBlockWeight;
                        while (list[i].BlockId != source)
                        {
                            list[i].LawsuitBlockWeight = ++(list[i].LawsuitBlockWeight);
                            DB.Entry(list[i]).State = EntityState.Modified;
                            i++;
                        }
                        list[i].LawsuitBlockWeight = targetWeight;
                        DB.Entry(list[i]).State = EntityState.Modified;
                        Utils.TrySaveChanges(DB, Defines.SaveChangesCases.Edit);
                        return;
                    }
                    if (list[i].BlockId == source)
                    {
                        sourceItem = list[i];
                        i++;
                        while (list[i].BlockId != target)
                        {
                            list[i].LawsuitBlockWeight = --(list[i].LawsuitBlockWeight);
                            DB.Entry(list[i]).State = EntityState.Modified;
                            i++;
                        }
                        sourceItem.LawsuitBlockWeight = list[i].LawsuitBlockWeight;
                        DB.Entry(sourceItem).State = EntityState.Modified;
                        list[i].LawsuitBlockWeight = --(list[i].LawsuitBlockWeight);
                        DB.Entry(list[i]).State = EntityState.Modified;
                        Utils.TrySaveChanges(DB, Defines.SaveChangesCases.Edit);
                        return;
                    }
                }      
            }
            else
            {
                LawsuitBlock pair = DB.LawsuitBlocks.Select(i => i).Where(i => i.BlockId == data.BlockId && i.LawsuitId == data.LawsuitId).First();
                DB.LawsuitBlocks.Remove(pair);
                Utils.TrySaveChanges(DB, Defines.SaveChangesCases.Remove);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Index(int lawsuitId, string button)
        {
            if (button == "fields")
                return RedirectToAction("Index", "LawsuitConstruct", new { lawsuitId = lawsuitId });
            var model = Utils.GetLawsuitHtmlBody(lawsuitId, DB);
            return View("Preview", model);
        }
    }
}