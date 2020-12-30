using LinkarDemoApp.Models;
using LinkarDemoApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LinkarDemoApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OrdersPage : ContentPage
	{
        OrdersViewModel viewModel;

        public OrdersPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new OrdersViewModel();
        }

        async void OnOrderSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var order = args.SelectedItem as Order;
            if (order == null)
                return;

            await Navigation.PushAsync(new OrderDetailPage(new OrderDetailViewModel(order)));

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

            if (viewModel.Orders.Count == 0)
                viewModel.LoadOrdersCommand.Execute(null);
        }
    }
}