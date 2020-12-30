using System;
using System.Collections.Generic;
using System.Text;

namespace LinkarDemoApp.Models
{
    public enum MenuItemType
    {
        About,
        Item,
        Customer,
        Order        
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
