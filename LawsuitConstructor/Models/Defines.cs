using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Models;

namespace MVC
{
    public static class Defines
    {
        public static SelectList BlockIncludeTypes = new SelectList(
        new List<Object>{
                        new { value = 0 , text = Translate.It("Automatic") },
                        new { value = 1 , text = Translate.It("By choice") },
                        }, "value", "text", 0);
        public static SelectList BlockContentTypes = new SelectList(
        new List<Object>{
                        new { value = 0 , text = Translate.It("Initial details") },
                        new { value = 1 , text = Translate.It("Introduction part") },
                        new { value = 2 , text = Translate.It("Reasoning part") },
                        new { value = 3 , text = Translate.It("Requesting part") },
                        new { value = 4 , text = Translate.It("Final details") },
                        }, "value", "text", 0);
        public static List <String> BlockContentTypesList = new List<string>()
        { Translate.It("Initial details"), Translate.It("Introduction part"), 
          Translate.It("Reasoning part"), Translate.It("Requesting part"), 
          Translate.It("Final details"), };
        public static SelectList TextStyleTags = new SelectList(
        new List<Object>{
                        new { value = 0 , text = Translate.It("Paragraph <p>") },
                        new { value = 1 , text = Translate.It("The largest <h1>") },
                        new { value = 2 , text = Translate.It("Large <h2>") },
                        new { value = 3 , text = Translate.It("Larger <h3>") },
                        new { value = 4 , text = Translate.It("Normal <h4>") },
                        new { value = 5 , text = Translate.It("Less <h5>") },
                        }, "value", "text", 0);
        public static SelectList TextStyleMarginLeft = new SelectList(
        new List<Object>{
                        new { value = 0 , text = Translate.It("none") },
                        new { value = 200 , text = Translate.It("less") },
                        new { value = 250 , text = Translate.It("heading") },
                        new { value = 300 , text = Translate.It("more") },
                        }, "value", "text", 0);
        public static SelectList TextStyleTextAlign = new SelectList(
        new List<Object>{
                        new { value = 0 , text = Translate.It("left") },
                        new { value = 1 , text = Translate.It("center") },
                        new { value = 2 , text = Translate.It("right") },
                        new { value = 3 , text = Translate.It("justify") },
                        }, "value", "text", 0);
        public static Dictionary<int, string> TextStyleTagsDictionary = new Dictionary<int, string>() 
        {
            {0, "p"}, {1, "h1"}, {2, "h2"}, {3, "h3"}, {4, "h4"}, {5, "h5"},
        };
        public static Dictionary<int, string> TextStyleTextAlignDictionary = new Dictionary<int, string>() 
        {
            {0, "left"}, {1, "center"}, {2, "right"}, {3, "justify"},
        };
        public static SelectList FieldDataTypes = new SelectList(
                new List<Object>{
                        new { value = 0 , text = Translate.It("text") },
                        new { value = 1 , text = Translate.It("number") },
                        new { value = 2 , text = Translate.It("date") },
                        new { value = 3 , text = Translate.It("money") },
                        new { value = 4 , text = Translate.It("question") },
                        }, "value", "text", 0);
        public enum BlockIncludeTypesEnum { Auto, Choice, All };
        public enum FieldDataTypesEnum { Text, Number, Date, Money, Question };
        public enum SaveChangesCases { Add, Edit, Remove };
        public enum FieldsTypesEnum { WithMarks, NoMarks };
        public enum StaticWebResoucesEnum { HomePage, Advices, СourtFee }
        public static List<string> StaticWebResoucesList = new List<string>()
        {
            "HomePage", "Advices", "СourtFee",
        };
        public static string LawsuitEnding = "<br /><p style='margin-left:0pt; text-align:justify'>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;" +
        "&nbsp;<strong>#DATE#</strong>&nbsp;<strong>року</strong>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; "+
        "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <strong>___________________________(ІНІЦІАЛИ,&nbsp;ПІДПИС)</strong></p>";
    }
}