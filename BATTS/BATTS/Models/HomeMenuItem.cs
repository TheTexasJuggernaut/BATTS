using System;
using System.Collections.Generic;
using System.Text;

namespace BATTS.Models
{
    public enum MenuItemType
    {
        Browse,
        About,
        Complete
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
