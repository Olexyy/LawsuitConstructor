using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using MVC.Models;
using System.Text.RegularExpressions;
using System.Text;

namespace MVC
{
    public static class Utils
    {
        #region GETTERS
        public static List<BlockJson> GetLawsuitBlocksJson(int lawsuitId, LawsuitDB DB)
        {
            List<BlockJson> blocksJson = new List<BlockJson>();
            foreach (Block block in Utils.GetLawsuitBlocks(lawsuitId, DB))
                blocksJson.Add(new BlockJson(block.BlockId, block.BlockName));
            return blocksJson;
        }
        public static List<Block> GetLawsuitBlocks(int lawsuitId, LawsuitDB DB, Defines.BlockIncludeTypesEnum includeType = Defines.BlockIncludeTypesEnum.All )
        {
            Lawsuit lawsuit = DB.Lawsuits.Select(i => i).Where(i => i.LawsuitId == lawsuitId).First();
            List<Block> blocks = new List<Block>();
            List<LawsuitBlock> lawsuitBlockList = lawsuit.LawsuitBlocks.ToList();
            lawsuitBlockList.Sort(Utils.BlockComparer);
            foreach (LawsuitBlock pair in lawsuitBlockList)
            {
                Block block = DB.Blocks.Where(i => i.BlockId == pair.BlockId).First();
                if (includeType == Defines.BlockIncludeTypesEnum.All)
                    blocks.Add(block);
                else if (block.BlockIncludeType == (int)includeType)
                    blocks.Add(block);
            }
            return blocks;
        }
        public static List<Block> GetLawsuitBlocks(Lawsuit lawsuit, LawsuitDB DB, Defines.BlockIncludeTypesEnum type = Defines.BlockIncludeTypesEnum.All)
        {
            return Utils.GetLawsuitBlocks(lawsuit.LawsuitId, DB, type);
        }
        public static Dictionary<String, List<Block>> GetBlocksDictionary(LawsuitDB DB)
        {
            Dictionary<String, List<Block>> blockDictionary = new Dictionary<string, List<Block>>();
            foreach (string blockContentType in Defines.BlockContentTypesList)
                blockDictionary.Add(blockContentType, new List<Block>());
            List<Block> blockList = DB.Blocks.ToList();
            foreach (Block block in blockList)
                blockDictionary[Defines.BlockContentTypesList[block.BlockContentType]].Add(block);
            foreach (List<Block> blocks in blockDictionary.Values)
                blocks.Sort(Utils.BlockComparer);
            return blockDictionary;
        }
        public static List<Field> GetFieldsList(LawsuitDB DB)
        {
            List<Field> fields = DB.Fields.ToList();
            fields.Sort(Utils.FieldsComparer);
            return fields;
        }
        public static List<Field> GetFields(Defines.FieldsTypesEnum type, LawsuitDB DB)
        {
            List<Field> fields = new List<Field>();
            foreach (Field field in DB.Fields.ToList())
            {
                if (field.FieldDataType == (int)Defines.FieldDataTypesEnum.Question && type == Defines.FieldsTypesEnum.NoMarks)
                    fields.Add(field);

                if (field.FieldDataType != (int)Defines.FieldDataTypesEnum.Question && type == Defines.FieldsTypesEnum.WithMarks)
                    fields.Add(field);
            }
            if (fields.Count == 0)
                fields.Add(new Field());
            fields.Sort(Utils.FieldsComparer);
            return fields;
        }
        public static Dictionary<String, Field> GetFieldsDictionaryByMarker(LawsuitDB DB)
        {
            Dictionary<string, Field> result = new Dictionary<string, Field>();
            foreach (Field field in DB.Fields.ToList())
            {
                if (field.FieldMarker != null)
                    result.Add(field.FieldMarker, field);
            }
            return result;
        }
        public static List<TextStyle> GetTextStylesList(LawsuitDB DB)
        {
            List<TextStyle> textStyleList = new List<TextStyle>();
            textStyleList = DB.TextStyles.ToList();
            if (textStyleList.Count == 0)
                textStyleList.Add(new TextStyle());
            textStyleList.Sort(Utils.TextStyleComparer);
            return textStyleList;
        }
        public static List<Category> GetCategoryList(LawsuitDB DB)
        {
            List<Category> result = new List<Category>();
            result = DB.Categories.ToList();
            result.Sort(Utils.CategoryComparer);
            return result;
        }
        public static List<MVC.Models.Group> GetGroupList(LawsuitDB DB)
        {
            List<MVC.Models.Group> result = new List<MVC.Models.Group>();
            result = DB.Groups.ToList();
            result.Sort(Utils.GroupComparer);
            return result;
        }
        public static List<SubCategory> GetSubCategoryList(LawsuitDB DB)
        {
            List<SubCategory> result = new List<SubCategory>();
            result = DB.SubCategories.ToList();
            result.Sort(Utils.SubCategoryComparer);
            return result;
        }
        public static List<SubCategory> GetSubCategoryList(Category category)
        {
            List<SubCategory> result = new List<SubCategory>();
            result = category.SubCategories.ToList();
            result.Sort(Utils.SubCategoryComparer);
            return result;
        }
        public static List<Lawsuit> GetLawsuitList(LawsuitDB DB)
        {
            List<Lawsuit> result = new List<Lawsuit>();
            result = DB.Lawsuits.ToList();
            result.Sort(Utils.LawsuitComparer);
            return result;
        }
        public static List<Lawsuit> GetLawsuitList(SubCategory subCategory)
        {
            List<Lawsuit> result = new List<Lawsuit>();
            result = subCategory.Lawsuits.ToList();
            result.Sort(Utils.LawsuitComparer);
            return result;
        }
        public static Dictionary<string, KeyValuePair<string, string>> GetFontSettings(TextStyle textStyle)
        {
            Dictionary<string, KeyValuePair<string, string>> fontSettings = new Dictionary<string, KeyValuePair<string, string>>();
            if (textStyle.TextStyleStong)
                fontSettings.Add("strong", new KeyValuePair<string, string>("<strong>", "</strong>"));
            else
                fontSettings.Add("strong", new KeyValuePair<string, string>("", ""));
            if (textStyle.TextStyleItalic)
                fontSettings.Add("italic", new KeyValuePair<string, string>("<em>", "</em>"));
            else
                fontSettings.Add("italic", new KeyValuePair<string, string>("", ""));
            if (textStyle.TextStyleUnderline)
                fontSettings.Add("underline", new KeyValuePair<string, string>("<u>", "</u>"));
            else
                fontSettings.Add("underline", new KeyValuePair<string, string>("", ""));
            return fontSettings;
        }
        public static List<string> GetPartMarkers(LawsuitDB DB, string partText)
        {
            string pattern = @"#(\w+)#";
            Regex regex = new Regex(pattern);
            List<string> partMarkers = new List<string>();
            MatchCollection matches = regex.Matches(partText);
            foreach (Match match in matches)
                partMarkers.Add(partText.Substring(match.Index + 1, match.Length - 2));
            return partMarkers;
        }
        public static List<string> GetPartMarkers(LawsuitDB DB, Part part)
        {
            return Utils.GetPartMarkers(DB, part.PartText);
        }
        public static List<string> GetMarkers(LawsuitDB DB)
        {
            List<string> markers = new List<string>();
            foreach (Field field in DB.Fields.ToList())
                markers.Add(field.FieldMarker);
            return markers;
        }
        public static List<FieldData> GetFieldData(int lawsuitId, List<Block> requiredBlocks, List<Block> choiceBlocks, LawsuitDB DB)
        {
            List<FieldData> fieldDataList = new List<FieldData>();
            Dictionary<String, Field> fieldsDictionaryByMarker = Utils.GetFieldsDictionaryByMarker(DB);
            List<string> addedFields = new List<string>();
            foreach (Block block in requiredBlocks)
            {
                foreach (Part part in Utils.GetBlockParts(block))
                {
                    List<string> partMarkers = Utils.GetPartMarkers(DB, part);
                    foreach (string marker in partMarkers)
                    {
                        if (!addedFields.Contains(marker))
                        {
                            addedFields.Add(marker);
                            FieldData fieldData = new FieldData();
                            fieldData.marker = marker;
                            fieldData.fieldText = fieldsDictionaryByMarker[marker].FieldText;
                            fieldData.dataType = fieldsDictionaryByMarker[marker].FieldDataType;
                            fieldData.lawsuitId = lawsuitId;
                            fieldDataList.Add(fieldData);
                        }
                    }
                }
            }
            foreach (Block block in choiceBlocks)
            {
                FieldData fieldData = new FieldData();
                fieldData.lawsuitId = lawsuitId;
                fieldData.blockId = block.BlockId;
                fieldData.fieldText = block.Field.FieldText;
                fieldData.dataType = (int)Defines.FieldDataTypesEnum.Question;
                fieldData.children = new List<FieldData>();
                foreach (Part part in Utils.GetBlockParts(block))
                {
                    List<string> partMarkers = Utils.GetPartMarkers(DB, part);
                    foreach (string marker in partMarkers)
                    {
                        if (!addedFields.Contains(marker))
                        {
                            addedFields.Add(marker);
                            FieldData childFieldData = new FieldData();
                            childFieldData.marker = marker;
                            childFieldData.fieldText = fieldsDictionaryByMarker[marker].FieldText;
                            childFieldData.dataType = fieldsDictionaryByMarker[marker].FieldDataType;
                            childFieldData.lawsuitId = lawsuitId;
                            fieldData.children.Add(childFieldData);
                        }
                    }
                }
                fieldDataList.Add(fieldData);
            }
            return fieldDataList;
        }
        public static List<Part> GetBlockParts(int blockId, LawsuitDB DB)
        {
            Block block = DB.Blocks.Where(i => i.BlockId == blockId).First();
            return Utils.GetBlockParts(block);
        }
        public static List<Part> GetBlockParts(Block block)
        {
            List<Part> blockParts = block.Parts.ToList();
            blockParts.Sort(Utils.PartComparer);
            return blockParts;
        }
        public static Dictionary<String, FieldData> FieldDataListToDictionary(List<FieldData> fieldDataList)
        {
            Dictionary<String, FieldData> FieldDataDictionary = new Dictionary<string, FieldData>();
            foreach (FieldData fieldData in fieldDataList)
                if (fieldData.marker != null)
                    FieldDataDictionary.Add(fieldData.marker, fieldData);
                else
                {
                    FieldDataDictionary.Add(fieldData.blockId.ToString(), fieldData);
                    if (fieldData.children != null && fieldData.children.Count > 0)
                        foreach (FieldData child in fieldData.children)
                            FieldDataDictionary.Add(child.marker, child);
                }
            return FieldDataDictionary;
        }
        public static string GetInitials(string fullName)
        {
            List<string> parsedFullname = fullName.Split(' ').ToList();
            StringBuilder initials = new StringBuilder();
            if (parsedFullname.Count == 1)
                return null;
            for (var i = 0; i < parsedFullname.Count; i++)
            {
                if (i == 0)
                    initials.Append(parsedFullname[i] + " ");
                else
                    initials.Append(parsedFullname[i].First() + ". ");
            }
            return initials.ToString();
        }
        #endregion
        #region HTML RENDERERS
        public static List<HtmlString> GetLawsuitHtmlBody(int lawsuitId, LawsuitDB DB, Dictionary<string, FieldData> fieldDataDictionary = null)
        {
            List<HtmlString> lawsuitHtmlBody = new List<HtmlString>();
            foreach (Block block in Utils.GetLawsuitBlocks(lawsuitId, DB))
                lawsuitHtmlBody.AddRange(Utils.GetBlockHtmlBody(block, DB, fieldDataDictionary));
            lawsuitHtmlBody.Add(new HtmlString(Utils.GetLawsuitEnding()));
            return lawsuitHtmlBody;
        }
        public static string GetLawsuitEnding()
        {
            StringBuilder ending = new StringBuilder(Defines.LawsuitEnding);
            ending.Replace("#DATE#", DateTime.Now.ToString("dd.MM.yyyy"));
            return ending.ToString();
        }

        public static List<HtmlString> GetBlockHtmlBody(Block block, LawsuitDB DB, Dictionary<string, FieldData> fieldDataDictionary = null)
        {
            if (fieldDataDictionary != null && block.BlockIncludeType == (int)Defines.BlockIncludeTypesEnum.Choice && 
                (!fieldDataDictionary.ContainsKey(block.BlockId.ToString()) || fieldDataDictionary[block.BlockId.ToString()].questionData == false))
                return new List<HtmlString>();
            List<HtmlString> htmlList = new List<HtmlString>();
            foreach (Part part in Utils.GetBlockParts(block))
                htmlList.Add(Utils.GetPartHtmlBody(part, DB, fieldDataDictionary));
            return htmlList;
        }
        public static HtmlString GetPartHtmlBody(Part part, LawsuitDB DB, Dictionary<string, FieldData> fieldDataDictionary = null)
        {
            string partBody = null;
            TextStyle textStyle = DB.TextStyles.Select(i => i).Where(i => i.TextStyleId == part.TextStyleId).First();
            string tag = Defines.TextStyleTagsDictionary[textStyle.TextStyleTag];
            string textAlign = Defines.TextStyleTextAlignDictionary[textStyle.TextStyleTextAlign];
            string marginLeft = textStyle.TextStyleMarginLeft.ToString();
            string partText = part.PartText;
            if (fieldDataDictionary != null)
                partText = Utils.SetBlockTextMarkers(partText, fieldDataDictionary);
            var font = Utils.GetFontSettings(textStyle);
            partBody = "<" + tag + " style='text-align:" + textAlign + ";margin-left:" + marginLeft + "pt;'>" + font["strong"].Key + font["italic"].Key + font["underline"].Key + partText + font["underline"].Value + font["italic"].Value + font["strong"].Value + "</" + tag + ">";
            HtmlString html = new HtmlString(partBody);
            return html;
        }
        #endregion
        #region SETTERS 
        public static String SetBlockTextMarkers(string blockText, Dictionary<string, FieldData> fieldDataDictionary)
        {
            StringBuilder text = new StringBuilder(blockText);
            string pattern = @"#\w+#";
            Regex regex = new Regex(pattern);
            MatchCollection matches = regex.Matches(blockText);
            foreach (Match match in matches) // TODO: think of more efficient way
                text = text.Replace(match.Value, fieldDataDictionary[match.Value.Replace("#", String.Empty)].ToString());
            return text.ToString();
        }
        #endregion
        #region COMPARERS
        public static int BlockComparer(LawsuitBlock a, LawsuitBlock b)
        {
            return a.LawsuitBlockWeight.CompareTo(b.LawsuitBlockWeight);
        }
        public static int BlockComparer(Block a, Block b)
        {
            return a.BlockWeight.CompareTo(b.BlockWeight);
        }
        public static int CategoryComparer(Category a, Category b)
        {
            return a.CategoryWeight.CompareTo(b.CategoryWeight);
        }
        public static int LawsuitComparer(Lawsuit a, Lawsuit b)
        {
            return a.LawsuitWeight.CompareTo(b.LawsuitWeight);
        }
        public static int SubCategoryComparer(SubCategory a, SubCategory b)
        {
            return a.SubCategoryWeight.CompareTo(b.SubCategoryWeight);
        }
        public static int FieldsComparer(Field a, Field b)
        {
            return a.FieldWeight.CompareTo(b.FieldWeight);
        }
        public static int PartComparer(Part a, Part b)
        {
            return a.PartWeight.CompareTo(b.PartWeight);
        }
        public static int TextStyleComparer(TextStyle a, TextStyle b)
        {
            return a.TextStyleWeight.CompareTo(b.TextStyleWeight);
        }
        public static int GroupComparer(MVC.Models.Group a, MVC.Models.Group b)
        {
            return a.GroupWeight.CompareTo(b.GroupWeight);
        }
        #endregion
        #region DB COMMIT QUERY
        public static string TrySaveChanges(LawsuitDB DB, Defines.SaveChangesCases Case)
        {
            try
            {
                DB.SaveChanges();
                return String.Empty;
            }
            catch (Exception ex)
            {
                string message = String.Empty;
                switch (Case)
                {
                    case Defines.SaveChangesCases.Add:
                        message = Translate.It("Data can't be added");
                        break;
                    case Defines.SaveChangesCases.Edit:
                        message = Translate.It("Data can't be changed");
                        break;
                    case Defines.SaveChangesCases.Remove:
                        message = Translate.It("Data can't be removed");
                        break;
                }
                return message;
            }
        }
        #endregion
        #region WEBRESOURCES
        public static List<string> GetMissingStaticWebResources(LawsuitDB DB)
        {
            var webResourcesList = DB.WebResources.ToList();
            List<string> missingStaticWebResources = Defines.StaticWebResoucesList.Select(i=>(string)i.Clone()).ToList();
            foreach (WebResource webResource in webResourcesList)
                if (missingStaticWebResources.Contains(webResource.WebResourceKey))
                    missingStaticWebResources.Remove(webResource.WebResourceKey);
            return missingStaticWebResources;
        }
        public static void SetMissingStaticWebResourses(List<string> missingStaticWebResources, LawsuitDB DB)
        {
            foreach (string key in missingStaticWebResources)
            {
                WebResource webResource = new WebResource();
                webResource.WebResourceKey = key;
                webResource.WebResourceTitle = Translate.It("Title");
                webResource.WebResourceBody = Translate.It("Body");
                DB.WebResources.Add(webResource);
            }
            Utils.TrySaveChanges(DB, Defines.SaveChangesCases.Add);
        }
        public static List<WebResource> GetStaticWebResourseList(LawsuitDB DB)
        {
            List<WebResource> webResourceList = DB.WebResources.ToList();
            List<WebResource> staticWebResourceList = new List<WebResource>();
            foreach (WebResource webResource in webResourceList)
                if (Defines.StaticWebResoucesList.Contains(webResource.WebResourceKey))
                    staticWebResourceList.Add(webResource);
            return staticWebResourceList;
        }
        public static WebResource GetStaticWebResourse(Defines.StaticWebResoucesEnum key, LawsuitDB DB)
        {
            var stringKey = key.ToString();
            try {
                var query = DB.WebResources.Where(i => i.WebResourceKey == stringKey).ToList();
                if (query.Count == 0)
                    SetMissingStaticWebResourses(GetMissingStaticWebResources(DB), DB);
                query = DB.WebResources.Where(i => i.WebResourceKey == stringKey).ToList();
                return query.First();
            }
            catch(Exception ex) {
                WebResource webResource = new WebResource();
                webResource.WebResourceTitle = Translate.It("Database error"); // todo translate
                return webResource;
            }
        }
        public static List<WebResource> GetCustomWebResourseList(LawsuitDB DB, bool withFirstEmpty = false)
        {
            var webResourcesList = DB.WebResources.ToList();
            List<WebResource> customWebResourcesList = new List<WebResource>();
            foreach (WebResource webResource in webResourcesList)
                if (!Defines.StaticWebResoucesList.Contains(webResource.WebResourceKey))
                    customWebResourcesList.Add(webResource);
            if(withFirstEmpty)
                customWebResourcesList.Insert(0, Utils.GetEmptyWebResource());
            return customWebResourcesList;
        }

        public static WebResource GetEmptyWebResource()
        {
            WebResource webResource = new WebResource();
            webResource.WebResourceKey = Translate.It("None");
            return webResource;
        }

        public static List<WebResource> GetWebResourseList(LawsuitDB DB)
        {
            return DB.WebResources.ToList();
        }
        #endregion
    }
}