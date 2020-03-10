using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MGSProject.Models
{
  
    public class Item
    {
        public string html_url { get; set; }
    }

    public class RootObject
    {
        public List<Item> items { get; set; }
    }
}