using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common;
using System.Data;
using DataService;
using Model;
using System.Web.SessionState;
using JrscSoft.Common;
using Newtonsoft.Json;

namespace Web.JT
{
    /// <summary>
    /// JTHandler 的摘要说明
    /// </summary>
    public class JTHandler : IHttpHandler, IRequiresSessionState
    {
        DataAccess data = new DataAccess();

        public void ProcessRequest(HttpContext context)
        {
            ResultInfo result = new ResultInfo();
            JTInfo jt1 = new JTInfo();
            //记录用户的行为
            string action = context.Request["action"].ToString();
            context.Response.ContentType = "text/plain";

            switch (action)
            {
                case "Add":
                    try
                    {
                        jt1.Name = context.Request.Form["Name"];
                        jt1.Address = context.Request.Form["Address"];
                        jt1.PostAddress = context.Request.Form["PostAddress"];
                        jt1.Introduce = context.Request.Form["Introduce"];
                        jt1.Phone = context.Request.Form["Phone"];
                        jt1.Email = context.Request.Form["Email"];
                        jt1.JW = context.Request.Form["JW"];
                        //保存数据到数据库中
                        result.Success = Add(jt1);
                        result.Message = "插入集团信息" + ((result.Success == true) ? "成功" : "失败") + "!";
                    }
                    catch (Exception ex)
                    {
                        result.Success = false;
                        result.Message = "异常:" + ex.Message;
                    }
                    //返回客户端信息
                    context.Response.Write(JsonHelper<ResultInfo>.ObjectToJsonString(result));
                    break;
                case "GetAll":
                    context.Response.Write(GetAll());
                    break;
                case "GetMenuId":
                    string menuName = context.Request.Form["menuName"];
                    context.Response.Write(GetMenuIdAddUrl(menuName));
                    break;
                case "GetAllJTInfo":
                    int page = int.Parse(context.Request.Form["page"]);
                    int rows = int.Parse(context.Request.Form["rows"]);
                    context.Response.Write(GetAllJTInfo(page, rows));
                    break;
                case "GetJTJZState":
                    int menuid = int.Parse(CheckLoginStatus.node);
                    string jtname = MenuHelper.GetMenuNameById(menuid);
                    context.Response.Write(GetJTJZState(jtname));
                    break;
                case "Update":
                    try
                    {
                        jt1 = new JTInfo();
                        jt1.ID = context.Request.Form["ID"];
                        jt1.Name = context.Request.Form["Name"];
                        jt1.Address = context.Request.Form["Address"];
                        jt1.PostAddress = context.Request.Form["PostAddress"];
                        jt1.Introduce = context.Request.Form["Introduce"];
                        jt1.Phone = context.Request.Form["Phone"];
                        jt1.Email = context.Request.Form["Email"];
                        jt1.JW = context.Request.Form["JW"];
                        result.Success = Update(jt1);
                        result.Message = "更新集团[" + jt1.Name + "]信息" + ((result.Success == true) ? "成功" : "失败") + "!";
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
                default:
                    break;
            }
        }

        /// <summary>
        /// 获取所有的集团【目录】
        /// </summary>
        /// <returns></returns>
        public string GetAll()
        {
            SelectorHelper shelper = new SelectorHelper();

            string sql = "SELECT * FROM `web`.`集团表`";
            DataTable dTable = data.GetTable(sql);
            for (int i = 0; i < dTable.Rows.Count; i++)
            {
                string name = dTable.Rows[i]["集团名称"].ToString();
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
        /// 获取集团所有机组的状态
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetJTJZState(string name)
        {
            string sql = "SELECT * FROM `web`.`机组状态一览表` where `集团名称`='" + name + "'";

            List<JTJZStatus> jts = new List<JTJZStatus>();
            DataTable dTable = data.GetTable(sql);
            for (int i = 0; i < dTable.Rows.Count; i++)
            {
                JTJZStatus jz = new JTJZStatus();

                jz.JTName = dTable.Rows[i]["集团名称"].ToString();
                jz.GCName = dTable.Rows[i]["工厂名称"].ToString();
                jz.Total = int.Parse(dTable.Rows[i]["总机组数"].ToString());
                jz.Work = int.Parse(dTable.Rows[i]["工作机组数"].ToString());
                jz.Warn = int.Parse(dTable.Rows[i]["报警故障机组数"].ToString());
                jz.Stop = int.Parse(dTable.Rows[i]["停机数"].ToString());
                jts.Add(jz);
            }

            return JsonHelper<JTJZStatus>.ListToJsonString(jts);
        }

        /// <summary>
        /// 获取所有的集团详细信息列表[分页]
        /// </summary>
        /// <returns></returns>
        public string GetAllJTInfo(int page, int rows)
        {
            int start = (page - 1) * rows;
            int end = page * rows;
            List<JTInfo> jts = new List<JTInfo>();
            string sql = "SELECT * FROM `web`.`集团表`";
            string where = "limit " + start + "," + end;
            Dictionary<string, object> d = new Dictionary<string, object>();
            //获取数据总数(注意是总数,不是一页中数据的条数)
            DataTable dTable = data.GetTable(sql);
            d.Add("total", data.GetTable(sql).Rows.Count);
            //获取page页的数据
            dTable = dTable.AsEnumerable().Skip((page - 1) * rows).Take(rows).CopyToDataTable();
            for (int i = 0; i < dTable.Rows.Count; i++)
            {
                JTInfo jt1 = new JTInfo();
                jt1.ID = dTable.Rows[i]["ID"].ToString();
                jt1.Name = dTable.Rows[i]["集团名称"].ToString();
                jt1.Address = dTable.Rows[i]["集团地址"].ToString();
                jt1.PostAddress = dTable.Rows[i]["集团邮编"].ToString();
                jt1.Introduce = dTable.Rows[i]["集团介绍"].ToString();
                jt1.Phone = dTable.Rows[i]["集团电话"].ToString();
                jt1.Email = dTable.Rows[i]["集团电子邮箱"].ToString();
                jt1.JW = dTable.Rows[i]["集团经纬度"].ToString();
                jts.Add(jt1);
            }
            d.Add("rows", jts);
            return JsonConvert.SerializeObject(d);
        }

        /// <summary>
        /// 保存集团的信息
        /// </summary>
        /// <param name="jt"></param>
        public bool Add(JTInfo jt)
        {
            string sql = "INSERT INTO `web`.`集团表`(`集团名称`, `集团地址`, `集团邮编`, `集团介绍`, `集团电话`, `集团电子邮箱`, `集团经纬度`) VALUES ('" +
                jt.Name + "', '" +
                 jt.Address + "', '" +
                 jt.PostAddress + "', '" +
                 jt.Introduce + "', '" +
                 jt.Phone + "', '" +
                 jt.Email + "', '" +
                 jt.JW + "')";
            return data.ExecSql(sql);
        }

        /// <summary>
        /// 删除集团信息
        /// </summary>
        /// <param name="user">集团信息</param>
        /// <returns>更新是否成功</returns>
        public bool Delete(string name)
        {
            string sql = "DELETE FROM `web`.`集团表` WHERE `集团名称` = '" + name + "'";
            return data.ExecSql(sql);
        }

        /// <summary>
        /// 更新集团信息
        /// </summary>
        /// <param name="jt">集团信息</param>
        /// <returns>更新是否成功</returns>
        public bool Update(JTInfo jt)
        {
            string sql = "Update `web`.`集团表` SET " +
                "`集团地址` = '" + jt.Address + "'," +
                "`集团邮编` = '" + jt.PostAddress + "'," +
                "`集团介绍` = '" + jt.Introduce + "'," +
                "`集团电话` = '" + jt.Phone + "'," +
                "`集团电子邮箱` = '" + jt.Email + "'," +
                "`集团名称` = '" + jt.Name + "'," +
                "`集团经纬度` = '" + jt.JW + "'" + " WHERE `ID` = " + jt.ID + "";
            return data.ExecSql(sql);
        }

        public string GetMenuIdAddUrl(string name)
        {

            DataTable dt = new DataTable();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string sql = string.Format(@"SELECT
	                                                                                    `菜单号`,
                                                                                            `URL`
                                                                                    FROM
	                                                                                    `菜单表`
                                                                                    WHERE
	                                                                                    `菜单名称` = '{0}'", name);
            dt = data.GetTable(sql);
            if (dt.Rows.Count>0)
            {
                dic.Add("menuId", dt.Rows[0][0].ToString());
                dic.Add("url", dt.Rows[0][1].ToString());
            }
            return JsonConvert.SerializeObject(dic);
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