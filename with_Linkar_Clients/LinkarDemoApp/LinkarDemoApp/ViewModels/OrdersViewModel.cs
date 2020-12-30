using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using LinkarDemoApp.Models;
using LinkarDemoApp.Views;
using System.Net.Http;

using LinkarDemoApp.Services;

namespace LinkarDemoApp.ViewModels
{
    public class OrdersViewModel : BaseViewModel
    {
        public OrderDataStore DataStore = new OrderDataStore();
        public ObservableCollection<Order> Orders { get; set; }
        public Command LoadOrdersCommand { get; set; }

        public OrdersViewModel()
        {
            Title = "Orders";
            Orders = new ObservableCollection<Order>();
            LoadOrdersCommand = new Command(async () => await ExecuteLoadOrdersCommand());
        }

        async Task ExecuteLoadOrdersCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Orders.Clear();
                var orders = await DataStore.GetOrdersAsync(true);
                foreach (var order in orders)
                {
                    Orders.Add(order);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}