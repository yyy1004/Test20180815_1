using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Web
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            // 在应用程序启动时运行的代码

        }

        void Application_End(object sender, EventArgs e)
        {
            //  在应用程序关闭时运行的代码

        }

        void Application_Error(object sender, EventArgs e)
        {
            //// 在出现未处理的错误时运行的代码
            //Exception lastError = Server.GetLastError();

            ////是否产生了错误
            //if (lastError != null)
            //{
            //    //发生的是否为Http请求错误
            //    if (lastError is HttpException)
            //    {
            //        //是否是404错误
            //        if (((HttpException)lastError).ErrorCode == 404)
            //        {
            //            Response.StatusCode = 404;
            //            Server.ClearError();
            //            return;
            //        }
            //    }
            //    //让用户重新登陆,并清除错误
            //    Response.Redirect("/Login.aspx");
            //    Server.ClearError();
            //}
        }

        void Session_Start(object sender, EventArgs e)
        {
            // 在新会话启动时运行的代码

        }

        void Session_End(object sender, EventArgs e)
        {
            // 在会话结束时运行的代码。 
            // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
            // InProc 时，才会引发 Session_End 事件。如果会话模式设置为 StateServer 
            // 或 SQLServer，则不会引发该事件。

        }

    }
}
