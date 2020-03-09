using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using LinkarDemoApp.Models;
using LinkarDemoApp.Views;
using LinkarDemoApp.ViewModels;

namespace LinkarDemoApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomersPage : ContentPage
    {
        CustomersViewModel viewModel;

        public CustomersPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new CustomersViewModel();
        }

        async void OnCustomerSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var customer = args.SelectedItem as Customer;
            if (customer == null)
                return;

            await Navigation.PushAsync(new CustomerDetailPage(new CustomerDetailViewModel(customer)));

            // Manually deselect item.
            //CustomersListView.SelectedItem = null;
        }

        //async void AddCustomer_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        //}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Customers.Count == 0)
                viewModel.LoadCustomersCommand.Execute(null);
        }
    }
}