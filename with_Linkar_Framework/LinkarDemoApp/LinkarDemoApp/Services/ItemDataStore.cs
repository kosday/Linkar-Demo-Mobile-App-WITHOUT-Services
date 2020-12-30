using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LinkarDemoApp.Models;

//NEWFRAMEWORK: Linkar Framework Libraries
using Linkar.Functions;

namespace LinkarDemoApp.Services
{
    public class ItemDataStore
    {
        List<Item> items;

        public ItemDataStore()
        {
            items = new List<Item>();
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            
            if (forceRefresh)
            {
                //var json = await client.GetStringAsync($"api/Linkar/GetItems?token=" + App.Token);
                //items = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Item>>(json));
                items = null;
                try
                {

                    string lkstring = App.linkarClt.Select("LK.ITEMS", "", "BY ID", "", "", new SelectOptions(false, false, 10, 1, true), "", 0); //NEWFRAMEWORK: Replace Select_Text for Select, remove DATAFORMATCRU_TYPE.MV

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

                        items = new List<Item>();

                        for (int i = 0; i < lstids.Length; i++)
                        {
                            Item record = new Item();
                            if (recordCalculateds != null && recordCalculateds != "")
                            {
                                record = LkItem.GetRecord(lstids[i], lstregs[i], lstcalcs[i]);
                            }
                            else
                                record = LkItem.GetRecord(lstids[i], lstregs[i], "");

                            items.Add(record);
                        }
                    }
                }
                catch (Exception ex)
                {
                    string error = App.GetException(ex);
                    await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("ERROR", error, "OK");
                }
            }

            return items;
        }

        public async Task<Item> GetItemAsync(string id)
        {
            Item result = null;
            if (id != null)
            {
                //var json = await client.GetStringAsync($"api/Linkar/GetItem?token=" + App.Token + "&code=" + id);
                //return await Task.Run(() => JsonConvert.DeserializeObject<Item>(json));
                
                try
                {
                    string lkstring = App.linkarClt.Read("LK.ITEMS", id, "", new ReadOptions(true), "", 0); //NEWFRAMEWORK: Replace Read_Text for Read, remove DATAFORMATCRU_TYPE.MV

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
                        result = LkItem.GetRecord(id, records, recordCalculateds);
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