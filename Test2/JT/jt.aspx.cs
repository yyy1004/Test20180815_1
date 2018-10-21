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

namespace Web.JT
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        DataAccess data = new DataAccess();
      
        private static string JT;
        public int count = 1;
        public ArrayList jtname = new ArrayList();
        public ArrayList jd = new ArrayList();
        public ArrayList wd = new ArrayList();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int menuid = Convert.ToInt32(Request.QueryString["node"].ToString());
                JT = MenuHelper.GetMenuNameById(menuid);
                // JT = "集团A";
                // count = wd.Count;              
                string url = "~/pic/" + JT + ".png";
                //  this.Image1.ImageUrl = url;   
                try
                {
                    getjwd();
                    if (System.IO.File.Exists(HttpContext.Current.Request.MapPath("~/pic/") + JT + ".png"))
                    {
                        //判断图片是否存在
                        this.Imagegc.ImageUrl = url;
                        // this.FileUpload1.Visible = false;
                        // this.JTPICB.Visible = true;
                        //MessageBox.Show("文件存在");
                        // this.JTPICB.ImageUrl= url;
                        //  this.Button1.Visible = false;
                        this.picsavebtn.Style["display"] = "none";
                        //隐藏后 js读取不到div标签
                        //   this.Image2.ImageUrl = url;
                    }
                    else
                    {
                        this.Panel1.Visible = true;
                        this.FileUpload1.Visible = true;
                        this.Button1.Visible = true;
                        this.Button1.Text = "保存";
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.ToString() + "')</script>");
                }
                BindGrid();
            }
        }

        public void getjwd()
        {
            jtname.Add(JT);

            string sql = "SELECT 集团经纬度 FROM `web`.`集团表` where `集团名称`='" + JT + "'";
            string[] jwd = data.GetValue(sql).ToString().Split(',');
            jd.Add(jwd[0]);
            wd.Add(jwd[1]);

            DataTable dTable = null;
            sql = "SELECT 工厂名称,工厂经纬度 FROM `web`.`工厂表` where `集团名称`='" + JT + "'";
            dTable = data.GetTable(sql);
            foreach (DataRow dr2 in dTable.Rows)
            {
                count++;
                jtname.Add(dr2["工厂名称"].ToString());
                jwd = dr2["工厂经纬度"].ToString().Split(',');
                jd.Add(jwd[0]);
                wd.Add(jwd[1]);
            }

        }

        public void BindGrid()
        {

            DataTable dTable = null;
            DataView dView = null;
            string sql = "SELECT * FROM `web`.`机组状态一览表菜单` where `集团名称`='" + JT + "'";
            ////整理菜单
            dTable = data.GetTable(sql);

            dView = dTable.DefaultView;
            GridUser.DataSource = dView;
            GridUser.DataBind();
            ViewState["rowCount"] = dView.Table.Rows.Count;
            GridUser.DataKeyNames = new string[] { "工厂名称" };

        }

        protected void GridUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex % 2 == 1)
                {
                    //设置偶数行背景颜色
                    e.Row.BackColor = System.Drawing.Color.FromName("#ECF9FC");//System.Drawing.Color.SeaShell;

                }
                else
                    System.Drawing.Color.FromName("#FFFFFF");

                //鼠标滑过背景颜色
                e.Row.Attributes.Add("onMouseOver", "Color=this.style.backgroundColor;this.style.backgroundColor='#D6F1F8';this.style.cursor='hand'");
                e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor=Color;");
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (this.FileUpload1.HasFile) //文件存在
            {
                string fileFullname = this.FileUpload1.FileName;

                // string fileName = fileFullname.Substring(fileFullname.LastIndexOf("\\"),1);
                string type = System.IO.Path.GetExtension(this.FileUpload1.PostedFile.FileName);// fileFullname.Substring(fileFullname.LastIndexOf("."),1);
                if (type == ".png" || type == ".PNG")
                {
                    string savePath = HttpContext.Current.Request.MapPath("~/pic/");
                    string saveName = JT + ".png";
                    string pathToCheck = savePath + saveName;


                    if (System.IO.File.Exists(pathToCheck))
                    {
                        System.IO.File.Delete(pathToCheck);
                    }
                    this.FileUpload1.SaveAs(pathToCheck);
                    //保存上传文件
                    string url = "~/pic/" + JT + ".png";
                    this.Imagegc.ImageUrl = url;
                    //this.FileUpload1.Visible = false;
                    //this.Button1.Visible = false;

                }
                else
                    Response.Write("<script>alert('文件类型不对，必须是png格式')</script>");
            }
            else
            {
                Response.Write("<script>alert('上传文件不存在！')</script>");
            }

        }

    
        //protected void ShowPicBtn(object sender, EventArgs e)
        //{
        //    this.FileUpload1.Visible = true;
        //    this.Button1.Visible = true;
        //}
    }
}