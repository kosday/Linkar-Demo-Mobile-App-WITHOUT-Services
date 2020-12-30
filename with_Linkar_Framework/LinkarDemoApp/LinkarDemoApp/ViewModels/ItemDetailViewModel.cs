using System;

using LinkarDemoApp.Models;
using LinkarDemoApp.Services;

namespace LinkarDemoApp.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public ItemDataStore DataStore = new ItemDataStore();
        public Item Item { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            Title = "Item Id: " + item?.Id;
            Item = item;
        }
    }
}
