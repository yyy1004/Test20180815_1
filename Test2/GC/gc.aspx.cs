
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataService;
using System.Data;
using System.Web.Security;
using System.IO;
using System.Web.SessionState;
using Common;
using Newtonsoft.Json;
using JrscSoft.Common;
using System.Collections;
using Model;

namespace Web.gc
{

    public partial class WebForm1 : System.Web.UI.Page
    {
        DataAccess data = new DataAccess();
       public static  string GCname;//菜单名称-工厂 机组 名称 //当点击菜单时 获取菜单名称。在gc.aspx.cs中给其赋值。
        public static int jzzs = 0;//机组-正常-数量
        public static int jzzc = 0;
        public static int jzbj = 0;
        public static  int jztj = 0;
        public static string node = "";//菜单号
        //public static string gcname = "";//当点击菜单时 获取菜单名称。在gc.aspx.cs中给其赋值。
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)//首次加载
            {
                //string kk = Request.QueryString["node"].ToString();
                //    if(kk.Contains("工厂"))
                //        GCname =Request.QueryString["node"].ToString();
                //    else
                //    {
                node = Request.QueryString["node"].ToString();//菜单号-string
                int menuid = Convert.ToInt32(Request.QueryString["node"].ToString());//菜单ID
                GCname = MenuHelper.GetMenuNameById(menuid);//菜单名称
                //gcname = GCname;//将菜单名称中的机组名称赋值给机组名称。以备调用。
                string sql;
                sql = "SELECT count(*) FROM `web`.`机组表` where `工厂名称`='" + GCname + "'";
                jzzs = Convert.ToInt16(data.GetValue(sql).ToString());//机组-所有-数量
                sql = "SELECT count(*) FROM `web`.`机组表` where `工厂名称`='" + GCname + "'and 状态=1";
                jzzc = Convert.ToInt16(data.GetValue(sql).ToString());//机组-正常-数量
                sql = "SELECT count(*) FROM `web`.`机组表` where `工厂名称`='" + GCname + "'and 状态=2";
                jzbj = Convert.ToInt16(data.GetValue(sql).ToString());//机组-
                sql = "SELECT count(*) FROM `web`.`机组表` where `工厂名称`='" + GCname + "'and 状态=3";
                jztj = Convert.ToInt16(data.GetValue(sql).ToString());//机组-停机-数量
                                                                      //    }
                                                                      //GCname = "工厂A1";
                                                                      // string sql = "SELECT 集团图片 FROM `web`.`集团表` where `集团名称`='" + JT + "'";
                                                                      //string url = data.GetValue(sql).ToString();

                //string url = "~/pic/" + GCname + ".png";

                //try
                //{
                //    if (System.IO.File.Exists(Server.MapPath("~/pic/") + GCname + ".png"))
                //    {
                //        this.Imagegc.ImageUrl = url;
                //        this.FileUpload1.Visible = false;
                //        // this.JTPICB.Visible = true;
                //        //MessageBox.Show("文件存在");
                //        // this.JTPICB.ImageUrl= url;

                //        this.Button1.Visible = false;

                //        //   this.Image2.ImageUrl = url;
                //    }
                //    else
                //    {

                //        this.Panel1.Visible = true;
                //        this.FileUpload1.Visible = true;
                //        this.Button1.Visible = true;
                //        this.Button1.Text = "上传";

                //    }
                //}
                //catch (Exception ex)
                //{
                //    Response.Write("<script>alert('" + ex.ToString() + "')</script>");
                //}
                ////      int i = 1;//测试用

            }
        }





        protected void Button1_Click(object sender, EventArgs e)
        {
            //if (this.FileUpload1.HasFile) //文件存在
            //{
            //    string fileFullname = this.FileUpload1.FileName;

            //    string fileName = fileFullname.Substring(fileFullname.LastIndexOf("\\"), 1);
            //    string type = System.IO.Path.GetExtension(this.FileUpload1.PostedFile.FileName);// fileFullname.Substring(fileFullname.LastIndexOf("."),1);
            //    if (type == ".png" || type == ".PNG")
            //    {
            //        string savePath = HttpContext.Current.Request.MapPath("~/pic/");
            //        string savePath = Server.MapPath("~/pic/");
            //        string saveName = GCname + ".png";
            //        string pathToCheck = savePath + saveName;


            //        if (System.IO.File.Exists(pathToCheck))
            //        {
            //            System.IO.File.Delete(pathToCheck);
            //        }
            //        this.FileUpload1.SaveAs(pathToCheck);//如果存在同名文件按，则先删除，再存储。
            //        string url = "~/pic/" + GCname + ".png";
            //        this.Imagegc.ImageUrl = url;
            //        this.FileUpload1.Visible = false;
            //        this.Button1.Visible = false;
            //        Response.Write("<script>window.reload();</script>");
            //        Response.Redirect("gc.aspx?node=" + node + "");
            //        Response.Write("<script language=javascript>window.parent.frameLeft.location.href='main.html'</script>");
            //        url = node.attributes.url + "?node=" + node.id;
            //        Response.Write("<script language=javascript>document.getElementById('myiframe').src='/GC/gc.aspx?node=" + node + "'</script>");


            //    }
            //    else
            //        Response.Write("<script>alert('文件类型不对，必须是png格式')</script>");
            //}
            //else
            //{
            //    Response.Write("<script>alert('上传文件不存在！')</script>");
            //}
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            
        }//没有用过。

        protected void Button1_Click1(object sender, EventArgs e)
        {
            //Response.Write("<script>window.showModelessDialog(getPicture.aspx?node=82'')</script>");
            Response.Redirect("~/getPicture.aspx?node=82", true);

        }
    }
}