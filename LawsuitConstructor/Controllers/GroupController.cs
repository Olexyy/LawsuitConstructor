using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Models;

namespace MVC.Controllers
{
    public class GroupController : Controller
    {
        private LawsuitDB DB = new LawsuitDB();
        //
        // GET: /Group
        public ActionResult Index()
        {
            return View(Utils.GetGroupList(DB));
        }
        //
        // GET: /Group/Add
        public ActionResult Add()
        {
            Group group = new Group();
            return View(group);
        }
        //
        // POST: /Group/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Group group)
        {
           // Utils.Validate(Defines.ValidationTypeEnum.Group, group, ModelState, DB);
            if (!ModelState.IsValid)
                return View(group);
            DB.Groups.Add(group);
            TempData["Message"] = Utils.TrySaveChanges(DB, Defines.SaveChangesCases.Add);
            return RedirectToAction("Index");  
        }
        //
        // GET: /Group/Edit
        public ActionResult Edit(int groupId = -1)
        {
            if (groupId == -1)
                return RedirectToAction("Index");
            Group group = DB.Groups.Select(i => i).Where(i => i.GroupId == groupId).First();
            return View(group);
        }
        //
        // POST: /Group/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Group group, string button)
        {
            if (button == "remove")
                return RedirectToAction("Remove", "Group", new { groupId = group.GroupId });
            if (!ModelState.IsValid)
                return View(group);
            DB.Entry(group).State = System.Data.EntityState.Modified;
            TempData["Message"] = Utils.TrySaveChanges(DB, Defines.SaveChangesCases.Edit);
            return RedirectToAction("Index");
        }
        //
        // GET: /Group/Delete
        public ActionResult Remove(int groupId = -1)
        {
            if (groupId != -1)
            {
                Group group = DB.Groups.Select(i => i).Where(i => i.GroupId == groupId).First();
                DB.Groups.Remove(group);
                TempData["Message"] = Utils.TrySaveChanges(DB, Defines.SaveChangesCases.Remove);
            }
            return RedirectToAction("Index");
        }
    }
}
