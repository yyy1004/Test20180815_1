using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web
{
    public class Menu
    {
        public Items[] Property1 { get; set; }
    }

    public class Items
    {
        public int id { get; set; }
        public string text { get; set; }
        public string state { get; set; }
        public Child[] children { get; set; }
    }

    public class Child
    {
        public int id { get; set; }
        public string text { get; set; }
        public string state { get; set; }
        public Attribute[] attributes { get; set; }
    }

    public class Attribute
    {
        public string url { get; set; }
    }

}