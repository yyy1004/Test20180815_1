using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.Security;
using System.Collections;



namespace Web
{
    public class CheckLoginStatus : IHttpModule, IRequiresSessionState
    {
        public static string node;
        private static bool flag = true;

        public void Dispose()
        {

        }

        public void Init(HttpApplication context)
        {
            context.AcquireRequestState += new EventHandler(context_AcquireRequestState);
        }

        void context_AcquireRequestState(object sender, EventArgs e)
        {
            //注意，这里必须进行强制类型转换，不能使用as转换
            HttpApplication application = (HttpApplication)sender;

            //获取请求Url的node参数
            string requestUrl = application.Request.Url.ToString();

            //过滤请求
            if (requestUrl.Contains("aspx"))
            {
                //// 用户尚未登录或者Session过期
                //if (application.Context.Session["用户名"] == null || application.Context.Session["用户名"].ToString().Trim() == "")
                //{
                //    // 如果请求的页面不是登录页面，刚重定向到登录页面。
                //    if (!requestUrl.Contains("Login.aspx"))
                //    {
                //        application.Server.Transfer("Login.aspx");
                //    }
                //}
            }
           
           // 获取树形菜单的node值
            if (GetQueryString("node", requestUrl) != "" && flag)
            {
                node = GetQueryString("node", requestUrl);
                flag = false;
            }
            else
            {
                flag = true;
            }
        }

        /// <summary>
        /// 获取url字符串参数，返回参数值字符串
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="url">url字符串</param>
        /// <returns></returns>
        public string GetQueryString(string name, string url)
        {
            System.Text.RegularExpressions.Regex re = new System.Text.RegularExpressions.Regex(@"(^|&)?(\w+)=([^&]+)(&|$)?", System.Text.RegularExpressions.RegexOptions.Compiled);
            System.Text.RegularExpressions.MatchCollection mc = re.Matches(url);
            foreach (System.Text.RegularExpressions.Match m in mc)
            {
                if (m.Result("$2").Equals(name))
                {
                    return m.Result("$3");
                }
            }
            return "";
        }
    }
}