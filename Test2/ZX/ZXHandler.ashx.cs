using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataService;
using Model;
using Common;
using System.Data;
using Newtonsoft.Json;

namespace Web.ZX
{
    /// <summary>
    /// ZXHandler 的摘要说明
    /// </summary>
    public class ZXHandler : IHttpHandler
    {
        DataAccess data = new DataAccess();
        public void ProcessRequest(HttpContext context)
        {
            ZXInfo zx = new ZXInfo();
            ResultInfo result = new ResultInfo();
            string action = context.Request["action"].ToString();
            context.Response.ContentType = "text/plain";

            switch (action)
            {
                case "Add":
                    //用户添加咨询信息
                    try
                    {
                        zx.JZId = context.Request.Form["JZId"];
                        zx.Name = context.Request.Form["Name"];
                        zx.StartTime = DateTime.Now.ToString();
                        //一个机组只能由一个专家进行诊断，多次提交，以第一个接收到提交内容的专家负责回复咨询内容
                        if (ExistsZXInfo(zx.JZId))
                        {
                            if (GetJZZXStatus(zx.JZId) == 0)
                            {
                                result.Success = false;
                                result.Message = "您的咨询已提交，请耐心等待专家回复!";
                            }
                            else if (GetJZZXStatus(zx.JZId) == 1)
                            {
                                result.Success = false;
                                result.Message = "您的咨询正在处理，请耐心等待!";
                            }
                        }
                        else
                        {
                            result.Success = Add(zx);
                            result.Message = "咨询信息添加" + ((result.Success == true) ? "成功" : "失败") + "!";
                        }
                    }
                    catch (Exception ex)
                    {
                        result.Success = false;
                        result.Message = "异常:" + ex.Message;
                    }
                    string jss = JsonHelper<ResultInfo>.ObjectToJsonString(result);
                    context.Response.Write(jss);
                    context.Response.End();
                    break;
                case "GetAll":
                    //获取所有的咨询记录包括已完成和未完成的咨询
                    int page = int.Parse(context.Request.Form["page"]);
                    int rows = int.Parse(context.Request.Form["rows"]);
                    context.Response.Write(GetAll(page, rows));
                    break;
                case "StartZD":
                    string jzid = context.Request.Form["JZId"].ToString();//注意：每个机组同时只能接收一个专家的诊断，同时不能够更改，每个机组在咨询表里只有一条记录，只在第一次咨询时添加。
                    result.Success = StartZD(jzid);
                    result.Message = "当前咨询开始诊断！";
                    jss = JsonHelper<ResultInfo>.ObjectToJsonString(result);
                    context.Response.Write(jss);//"{\"Success\":\"false\",\"Message\":\"更新用户信息失败!\"}"
                    break;
                case "EndZD":
                    string jzid2 = context.Request.Form["JZId"].ToString();//注意：每个机组同时只能接收一个专家的诊断，同时不能够更改，每个机组在咨询表里只有一条记录，只在第一次咨询时添加。
                    result.Success = EndZD(jzid2);
                    result.Message = "专家您好，当前咨询已结束！";
                    jss = JsonHelper<ResultInfo>.ObjectToJsonString(result);
                    context.Response.Write(jss);//"{\"Success\":\"false\",\"Message\":\"更新用户信息失败!\"}"
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// 保存用户的信息
        /// </summary>
        /// <param name="zx"></param>
        public bool Add(ZXInfo zx)
        {
            string sql = "INSERT INTO `web`.`咨询专家表`(`专家名`, `机组编号`,`提交时间`) VALUES ('" +
                zx.Name + "', '" +
                zx.JZId + "', '" +
                 zx.StartTime + "')";
            return data.ExecSql(sql);
        }

        /// <summary>
        /// 查看该机组是否已经添加过咨询(不包含已经完成的咨询，即状态为2的咨询)
        /// </summary>
        /// <param name="zx"></param>
        public bool ExistsZXInfo(string jzid)
        {
            string sql = "SELECT count(*) FROM `web`.`咨询专家表` where 机组编号='" + jzid + "' and 状态<2";
            return (int.Parse(data.GetValue(sql).ToString()) > 0);
        }


        /// <summary>
        /// 查看该机组咨询的进展状态
        /// </summary>
        /// <param name="zx"></param>
        public int GetJZZXStatus(string jzid)
        {
            string sql = "SELECT 状态 FROM `web`.`咨询专家表` where 机组编号='" + jzid + "'";
            return int.Parse(data.GetValue(sql).ToString());
        }

        /// <summary>
        /// 获取所有咨询信息
        /// </summary>
        /// <returns>用户信息列表的json字符串形式</returns>
        public string GetAll(int page, int rows)
        {
            int start = (page - 1) * rows;
            int end = page * rows;
            List<ZXInfo> zxs = new List<ZXInfo>();
            string sql = "SELECT * FROM `web`.`咨询专家表`";
            string where = "limit " + start + "," + end;
            Dictionary<string, object> d = new Dictionary<string, object>();
            //获取数据总数(注意是总数,不是一页中数据的条数)
            DataTable dTable = data.GetTable(sql);
            d.Add("total", data.GetTable(sql).Rows.Count);
            //获取page页的数据
            dTable = dTable.AsEnumerable().Skip((page - 1) * rows).Take(rows).CopyToDataTable();
            for (int i = 0; i < dTable.Rows.Count; i++)
            {
                ZXInfo zx = new ZXInfo();
                zx.Id = int.Parse(dTable.Rows[i]["咨询编号"].ToString());
                zx.Name = dTable.Rows[i]["专家名"].ToString();
                zx.JZId = dTable.Rows[i]["机组编号"].ToString();
                zx.StartTime = dTable.Rows[i]["提交时间"].ToString();
                zx.EndTime = dTable.Rows[i]["完成时间"].ToString();
                zx.Status = int.Parse(dTable.Rows[i]["状态"].ToString());

                zxs.Add(zx);
            }
            d.Add("rows", zxs);

            return JsonConvert.SerializeObject(d);
        }

        /// <summary>
        /// 专家开始诊断
        /// </summary>
        /// <returns>更新是否成功</returns>
        public bool StartZD(string id)
        {
            string sql = "Update `web`.`咨询专家表` SET `状态` = 1 WHERE `机组编号` = '" + id + "'";
            return data.ExecSql(sql);
        }

        /// <summary>
        /// 专家完成诊断
        /// </summary>
        /// <returns>更新是否成功</returns>
        public bool EndZD(string id)
        {
            string sql = "Update `web`.`咨询专家表` SET `状态` = 2,完成时间='" + DateTime.Now.ToString() + "' WHERE `机组编号` = '" + id + "'";
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