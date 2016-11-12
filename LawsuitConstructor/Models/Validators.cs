using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using MVC.Models;
using System.Text.RegularExpressions;
using System.Text;
using System.ComponentModel.DataAnnotations;


namespace MVC.Models
{
    public static class Validators
    {
        public static bool IsMarkerInUse(string marker, LawsuitDB DB)
        { // no Regex for speed
            string formattedMarker = "#" + marker + "#";
            foreach (Part part in DB.Parts)
                if (part.PartText.Contains(formattedMarker))
                    return true;
            return false;
        }
        public static bool ValidateMarkers(Part part, LawsuitDB DB)
        {
            List<string> partMarkers = Utils.GetPartMarkers(DB, part);
            if (partMarkers.Count > 0)
            {
                List<string> existingMarkers = Utils.GetMarkers(DB);
                foreach (string partMarker in partMarkers)
                    if (!existingMarkers.Contains(partMarker))
                        return false;
            }
            return true;
        }
        public static bool ValidateMarker(String marker)
        {
            string pattern = @"^(\w+)$";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(marker);
            if (match.Success)
                return true;
            return false;
        }
    } 
    public partial class Category : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (String.IsNullOrEmpty(this.CategoryName))
                yield return new ValidationResult(Translate.It("Value can't be empty"), new[] { "CategoryName" });
        }
    }
    public partial class Part : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (String.IsNullOrEmpty(this.PartName))
                yield return new ValidationResult(Translate.It("Value can't be empty"), new[] { "PartName" });
            if (String.IsNullOrEmpty(this.PartText))
                yield return new ValidationResult(Translate.It("Value can't be empty"), new[] { "PartText" });
            else if (!Validators.ValidateMarkers(this, new LawsuitDB()))
                yield return new ValidationResult(Translate.It("Marker not defined"), new[] { "PartText" });
        }
    }
    public partial class Block : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.BlockIncludeType == (int)Defines.BlockIncludeTypesEnum.Auto)
                this.Field = null; // for DB integrity
            if (String.IsNullOrEmpty(this.BlockName))
                yield return new ValidationResult(Translate.It("Value can't be empty"), new[] { "BlockName" });
        }
    }
    public partial class SubCategory : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (String.IsNullOrEmpty(this.SubCategoryName))
                yield return new ValidationResult(Translate.It("Value can't be empty"), new[] { "SubCategoryName" });
        }
    }
    public partial class Lawsuit : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.WebResourceId == 0)
                this.WebResourceId = null; // for DB integrity
            if (String.IsNullOrEmpty(this.LawsuitName))
                yield return new ValidationResult(Translate.It("Value can't be empty"), new[] { "LawsuitName" });
        }
    }
    public partial class Group : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (String.IsNullOrEmpty(this.GroupName))
                yield return new ValidationResult(Translate.It("Value can't be empty"), new[] { "GroupName" });
        }
    }
    public partial class TextStyle : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (String.IsNullOrEmpty(this.TextStyleName))
                yield return new ValidationResult(Translate.It("Value can't be empty"), new[] { "TextStyleName" });
        }
    }
    public partial class WebResource : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (String.IsNullOrEmpty(this.WebResourceKey))
                yield return new ValidationResult(Translate.It("Value can't be empty"), new[] { "WebResourceKey" });
        }
    }
    public partial class Field : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.FieldDataType == (int)Defines.FieldDataTypesEnum.Question)
                this.FieldMarker = null; // for DB integrity
            else if (String.IsNullOrEmpty(this.FieldMarker))
                yield return new ValidationResult(Translate.It("Value can't be empty"), new[] { "FieldMarker" });
            else if (!Validators.ValidateMarker(this.FieldMarker))
                yield return new ValidationResult(Translate.It("Marker not in format"), new[] { "FieldMarker" });
            if (String.IsNullOrEmpty(this.FieldName))
                yield return new ValidationResult(Translate.It("Value can't be empty"), new[] { "FieldName" });
            if (String.IsNullOrEmpty(this.FieldText))
                yield return new ValidationResult(Translate.It("Value can't be empty"), new[] { "FieldText" });
        }
    }
    public partial class FieldData : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            switch (this.dataType)
            {
                case (int)Defines.FieldDataTypesEnum.Text:
                    if (String.IsNullOrEmpty(this.textData))
                        yield return new ValidationResult(Translate.It("Value can't be empty"), new[] { "textData" });
                    break;
                case (int)Defines.FieldDataTypesEnum.Number:
                    if (this.numberData <= 0)
                        yield return new ValidationResult(Translate.It("Value can't be empty"), new[] { "numberData" });
                    break;
                case (int)Defines.FieldDataTypesEnum.Date:
                    if (String.IsNullOrEmpty(this.dateData))
                        yield return new ValidationResult(Translate.It("Value can't be empty"), new[] { "dateData" });
                    else
                    {
                        string pattern = @"^\d\d\.\d\d\.\d\d\d\d$";
                        Regex regex = new Regex(pattern);
                        Match match = regex.Match(this.dateData);
                        if (!match.Success)
                             yield return new ValidationResult(Translate.It("Date is not valid"), new[] { "dateData" });
                    }
                    break;
                case (int)Defines.FieldDataTypesEnum.Money:
                    if (this.moneyData <= 0)
                        yield return new ValidationResult(Translate.It("Value can't be null or negative"), new[] { "moneyData" });
                    break;
                default:
                    break;
            }
        }
    }
}