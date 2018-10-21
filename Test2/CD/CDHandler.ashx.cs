using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataService;
using Model;
using System.Data;
using Common;
using Newtonsoft.Json;

namespace Web.CD
{
    /// <summary>
    /// CDHandler 的摘要说明
    /// </summary>
    public class CDHandler : IHttpHandler
    {
        DataAccess data = new DataAccess();

        public void ProcessRequest(HttpContext context)
        {
            ResultInfo result = new ResultInfo();
            CDInfo cd = new CDInfo();
            string action = context.Request["action"].ToString();
            context.Response.ContentType = "text/plain";

            switch (action)
            {
                case "GetAll":
                    int page = int.Parse(context.Request.Form["page"]);
                    int rows = int.Parse(context.Request.Form["rows"]);
                    context.Response.Write(GetAll(page, rows));
                    break;
                case "Add":
                    try
                    {
                        cd.JZId = context.Request.Form["JZId"];
                        cd.Name = context.Request.Form["Name"];
                        cd.Introduce = context.Request.Form["Introduce"];
                        cd.value1 = int.Parse(context.Request.Form["yjz"]);
                        cd.value2 = int.Parse(context.Request.Form["bjz"]);
                        //保存数据到数据库中
                        result.Success = Add(cd);
                        result.Message = "插入测点信息" + ((result.Success == true) ? "成功" : "失败") + "!";
                    }
                    catch (Exception ex)
                    {
                        result.Success = false;
                        result.Message = ex.Message;
                    }
                    //返回客户端信息
                    context.Response.Write(JsonHelper<ResultInfo>.ObjectToJsonString(result));
                    break;
                case "Update":
                    try
                    {
                        cd = new CDInfo();
                        cd.Id = int.Parse(context.Request.Form["Id"]);
                        cd.JZId = context.Request.Form["JZId"];
                        cd.Name = context.Request.Form["Name"];
                        cd.Introduce = context.Request.Form["Introduce"];
                        cd.value1 = int.Parse(context.Request.Form["yjz"]);
                        cd.value2 = int.Parse(context.Request.Form["bjz"]);
                        string oldName = GetCDInfo(cd.Id).Name;
                        result.Success = Update(cd, oldName);
                        result.Message = "更新测点[" + cd.Name + "]信息" + ((result.Success == true) ? "成功" : "失败") + "!";
                    }
                    catch (Exception ex)
                    {
                        result.Success = false;
                        result.Message = ex.Message;
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
                            result.Success = Delete(GetCDInfo(int.Parse(ids[i])));
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
                        result.Message = ex.Message;
                    }
                    //返回客户端信息
                    context.Response.Write(JsonHelper<ResultInfo>.ObjectToJsonString(result));
                    break;
                case "GetAllCDInfo":
                    int menuid = int.Parse(CheckLoginStatus.node);
                    string jzname = MenuHelper.GetMenuNameById(menuid);
                    string sql = "SELECT * FROM `web`.`机组表` where `机组名称`='" + jzname + "'";
                    DataTable dTable = data.GetTable(sql);
                    string jzid = dTable.Rows[0]["机组编号"].ToString();
                    string ret = "[" + GetAllCDInfo(jzid) + "]";
                    context.Response.Write(ret);
                    break;
                case "GetCDInfo":
                    string jzid2 = context.Request.Form["JZId"].ToString();
                    string cdname = context.Request.Form["CDName"].ToString();
                    string startdate = context.Request.Form["Start"].ToString();
                    string enddate = context.Request.Form["End"].ToString();
                    string[] cdnames = cdname.Split(',');//测点名称
                    sql = "SELECT " + "测试日期,测试时间,"+ cdname + " FROM `web`.`" + jzid2 + "` where 测试日期 between '" + startdate + "' and '" + enddate + "'";
                    dTable = data.GetTable(sql);
                    List<GraphInfo> gs = new List<GraphInfo>();
                    string[] x = new string[dTable.Rows.Count];
                    for (int i = 0; i < dTable.Rows.Count; i++)
                    {
                        x[i] = DateTime.Parse(dTable.Rows[i]["测试日期"].ToString()).ToShortDateString() + " " + dTable.Rows[i]["测试时间"].ToString();
                    }
                    //外层是各个测试点，内层是数据总数
                    for (int j = 0; j < cdnames.Length; j++)
                    {
                        GraphInfo g = new GraphInfo();
                        g.name = cdnames[j];
                        g.type = "line";
                        g.data = new double[dTable.Rows.Count];
                        for (int i = 0; i < dTable.Rows.Count; i++)
                        {
                            g.data[i] = double.Parse(dTable.Rows[i][cdnames[j]].ToString());
                        }
                        gs.Add(g);
                    }
                    GraphInfo2 gi = new GraphInfo2();
                    gi.series = gs;
                    gi.x = x;
                    string ret2 = JsonConvert.SerializeObject(gi);
                    context.Response.Write(ret2);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 保存测点的信息
        /// </summary>
        /// <param name="cd"></param>
        public bool Add(CDInfo cd)
        {
            string sql = "INSERT INTO `web`.`测点表`(`机组编号`, `测点名称`, `测点描述`, `预警值`, `报警值`) VALUES ('" +
                cd.JZId + "', '" +
                 cd.Name + "', '" +
                  cd.Introduce + "', " +
                   cd.value1 + ", " +
                 cd.value2 + ");";
            //alter table [表名] add 字段名 double default 0 增加数字字段，双精度型，缺省值为0
            sql += "alter table `" + cd.JZId + "` add `" + cd.Name + "` double default 0;";
            return data.ExecSql(sql);
        }

        /// <summary>
        /// 根据Id获取测点信息
        /// </summary>
        /// <param name="id">测点编号</param>
        /// <returns>测点信息</returns>
        public CDInfo GetCDInfo(int id)
        {
            string sql = "SELECT * FROM `web`.`测点表` where `测点编号`=" + id;
            DataTable dTable = data.GetTable(sql);
            CDInfo cd = new CDInfo();
            cd.Id = int.Parse(dTable.Rows[0]["测点编号"].ToString());
            cd.JZId = dTable.Rows[0]["机组编号"].ToString();
            cd.Name = dTable.Rows[0]["测点名称"].ToString();
            cd.Introduce = dTable.Rows[0]["测点描述"].ToString();
            cd.value1 = int.Parse(dTable.Rows[0]["预警值"].ToString());
            cd.value2 = int.Parse(dTable.Rows[0]["报警值"].ToString());
            return cd;
        }

        /// <summary>
        /// 获取所有测点信息
        /// </summary>
        /// <returns>测点信息列表的json字符串形式</returns>
        public string GetAll(int page, int rows)
        {
            int start = (page - 1) * rows;
            int end = page * rows;
            string sql = "SELECT * FROM `web`.`测点表`";
            List<CDInfo> cds = new List<CDInfo>();
            string where = "limit " + start + "," + end;
            Dictionary<string, object> d = new Dictionary<string, object>();
            //获取数据总数(注意是总数,不是一页中数据的条数)
            DataTable dTable = data.GetTable(sql);
            d.Add("total", data.GetTable(sql).Rows.Count);
            //获取page页的数据
            dTable = dTable.AsEnumerable().Skip((page - 1) * rows).Take(rows).CopyToDataTable();
            for (int i = 0; i < dTable.Rows.Count; i++)
            {
                CDInfo cd = new CDInfo();
                cd.Id = int.Parse(dTable.Rows[i]["测点编号"].ToString());
                cd.JZId = dTable.Rows[i]["机组编号"].ToString();
                cd.Name = dTable.Rows[i]["测点名称"].ToString();
                cd.Introduce = dTable.Rows[i]["测点描述"].ToString();
                cd.value1 = int.Parse(dTable.Rows[i]["预警值"].ToString());
                cd.value2 = int.Parse(dTable.Rows[i]["报警值"].ToString());
                cds.Add(cd);
            }
            d.Add("rows", cds);
            return JsonConvert.SerializeObject(d);
        }

        /// <summary>
        /// 删除测点信息
        /// </summary>
        /// <param name="cd">测点信息</param>
        /// <returns>删除是否成功</returns>
        public bool Delete(CDInfo cd)
        {
            string sql = "Delete from `web`.`测点表` WHERE `测点编号` = " + cd.Id + ";";
            sql += "alter table `" + cd.JZId + "` drop `" + cd.Name + "`;";
            return data.ExecSql(sql);
        }

        /// <summary>
        /// 更新测点信息
        /// </summary>
        /// <param name="cd">新测点信息</param>
        /// <param name="old">旧测点名称</param>
        /// <returns></returns>
        public bool Update(CDInfo cd, string old)
        {
            string sql = "Update `web`.`测点表` SET " +
                "`测点名称` = '" + cd.Name + "'," +
                "`预警值` = " + cd.value1 + "," +
                "`报警值` = " + cd.value2 + "," +
                "`测点描述` = '" + cd.Introduce + "' " + " WHERE `测点编号` = " + cd.Id + ";";
            if (!old.Equals(cd.Name))
            {
                sql += "alter table `" + cd.JZId + "` change `" + old + "`  `" + cd.Name + "` double default 0;";
            }
            return data.ExecSql(sql);
        }

        /// <summary>
        /// 根据机组id获取所有的测点信息【目录】
        /// </summary>
        /// <param name="cd"></param>
        public string GetAllCDInfo(string jzid)
        {
            string sql = "SELECT * FROM `web`.`测点表` " + "WHERE `机组编号` = " + jzid;
            Tree<string> root = new Tree<string>();
            root.Data = jzid;
            root.Open = true;
            //获取数据总数(注意是总数,不是一页中数据的条数)
            DataTable dTable = data.GetTable(sql);
            for (int i = 0; i < dTable.Rows.Count; i++)
            {
                Tree<string> node = new Tree<string>();
                node.Id = int.Parse(dTable.Rows[i]["测点编号"].ToString());
                node.Data = dTable.Rows[i]["测点名称"].ToString();
                root.AddNode(node);
            }
            return Tree<string>.ToJson(root);
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