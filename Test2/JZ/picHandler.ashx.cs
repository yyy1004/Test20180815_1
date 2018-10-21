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
using System.Web.UI;
using System.Text;
using System.Web.Script.Serialization;
using System.IO;

namespace Web.picHandler
{
    /// <summary>
    /// JZHandler 的摘要说明
    /// </summary>
    public class picHandler : IHttpHandler
    {
        DataAccess data = new DataAccess();
        public static string gcname = "";//当点击菜单时 获取菜单名称。在gc.aspx.cs中给其赋值。

       
        public void ProcessRequest(HttpContext context)
        {
            ResultInfo result = new ResultInfo();
            jzjlInfo jz = new jzjlInfo();
            string action = context.Request["action"].ToString();
            // context.Response.ContentType = "text/plain";
            context.Response.ContentType = "charset=UTF-8";
            int page;
            int rows;
            int menuid;
            string gcname;
            string t1;
            string t2;
            JavaScriptSerializer jss = new JavaScriptSerializer();
            menuid = int.Parse(CheckLoginStatus.node);//菜单ID
            gcname = MenuHelper.GetMenuNameById(menuid);//菜单名称 工厂名称
            StringBuilder sb = new StringBuilder();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            DocInfo file = new DocInfo();
            switch (action)
            {

                case "picexist":
                  //  string picname = gcname + "." +"png";
                    if (System.IO.File.Exists(HttpContext.Current.Request.MapPath("~/pic/"+gcname + "." + "png") )|| System.IO.File.Exists(HttpContext.Current.Request.MapPath("~/" + gcname + "." + "jpg")))
                    {
                        result.Success = true;
                        dic.Add("success", result.Success);
                    }
                    string rtjss=jss.Serialize(dic);
                    context.Response.Write(rtjss);
                    break;


                case "SavePic":
                    string url = "~/pic/" + gcname + ".png";
                    //如果有图片就直接展示；
                    if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath("~/pic/") + gcname + ".png"))
                    {
                        sb.Append(string.Format(@"  <img alt="""" src=""/pic/{0}.png"" width=""500px"" height=""400px"" />", gcname));
                        result.Success = true;
                        dic.Add("pichtml", sb.ToString());
                        dic.Add("success", result.Success);

                    }
                    #region MyRegion
                    //this.Imagegc.ImageUrl = url;
                    //    this.FileUpload1.Visible = false;
                    // this.JTPICB.Visible = true;
                    //MessageBox.Show("文件存在");
                    //this.JTPICB.ImageUrl = url;

                    //this.Button1.Visible = false;

                    //   this.Image2.ImageUrl = url;
                    //}

                    //如果不存在，显示上传按钮。 
                    #endregion
                    else
                    {
                        sb.Append(string.Format(@""));

                        result.Success = false;
                        dic.Add("pichtml", sb.ToString());
                        dic.Add("success", result.Success);
                    }
                    //  jss.Serialize(dic);
                    context.Response.Write(jss.Serialize(dic));
                    //context.Response.End();

                    break;


                case "UploadPic":
                    HttpPostedFile f = context.Request.Files["File"];
                  //  string f = context.Request.Form["File"];
                    //string city = Request.QueryString["city"];

                    //string filename = System.IO.Path.GetFileName(f);//文件名: “pic.png”
                    //string extension = System.IO.Path.GetExtension(f);//扩展名：”.png“
                    //string filenameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(f);//文件名（无扩展名）。
                    //string saveFileName = gcname + "." + extension ; //服务器上的文件

                    if (f==null)
                    {
                        result.Success = false;
                        result.Message = "读取文件不成功！请选择文件!";
                    }
                    else
                    {
                        //获取网站根目录的物理路径
                        string path = HttpContext.Current.Request.MapPath("~/");
                        //获取文件名
                        string name = Path.GetFileName(f.FileName);
                        //获取文件的扩展名
                        string ext = Path.GetExtension(name);
                        //服务器上的文件
                        string presentFile = gcname + "." + ext;

                        if (true) //文件存在
                        {
                            //string fileFullname = this.FileUpload1.FileName;

                            //string fileName = fileFullname.Substring(fileFullname.LastIndexOf("\\"), 1);
                            //string type = System.IO.Path.GetExtension(this.FileUpload1.PostedFile.FileName);
                            // fileFullname.Substring(fileFullname.LastIndexOf("."),1);
                            if (ext.ToLower()  == ".png" || ext.ToLower()== ".jgp"|| ext.ToLower()==".gif")
                            {
                           //     string savePath = HttpContext.Current.Request.MapPath("~/pic/");
                                string savePath = "~/pic/";
                                string saveName = gcname + ".png";
                                string pathToCheck = savePath + saveName;


                                if (System.IO.File.Exists(pathToCheck))
                                {
                                    
                                    System.IO.File.Delete(pathToCheck);
                                }
                                f.SaveAs(context.Request.MapPath(pathToCheck));//保存到服务器上
                              
                                //    this.FileUpload1.SaveAs(pathToCheck);//如果存在同名文件按，则先删除，再存储。
                                //string url = "~/pic/" + GCname + ".png";
                                //this.Imagegc.ImageUrl = url;
                                //this.FileUpload1.Visible = false;
                                //this.Button1.Visible = false;
                                //Response.Write("<script>window.reload();</script>");
                                //Response.Redirect("gc.aspx?node=" + node + "");
                                //Response.Write("<script language=javascript>window.parent.frameLeft.location.href='main.html'</script>");
                                //url = node.attributes.url + "?node=" + node.id;
                                //Response.Write("<script language=javascript>document.getElementById('myiframe').src='/GC/gc.aspx?node=" + node + "'</script>");
                                result.Success = true;
                                result.Message = "图片保存成功！！";

                            }
                            else
                            {
                                result.Success = false;
                                result.Message = "不支持的文件类型，请上传jpg/png/gif格式的图片。";
                            }
                        }
                    }
                 string jss1 = JsonHelper<ResultInfo>.ObjectToJsonString(result);
                  context.Response.Write(jss1);
                  context.Response.End();
                    break;

                #region MyRegion
                //try
                //{
                //    HttpPostedFile f = context.Request.Files["File"];
                //    if (f == null)
                //    {
                //        result.Success = false;
                //        result.Message = "请选择文件!";
                //    }
                //    else
                //    {
                //        //获取网站根目录的物理路径
                //        string path = HttpContext.Current.Request.MapPath("~/");
                //        //获取文件名
                //        string name = Path.GetFileName(f.FileName);
                //        //获取文件的扩展名
                //        string ext = Path.GetExtension(name);
                //        //支持的文件上传类型列表
                //        List<string> ExtList = new List<string>(new string[] { ".doc", ".docx", ".xls" });
                //        string dir = "";
                //        //if (ExtList.Contains(ext))
                //        if (true)
                //        {
                //            dir = "/Files/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/";
                //            //新建文件夹
                //            string dirtocreate = path + Path.GetDirectoryName(dir);
                //            if (!Directory.Exists(dirtocreate))
                //            {
                //                Directory.CreateDirectory(dirtocreate);
                //            }
                //            string newfileName = Guid.NewGuid().ToString();//新的文件名
                //            string fullDir = dir + newfileName + ext;//完整路径
                //            f.SaveAs(context.Request.MapPath(fullDir));//保存到服务器上
                //            //将文件信息保存到数据库中
                //            file.JZId = context.Request.Form["JZId"];
                //            file.Name = name;//文件名
                //            file.UploadTime = DateTime.Now.ToString();//文件上传时间
                //            file.Note = dir + newfileName + ext;//备注信息存储的是文件的物理地址
                //            file.Type = context.Request.Form["Type"];
                //            //result.Success = UploadPic(file);
                //            result.Message = "添加文档信息" + ((result.Success == true) ? "成功" : "失败") + "!";
                //        }
                //        else
                //        {
                //            result.Success = false;
                //            result.Message = "不支持的文件类型!";
                //        }
                //    }
                //}
                //catch (Exception ex)
                //{
                //    result.Success = false;
                //    result.Message = "异常:" + ex.Message;
                //}
                //string jss1 = JsonHelper<ResultInfo>.ObjectToJsonString(result);
                //context.Response.Write(jss1);
                //context.Response.End();
                //break; 
                #endregion


                #region MyRegion
                // HttpPostedFile f = context.Request.Files["File"];
                //if (f == null)
                //{
                //    result.Success = false;
                //    result.Message = "请选择文件!";
                //}
                //else
                //{
                //    //获取网站根目录的物理路径
                //    string path = HttpContext.Current.Request.MapPath("~/");
                //    //获取文件名
                //    string name = Path.GetFileName(f.FileName);
                //    //获取文件的扩展名
                //    string ext = Path.GetExtension(name);
                //    //支持的文件上传类型列表
                //    List<string> ExtList = new List<string>(new string[] { ".doc", ".docx", ".xls" });
                //    string dir = "";
                //    //if (ExtList.Contains(ext))
                //    if (true)
                //    {
                //        dir = "/Files/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/";
                //        //新建文件夹
                //        string dirtocreate = path + Path.GetDirectoryName(dir);
                //        if (!Directory.Exists(dirtocreate))
                //        {
                //            Directory.CreateDirectory(dirtocreate);
                //        }
                //        string newfileName = Guid.NewGuid().ToString();//新的文件名
                //        string fullDir = dir + newfileName + ext;//完整路径
                //        f.SaveAs(context.Request.MapPath(fullDir));//保存到服务器上
                //        //将文件信息保存到数据库中
                //        file.JZId = context.Request.Form["JZId"];
                //        file.Name = name;//文件名
                //        file.UploadTime = DateTime.Now.ToString();//文件上传时间
                //        file.Note = dir + newfileName + ext;//备注信息存储的是文件的物理地址
                //        file.Type = context.Request.Form["Type"];
                //        result.Success = Add(file);
                //        result.Message = "添加文档信息" + ((result.Success == true) ? "成功" : "失败") + "!";
                //    }
                //    else
                //    {
                //        result.Success = false;
                //        result.Message = "不支持的文件类型!";
                //    }
                //}

                //string jss = JsonHelper<ResultInfo>.ObjectToJsonString(result);
                //context.Response.Write(jss);
                //context.Response.End();
                //break; 
                #endregion

                case "GetAll":
                    // page = int.Parse(context.Request.Form["page"]);
                    // rows = int.Parse(context.Request.Form["rows"]);
                    //
                    page = int.Parse(context.Request.Form["page"]);
                    rows = int.Parse(context.Request.Form["rows"]);
                    menuid = int.Parse(CheckLoginStatus.node);
                    gcname = MenuHelper.GetMenuNameById(menuid);

                    context.Response.Write(GetAll(page, rows));
                    break;
                case "GetAlljz":
                    page = int.Parse(context.Request.Form["page"]);
                    rows = int.Parse(context.Request.Form["rows"]);
                    menuid = int.Parse(CheckLoginStatus.node);
                    gcname = MenuHelper.GetMenuNameById(menuid);

                    context.Response.Write(GetAllJZInfo(gcname, page, rows));
                    break;
                case "GetAllGCJZ":
                    page = int.Parse(context.Request.Form["page"]);
                    rows = int.Parse(context.Request.Form["rows"]);
                    menuid = int.Parse(CheckLoginStatus.node);
                    gcname = MenuHelper.GetMenuNameById(menuid);
                    // string gcname = System.Web.HttpContext.Current.Request.QueryString["node"].ToString();
                    context.Response.Write(GetAllGCJZInfo(gcname, page, rows));
                    break;

                case "GetAllGCJZt":
                    page = int.Parse(context.Request.Form["page"]);
                    rows = int.Parse(context.Request.Form["rows"]);
                    t1 = context.Request.Form["Start"];
                    t2 = context.Request.Form["End"];
                    // menuid = int.Parse(CheckLoginStatus.node);
                    //  gcname = MenuHelper.GetMenuNameById(menuid);
                    gcname = context.Request.Form["gcname"]; ;
                    context.Response.Write(GetAllGCJZInftime(gcname, page, rows, t1, t2));

                    break;
                case "GetAllJZt":
                    page = int.Parse(context.Request.Form["page"]);
                    rows = int.Parse(context.Request.Form["rows"]);
                    t1 = context.Request.Form["Start"];
                    t2 = context.Request.Form["End"];
                    // menuid = int.Parse(CheckLoginStatus.node);
                    //  gcname = MenuHelper.GetMenuNameById(menuid);
                    gcname = context.Request.Form["gcname"]; ;
                    context.Response.Write(GetAllJZInftime(gcname, page, rows, t1, t2));

                    break;
                default:
                    break;
            }

        }






        /// <summary>
        /// 获取所有的机组详细信息列表
        /// </summary>
        /// <returns></returns>
        public string GetAll(int page, int rows)
        {
            int start = (page - 1) * rows;
            int end = page * rows;
            List<jzjlInfo> jzs = new List<jzjlInfo>();
            string sql = "SELECT * FROM `web`.`工厂机组运行记录` where `工厂名称`='" + gcname + "'order by ID desc ";
            string where = "limit " + start + "," + end;
            Dictionary<string, object> d = new Dictionary<string, object>();
            //获取数据总数(注意是总数,不是一页中数据的条数)
            DataTable dTable = data.GetTable(sql);
            d.Add("total", data.GetTable(sql).Rows.Count);
            //获取page页的数据
            dTable = dTable.AsEnumerable().Skip((page - 1) * rows).Take(rows).CopyToDataTable();
            for (int i = 0; i < dTable.Rows.Count; i++)
            {
                jzjlInfo jz = new jzjlInfo();
                jz.id = long.Parse(dTable.Rows[i]["ID"].ToString());
                jz.jzname = dTable.Rows[i]["机组名称"].ToString();
                jz.jzid = dTable.Rows[i]["机组编号"].ToString();
                jz.stime = dTable.Rows[i]["时间"].ToString();
                jz.ms = dTable.Rows[i]["状态描述"].ToString();
                jz.status = int.Parse(dTable.Rows[i]["状态"].ToString());
                jz.mid = int.Parse(dTable.Rows[i]["菜单号"].ToString());
                jzs.Add(jz);
            }
            d.Add("rows", jzs);

            return JsonConvert.SerializeObject(d);
        }

        /// <summary>
        /// 获取工厂所有机组的状态
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetAllGCJZInfo(string name, int page, int rows)
        {
            int start = (page - 1) * rows;
            int end = page * rows;
            List<jzjlInfo> jzs = new List<jzjlInfo>();
            string sql = "SELECT * FROM `web`.`工厂机组运行记录` WHERE `工厂名称`='" + name + "' order by ID desc ";
            string where = "limit " + start + "," + end;
            Dictionary<string, object> d = new Dictionary<string, object>();
            //获取数据总数(注意是总数,不是一页中数据的条数)
            DataTable dTable = data.GetTable(sql);
            d.Add("total", data.GetTable(sql).Rows.Count);
            //获取page页的数据
            dTable = dTable.AsEnumerable().Skip((page - 1) * rows).Take(rows).CopyToDataTable();
            for (int i = 0; i < dTable.Rows.Count; i++)
            {
                jzjlInfo jz = new jzjlInfo();
                jz.id = long.Parse(dTable.Rows[i]["ID"].ToString());
                jz.jzname = dTable.Rows[i]["机组名称"].ToString();
                jz.jzid = dTable.Rows[i]["机组编号"].ToString();
                jz.stime = dTable.Rows[i]["时间"].ToString();
                jz.ms = dTable.Rows[i]["状态描述"].ToString();
                jz.status = int.Parse(dTable.Rows[i]["状态"].ToString());
                jz.mid = int.Parse(dTable.Rows[i]["菜单号"].ToString());
                jzs.Add(jz);
            }
            d.Add("rows", jzs);

            return JsonConvert.SerializeObject(d);
        }

        /// <summary>
        /// 获取工厂所有机组的状态
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetAllJZInfo(string name, int page, int rows)
        {
            int start = (page - 1) * rows;
            int end = page * rows;
            List<jzjlInfo> jzs = new List<jzjlInfo>();
            string sql = "SELECT * FROM `web`.`工厂机组运行记录` WHERE `机组名称`='" + name + "' order by ID desc ";
            string where = "limit " + start + "," + end;
            Dictionary<string, object> d = new Dictionary<string, object>();
            //获取数据总数(注意是总数,不是一页中数据的条数)
            DataTable dTable = data.GetTable(sql);
            d.Add("total", data.GetTable(sql).Rows.Count);
            //获取page页的数据
            dTable = dTable.AsEnumerable().Skip((page - 1) * rows).Take(rows).CopyToDataTable();
            for (int i = 0; i < dTable.Rows.Count; i++)
            {
                jzjlInfo jz = new jzjlInfo();
                jz.id = long.Parse(dTable.Rows[i]["ID"].ToString());
                jz.jzname = dTable.Rows[i]["机组名称"].ToString();
                jz.jzid = dTable.Rows[i]["机组编号"].ToString();
                jz.stime = dTable.Rows[i]["时间"].ToString();
                jz.ms = dTable.Rows[i]["状态描述"].ToString();
                jz.status = int.Parse(dTable.Rows[i]["状态"].ToString());
                jz.mid = int.Parse(dTable.Rows[i]["菜单号"].ToString());
                jzs.Add(jz);
            }
            d.Add("rows", jzs);

            return JsonConvert.SerializeObject(d);
        }
        /// <summary>
        /// 获取工厂所有机组的状态
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetAllGCJZInftime(string name, int page, int rows, string t1, string t2)
        {
            int start = (page - 1) * rows;
            int end = page * rows;
            List<jzjlInfo> jzs = new List<jzjlInfo>();
            string sql = "SELECT * FROM `web`.`工厂机组运行记录` WHERE `工厂名称`='" + name + "' and 时间>='" + t1 + "'   and 时间<='" + t2 + "'  ";
            string where = "limit " + start + "," + end;
            Dictionary<string, object> d = new Dictionary<string, object>();
            //获取数据总数(注意是总数,不是一页中数据的条数)
            DataTable dTable = data.GetTable(sql);
            d.Add("total", data.GetTable(sql).Rows.Count);
            //获取page页的数据
            dTable = dTable.AsEnumerable().Skip((page - 1) * rows).Take(rows).CopyToDataTable();
            for (int i = 0; i < dTable.Rows.Count; i++)
            {
                jzjlInfo jz = new jzjlInfo();
                jz.id = long.Parse(dTable.Rows[i]["ID"].ToString());
                jz.jzname = dTable.Rows[i]["机组名称"].ToString();
                jz.jzid = dTable.Rows[i]["机组编号"].ToString();
                jz.stime = dTable.Rows[i]["时间"].ToString();
                jz.ms = dTable.Rows[i]["状态描述"].ToString();
                jz.status = int.Parse(dTable.Rows[i]["状态"].ToString());
                jz.mid = int.Parse(dTable.Rows[i]["菜单号"].ToString());
                jzs.Add(jz);
            }
            d.Add("rows", jzs);

            return JsonConvert.SerializeObject(d);
        }


        public string GetAllJZInftime(string name, int page, int rows, string t1, string t2)
        {
            int start = (page - 1) * rows;
            int end = page * rows;
            List<jzjlInfo> jzs = new List<jzjlInfo>();
            string sql = "SELECT * FROM `web`.`工厂机组运行记录` WHERE `机组编号`='" + name + "' and 时间>='" + t1 + "'   and 时间<='" + t2 + "'  ";
            string where = "limit " + start + "," + end;
            Dictionary<string, object> d = new Dictionary<string, object>();
            //获取数据总数(注意是总数,不是一页中数据的条数)
            DataTable dTable = data.GetTable(sql);
            d.Add("total", data.GetTable(sql).Rows.Count);
            //获取page页的数据
            dTable = dTable.AsEnumerable().Skip((page - 1) * rows).Take(rows).CopyToDataTable();
            for (int i = 0; i < dTable.Rows.Count; i++)
            {
                jzjlInfo jz = new jzjlInfo();
                jz.id = long.Parse(dTable.Rows[i]["ID"].ToString());
                jz.jzname = dTable.Rows[i]["机组名称"].ToString();
                jz.jzid = dTable.Rows[i]["机组编号"].ToString();
                jz.stime = dTable.Rows[i]["时间"].ToString();
                jz.ms = dTable.Rows[i]["状态描述"].ToString();
                jz.status = int.Parse(dTable.Rows[i]["状态"].ToString());
                jz.mid = int.Parse(dTable.Rows[i]["菜单号"].ToString());
                jzs.Add(jz);
            }
            d.Add("rows", jzs);

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