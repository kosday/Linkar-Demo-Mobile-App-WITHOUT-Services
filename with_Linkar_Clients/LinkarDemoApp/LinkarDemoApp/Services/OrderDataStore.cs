using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LinkarCommon;
using LinkarDemoApp.Models;

namespace LinkarDemoApp.Services
{
    public class OrderDataStore
    {
        List<Order> orders;

        public OrderDataStore()
        {
            orders = new List<Order>();
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync(bool forceRefresh = false)
        {
            if (forceRefresh)
            {
                //var json = await client.GetStringAsync($"api/Linkar/GetOrders?token=" + App.Token);
                //orders = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Order>>(json));

                orders = null;
                try
                {
                    string lkstring = App.linkarClt.Select_Text("LK.ORDERS", "", "BY CODE", "", "", new SelectOptions(false, false, 10, 1, true), DATAFORMAT_TYPE.MV, "", 0);

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

                        orders = new List<Order>();

                        for (int i = 0; i < lstids.Length; i++)
                        {
                            Order record = null;
                            if (recordCalculateds != null && recordCalculateds != "")
                            {
                                record = LkOrder.GetRecord(lstids[i], lstregs[i], lstcalcs[i]);
                            }
                            else
                                record = LkOrder.GetRecord(lstids[i], lstregs[i], "");

                            orders.Add(record);
                        }
                    }
                }
                catch (Exception ex)
                {
                    string error = App.GetException(ex);
                    await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("ERROR", error, "OK");
                }
            }

            return orders;
        }

        public async Task<Order> GetOrderAsync(string id)
        {
            Order result = null;
            if (id != null)
            {
                //var json = await client.GetStringAsync($"api/Linkar/GetOrder?token=" + App.Token + "&code=" + id);
                //return await Task.Run(() => JsonConvert.DeserializeObject<Order>(json));
                
                try
                {
                    string lkstring = App.linkarClt.Read_Text("LK.ORDERS", id, "", new ReadOptions(true), DATAFORMAT_TYPE.MV, "", 0);

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
                        result = LkOrder.GetRecord(id, records, recordCalculateds);
                }
                catch (Exception ex)
                {
                    string error = App.GetException(ex);
                    await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("ERROR", error, "OK");
                }
            }

            return result;
        }
    }
}