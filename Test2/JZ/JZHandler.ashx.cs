using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataService;
using Common;
using System.Data;
using Model;
using Newtonsoft.Json;
using JrscSoft.Common;
using System.IO;

namespace Web.JZ
{
    /// <summary>
    /// JZHandler 的摘要说明
    /// </summary>
    public class JZHandler : IHttpHandler
    {
        DataAccess data = new DataAccess();
        private static string gcname = "";
        public string tableName = Web.JZ.jz1.JZBH;//机组编号，机组运行记录表名称
        public void ProcessRequest(HttpContext context)
        {
            ResultInfo result = new ResultInfo();
            JZInfo jz = new JZInfo();
           // context.Response.Write(context.Request["action"].ToString());
            string action = context.Request["action"].ToString();
            context.Response.ContentType = "text/plain";

            switch (action)
            {
                case "GetJZCDInfo":
                    context.Response.Write(GetJZCDInfo());
                    break;
                case "Q1":
                    context.Response.Write(GetCDVALUE(4));
                    break;
                case "Q2":
                    context.Response.Write(GetCDVALUE(5));
                    break;
                case "Q3":
                    context.Response.Write(GetCDVALUE(6));
                    break;
                case "Q4":
                    context.Response.Write(GetCDVALUE(7));
                    break;
                case "Q5":
                    context.Response.Write(GetCDVALUE(8));
                    break;
                case "Q6":
                    context.Response.Write(GetCDVALUE(9));
                    break;
                case "Q7":
                    context.Response.Write(GetCDVALUE(10));
                    break;
                case "Q8":
                    context.Response.Write(GetCDVALUE(11));
                    break;
                case "Qstatus":
                    context.Response.Write(Getstatus());
                    break;
                case "newTime":
                    context.Response.Write(GetNewTime());
                    break;
                case "newCdData":
                    string targetcdname = context.Request.Form["cdname"].ToString();
                    context.Response.Write(GetNewCdData(targetcdname));
                    break;
                case "GetAll":
                    context.Response.Write(GetAll());
                    break;
                case "GetJZState":
                    string jzid = context.Request.Form["JZId"];
                    context.Response.Write(GetJZState(jzid));
                    break;
                case "GetGC":
                    string jt = context.Request["jt"];
                    string gc = context.Request["gc"];
                    context.Response.Write(GetJZ(jt, gc));
                    break;
                case "Add":
                    try
                    {
                        jz.Id = context.Request.Form["Id"];
                        jz.Name = context.Request.Form["Name"];
                        jz.Introduce = context.Request.Form["Introduce"];
                        jz.JTName = context.Request.Form["JTName"];
                        jz.GCName = context.Request.Form["GCName"];
                        jz.Boss = context.Request.Form["Boss"];
                        jz.JW = context.Request.Form["JW"];
                        //保存数据到数据库中
                        result.Success = Add(jz);
                        result.Message = "插入机组信息" + ((result.Success == true) ? "成功" : "失败") + "!";
                    }
                    catch (Exception ex)
                    {
                        result.Success = false;
                        result.Message = "异常:" + ex.Message;
                    }
                    //返回客户端信息
                    context.Response.Write(JsonHelper<ResultInfo>.ObjectToJsonString(result));
                    break;
                case "GetAllJZId":
                    context.Response.Write(GetAllJZId());
                    break;
                case "Update":
                    try
                    {
                        jz = new JZInfo();
                        jz.Id = context.Request.Form["Id"];
                        jz.Name = context.Request.Form["Name"];
                        jz.Introduce = context.Request.Form["Introduce"];
                        jz.JTName = context.Request.Form["JTName"];
                        jz.GCName = context.Request.Form["GCName"];
                        jz.Boss = context.Request.Form["Boss"];
                        jz.JW = context.Request.Form["JW"];
                        result.Success = Update(jz);
                        result.Message = "更新机组[" + jz.Id + "]信息" + ((result.Success == true) ? "成功" : "失败") + "!";
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
                case "GetAllJZInfo":
                    int page = int.Parse(context.Request.Form["page"]);
                    int rows = int.Parse(context.Request.Form["rows"]);
                    context.Response.Write(GetAllJZInfo(page, rows));
                    break;
                case "StoreGCName":
                    gcname = context.Request.Form["GCName"].ToString();
                    if (gcname != null && gcname != "")
                    {
                        result.Success = true;
                        result.Message = "集团名称临时存储成功！";
                    }
                    context.Response.Write(JsonHelper<ResultInfo>.ObjectToJsonString(result));
                    break;
                case "GetAllGZJZInfo":
                    context.Response.Write(GetAllGZJZInfo(gcname));
                    break;
                case "ToExcel":
                    context.Response.ContentType = "text/plain";
                    DocInfo file = new DocInfo();
                    //  string tableName = context.Request.Form["JZId"].ToString();
                    //jz1 myjz = new jz1();//实例化 非静态类
                    //string tableName =myjz.JZBH;

                    string startTime = context.Request.Form["Start"].ToString();
                    string endTime = context.Request.Form["End"].ToString();
                    string cdname = context.Request.Form["cdname"].ToString();
                    if (cdname == "" || cdname=="全部测点")
                    {
                        cdname = "allCd";
                    }
                    string filepath = tableName + "-" + cdname + "-" + startTime + "-" + endTime + ".csv";
                    // string JZBH = Web.File.FileHandler.jzidstr;
                    string path = HttpContext.Current.Request.MapPath("~/");
                    string dir = "/Files/" + tableName + "/";//  /可能要去掉
                    string dirtocreate = path + Path.GetDirectoryName(dir);
                    if (!Directory.Exists(dirtocreate))
                    {
                        Directory.CreateDirectory(dirtocreate);
                    }

                    string fullDir = dir + filepath;//完整路径

                    try
                    {
                        // string sql = string.Format("SELECT ID,`测试日期`,`测试时间`,{3} FROM `{0}` where 测试日期 between '{1}' and '{2}' LIMIT 0,{4}", tableName, startTime, endTime, cdname,300);
                        string sql;
                        if (cdname == "allCd")
                        {
                            sql = string.Format("SELECT * FROM `{0}` where 测试日期 between '{1}' and '{2}'  and ID>0  ", tableName, startTime, endTime);
                        }
                        else
                        {
                            sql = string.Format("SELECT ID,`测试日期`,`测试时间`,{3} FROM `{0}` where 测试日期 between '{1}' and '{2}'   and ID>0  ", tableName, startTime, endTime, cdname);
                        }

                        DataTable dTable = data.GetTable(sql);
                        dTable.TableName = tableName;

                        SaveCSV(dTable, fullDir);
                        //file.JZId = tableName;
                        //file.Name = filepath;//文件名
                        //file.UploadTime = DateTime.Now.ToString();//文件上传时间
                        //file.Note = fullDir;//备注信息存储的是文件的物理地址
                        //file.Type = ".csv";

                        //string sqlinsert = "INSERT INTO `web`.`文档库`(`机组编号`, `文档名`, `上传时间`, `备注`, `类型`) VALUES ('"+file.JZId + "','" +file.Name + "','" +file.UploadTime + "', '" +  file.Note + "', '" + file.Type + "')";
                        if (dTable.Rows.Count > 0)
                            result.Success = true;
                        else
                            result.Success = false;
                        // 进行Excel转换操作，并返回转换的文件下载链接
                        // string urlPath = ExcelHelper.EntityListToExcel2003(dTable);
                     //   context.Response.ContentType = "text/plain";
                        context.Response.Write(fullDir); // 返回Json格式的内容
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    break;

                case "GetJZCDData":
                    //string cdname1 = "定辊低速级行星齿";
                    cdname = context.Request.Form["cdname"];
                    context.Response.Write(GetJZCDData(cdname));
                    break;
                case "GetJZCDfft":
                    cdname = context.Request.Form["cdname"];
                    context.Response.Write(GetJZCDfft(cdname));
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 获取所有的机组信息列表【目录】
        /// </summary>
        /// <returns></returns>
        public string GetAll()
        {
            SelectorHelper shelper = new SelectorHelper();

            string sql = "SELECT * FROM `web`.`机组表`";
            DataTable dTable = data.GetTable(sql);
            for (int i = 0; i < dTable.Rows.Count; i++)
            {
                string name = dTable.Rows[i]["机组名称"].ToString();
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
        /// 获取部分机组信息列表【目录】
        /// </summary>
        /// <returns></returns>
        public string GetJZ(string jt, string gc)
        {
            SelectorHelper shelper = new SelectorHelper();

            string sql = "SELECT * FROM `web`.`机组表` where 集团名称='" + jt + "' and 工厂名称='" + gc + "'";
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
        /// 获取所有的机组信息列表【目录】
        /// </summary>
        /// <returns></returns>
        public string GetAllJZId()
        {
            SelectorHelper shelper = new SelectorHelper();

            string sql = "SELECT * FROM `web`.`机组表`";
            DataTable dTable = data.GetTable(sql);
            for (int i = 0; i < dTable.Rows.Count; i++)
            {
                string name = dTable.Rows[i]["机组编号"].ToString();
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
        /// 获取机组状态
        /// </summary>
        /// <returns></returns>
        public int GetJZState(string jzid)
        {
            string sql = "SELECT 状态 FROM `web`.`机组表` where 机组编号='" + jzid + "'";
            return (int)data.GetValue(sql);
        }

        /// <summary>
        /// 获取所有的机组详细信息列表
        /// </summary>
        /// <returns></returns>
        public string GetAllJZInfo(int page, int rows)
        {
            int start = (page - 1) * rows;
            int end = page * rows;
            List<JZInfo> jzs = new List<JZInfo>();
            string sql = "SELECT * FROM `web`.`机组表`";
            string where = "limit " + start + "," + end;
            Dictionary<string, object> d = new Dictionary<string, object>();
            //获取数据总数(注意是总数,不是一页中数据的条数)
            DataTable dTable = data.GetTable(sql);
            d.Add("total", data.GetTable(sql).Rows.Count);
            //获取page页的数据
            dTable = dTable.AsEnumerable().Skip((page - 1) * rows).Take(rows).CopyToDataTable();
            for (int i = 0; i < dTable.Rows.Count; i++)
            {
                JZInfo jz = new JZInfo();
                jz.Id = dTable.Rows[i]["机组编号"].ToString();
                jz.Name = dTable.Rows[i]["机组名称"].ToString();
                jz.Introduce = dTable.Rows[i]["机组描述"].ToString();
                jz.JTName = dTable.Rows[i]["集团名称"].ToString();
                jz.GCName = dTable.Rows[i]["工厂名称"].ToString();
                jz.Boss = dTable.Rows[i]["负责人"].ToString();
                jz.JW = dTable.Rows[i]["机组经纬度"].ToString();
                jz.Status = int.Parse(dTable.Rows[i]["状态"].ToString());

                jzs.Add(jz);
            }
            d.Add("rows", jzs);

            return JsonConvert.SerializeObject(d);
        }

        /// <summary>
        /// 获取所有的报警机组详细信息列表
        /// </summary>
        /// <returns></returns>
        public string GetAllGZJZInfo(string gcname)
        {
            List<JZInfo> jzs = new List<JZInfo>();
            string sql = "SELECT * FROM `web`.`机组表` where 状态>1 and 工厂名称='" + gcname + "'";
            Dictionary<string, object> d = new Dictionary<string, object>();
            DataTable dTable = data.GetTable(sql);
            d.Add("total", data.GetTable(sql).Rows.Count);
            for (int i = 0; i < dTable.Rows.Count; i++)
            {
                JZInfo jz = new JZInfo();
                jz.Id = dTable.Rows[i]["机组编号"].ToString();
                jz.Name = dTable.Rows[i]["机组名称"].ToString();
                jz.Introduce = dTable.Rows[i]["机组描述"].ToString();
                jz.JTName = dTable.Rows[i]["集团名称"].ToString();
                jz.GCName = dTable.Rows[i]["工厂名称"].ToString();
                jz.Boss = dTable.Rows[i]["负责人"].ToString();
                jz.JW = dTable.Rows[i]["机组经纬度"].ToString();
                jz.Status = int.Parse(dTable.Rows[i]["状态"].ToString());

                jzs.Add(jz);
            }
            d.Add("rows", jzs);

            return JsonConvert.SerializeObject(d);
        }


        /// <summary>
        /// 保存机组的信息并创建测点表  JZHandler.ashx（2018.10.19创建）
        /// </summary>
        /// <param name="jz"></param>
        public bool Add(JZInfo jz)
        {
            string sql = "INSERT INTO `web`.`机组表`(`机组编号`, `机组名称`, `机组描述`, `集团名称`, `工厂名称`, `负责人`, `机组经纬度`) VALUES ('" +
                jz.Id + "', '" +
                jz.Name + "', '" +
                 jz.Introduce + "', '" +
                 jz.JTName + "', '" +
                 jz.GCName + "', '" +
                 jz.Boss + "', '" +
                 jz.JW + "');" +
                 "DROP TABLE IF EXISTS `" + jz.Id + "`;" +
                "CREATE TABLE `" + jz.Id + "` " +
                "(`ID` bigint(100) NOT NULL, " +
                "`测试日期` date DEFAULT NULL," +
                "`测试时间` time DEFAULT NULL," +
                "`备注` varchar(2) DEFAULT NULL," +
                " PRIMARY KEY (`ID`)) ENGINE=InnoDB DEFAULT CHARSET=utf8 partition by hash(ID MOD 500000)  partitions 300;" +
                 "DROP TABLE IF EXISTS `" + jz.Id + "F" + "`;" +
             "CREATE TABLE `" + jz.Id + "F" + "` " +
                "(`ID` bigint(20) DEFAULT NULL, `R_NAME` varchar(50) DEFAULT NULL,`T_POINT` int(11) DEFAULT NULL,`T_DATE` varchar(10) DEFAULT NULL,`T_TIME` varchar(10) DEFAULT NULL,`F_MAX` float DEFAULT NULL,`F_MIN` float DEFAULT NULL,`F_MEAN` float DEFAULT NULL,`F_RMS` float DEFAULT NULL,`F_BF` float DEFAULT NULL,`F_BX` float DEFAULT NULL,`F_CW` float DEFAULT NULL,`F_CQ` float DEFAULT NULL) ENGINE=InnoDB DEFAULT CHARSET=latin1;";
            return data.ExecSql(sql);
        }



        /// <summary>
        /// 删除机组信息
        /// </summary>
        /// <param name="id">机组编号</param>
        /// <returns>更新是否成功</returns>
        public bool Delete(string id)
        {
            string sql = "DELETE FROM `web`.`机组表` WHERE `机组编号` = '" + id + "'";
            return data.ExecSql(sql);
        }

        /// <summary>
        /// 更新机组信息
        /// </summary>
        /// <param name="jz">机组信息</param>
        /// <returns>更新是否成功</returns>
        public bool Update(JZInfo jz)
        {
            string sql = "Update `web`.`机组表` SET " +
                "`机组名称` = '" + jz.Name + "'," +
                "`机组描述` = '" + jz.Introduce + "'," +
                "`集团名称` = '" + jz.JTName + "'," +
                "`工厂名称` = '" + jz.GCName + "'," +
                "`负责人` = '" + jz.Boss + "'," +
                "`机组经纬度` = '" + jz.JW + "'" + " WHERE `机组编号` = '" + jz.Id + "'";
            return data.ExecSql(sql);
        }

        /// <summary>
        /// 查询指定日期区间的机组信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetTermJZInfo(string tablename, string start, string end)
        {
            //string sql = "SELECT * FROM `" + tablename + "` where 测试日期 between '" + start + "' and '" + end + "'"+ " LIMIT 0,100";
            string sql = string.Format("SELECT * FROM `{0}` where 测试日期 between '{1}' and '{2}'  LIMIT 0,{3}", tablename, start, end, 20000);
            //获取数据总数(注意是总数,不是一页中数据的条数)
            DataTable dTable = data.GetTable(sql);
            dTable.TableName = tablename;
            return dTable;
        }

        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="s_fileName">服务器上的地址</param>
        public void DownloadFile(HttpContext context, string s_fileName)
        {
            string path = s_fileName;
            System.IO.FileInfo file = new System.IO.FileInfo(System.Web.HttpContext.Current.Server.MapPath(path));
            context.Response.Clear();
            context.Response.Charset = "UTF-8";
            context.Response.ContentEncoding = System.Text.Encoding.UTF8;
            context.Response.AddHeader("Content-Type", "application/octet-stream");
            // 添加头信息，为"文件下载/另存为"对话框指定默认文件名,设定编码为UTF8,防止中文文件名出现乱码
            context.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode(file.Name, System.Text.Encoding.UTF8));
            // 添加头信息，指定文件大小，让浏览器能够显示下载进度
            context.Response.AddHeader("Content-Length", file.Length.ToString());
            //// 指定返回的是一个不能被客户端读取的流，必须被下载
            context.Response.ContentType = "application/ms-excel";
            // 把文件流发送到客户端
            context.Response.WriteFile(file.FullName);
            // 停止页面的执行
            context.Response.End();
        }
        /// <summary>
        /// 获取测点数据
        /// </summary>
        /// <param name="ceid">测点id</param>
        /// <returns>测点json格式数据</returns>
        public string GetCDVALUE(int ceid)
        {
            Dictionary<string, object> d = new Dictionary<string, object>();
            string fieldname = "";
            string fieldvalue = "";
            string yjz = "";
            string bjz = "";
            string JZBH = Web.File.FileHandler.jzidstr;
            string sql = "SELECT * FROM `web`.`" + JZBH + "` ORDER BY ID   limit 1 ";//升序
            DataTable dt = data.GetTable(sql);
            if (dt.Rows.Count > 0)
                if (dt.Columns.Count >= ceid)
                {
                    fieldname = dt.Columns[ceid].ColumnName.ToString();
                    fieldvalue = dt.Rows[0][ceid].ToString();
                }
            string sqlgetjingjiezhi = "select 预警值,报警值 from 测点表  where 机组编号='" + JZBH + "' ";
            DataTable dt2 = data.GetTable(sqlgetjingjiezhi);
            if (dt2.Rows.Count > 0)
            {
                yjz = dt2.Rows[ceid - 4][0].ToString();
                bjz = dt2.Rows[ceid - 4][1].ToString();
            }
            d.Add("fd", fieldname);
            d.Add("fv", fieldvalue);
            d.Add("yjz", yjz);
            d.Add("bjz", bjz);
            return JsonConvert.SerializeObject(d);

        }
        public string Getstatus()
        {
            Dictionary<string, object> d = new Dictionary<string, object>();

            string JZBH = Web.File.FileHandler.jzidstr;
            string sql2 = "SELECT 状态 FROM `web`.`机组表` where 机组编号=\"" + JZBH + "\" ";
            int status = Convert.ToInt16(data.GetValue(sql2).ToString());
            string sql3 = "SELECT CONCAT(测试日期,' ',测试时间) FROM `web`.`" + JZBH + "` order by ID  limit 1 ";//升序
            string newtime = data.GetValue(sql3).ToString();
            //if (DateTime.Now.AddMinutes(-5) > Convert.ToDateTime(newtime))
            //    status = 4;
            d.Add("status", status.ToString());
            d.Add("newtime", newtime);
            return JsonConvert.SerializeObject(d);

        }
        public string GetNewTime()
        {
           // Dictionary<string, object> dic = new Dictionary<string, object>();
            string JZBH = Web.JZ.jz1.JZBH;//jz.aspx.cs里定义。静态变量
            string sql = "SELECT CONCAT(测试日期,' ',测试时间) FROM `web`.`" + JZBH + "` order by ID  limit 1 ";//升序
            string newtime = data.GetValue(sql).ToString();
           // dic.Add("newtime", newtime);
            return JsonConvert.SerializeObject(newtime);
        }
        public string GetNewCdData(string cdname)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            string fieldname = "";
            string fieldvalue = "";
            string JZBH = Web.JZ.jz1.JZBH;
            string sql = "SELECT "+cdname+" FROM `web`.`" + JZBH + "`  order by ID  limit 1 ";//升序
            string cdvalue  = data.GetValue(sql).ToString();
            return JsonConvert.SerializeObject(cdvalue);

        }
        public string GetJZCDInfo()
        {

            Dictionary<string, object> d = new Dictionary<string, object>();
            string JZBH = Web.File.FileHandler.jzidstr;
            string sql = "SELECT * FROM `web`.`测点表` " + "WHERE `机组编号` = '" + JZBH + "'";
            List<CDNAME> CDS = new List<CDNAME>();
            //获取数据总数(注意是总数,不是一页中数据的条数)
            DataTable dTable = data.GetTable(sql);
            d.Add("cdnum", dTable.Rows.Count);
            for (int i = 0; i < dTable.Rows.Count; i++)
            {
                CDNAME CD = new CDNAME();
                CD.id = (i + 1).ToString();
                CD.text = dTable.Rows[i]["测点名称"].ToString();
                CDS.Add(CD);
            }
            d.Add("rows", CDS);
            return JsonConvert.SerializeObject(d);
        }


        /// <summary>
        /// 获取测点近5000条数据。
        /// </summary>
        /// <param name="CDNAME">测点名称</param>
        /// <returns></returns>
        public string GetJZCDData1(string CDNAME)
        {

            Dictionary<string, object> d = new Dictionary<string, object>();
            string JZBH = Web.File.FileHandler.jzidstr;
            string sql = "SELECT " + CDNAME + " FROM `web`.`" + JZBH + "` ORDER BY ID desc  limit 1,1500 ";
            List<CDdata> CDS = new List<CDdata>();
            //获取数据总数(注意是总数,不是一页中数据的条数)
            DataTable dTable = data.GetTable(sql);
            d.Add("datanum", dTable.Rows.Count);
            for (int i = 0; i < dTable.Rows.Count; i++)
            {

                CDdata CD = new CDdata();

                CD.vlaue = dTable.Rows[i][0].ToString();
                CDS.Add(CD);
            }
            d.Add("rows", CDS);
            return JsonConvert.SerializeObject(d);
        }
      public string GetJZCDData(string cdname)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (cdname==""||cdname=="选择测点")
            {
                dic.Add("valueData", "");
                return JsonConvert.SerializeObject(dic);
            }
            string JZBH = Web.JZ.jz1.JZBH;
            string sql = "SELECT " + cdname + " FROM `web`.`" + JZBH + "` ORDER BY ID desc  limit 1,5000 ";
            DataTable dt = data.GetTable(sql);
            int len = dt.Rows.Count;//数据条数
            string[] arrayStr = new string[len];
            for (int i = 0; i < len; i++)
            {
                arrayStr[i] = dt.Rows[i][cdname].ToString();
            }
            dic.Add("valueData", arrayStr);
            return JsonConvert.SerializeObject(dic);
        }
        /// <summary>
        /// 获取测点频率表
        /// </summary>
        /// <param name="CDNAME">测点名称</param>
        /// <returns>测点数据总数和频率表</returns>
        public string GetJZCDfft(string cdname)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (cdname == "" || cdname == "选择测点")
            {
                dic.Add("valueData", "");
                return JsonConvert.SerializeObject(dic);
            }
            string JZBH = Web.JZ.jz1.JZBH;
            string sql = "SELECT " + cdname + " FROM `web`.`" + JZBH + "` ORDER BY ID desc  limit 1,5000 ";
            List<CDfft> CDS = new List<CDfft>();
            //获取数据总数(注意是总数,不是一页中数据的条数)
            DataTable dTable = data.GetTable(sql);
            dic.Add("datanum", dTable.Rows.Count);
            double[] cddata = new double[dTable.Rows.Count];//测点值。
            double[] cdfftvalue = new double[dTable.Rows.Count];//测点傅里叶频率值
            int len = dTable.Rows.Count;
            for (int i = 0; i < dTable.Rows.Count; i++)
            {
                cddata[i] = Convert.ToDouble(dTable.Rows[i][0].ToString());
            }
            cdfftvalue = Common.FFTHelp.dft(cddata);
          //  string[] arrayStr = new string[len];
          //for (int i = 0; i < len; i++)
          //  {
          //      arrayStr[i] = dTable.Rows[i][0].ToString();
          //  }
            dic.Add("valueData", cdfftvalue);
            //for (int i = 0; i < dTable.Rows.Count; i++)
            //{

            //    CDfft CD = new CDfft();

            //    CD.fz = cdfftvalue[i].ToString();
            //    CD.pl = (i + 1).ToString();
            //    CDS.Add(CD);
            //}
          //  d.Add("rows", CDS);
            return JsonConvert.SerializeObject(dic);
        }


        /// <summary>
        /// 测点趋势图 2018.10.19创建
        /// </summary>
        /// <param name="CDNUM">测点编号</param>
        /// <returns></returns>
        public string GetJZCDQST(int CDNUM)
        {

            Dictionary<string, object> d = new Dictionary<string, object>();
            string JZBH = Web.File.FileHandler.jzidstr;
            string sql = "SELECT  T_DATE,T_TIME,F_RMS FROM `web`.`" + JZBH + "F" + "` WHERE T_POINT=" + CDNUM.ToString() + "  ORDER BY ID  ";
            List<CDfft> CDS = new List<CDfft>();
            DataTable dTable = data.GetTable(sql);
            d.Add("datanum", dTable.Rows.Count);
            for (int i = 0; i < dTable.Rows.Count; i++)
            {

                CDfft CD = new CDfft();
                CD.pl = dTable.Rows[i][0].ToString() + dTable.Rows[i][1].ToString();
                CD.fz = dTable.Rows[i][2].ToString();
                CDS.Add(CD);
            }
            d.Add("rows", CDS);
            return JsonConvert.SerializeObject(d);
        }


        /// <summary>
        /// 获取幅值表
        /// </summary>
        /// <param name="CDNAME">测定名称</param>
        /// <returns></returns>
        //public string GetJZCDfft(string CDNAME)
        //{

        //    Dictionary<string, object> d = new Dictionary<string, object>();
        //    string JZBH = Web.File.FileHandler.jzidstr;
        //    string sql = "SELECT " + CDNAME + " FROM `web`.`" + JZBH + "` ORDER BY ID desc  limit 1,5000 ";
        //    List<CDfft> CDS = new List<CDfft>();
        //    //获取数据总数(注意是总数,不是一页中数据的条数)
        //    DataTable dTable = data.GetTable(sql);
        //    d.Add("datanum", dTable.Rows.Count);
        //    for (int i = 0; i < dTable.Rows.Count; i++)
        //    {
        //        CDfft CD = new CDfft();

        //        CD.fz = dTable.Rows[i][0].ToString();
        //        CD.pl = (i + 1).ToString();
        //        CDS.Add(CD);
        //    }
        //    d.Add("rows", CDS);
        //    return JsonConvert.SerializeObject(d);
        //}

        public void SaveCSV(DataTable dt, string fullPath)//table数据写入csv
        {
            fullPath = System.Web.HttpContext.Current.Server.MapPath("~/" + fullPath);//获取web app的绝对地址。
            System.IO.FileInfo fi = new System.IO.FileInfo(fullPath);
            if (!fi.Directory.Exists)
            {
                fi.Directory.Create();//创建文件。
            }
            System.IO.FileStream fs = new System.IO.FileStream(fullPath, System.IO.FileMode.Create,
                System.IO.FileAccess.Write);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(fs, System.Text.Encoding.UTF8);
            string data = "";

            for (int i = 0; i < dt.Columns.Count; i++)//写入列名
            {
                data += dt.Columns[i].ColumnName.ToString();
                if (i < dt.Columns.Count - 1)
                {
                    data += ",";
                }
            }
            sw.WriteLine(data);

            for (int i = 0; i < dt.Rows.Count; i++) //写入各行数据
            {
                data = "";
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string str = dt.Rows[i][j].ToString();
                    str = str.Replace("\"", "\"\"");//替换英文冒号 英文冒号需要换成两个冒号
                    if (str.Contains(',') || str.Contains('"')
                        || str.Contains('\r') || str.Contains('\n')) //含逗号 冒号 换行符的需要放到引号中
                    {
                        str = string.Format("\"{0}\"", str);
                    }

                    data += str;
                    if (j < dt.Columns.Count - 1)
                    {
                        data += ",";
                    }
                }
                sw.WriteLine(data);
            }
            sw.Close();
            fs.Close();
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