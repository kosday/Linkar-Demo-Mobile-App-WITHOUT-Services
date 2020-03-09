using System;

using LinkarDemoApp.Models;
using LinkarDemoApp.Services;

namespace LinkarDemoApp.ViewModels
{
    public class OrderDetailViewModel : BaseViewModel
    {
        public OrderDataStore DataStore = new OrderDataStore();
        public Order Order { get; set; }
        public OrderDetailViewModel(Order order = null)
        {
            Title = "Order Number: " + order?.Code;
            Order = order;
        }
    }

    public class OrderMvDetailViewModel : BaseViewModel
    {
        public MV_LstItems_CLkOrder OrderMv { get; set; }
        public OrderMvDetailViewModel(MV_LstItems_CLkOrder orderMv = null)
        {
            Title = "Item Code: " + orderMv?.Item;
            OrderMv = orderMv;
        }
    }

    public class OrderSvDetailViewModel : BaseViewModel
    {
        public SV_LstPartial_CLkOrder OrderSv { get; set; }
        public OrderSvDetailViewModel(SV_LstPartial_CLkOrder orderSv = null)
        {
            Title = "Delivery Date: " + orderSv?.DeliveryDateTime?.ToShortDateString();
            OrderSv = orderSv;
        }
    }
}
