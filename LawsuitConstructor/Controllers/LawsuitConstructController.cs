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
    public class LawsuitConstructController : Controller
    {
        private LawsuitDB DB = new LawsuitDB();      
        //
        // GET: /LawsuitConstruct/Index
        public ActionResult Index(int lawsuitId = -1)
        {
            if (lawsuitId == -1)
                return View("Error");
            else
            {
                List<Block> requiredBlocks = Utils.GetLawsuitBlocks(lawsuitId, DB, Defines.BlockIncludeTypesEnum.Auto);
                List<Block> choiceBlocks = Utils.GetLawsuitBlocks(lawsuitId, DB, Defines.BlockIncludeTypesEnum.Choice);
                List<FieldData> fieldData = Utils.GetFieldData(lawsuitId, requiredBlocks, choiceBlocks, DB);
                if (fieldData.Count > 0)
                    return View(fieldData); // TODO: REDO VIEW (TEST) AND MANAGE JQUERY ONSELECT;... REDO VALIDATE!!! DO HTML!!!
                else  //TEST MODEL RETURN CHILDREN!!!
                {
                    List<HtmlString> LawsuitHtmlBody = Utils.GetLawsuitHtmlBody(lawsuitId, DB); // TODO: TEST (NO FIELDS)
                    return View("Preview", LawsuitHtmlBody);
                }
            }
        }
        //
        // POST: /LawsuitConstruct/Index
        [HttpPost]
        public ActionResult Index(List<FieldData> fieldData)
        {
            if (!ModelState.IsValid)
                return View(fieldData);
            else
            {
                int lawsuitId = fieldData.First().lawsuitId;
                Dictionary<string, FieldData> fieldDataDictionary = Utils.FieldDataListToDictionary(fieldData);
                List<HtmlString> LawsuitHtmlBody = Utils.GetLawsuitHtmlBody(lawsuitId, DB, fieldDataDictionary);
                return View("Preview", LawsuitHtmlBody);
            }
        }
    }
}