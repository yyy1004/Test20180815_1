using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using Model;
using DataService;
using Newtonsoft.Json;

namespace Common
{
    public class MenuHelper
    {
        private static DataAccess data = new DataAccess();

        /// <summary>
        /// 获取所有菜单信息表
        /// </summary>
        /// <returns></returns>
        public static Hashtable GetAllMenu()
        {
            string sql = "SELECT * FROM `web`.`菜单表`";
            DataTable dTable = data.GetTable(sql);
            Hashtable menus = new Hashtable();
            for (int i = 0; i < dTable.Rows.Count; i++)
            {
                MenuInfo menu = new MenuInfo();
                menu.Id = int.Parse(dTable.Rows[i]["菜单号"].ToString());
                menu.Name = dTable.Rows[i]["菜单名称"].ToString();
                menu.Url = dTable.Rows[i]["URL"].ToString();
                menus.Add(menu.Name, menu);
            }
            return menus;
        }

        /// <summary>
        /// 根据菜单ID获取菜单名
        /// </summary>
        /// <returns></returns>
        public static string GetMenuNameById(int id)
        {
            string sql = "SELECT * FROM `web`.`菜单表` where `菜单号`=" + id;
            DataTable dTable = data.GetTable(sql);
            return dTable.Rows[0]["菜单名称"].ToString();
        }
        /// <summary>
        /// 根据菜单ID获取菜单名
        /// </summary>
        /// <returns></returns>
        public static string GetIdByMenuName(string name)
        {
            string sql = "SELECT * FROM `web`.`菜单表` where `菜单名称`='" + name + "'";
            DataTable dTable = data.GetTable(sql);
            return dTable.Rows[0]["菜单号"].ToString();
        }
    }
}
