using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC;

namespace MVC.Models
{
    public class BlockJson
    {
        public int BlockId { get; set; }
        public string BlockName { get; set; }
        public BlockJson(int blockId = 0, string blockName = null)
        {
            this.BlockId = blockId;
            this.BlockName = blockName;
        }
    }
    public class LawsuitBlockData
    {
        public enum Types { Add = 0, Remove = 1, ChangeWeight = 2, };
        public Types Type { get; set; }
        public int LawsuitId { get; set; }
        public int BlockId { get; set; }
        public int TargetBlockId { get; set; }
    }
    public class LawsuitWithBlockListData
    {
        public Lawsuit Lawsuit { get; set; }
        public Dictionary<String, List<Block>> Blocks { get; set; }
    } 
    public partial class FieldData
    {
        public string marker { get; set; }
        public string fieldText { get; set; }
        public int dataType { get; set; }
        public string textData { get; set; }
        public double numberData { get; set; }
        public string dateData { get; set; }
        public decimal moneyData { get; set; }
        public bool questionData { get; set; }
        public int lawsuitId { get; set; }
        public int blockId { get; set; }
        public List<FieldData> children { get; set; }
        public override string ToString()
        {
            switch (this.dataType)
            {
                case (int)Defines.FieldDataTypesEnum.Text:
                    return " " + this.textData.ToString() + " ";
                case (int)Defines.FieldDataTypesEnum.Number:
                    return " " + this.numberData.ToString() + " ";
                case (int)Defines.FieldDataTypesEnum.Date:
                    return " " + this.dateData.ToString() + " ";
                case (int)Defines.FieldDataTypesEnum.Money:
                    return " " + this.moneyData.ToString() + " ";
                case (int)Defines.FieldDataTypesEnum.Question:
                    return " " + this.questionData.ToString() + " ";
                default:
                    return this.GetType().ToString();
            }
        }
    }
}