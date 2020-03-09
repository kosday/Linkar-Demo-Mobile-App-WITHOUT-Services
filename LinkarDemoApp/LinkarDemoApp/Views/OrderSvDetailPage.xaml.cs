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
	public partial class OrderSvDetailPage : ContentPage
	{
        OrderSvDetailViewModel viewModel;

        public OrderSvDetailPage(OrderSvDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public OrderSvDetailPage()
        {
            InitializeComponent();

            //var customer = new Customer
            //{
            //    Text = "Item 1",
            //    Description = "This is an customer description."
            //};

            viewModel = new OrderSvDetailViewModel();//customer);
            BindingContext = viewModel;
        }
    }
}