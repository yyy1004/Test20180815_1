using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common;
using System.Data;
using DataService;
using Model;
using System.Web.SessionState;
using Newtonsoft.Json;

namespace Web.GC
{
    /// <summary>
    /// GCHandler 的摘要说明
    /// </summary>
    public class GCHandler : IHttpHandler, IRequiresSessionState
    {
        DataAccess data = new DataAccess();
        private string gcurl = "/GC/Index.html";
        MenuInfo menu = new MenuInfo();
       

        public void ProcessRequest(HttpContext context)
        {
            ResultInfo result = new ResultInfo();
            GCInfo gc1 = new GCInfo();
            string action = context.Request["action"].ToString();
            context.Response.ContentType = "text/plain";

            switch (action)
            {
                case "GetAll":
                    context.Response.Write(GetAll());
                    break;
                case "GetGC":
                    string jt = HttpUtility.UrlDecode(context.Request["jt"]);
                    context.Response.Write(GetGC(jt));
                    break;
                case "Add":
                    try
                    {
                        gc1.Name = context.Request.Form["Name"];
                        gc1.Address = context.Request.Form["Address"];
                        gc1.PostAddress = context.Request.Form["PostAddress"];
                        gc1.Introduce = context.Request.Form["Introduce"];
                        gc1.Phone = context.Request.Form["Phone"];
                        gc1.Email = context.Request.Form["Email"];
                        gc1.JTName = context.Request.Form["JTName"];
                        gc1.JW = context.Request.Form["JW"];
                        //保存数据到数据库中
                        result.Success = Add(gc1);
                        result.Message = "插入工厂信息" + ((result.Success == true) ? "成功" : "失败") + "!";
                    }
                    catch (Exception ex)
                    {
                        result.Success = false;
                        result.Message = "异常:" + ex.Message;
                    }
                    //返回客户端信息
                    context.Response.Write(JsonHelper<ResultInfo>.ObjectToJsonString(result));
                    break;
                case "Update":
                    try
                    {
                        gc1 = new GCInfo();
                        gc1.ID = context.Request.Form["ID"];
                        gc1.Name = context.Request.Form["Name"];
                        gc1.Address = context.Request.Form["Address"];
                        gc1.PostAddress = context.Request.Form["PostAddress"];
                        gc1.Introduce = context.Request.Form["Introduce"];
                        gc1.Phone = context.Request.Form["Phone"];
                        gc1.Email = context.Request.Form["Email"];
                        gc1.JTName = context.Request.Form["JTName"];
                        gc1.JW = context.Request.Form["JW"];
                        result.Success = Update(gc1);
                        result.Message = "更新工厂[" + gc1.Name + "]信息" + ((result.Success == true) ? "成功" : "失败") + "!";
                    }
                    catch (Exception ex)
                    {
                        result.Success = false;
                        result.Message = "异常:" + ex.Message;
                    }
                    //返回客户端信息
                    context.Response.Write(JsonHelper<ResultInfo>.ObjectToJsonString(result));
                    break;
                case "Delete":
                    try
                    {
                        int count = 0;
                        string id = context.Request["Id"];
                        string[] ids = id.Split(',');
                        for (int i = 0; i < ids.Length; i++)
                        {
                            result.Success = Delete(ids[i]);
                            if (result.Success == false)
                            {
                                break;
                            }
                            count++;
                        }
                        result.Message = ((result.Success == true) ? "操作成功:" : "操作失败:") + "共删除" + count + "条信息!";
                    }
                    catch (Exception ex)
                    {
                        result.Success = false;
                        result.Message = "异常:" + ex.Message;
                    }
                    //返回客户端信息
                    context.Response.Write(JsonHelper<ResultInfo>.ObjectToJsonString(result));
                    break;
                case "GetAllGCInfo":
                    int page = int.Parse(context.Request.Form["page"]);
                    int rows = int.Parse(context.Request.Form["rows"]);
                    context.Response.Write(GetAllGCInfo(page, rows));
                    break;
                case "GetAllGCJZInfo":
                   int menuid = int.Parse(CheckLoginStatus.node);
                  string gcname = MenuHelper.GetMenuNameById(menuid);
                   // string gcname = System.Web.HttpContext.Current.Request.QueryString["node"].ToString();
                    context.Response.Write(GetAllGCJZInfo(gcname));
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 获取所有的工厂信息列表【目录】
        /// </summary>
        /// <returns></returns>
        public string GetAll()
        {
            
            SelectorHelper shelper = new SelectorHelper();

            string sql = "SELECT * FROM `web`.`工厂表`";
            DataTable dTable = data.GetTable(sql);
            for (int i = 0; i < dTable.Rows.Count; i++)
            {
                string name = dTable.Rows[i]["工厂名称"].ToString();
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

        /// <summary>
        /// 获取某个集团所有的工厂信息列表【目录】
        /// </summary>
        /// <param name="name">集团名称</param>
        /// <returns></returns>
        public string GetGC(string name)
        {
            SelectorHelper shelper = new SelectorHelper();

            string sql = "SELECT * FROM `web`.`工厂表` where 集团名称='" + name + "'";
            DataTable dTable = data.GetTable(sql);
            for (int i = 0; i < dTable.Rows.Count; i++)
            {
                string n = dTable.Rows[i]["工厂名称"].ToString();
                if (i == 0)
                {
                    shelper.AddItem(n, n, true);
                }
                else
                {
                    shelper.AddItem(n, n, false);
                }
            }
            return shelper.ToString();
        }

        /// <summary>
        /// 保存工厂的信息
        /// </summary>
        /// <param name="user"></param>
        public bool Add(GCInfo gc)
        {
            string sql = "INSERT INTO `web`.`工厂表`(`工厂名称`, `工厂地址`, `工厂邮编`, `工厂介绍`, `工厂电话`, `工厂电子邮箱`, `集团名称`, `工厂经纬度`) VALUES ('" +
                gc.Name + "', '" +
                 gc.Address + "', '" +
                 gc.PostAddress + "', '" +
                 gc.Introduce + "', '" +
                 gc.Phone + "', '" +
                 gc.Email + "', '" +
                 gc.JTName + "', '" +
                 gc.JW + "')";
            return data.ExecSql(sql);
        }

        /// <summary>
        /// 获取所有工厂信息
        /// </summary>
        /// <returns>工厂信息列表的json字符串形式</returns>
        public string GetAllGCInfo(int page, int rows)
        {
            int start = (page - 1) * rows;
            int end = page * rows;
            string where = "limit " + start + "," + end;
            string sql = "SELECT * FROM `web`.`工厂表` order by 集团名称 desc ";
            List<GCInfo> gcs = new List<GCInfo>();
            Dictionary<string, object> d = new Dictionary<string, object>();
            //获取数据总数(注意是总数,不是一页中数据的条数)
            DataTable dTable = data.GetTable(sql);
            d.Add("total", data.GetTable(sql).Rows.Count);
            //获取page页的数据
            dTable = dTable.AsEnumerable().Skip((page - 1) * rows).Take(rows).CopyToDataTable();
            for (int i = 0; i < dTable.Rows.Count; i++)
            {
                GCInfo gc = new GCInfo();
                gc.ID = dTable.Rows[i]["ID"].ToString();
                gc.Name = dTable.Rows[i]["工厂名称"].ToString();
                gc.Address = dTable.Rows[i]["工厂地址"].ToString();
                gc.PostAddress = dTable.Rows[i]["工厂邮编"].ToString();
                gc.Introduce = dTable.Rows[i]["工厂介绍"].ToString();
                gc.Phone = dTable.Rows[i]["工厂电话"].ToString();
                gc.Email = dTable.Rows[i]["工厂电子邮箱"].ToString();
                gc.JTName = dTable.Rows[i]["集团名称"].ToString();
                gc.JW = dTable.Rows[i]["工厂经纬度"].ToString();
                gcs.Add(gc);
            }
            d.Add("rows", gcs);
            return JsonConvert.SerializeObject(d);
        }

        /// <summary>
        /// 获取工厂所有机组的状态
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetAllGCJZInfo(string name)
        {
            string sql = "SELECT * FROM `web`.`机组表` where `工厂名称`='" + name + "'";

            List<GCJZStatus> gcs = new List<GCJZStatus>();
            DataTable dTable = data.GetTable(sql);
            for (int i = 0; i < dTable.Rows.Count; i++)
            {
                GCJZStatus jz = new GCJZStatus();
                jz.Name = dTable.Rows[i]["机组名称"].ToString();
                jz.Status = dTable.Rows[i]["状态"].ToString();
                gcs.Add(jz);
            }

            return JsonHelper<GCJZStatus>.ListToJsonString(gcs);
        }

        /// <summary>
        /// 删除工厂信息
        /// </summary>
        /// <param name="user">工厂名称</param>
        /// <returns>更新是否成功</returns>
        public bool Delete(string name)
        {
            string sql = "Delete from `web`.`工厂表` WHERE `工厂名称` = '" + name + "'";
            return data.ExecSql(sql);
        }

        /// <summary>
        /// 更新工厂信息
        /// </summary>
        /// <param name="gc">工厂信息</param>
        /// <returns>更新是否成功</returns>
        public bool Update(GCInfo gc)
        {
            string sql = "Update `web`.`工厂表` SET " +
                "`工厂地址` = '" + gc.Address + "'," +
                "`工厂邮编` = '" + gc.PostAddress + "'," +
                "`工厂介绍` = '" + gc.Introduce + "'," +
                "`工厂电话` = '" + gc.Phone + "'," +
                "`工厂电子邮箱` = '" + gc.Email + "'," +
                "`集团名称` = '" + gc.JTName + "'," +
                "`工厂经纬度` = '" + gc.JW + "'," + "`工厂名称` = '" + gc.Name + "'" +
                " WHERE `ID` = " + gc.ID + "";
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