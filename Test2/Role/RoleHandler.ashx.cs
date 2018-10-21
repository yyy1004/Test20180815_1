using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataService;
using Common;
using System.Data;

namespace Web.Role
{
    /// <summary>
    /// RoleHandler 的摘要说明
    /// </summary>
    public class RoleHandler : IHttpHandler
    {
        DataAccess data = new DataAccess();

        public void ProcessRequest(HttpContext context)
        {
            string action = context.Request["action"];
            string result = "";

            switch (action)
            {
                case "GetAll":
                    result = GetAll();
                    break;
                default:
                    break;
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(result);
        }

        /// <summary>
        /// 获取所有的机组信息列表
        /// </summary>
        /// <returns></returns>
        public string GetAll()
        {
            SelectorHelper shelper = new SelectorHelper();

            string sql = "SELECT * FROM `web`.`角色表`";
            DataTable dTable = data.GetTable(sql);
            for (int i = 0; i < dTable.Rows.Count; i++)
            {
                string name = dTable.Rows[i]["角色名称"].ToString();
                if (i == 0)
                {
                    shelper.AddItem(name, name, true);
                }
                else
                {
                    shelper.AddItem(name, name, false);
                }
            }
            return shelper.ToString();
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