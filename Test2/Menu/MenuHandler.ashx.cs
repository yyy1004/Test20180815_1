using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model;
using DataService;
using System.Data;
using Common;
using System.Collections;
using System.Web.SessionState;

namespace Web.Menu
{
    /// <summary>
    /// MenuHandler 的摘要说明
    /// </summary>
    public class MenuHandler : IHttpHandler, IRequiresSessionState
    {
        DataAccess data = new DataAccess();

        public void ProcessRequest(HttpContext context)
        {
            string action = context.Request["action"].ToString();
            context.Response.ContentType = "text/plain";

            switch (action)
            {
                case "Add":
                    MenuInfo menu1 = new MenuInfo();
                    menu1.Id = context.Request.Form["Id"];
                    menu1.Name = context.Request.Form["Name"];
                    menu1.ParentId = context.Request.Form["ParentId"];
                    menu1.Num = context.Request.Form["Num"];
                    menu1.Disc = context.Request.Form["Disc"];
                    menu1.Url = context.Request.Form["Url"];
                    menu1.Ico = context.Request.Form["Ico"];
                    menu1.Level = context.Request.Form["Level"];
                    //保存数据到数据库中
                    bool success = Add(menu1);
                    context.Response.Write(action + ":操作" + (success ? "成功" : "失败") + "！");
                    break;
                case "GetAll":
                    context.Response.Write(GetAll());
                    break;
                case "GetUser":
                    string name1 = context.Request["Name"];
                    context.Response.Write(GetUser(name1));
                    break;
                case "Update":
                    menu1 = new MenuInfo();
                    menu1.Id = context.Request.Form["Id"];
                    menu1.Name = context.Request.Form["Name"];
                    menu1.ParentId = context.Request.Form["ParentId"];
                    menu1.Num = context.Request.Form["Num"];
                    menu1.Disc = context.Request.Form["Disc"];
                    menu1.Url = context.Request.Form["Url"];
                    menu1.Ico = context.Request.Form["Ico"];
                    menu1.Level = context.Request.Form["Level"];
                    context.Response.Write("更新：" + ((Update(menu1) == true) ? "成功" : "失败") + "!");
                    break;
                case "Delete":
                    string id = context.Request["Id"];
                    context.Response.Write(Delete(id));
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 保存菜单信息
        /// </summary>
        /// <param name="menu"></param>
        public bool Add(MenuInfo menu)
        {
            string sql = "INSERT INTO `web`.`菜单表`(`菜单号`, `菜单名称`, `父菜单号`, `菜单序号`, `菜单描述`, `URL`, `菜单图标`, `菜单等级`) VALUES (" +
                menu.Id + ", '" +
                menu.Name + "', " +
                 menu.ParentId + ", " +
                 menu.Num + ", '" +
                 menu.Disc + "', '" +
                 menu.Url + "', '" +
                 menu.Ico + "', " +
                 menu.Level + ")";
            return data.ExecSql(sql);
        }

        /// <summary>
        /// 获取所有菜单信息
        /// </summary>
        /// <returns>用户信息列表的json字符串形式</returns>
        public string GetAll()
        {
            List<MenuInfo> menus = new List<MenuInfo>();
            string sql = "SELECT * FROM `web`.`菜单表`";
            DataTable dTable = data.GetTable(sql);
            for (int i = 0; i < dTable.Rows.Count; i++)
            {
                MenuInfo menu = new MenuInfo();
                menu.Id = dTable.Rows[i]["菜单号"].ToString();
                menu.Name = dTable.Rows[i]["菜单名称"].ToString();
                menu.ParentId = dTable.Rows[i]["父菜单号"].ToString();
                menu.Num = dTable.Rows[i]["菜单序号"].ToString();
                menu.Disc = dTable.Rows[i]["菜单描述"].ToString();
                menu.Url = dTable.Rows[i]["URL"].ToString();
                menu.Ico = dTable.Rows[i]["菜单图标"].ToString();
                menu.Level = dTable.Rows[i]["菜单等级"].ToString();

                menus.Add(menu);
            }
            string result = JsonHelper<MenuInfo>.ListToJsonString(menus);
            return result;
        }

        /// <summary>
        /// 获取指定菜单项的json信息
        /// </summary>
        /// <param name="uname"></param>
        /// <returns></returns>
        public string GetUser(string uname)
        {
            string sql = "SELECT * FROM `web`.`菜单表` where 菜单名称='" + uname + "'";

            DataTable dTable = data.GetTable(sql);
            MenuInfo menu = new MenuInfo();
            menu.Id = dTable.Rows[0]["菜单号"].ToString();
            menu.Name = dTable.Rows[0]["菜单名称"].ToString();
            menu.ParentId = dTable.Rows[0]["父菜单号"].ToString();
            menu.Num = dTable.Rows[0]["菜单序号"].ToString();
            menu.Disc = dTable.Rows[0]["菜单描述"].ToString();
            menu.Url = dTable.Rows[0]["URL"].ToString();
            menu.Ico = dTable.Rows[0]["菜单图标"].ToString();
            menu.Level = dTable.Rows[0]["菜单等级"].ToString();

            return JsonHelper<MenuInfo>.ObjectToJsonString(menu);
        }

        /// <summary>
        /// 删除菜单信息
        /// </summary>
        /// <param name="user">菜单编号</param>
        /// <returns>删除是否成功</returns>
        public bool Delete(string id)
        {
            string sql = "Delete from `web`.`菜单表` WHERE `菜单号` = " + id;
            return data.ExecSql(sql);
        }

        /// <summary>
        /// 更新菜单信息
        /// </summary>
        /// <param name="menu">菜单信息</param>
        /// <returns>更新是否成功</returns>
        public bool Update(MenuInfo menu)
        {
            string sql = "Update `web`.`菜单表` SET " +
                "`菜单名称` = '" + menu.Name + "'," +
                "`父菜单号` = " + menu.ParentId + "," +
                "`菜单序号` = " + menu.Num + "," +
                "`菜单描述` = '" + menu.Disc + "'," +
                "`URL` = '" + menu.Url + "'," +
                "`菜单图标` = '" + menu.Ico + "'," +
                "`菜单等级` = " + menu.Level + "  WHERE `菜单号` = " + menu.Id;
            return data.ExecSql(sql);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}