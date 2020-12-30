using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LinkarDemoApp.Models;

using LinkarClient;
using LinkarCommon;

namespace LinkarDemoApp.Services
{
    public class ItemDataStore
    {
        //IEnumerable<Item> items;
        CredentialsOptions credentialsOptions;

        public ItemDataStore()
        {
            credentialsOptions = new CredentialsOptions("94.130.139.208", 21301, "DEMODESKTOP", "54poimnmaa;", "DEMOLINKAR");
            //items = new List<Item>();
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            //var json = await client.GetStringAsync($"api/Linkar/GetItems?token=" + App.Token);
            //items = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Item>>(json));

            //LkData lkdata = LinkarClt.GetVersion(credentialsOptions);
            List<Item> items = null;
            string lkstring = LinkarClt.Select_Text(credentialsOptions,"LK.ITEMS", "", "BY CODE", "", "", new SelectOptions(false, false, 10, 1, true), DataFormat.TYPE.MV, "", 0);

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
                        //if (ThisList[i].Equals("ERRORS"))
                        //{
                        //    if (parts[i] != null && parts[i].Length > 0)
                        //        this.LstErrors = new List<string>(parts[i].Split(DBMV_Mark.AM));
                        //    else
                        //        this.LstErrors = new List<string>();
                        //}
                    }
                }

                //Fill all the records with response data
                String[] lstids = recordIds.Split(ASCII_Chars.RS_chr);
                String[] lstregs = records.Split(ASCII_Chars.RS_chr);
                String[] lstcalcs = recordCalculateds.Split(ASCII_Chars.RS_chr);

                items = new List<Item>();

                for (int i = 0; i < lstids.Length; i++)
                {
                    Item record = new Item();//_LinkarClt);
                                             //record.RecordOriginalContent = lstregs[i];
                    if (recordCalculateds != null && recordCalculateds != "")
                    {
                        //record.RecordOriginalContentItypes = lstcalcs[i];
                        record = LkItem.GetRecord(lstids[i], lstregs[i], lstcalcs[i]);
                    }
                    else
                        record = LkItem.GetRecord(lstids[i], lstregs[i], "");

                    //if (this.LstErrors != null && this.LstErrors.Count > 0)
                    //{
                    //    for (int j = 0; j < this.LstErrors.Count; j++)
                    //    {
                    //        if (!string.IsNullOrEmpty(this.LstErrors[i]))
                    //        {
                    //            string[] errorParts = this.LstErrors[i].Split(DBMV_Mark.VM);
                    //            if (errorParts.Length > 2 && errorParts[2] == record.Code)
                    //                record.LstErrors.Add(this.LstErrors[i]);
                    //        }
                    //    }
                    //}

                    //record.Status = LinkarMainClass.StatusTypes.READED;
                    items.Add(record);
                }
            }

            return items;
        }

        public async Task<Item> GetItemAsync(string id)
        {
            Item item = null;
            if (id != null)
            {
                //LkData lkdata = LinkarClt.GetVersion(credentialsOptions);

                //var json = await client.GetStringAsync($"api/Linkar/GetItem?token=" + App.Token + "&code=" + id);
                //return await Task.Run(() => JsonConvert.DeserializeObject<Item>(json));

                string lkstring = LinkarClt.Read_Text(credentialsOptions,"LK.ITEMS", id, "", new ReadOptions(true), DataFormat.TYPE.MV, "", 0);

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
                        //if (ThisList[i].Equals("ERRORS"))
                        //{
                        //    if (parts[i] != null && parts[i].Length > 0)
                        //        this.LstErrors = new List<string>(parts[i].Split(DBMV_Mark.AM));
                        //    else
                        //        this.LstErrors = new List<string>();
                        //}
                    }
                }

                //Fill the class with response data
                if (records != null && records != "")
                    item = LkItem.GetRecord(id, records, recordCalculateds);
            }

            return item;
        }
    }
}