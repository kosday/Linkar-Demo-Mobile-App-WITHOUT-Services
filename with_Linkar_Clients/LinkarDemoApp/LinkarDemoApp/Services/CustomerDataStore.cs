using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LinkarCommon;
using LinkarDemoApp.Models;

namespace LinkarDemoApp.Services
{
    public class CustomerDataStore
    {
        List<Customer> customers;

        public CustomerDataStore()
        {
            customers = new List<Customer>();
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync(bool forceRefresh = false)
        {
            if (forceRefresh)
            {
                //var json = await client.GetStringAsync($"api/Linkar/GetCustomers?token=" + App.Token);
                //customers = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Customer>>(json));

                customers = null;
                try
                {

                    string lkstring = App.linkarClt.Select_Text("LK.CUSTOMERS", "", "BY CODE", "", "", new SelectOptions(false, false, 10, 1, true), DATAFORMAT_TYPE.MV, "", 0);

                    if (!string.IsNullOrEmpty(lkstring))
                    {
                        char delimiter = ASCII_Chars.FS_chr;
                        char delimiterThisList = DBMV_Mark.AM;
                        String recordIds = "";
                        String records = "";
                        String recordCalculateds = "";
                        String[] parts = lkstring.Split(delimiter);
                        if (parts.Length >= 1)
                        {
                            String[] ThisList = parts[0].Split(delimiterThisList);
                            int numElements = ThisList.Length;
                            for (int i = 1; i < numElements; i++)
                            {
                                if (ThisList[i].Equals("RECORD_ID"))
                                {
                                    recordIds = parts[i];
                                }
                                if (ThisList[i].Equals("RECORD"))
                                {
                                    records = parts[i];
                                }
                                if (ThisList[i].Equals("CALCULATED"))
                                {
                                    recordCalculateds = parts[i];
                                }
                            }
                        }

                        //Fill all the records with response data
                        String[] lstids = recordIds.Split(ASCII_Chars.RS_chr);
                        String[] lstregs = records.Split(ASCII_Chars.RS_chr);
                        String[] lstcalcs = recordCalculateds.Split(ASCII_Chars.RS_chr);

                        customers = new List<Customer>();

                        for (int i = 0; i < lstids.Length; i++)
                        {
                            Customer record = new Customer();
                            if (recordCalculateds != null && recordCalculateds != "")
                            {
                                record = LkCustomer.GetRecord(lstids[i], lstregs[i], lstcalcs[i]);
                            }
                            else
                                record = LkCustomer.GetRecord(lstids[i], lstregs[i], "");

                            customers.Add(record);
                        }
                    }
                }
                catch (Exception ex)
                {
                    string error = App.GetException(ex);
                    await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("ERROR", error, "OK");
                }

            }

            return customers;
        }

        public async Task<Customer> GetCustomerAsync(string id)
        {
            Customer result = null;
            if (id != null)
            {                
                try
                {
                    string lkstring = App.linkarClt.Read_Text("LK.CUSTOMERS", id, "", new ReadOptions(true), DATAFORMAT_TYPE.MV, "", 0);

                    char delimiter = ASCII_Chars.FS_chr;
                    char delimiterThisList = DBMV_Mark.AM;
                    string records = "";
                    string recordCalculateds = "";
                    string[] parts = lkstring.Split(delimiter);
                    if (parts.Length >= 1)
                    {
                        string[] ThisList = parts[0].Split(delimiterThisList);
                        int numElements = ThisList.Length;
                        for (int i = 1; i < numElements; i++)
                        {
                            if (ThisList[i].Equals("RECORD"))
                            {
                                records = parts[i];
                            }
                            if (ThisList[i].Equals("CALCULATED"))
                            {
                                recordCalculateds = parts[i];
                            }
                        }
                    }

                    //Fill the class with response data
                    if (records != null && records != "")
                        result = LkCustomer.GetRecord(id, records, recordCalculateds);

                }
                catch (Exception ex)
                {
                    string error = App.GetException(ex);
                    await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("ERROR", error, "OK");
                }

                //var json = await client.GetStringAsync($"api/Linkar/GetCustomer?token=" + App.Token + "&code=" + id);
                //return await Task.Run(() => JsonConvert.DeserializeObject<Customer>(json));
            }

            return result;
        }
    }
}