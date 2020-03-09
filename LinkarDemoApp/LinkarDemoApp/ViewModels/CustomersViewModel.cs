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
    public class CustomersViewModel : BaseViewModel
    {
        public CustomerDataStore DataStore = DependencyService.Get<CustomerDataStore>() ?? new CustomerDataStore();

        public ObservableCollection<Customer> Customers { get; set; }
        public Command LoadCustomersCommand { get; set; }

        public CustomersViewModel()
        {
            Title = "Customers";
            Customers = new ObservableCollection<Customer>();
            LoadCustomersCommand = new Command(async () => await ExecuteLoadCustomersCommand());
        }

        async Task ExecuteLoadCustomersCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Customers.Clear();
                var customers = await DataStore.GetCustomersAsync(true);
                foreach (var customer in customers)
                {
                    Customers.Add(customer);
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