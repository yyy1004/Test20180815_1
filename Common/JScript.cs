using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace JrscSoft.Common
{
    ///<summary>
    ///模块编号：
    ///描述：通过静态成员函数提供常用JavaScript代码输出功能
    ///作者：赵现发
    ///创建日期：2006-12-29
    ///</summary>
    public class JScript
    {
        public JScript()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 以Alert窗体显示提示信息
        /// </summary>
        /// <param name="Message">要显示的提示信息</param>
        /// 作者：赵现发
        /// 创建日期：2006-12-29
        public static void Alert(object Message)
        {
            string Js = @"<Script language='JavaScript'>
						alert('{0}');
					</script>";
            HttpContext.Current.Response.Write(string.Format(Js, Message.ToString()));
        }
        /// <summary>
        /// 这个可以防止调用页样式乱
        /// </summary>
        /// <param name="page">调用页</param>
        /// <param name="values">信息</param>
        /// 作者： 赵现发
        /// 创建日期： 2010年4月25日
        public static void Alert(System.Web.UI.Page page, string values)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "", "<script language=javascript>alert('" + values + "')</script>");
        }

        /// <summary>
        /// 全屏打开一个新窗口并关闭源窗口
        /// </summary>
        /// <param name="page">当前页</param>
        /// <param name="url">打开的url</param>
        /// 作者： 赵现发
        /// 创建日期： 2010年4月25日
        public static void OpenNewWinCloseOldWin(System.Web.UI.Page page,string url)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "start", "var win=window.open('" + url + "','','top=0,left=0,scrollbars=no,status=0,directory=0,location=0');var h = screen.availHeight;var w = screen.availWidth;win.resizeTo(w,h);window.opener=null;window.open('','_self');window.close();", true);   
        }
        /// <summary>
        /// 全屏打开一个新窗口
        /// </summary>
        /// <param name="page">当前页</param>
        /// <param name="url">打开的url</param>
        /// 作者： 赵现发
        /// 创建日期： 2010年4月25日
        public static void OpenNewWin(System.Web.UI.Page page, string url)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "start", "var win=window.open('" + url + "','','top=0,left=0,scrollbars=no,status=0,directory=0,location=0');var h = screen.availHeight;var w = screen.availWidth;win.resizeTo(w,h);window.opener=null;window.open('','_self');", true);
        }
        /// <summary>
        /// 打开一个新窗口
        /// </summary>
        /// <param name="page">当前页</param>
        /// <param name="url">url地址</param>
        /// <param name="top">top距离</param>
        /// <param name="left">left距离</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// 作者： 赵现发
        /// 创建日期： 2010年4月25日
        public static void OpenNewWin(System.Web.UI.Page page, string url,string top,string left,string width,string height)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "start", "var win=window.open('" + url + "','','top="+ top +",left="+left+",scrollbars=no,status=0,directory=0,location=0');win.resizeTo(width,height);window.opener=null;window.open('','_self');", true);
        }
        /// <summary>
        /// 在HTML页而后“window.onload()”事件中以Alert窗体显示提示信息
        /// </summary>
        /// <param name="Message">要显示的提示信息</param>
        /// 作者：赵现发
        /// 创建日期：2006-12-29
        public static void AlertInWinLoad(object Message)
        {
            string Js = @"<Script language='JavaScript'>function window.onload(){alert('" + Message.ToString() + "');}</script>";
            HttpContext.Current.Response.Write(Js);
        }
        /// <summary>
        /// 在HTML页而后“window.onload()”事件中以Alert窗体显示提示信息,并关闭窗体
        /// </summary>
        /// <param name="Message">要显示的提示信息</param>
        /// 作者：赵现发
        /// 创建日期：2006-12-29
        public static void AlertInWinLoadAndClose(object Message)
        {
            string Js = @"<Script language='JavaScript'>function window.onload(){alert('" + Message.ToString() + "');window.close();}</script>";
            HttpContext.Current.Response.Write(Js);
        }
        /// <summary>
        /// 在HTML页而后“window.onload()”事件中以Alert窗体显示提示信息,并关闭窗体
        /// </summary>
        /// <param name="Message">要显示的提示信息</param>
        /// 作者：赵现发
        /// 创建日期：2006-12-29
        public static void AlertAndClose(object Message)
        {
            string Js = @"<Script language='JavaScript'>alert('" + Message.ToString() + "');window.close();</script>";
            HttpContext.Current.Response.Write(Js);
        }
        /// <summary>
        /// 在HTML页而后“window.onload()”事件中以Alert窗体显示提示信息,并跳转到指定页面
        /// </summary>
        /// <param name="Message">要显示的提示信息</param>
        /// <param name="Url">要跳转的页面</param>
        /// 作者：赵现发
        /// 创建日期：2006-12-29
        public static void AlertInWinLoadAndClose(object Message, string Url)
        {
            string Js = @"<Script language='JavaScript'>function window.onload(){alert('" + Message.ToString() + "');window.close();window.location.replace('" + Url + "');}</script>";
            HttpContext.Current.Response.Write(Js);
        }
        /// <summary>
        /// 显示提示信息,并在当前窗口内打开指定的页面
        /// </summary>
        /// <param name="Message">要提示的信息</param>
        /// <param name="URL">要在当前窗口的页面的URL</param>
        /// 作者：赵现发
        /// 创建日期：2006-12-29
        public static void AlertInWinLoadAndRedirect(object Message, string URL)
        {
            string Js = @"<Script language='JavaScript'>function window.onload(){alert('" + Message.ToString() + "');window.location.replace('" + URL + "');}</Script>";
            HttpContext.Current.Response.Write(Js);

        }
        /// <summary>
        /// 显示提示信息,并在当前窗口内打开指定的页面
        /// </summary>
        /// <param name="Message">要提示的信息</param>
        /// <param name="URL">要在当前窗口的页面的URL</param>
        /// 作者：赵现发
        /// 创建日期：2006-12-29
        public static void AlertAndRedirect(object Message, string URL)
        {
            string Js = @"<Script language='JavaScript'>
							alert('{0}');
							window.location.replace('{1}');
						</Script>";
            HttpContext.Current.Response.Write(string.Format(Js, Message.ToString(), URL));

        }

        /// <summary>
        /// 显示提示信息,并跳转父窗口
        /// </summary>
        /// <param name="Message">要提示的信息</param>
        /// <param name="URL">要在当前窗口的页面的URL</param>
        /// 作者：赵现发
        /// 创建日期：2006-12-29
        public static void AlertAndRedirectParent(object Message, string URL)
        {
            string Js = @"<Script language='JavaScript'>
							alert('{0}');
							window.parent.location.replace('{1}');
						</Script>";
            HttpContext.Current.Response.Write(string.Format(Js, Message.ToString(), URL));

        }

        /// <summary>
        /// 关闭当前窗口
        /// </summary>
        /// 作者：赵现发
        /// 创建日期：2006-12-29
        public static void CloseParent()
        {
            string Js = @"<Script language='JavaScript'>
							window.parent.close();
						</script>";
            HttpContext.Current.Response.Write(Js);
            HttpContext.Current.Response.End();
        }


        /// <summary>
        /// 关闭当前窗口
        /// </summary>
        /// 作者：赵现发
        /// 创建日期：2006-12-29
        public static void CloseWindow()
        {
            string Js = @"<Script language='JavaScript'>
							window.close();
						</script>";
            HttpContext.Current.Response.Write(Js);
            HttpContext.Current.Response.End();
        }
        /// <summary>
        /// 打开一个新窗口，并在新窗口中显示指定的URL
        /// </summary>
        /// <param name="URL">要在新窗口中显示的URL</param>
        /// 作者：赵现发
        /// 创建日期：2006-12-29
        public static void OpenWebForm(string URL)
        {
            string Js = @"<Script language='JavaScript'>
							window.open('" + URL + @"')
						</script>";
            HttpContext.Current.Response.Write(Js);
        }
        /// <summary>
        /// 输出一个JavaScript代码块
        /// </summary>
        /// <param name="strJs">要输出的JavaScript代码</param>
        /// 作者：赵现发
        /// 创建日期：2006-12-29
        public static void OutputJsInWinLoad(string strJs)
        {
            string Js = @"<Script language='JavaScript'>
							function window.onload()
							{
								" + strJs + @"
							}
						</script>";
            HttpContext.Current.Response.Write(Js);
        }

        /// <summary>
        /// 将当前页转向指定的URL
        /// </summary>
        /// <param name="URL">要转向的URL</param>
        /// 作者：赵现发
        /// 创建日期：2006-12-29
        public static void LocationRedirect(string URL)
        {
            string Js = @"<Script language='JavaScript'>
						window.location.replace('{0}');
					</Script>";
            Js = string.Format(Js, URL);
            HttpContext.Current.Response.Write(Js);
        }
        /// <summary>
        /// 在带有“确定”、“取消”按钮的对话框中显示提示信息，并返回用户的选择。
        /// </summary>
        /// <param name="message">要在对话框中显示的信息</param>
        /// <param name="sWinCtrl">用户的选择（TRUE或FALSE）</param>
        /// 作者：赵现发
        /// 创建日期：2006-12-29
        public static void ReturnMsgboxResult(object message, string sWinCtrl)
        {
            string Js = @"<Script language='JavaScript'>
							sWinCtrl=false;
							sWinCtrl=if(confrim('{0}')) return false;</Script>";
            HttpContext.Current.Response.Write(string.Format(Js, message.ToString()));
        }
        /// <summary>
        /// 显示历史页
        /// </summary>
        /// <param name="val">-1/1，前一页或后一页</param>
        /// 作者：赵现发
        /// 创建日期：2006-12-29
        public static void GotoHistory(int val)
        {
            string Js = @"<Script language='JavaScript'>
							history.go(" + val + @");
						</Script>";
            HttpContext.Current.Response.Write(Js);
        }

        /// <summary>
        /// 刷新当前窗口的父窗口
        /// </summary>
        /// 作者：赵现发
        /// 创建日期：2006-12-29
        public static void RefreshParent()
        {
            string Js = @"<Script language='JavaScript'>
							parent.location.reload();
						</script>";
            HttpContext.Current.Response.Write(Js);
        }
        /// <summary>
        /// 刷新打开当前窗口的窗口
        /// </summary>
        /// 作者：赵现发
        /// 创建日期：2006-12-29
        public static void RefreshOpener()
        {
            string Js = @"<Script language='JavaScript'>
							opener.location.reload();
						</Script>";
            HttpContext.Current.Response.Write(Js);
        }

        

        /// <summary>
        /// 在指定的FRAME内打开指定的页面。
        /// </summary>
        /// <param name="FrameName">Frame对象的名称</param>
        /// <param name="URL">要打开的页面的URL</param>
        /// 作者：赵现发
        /// 创建日期：2006-12-29
        public static void ShowNewUrlInFrame(string FrameName, string URL)
        {
            string Js = @"<Script language='JavaScript'>
							@FN.location.replace('{0}');
						</Script>";
            Js.Replace("@FN", FrameName);
            string.Format(Js, URL);
            HttpContext.Current.Response.Write(Js);
        }
        /// <summary>
        /// 刷新左侧列表，本项目专用
        /// </summary>
        public static void RefreshLeftFrame()
        {
            //string Js = @"<Script language='JavaScript'>window.parent.parent.frames['left'].document.forms[0].submit();</script>";
            //string Js = @"<Script language='JavaScript'>document.frames('left').location.reload();</script>";
                        //window.parent.parent.frames['left'].document.forms[0].__EVENTTARGET.value = 'LbSetting';
                    //    window.parent.parent.frames['ActorList'].document.forms[0].__EVENTARGUMENT.value = '';
                    //    window.parent.parent.frames['ActorList'].document.forms[0].submit();
                    //</script>";
           // HttpContext.Current.Response.Write(Js);
            HttpContext.Current.Response.Write("<Script language='JavaScript'>window.parent.left.location.reload();</script>");
        }
        public static void RefreshImportOpener()
        {
            string Js = @"<Script language='JavaScript'>
						window.parent.opener.document.forms[0].__EVENTTARGET.value = 'LbSetting';
						window.parent.opener.document.forms[0].__EVENTARGUMENT.value = '';
						window.parent.opener.document.forms[0].submit();
					</script>";
            HttpContext.Current.Response.Write(Js);
        }

        /// <summary>
        /// 刷新打开当前窗口
        /// </summary>
        /// 作者：赵现发
        /// 创建日期：2010-5-19
        public static void Refresh()
        {
            string Js = @"<Script language='JavaScript'>
							location.reload();
						</Script>";
            HttpContext.Current.Response.Write(Js);
        }
    }
}
