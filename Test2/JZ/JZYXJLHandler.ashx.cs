using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataService;
using Model;
using Common;
using System.Data;
using Newtonsoft.Json;

namespace Web.JZYXJL
{
    /// <summary>
    /// ZXHandler 的摘要说明
    /// </summary>
    public class JZYXJL : IHttpHandler
    {
        DataAccess data = new DataAccess();
        public void ProcessRequest(HttpContext context)
        {
            jzyxjlInfo zx = new jzyxjlInfo();
            ResultInfo result = new ResultInfo();
            string action = context.Request["action"].ToString();
            context.Response.ContentType = "text/plain";

            switch (action)
            {
                
                case "GetAll":
                    //获取所有的咨询记录包括已完成和未完成的咨询
                    int page = int.Parse(context.Request.Form["page"]);
                    int rows = int.Parse(context.Request.Form["rows"]);
                    context.Response.Write(GetAll(page, rows));
                    break;
             
                default:
                    break;
            }
        }


       
       

        /// <summary>
        /// 获取所有咨询信息
        /// </summary>
        /// <returns>用户信息列表的json字符串形式</returns>
        public string GetAll(int page, int rows)
        {
            int start = (page - 1) * rows;
            int end = page * rows;
            List<jzyxjlInfo> zxs = new List<jzyxjlInfo>();
            string sql = "SELECT * FROM `web`.`机组运行记录表`";
            string where = "limit " + start + "," + end;
            Dictionary<string, object> d = new Dictionary<string, object>();
            //获取数据总数(注意是总数,不是一页中数据的条数)
            DataTable dTable = data.GetTable(sql);
            d.Add("total", data.GetTable(sql).Rows.Count);
            //获取page页的数据
            dTable = dTable.AsEnumerable().Skip((page - 1) * rows).Take(rows).CopyToDataTable();
            for (int i = 0; i < dTable.Rows.Count; i++)
            {
                jzyxjlInfo zx = new jzyxjlInfo();
                zx.Id = int.Parse(dTable.Rows[i]["ID"].ToString());
                zx.JZName = dTable.Rows[i]["机组名称"].ToString();
                zx.JZId = dTable.Rows[i]["机组编号"].ToString();
                zx.Time = dTable.Rows[i]["时间"].ToString();
                zx.MS = dTable.Rows[i]["状态描述"].ToString();
                zx.Status = int.Parse(dTable.Rows[i]["状态"].ToString());

                zxs.Add(zx);
            }
            d.Add("rows", zxs);

            return JsonConvert.SerializeObject(d);
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