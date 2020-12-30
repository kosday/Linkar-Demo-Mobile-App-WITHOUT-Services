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
	public partial class OrderMvDetailPage : ContentPage
	{
        OrderMvDetailViewModel viewModel;

        public OrderMvDetailPage(OrderMvDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public OrderMvDetailPage()
        {
            InitializeComponent();

            //var customer = new Customer
            //{
            //    Text = "Item 1",
            //    Description = "This is an customer description."
            //};

            viewModel = new OrderMvDetailViewModel();//customer);
            BindingContext = viewModel;
        }

        async void OnSvSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var sv = args.SelectedItem as SV_LstPartial_CLkOrder;
            if (sv == null)
                return;

            await Navigation.PushAsync(new OrderSvDetailPage(new OrderSvDetailViewModel(sv)));

            // Manually deselect item.
            //CustomersListView.SelectedItem = null;
        }
    }
}