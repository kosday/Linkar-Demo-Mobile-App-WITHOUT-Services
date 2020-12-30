using LinkarClient;
using LinkarCommon;
using System;

namespace LinkarDemoApp.Models
{
    //public class Item
    //{
    //    public string Id { get; set; }
    //    public string Text { get; set; }
    //    public string Description { get; set; }
    //}

    public class Item
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public double Stock { get; set; }
    }

    public static class LkItem
    {
        public static Item GetRecord(string recordID, string record, string recordCalculated)
        {
            Item item = new Item();
            //Create empty record
            string[] reg = new string[2];
            for (int j = 0; j < reg.Length; j++)
                reg[j] = "";

            //Fill the record
            if (record != null && record != "")
            {
                string[] aux = record.Split(DBMV_Mark.AM);
                for (int j = 0; j < aux.Length; j++)
                    reg[j] = aux[j];
            }

            //Create empty calculated record
            string[] regI = new string[0];
            for (int k = 0; k < regI.Length; k++)
                regI[k] = "";

            //Fill the calculated record
            if (recordCalculated != null && recordCalculated != "")
            {
                string[] auxI = recordCalculated.Split(DBMV_Mark.AM);
                int k = 0;
                for (; k < auxI.Length; k++)
                    regI[k] = auxI[k];
            }

            //Fill the record ID property
            item.Code = recordID;
            if (record == null || record == "")
                return null;

            //Fill the class properties
            item.Description = LinkarDataTypes.GetAlpha(reg[0]);
            item.Stock = LinkarDataTypes.GetDecimal(reg[1]);
            return item;
        }
    }
}