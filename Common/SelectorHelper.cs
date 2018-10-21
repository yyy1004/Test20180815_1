using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class SelectorHelper
    {
        /// <summary>
        /// 所有的下拉列表项
        /// </summary>
        private List<Item> Items;

        public SelectorHelper()
        {
            Items = new List<Item>();
        }

        /// <summary>
        /// 添加下拉列表项
        /// </summary>
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <param name="selected"></param>
        /// <returns></returns>
        public List<Item> AddItem(string id, string text, bool selected)
        {
            Item item = new Item();
            item.Id = id;
            item.Text = text;
            item.Selected = selected;
            Items.Add(item);
            return this.Items;
        }

        public override string ToString()
        {
            string result = "";

            foreach (var item in Items)
            {
                result += item.ToString() + ",";
            }

            result = result.Substring(0, result.Length - 1);

            return "[" + result + "]";
        }
    }

    public class Item
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public bool Selected { get; set; }

        public override string ToString()
        {
            string ret = (Selected == true) ? "true" : "false";
            return "{\"id\":\"" + Id + "\"," + "\"text\":\"" + Text + "\"," + "\"selected\":" + ret + "}";
        }
    }
}
