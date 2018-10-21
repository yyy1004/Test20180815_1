using DataService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web
{
    public partial class Index1 : System.Web.UI.Page
    {
        DataAccess data = new DataAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["用户名"]==null|| Session["角色"]==null|| Session["IP"]==null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                string sql = string.Format("select * from 用户表 where 用户名='{0}'", Session["用户名"]);
                DataTable table = data.GetTable(sql);
                if (table.Rows.Count==0)
                {
                    Response.Redirect("Login.aspx");
                }
                else if (table.Rows[0]["密码"].ToString()!=Session["密码"].ToString())
                {
                    Response.Redirect("Login.aspx");
                }
                else if (Session["IP"].ToString() != this.Page.Request.UserHostAddress.ToString())
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }


        protected void btnLogoff(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Clear();
            Response.Redirect("Login.aspx");
        }
        //public void logoff(object sender, EventArgs e)
        //{
        //    Session.Abandon();
        //    Session.Clear();
        //    Response.Redirect("Login.aspx");
        //}
    }
}