using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataService;
using System.Data;
using System.Web.Security;

namespace Web
{
    public partial class Login : System.Web.UI.Page
    {
        private string UserName;
        private string UserPwd;
        DataAccess data = new DataAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnLogin_Click(object sender, ImageClickEventArgs e)
        {
            UserName = txtUsername.Value.ToString();
            UserPwd = txtPwd.Value.ToString();
            string sql = string.Format("select * from 用户表 where 用户名='{0}'", UserName);
            DataTable table = data.GetTable(sql);

            if (table.Rows.Count == 0)
            {
                lblMsg.Text = "当前用户不存在";
            }
            else if(table.Rows[0]["密码"].ToString() == UserPwd)
            {
                //除了Session对象判断.和Cookies来判断还能用到
                //FormsAuthentication.RedirectFromLoginPage 登录 
                //首先我们在网站的根目录下的web.config配置
                //<authentication mode="Forms"> 
                //<forms name=".ASPXUSERDEMO" loginUrl="Login.aspx" protection="All" timeout="30"/>
                //</authentication>
                //name=".ASPXUSERDEMO"是默认的可写可不写
               // FormsAuthentication.RedirectFromLoginPage(UserName, false);

                Session["用户名"] = UserName;
                Session["IP"] = this.Page.Request.UserHostAddress.ToString();
                Session["角色"] = table.Rows[0]["角色名称"].ToString();
                Session["密码"] = UserPwd;
                Session["id"] = table.Rows[0]["用户编号"].ToString();

                Response.Redirect("Index.aspx");
            }
            else
            {
                lblMsg.Text = "密码错误";
            }
        }
    }
}