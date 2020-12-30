using System;
using System.Collections.Generic;
using System.Linq;
using Linkar.Functions;

namespace LinkarDemoApp.Models
{
    public class Order
    {
        public string Id { get; set; }
        public string Customer { get; set; }
        public DateTime? Date { get; set; }
        public double ITotalOrder { get; set; }
        public string ICustomerName { get; set; }
        public List<MV_LstItems_CLkOrder> LstLstItems { get; set; }
    }

    public class MV_LstItems_CLkOrder 
    {
        public string Item { get; set; }
        public double Qty { get; set; }
        public double Price { get; set; }
        public double ITotalLine { get; set; }
        public string IItemDescription { get; set; }
        public double IItemStock { get; set; }

        public List<SV_LstPartial_CLkOrder> LstLstPartial { get; set; }
    }

    public class SV_LstPartial_CLkOrder 
    {
        public DateTime? DeliveryDateTime { get; set; }
        public double PartialQuantity { get; set; }
    }

    public static class LkOrder
    {
        public static Order GetRecord(string recordID, string record, string recordCalculated)
        {
            Order order = new Order();
            //Create empty record
            string[] reg = new string[7];
            for (int j = 0; j < reg.Length; j++)
                reg[j] = "";

            //Fill the record
            if (!string.IsNullOrEmpty(record))
            {
                string[] aux = record.Split(DBMV_Mark.AM);
                for (int j = 0; j < aux.Length; j++)
                    reg[j] = aux[j];
            }

            //Create empty calculated record
            string[] regI = new string[6];
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
            order.Id = recordID;
            if (record == null || record == "")
                return null;

            //Fill the class properties
            order.Customer = LinkarDataTypes.GetAlpha(reg[0]);
            string _Date = LinkarDataTypes.GetDateTime(reg[1]);
            if (!string.IsNullOrWhiteSpace(_Date))
            {
                int dias;
                int.TryParse(_Date, out dias);
                order.Date = new DateTime(1967, 12, 31).AddDays(dias);
            }
            else
                order.Date = null;
            order.ITotalOrder = LinkarDataTypes.GetDecimal(regI[4]);
            order.ICustomerName = LinkarDataTypes.GetAlpha(regI[0]);

            #region Multivalues

            //Get the max number of multivalues dinamically
            int numMV_LstItems = 0;
            int tmpCountMV_LstItems = 0;
            tmpCountMV_LstItems = MvOperations.LkDCount(reg[2], DBMV_Mark.VM_str); //NEWFRAMEWORK: Replace MvFunctions for MvOperations
            if (tmpCountMV_LstItems > numMV_LstItems) numMV_LstItems = tmpCountMV_LstItems;
            tmpCountMV_LstItems = MvOperations.LkDCount(reg[3], DBMV_Mark.VM_str); //NEWFRAMEWORK: Replace MvFunctions for MvOperations
            if (tmpCountMV_LstItems > numMV_LstItems) numMV_LstItems = tmpCountMV_LstItems;
            tmpCountMV_LstItems = MvOperations.LkDCount(reg[4], DBMV_Mark.VM_str); //NEWFRAMEWORK: Replace MvFunctions for MvOperations
            if (tmpCountMV_LstItems > numMV_LstItems) numMV_LstItems = tmpCountMV_LstItems;
            tmpCountMV_LstItems = MvOperations.LkDCount(regI[3], DBMV_Mark.VM_str); //NEWFRAMEWORK: Replace MvFunctions for MvOperations
            if (tmpCountMV_LstItems > numMV_LstItems) numMV_LstItems = tmpCountMV_LstItems;
            tmpCountMV_LstItems = MvOperations.LkDCount(regI[1], DBMV_Mark.VM_str); //NEWFRAMEWORK: Replace MvFunctions for MvOperations
            if (tmpCountMV_LstItems > numMV_LstItems) numMV_LstItems = tmpCountMV_LstItems;
            tmpCountMV_LstItems = MvOperations.LkDCount(regI[2], DBMV_Mark.VM_str); //NEWFRAMEWORK: Replace MvFunctions for MvOperations
            if (tmpCountMV_LstItems > numMV_LstItems) numMV_LstItems = tmpCountMV_LstItems;
            tmpCountMV_LstItems = MvOperations.LkDCount(reg[5], DBMV_Mark.VM_str); //NEWFRAMEWORK: Replace MvFunctions for MvOperations
            if (tmpCountMV_LstItems > numMV_LstItems) numMV_LstItems = tmpCountMV_LstItems;
            tmpCountMV_LstItems = MvOperations.LkDCount(reg[6], DBMV_Mark.VM_str); //NEWFRAMEWORK: Replace MvFunctions for MvOperations
            if (tmpCountMV_LstItems > numMV_LstItems) numMV_LstItems = tmpCountMV_LstItems;

            if (order.LstLstItems == null)
                order.LstLstItems = new List<MV_LstItems_CLkOrder>();

            //Iterate multivalues
            for (int i = 0; i < numMV_LstItems; i++)
            {
                if (i < order.LstLstItems.Count())
                    order.LstLstItems[i] = GetMVRecord(reg, regI, i);
                else
                {
                    MV_LstItems_CLkOrder regmv = new MV_LstItems_CLkOrder();
                    regmv = GetMVRecord(reg, regI, i);
                    order.LstLstItems.Add(regmv);
                }
            }
            if (numMV_LstItems < order.LstLstItems.Count())
            {
                int offset = order.LstLstItems.Count() - numMV_LstItems;
                for (int i = 0; i < offset; i++)
                {
                    order.LstLstItems.RemoveAt(order.LstLstItems.Count() - 1);
                }
            }

            #endregion

            return order;

        }

        private static MV_LstItems_CLkOrder GetMVRecord(string[] reg, string[] regI, int mvNumber)
        {
            MV_LstItems_CLkOrder mvorder = new MV_LstItems_CLkOrder();
            //Fill the class Multivalue properties
            mvorder.Item = LinkarDataTypes.GetAlpha(reg[2], mvNumber);
            mvorder.Qty = LinkarDataTypes.GetDecimal(reg[3], mvNumber);
            mvorder.Price = LinkarDataTypes.GetDecimal(reg[4], mvNumber);
            mvorder.ITotalLine = LinkarDataTypes.GetDecimal(regI[3], mvNumber);
            mvorder.IItemDescription = LinkarDataTypes.GetAlpha(regI[1], mvNumber);
            mvorder.IItemStock = LinkarDataTypes.GetDecimal(regI[2], mvNumber);

            #region Subvalues

            //Get the max number of subvalues dinamically
            int numSV_LstPartial = 0;
            int tmpCountSV_LstPartial = 0;
            tmpCountSV_LstPartial = MvOperations.LkDCount(reg[5].Split(DBMV_Mark.VM)[mvNumber], DBMV_Mark.SM_str); //NEWFRAMEWORK: Replace MvFunctions for MvOperations
            if (tmpCountSV_LstPartial > numSV_LstPartial) numSV_LstPartial = tmpCountSV_LstPartial;
            tmpCountSV_LstPartial = MvOperations.LkDCount(reg[6].Split(DBMV_Mark.VM)[mvNumber], DBMV_Mark.SM_str); //NEWFRAMEWORK: Replace MvFunctions for MvOperations
            if (tmpCountSV_LstPartial > numSV_LstPartial) numSV_LstPartial = tmpCountSV_LstPartial;

            if (mvorder.LstLstPartial == null)
                mvorder.LstLstPartial = new List<SV_LstPartial_CLkOrder>();

            //Iterate subvalues
            for (int i = 0; i < numSV_LstPartial; i++)
            {
                if (i < mvorder.LstLstPartial.Count())
                    mvorder.LstLstPartial[i] = GetSVRecord(reg, regI, mvNumber, i);
                else
                {
                    SV_LstPartial_CLkOrder regsv = new SV_LstPartial_CLkOrder();
                    regsv = GetSVRecord(reg, regI, mvNumber, i);
                    mvorder.LstLstPartial.Add(regsv);
                }
            }
            if (numSV_LstPartial < mvorder.LstLstPartial.Count())
            {
                int offset = mvorder.LstLstPartial.Count() - numSV_LstPartial;
                for (int i = 0; i < offset; i++)
                {
                    mvorder.LstLstPartial.RemoveAt(mvorder.LstLstPartial.Count() - 1);
                }
            }

            #endregion

            return mvorder;
        }

        private static SV_LstPartial_CLkOrder GetSVRecord(string[] reg, string[] regI, int mvNumber, int svNumber)
        {
            SV_LstPartial_CLkOrder svorder = new SV_LstPartial_CLkOrder();
            //Fill the class Multivalue properties
            string _DeliveryDateTime = LinkarDataTypes.GetDateTime(reg[5], mvNumber, svNumber);
            if (!string.IsNullOrWhiteSpace(_DeliveryDateTime))
            {
                int dias;
                int.TryParse(_DeliveryDateTime, out dias);
                svorder.DeliveryDateTime = new DateTime(1967, 12, 31).AddDays(dias);
            }
            else
                svorder.DeliveryDateTime = null;
            svorder.PartialQuantity = LinkarDataTypes.GetDecimal(reg[6], mvNumber, svNumber);
            return svorder;
        }
    }
}