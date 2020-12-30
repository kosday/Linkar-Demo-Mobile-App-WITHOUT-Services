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
	public partial class OrderDetailPage : ContentPage
	{
        OrderDetailViewModel viewModel;

        public OrderDetailPage(OrderDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public OrderDetailPage()
        {
            InitializeComponent();

            //var customer = new Customer
            //{
            //    Text = "Item 1",
            //    Description = "This is an customer description."
            //};

            viewModel = new OrderDetailViewModel();//customer);
            BindingContext = viewModel;
        }

        async void OnMvSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var mv = args.SelectedItem as MV_LstItems_CLkOrder;
            if (mv == null)
                return;

            await Navigation.PushAsync(new OrderMvDetailPage(new OrderMvDetailViewModel(mv)));

            // Manually deselect item.
            //CustomersListView.SelectedItem = null;
        }
    }
}