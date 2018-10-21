using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model;
using Common;
using DataService;
using System.Data;
using System.IO;
using Newtonsoft.Json;

namespace Web.jzjl
{
    /// <summary>
    /// FileHandler 的摘要说明
    /// </summary>
    public class jzjl : IHttpHandler
    {
        DataAccess data = new DataAccess();
        private static string jzidstr = "";

        public void ProcessRequest(HttpContext context)
        {
            string action = context.Request["action"].ToString();
            context.Response.ContentType = "text/plain";
            ResultInfo result = new ResultInfo();
            DocInfo file = new DocInfo();

            switch (action)
            {
                case "Add":
                    try
                    {
                        HttpPostedFile f = context.Request.Files["File"];
                        if (f == null)
                        {
                            result.Success = false;
                            result.Message = "请选择文件!";
                        }
                        else
                        {
                            //获取网站根目录的物理路径
                            string path = HttpContext.Current.Request.MapPath("~/");
                            //获取文件名
                            string name = Path.GetFileName(f.FileName);
                            //获取文件的扩展名
                            string ext = Path.GetExtension(name);
                            //支持的文件上传类型列表
                            List<string> ExtList = new List<string>(new string[] { ".doc", ".docx", ".xls" });
                            string dir = "";
                            //if (ExtList.Contains(ext))
                            if (true)
                            {
                                dir = "/Files/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/";
                                //新建文件夹
                                string dirtocreate = path + Path.GetDirectoryName(dir);
                                if (!Directory.Exists(dirtocreate))
                                {
                                    Directory.CreateDirectory(dirtocreate);
                                }
                                string newfileName = Guid.NewGuid().ToString();//新的文件名
                                string fullDir = dir + newfileName + ext;//完整路径
                                f.SaveAs(context.Request.MapPath(fullDir));//保存到服务器上
                                //将文件信息保存到数据库中
                                file.JZId = context.Request.Form["JZId"];
                                file.Name = name;//文件名
                                file.UploadTime = DateTime.Now.ToString();//文件上传时间
                                file.Note = dir + newfileName + ext;//备注信息存储的是文件的物理地址
                                file.Type = context.Request.Form["Type"];
                                result.Success = Add(file);
                                result.Message = "添加文档信息" + ((result.Success == true) ? "成功" : "失败") + "!";
                            }
                            else
                            {
                                result.Success = false;
                                result.Message = "不支持的文件类型!";
                            }
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
                    int page = int.Parse(context.Request.Form["page"]);
                    int rows = int.Parse(context.Request.Form["rows"]);
                    context.Response.Write(GetAll(page, rows));
                    break;
                case "GetFile":
                    string filename = context.Request.Form["Name"].ToString();
                    context.Response.Write(GetFile(filename));
                    break;
                case "GetJZRelatedFile":
                    context.Response.Write(GetJZRelatedFile(jzidstr));
                    break;
                case "Delete":
                    try
                    {
                        int count = 0;
                        string id = context.Request["Id"];
                        string[] ids = id.Split(',');
                        for (int i = 0; i < ids.Length; i++)
                        {
                            string filepath = context.Request.MapPath(GetDocInfoById(int.Parse(ids[i])).Note);
                            result.Success = Delete(int.Parse(ids[i]));
                            if (result.Success == false)
                            {
                                break;
                            }
                            else
                            {
                                //判断文件是否存在,若存在,则删除
                                if (System.IO.File.Exists(filepath))
                                {
                                    System.IO.File.Delete(filepath);
                                }
                                count++;
                            }
                        }
                        result.Message = ((result.Success == true) ? "操作成功:" : "操作失败:") + "共删除" + count + "条信息!";
                    }
                    catch (Exception ex)
                    {
                        result.Success = false;
                        result.Message = "异常:" + ex.Message;
                    }
                    jss = JsonHelper<ResultInfo>.ObjectToJsonString(result);
                    context.Response.Write(jss);
                    context.Response.End();
                    break;
                case "StoreJZId":
                    jzidstr = context.Request.Form["JZId"].ToString();
                    if (jzidstr != null && jzidstr != "")
                    {
                        result.Success = true;
                        result.Message = "机组编号临时存储成功！";
                    }
                    context.Response.Write(JsonHelper<ResultInfo>.ObjectToJsonString(result));
                    break;
                case "StoreJZId2":
                    jzidstr = context.Request.Form["GCName"].ToString();

                    if (jzidstr != null && jzidstr != "")
                    {     
                        result.Success = true;
                        result.Message = MenuHelper.GetIdByMenuName(jzidstr); ;
                    }
                  //  context.Response.Write(JsonHelper<ResultInfo>.ObjectToJsonString(result));
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 保存文档的信息
        /// </summary>
        /// <param name="file"></param>
        public bool Add(DocInfo file)
        {
            string sql = "INSERT INTO `web`.`文档库`(`机组编号`, `文档名`, `上传时间`, `备注`, `类型`) VALUES ('" +
                file.JZId + "', '" +
                 file.Name + "', '" +
                 file.UploadTime + "', '" +
                 file.Note + "', '" +
                 file.Type + "')";
            return data.ExecSql(sql);
        }

        /// <summary>
        /// 获取所有文档信息
        /// </summary>
        /// <returns>文档信息列表的json字符串形式</returns>
        public string GetAll(int page, int rows)
        {
            int start = (page - 1) * rows;
            int end = page * rows;
            List<DocInfo> files = new List<DocInfo>();
            string sql = "SELECT * FROM `web`.`文档库`";
            string where = "limit " + start + "," + end;
            Dictionary<string, object> d = new Dictionary<string, object>();
            //获取数据总数(注意是总数,不是一页中数据的条数)
            DataTable dTable = data.GetTable(sql);
            d.Add("total", data.GetTable(sql).Rows.Count);
            //获取page页的数据
            dTable = dTable.AsEnumerable().Skip((page - 1) * rows).Take(rows).CopyToDataTable();
            for (int i = 0; i < dTable.Rows.Count; i++)
            {
                DocInfo file = new DocInfo();
                file.Id = int.Parse(dTable.Rows[i]["文档编号"].ToString());
                file.JZId = dTable.Rows[i]["机组编号"].ToString();
                file.UploadTime = dTable.Rows[i]["上传时间"].ToString();
                file.Note = dTable.Rows[i]["备注"].ToString();
                file.Name = dTable.Rows[i]["文档名"].ToString();
                file.Type = dTable.Rows[i]["类型"].ToString();
                files.Add(file);
            }
            d.Add("rows", files);
            return JsonConvert.SerializeObject(d);
        }

        /// <summary>
        /// 获取机组所有相关文档信息
        /// </summary>
        /// <returns>文档信息列表的json字符串形式</returns>
        public string GetJZRelatedFile(string jzid)
        {
            List<DocInfo> files = new List<DocInfo>();
            string sql = "SELECT * FROM `web`.`文档库` where 机组编号='" + jzid + "'";
            Dictionary<string, object> d = new Dictionary<string, object>();
            //获取数据总数(注意是总数,不是一页中数据的条数)
            DataTable dTable = data.GetTable(sql);
            d.Add("total", data.GetTable(sql).Rows.Count);
            for (int i = 0; i < dTable.Rows.Count; i++)
            {
                DocInfo file = new DocInfo();
                file.Id = int.Parse(dTable.Rows[i]["文档编号"].ToString());
                file.JZId = dTable.Rows[i]["机组编号"].ToString();
                file.UploadTime = dTable.Rows[i]["上传时间"].ToString();
                file.Note = dTable.Rows[i]["备注"].ToString();
                file.Name = dTable.Rows[i]["文档名"].ToString();
                file.Type = dTable.Rows[i]["类型"].ToString();
                files.Add(file);
            }
            d.Add("rows", files);
            return JsonConvert.SerializeObject(d);
        }
        
        /// <summary>
        /// 获取所有文档信息
        /// </summary>
        /// <returns>文档信息列表的json字符串形式</returns>
        public string GetFile(string filename)
        {
            List<DocInfo> files = new List<DocInfo>();
            string sql = "SELECT * FROM `web`.`文档库` where 文档名 like '%" + filename + "%'";
            Dictionary<string, object> d = new Dictionary<string, object>();
            //获取数据总数(注意是总数,不是一页中数据的条数)
            DataTable dTable = data.GetTable(sql);
            d.Add("total", data.GetTable(sql).Rows.Count);
            for (int i = 0; i < dTable.Rows.Count; i++)
            {
                DocInfo file = new DocInfo();
                file.Id = int.Parse(dTable.Rows[i]["文档编号"].ToString());
                file.JZId = dTable.Rows[i]["机组编号"].ToString();
                file.UploadTime = dTable.Rows[i]["上传时间"].ToString();
                file.Note = dTable.Rows[i]["备注"].ToString();
                file.Name = dTable.Rows[i]["文档名"].ToString();
                file.Type = dTable.Rows[i]["类型"].ToString();
                files.Add(file);
            }
            d.Add("rows", files);
            return JsonConvert.SerializeObject(d);
        }

        /// <summary>
        /// 根据Id获取文档信息
        /// </summary>
        /// <returns>文档信息</returns>
        public DocInfo GetDocInfoById(int id)
        {
            string sql = "SELECT * FROM `web`.`文档库` where `文档编号`=" + id;
            DataTable dTable = data.GetTable(sql);
            DocInfo file = new DocInfo();
            file.Id = int.Parse(dTable.Rows[0]["文档编号"].ToString());
            file.JZId = dTable.Rows[0]["机组编号"].ToString();
            file.UploadTime = dTable.Rows[0]["上传时间"].ToString();
            file.Note = dTable.Rows[0]["备注"].ToString();
            file.Name = dTable.Rows[0]["文档名"].ToString();
            file.Type = dTable.Rows[0]["类型"].ToString();
            return file;
        }

        /// <summary>
        /// 删除文档信息
        /// </summary>
        /// <param name="id">文档信息</param>
        /// <returns>更新是否成功</returns>
        public bool Delete(int id)
        {
            string sql = "Delete from `web`.`文档库` WHERE `文档编号` = " + id;
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