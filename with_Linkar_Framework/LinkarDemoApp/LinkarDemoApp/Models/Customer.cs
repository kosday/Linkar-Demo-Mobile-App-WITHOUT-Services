using System;
using Linkar.Functions;

namespace LinkarDemoApp.Models
{
    public class Customer
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }

    public static class LkCustomer
    {
        public static Customer GetRecord(string recordID, string record, string recordCalculated)
        {
            Customer customer = new Customer();
            //Create empty record
            string[] reg = new string[3];
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
            customer.Id = recordID;
            if (record == null || record == "")
                return null;

            //Fill the class properties
            customer.Name = LinkarDataTypes.GetAlpha(reg[0]);
            customer.Address = LinkarDataTypes.GetAlpha(reg[1]);
            customer.Phone = LinkarDataTypes.GetAlpha(reg[2]);

            return customer;
        }
    }
}