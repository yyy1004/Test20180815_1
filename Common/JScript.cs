using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace JrscSoft.Common
{
    ///<summary>
    ///ģ���ţ�
    ///������ͨ����̬��Ա�����ṩ����JavaScript�����������
    ///���ߣ����ַ�
    ///�������ڣ�2006-12-29
    ///</summary>
    public class JScript
    {
        public JScript()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        /// <summary>
        /// ��Alert������ʾ��ʾ��Ϣ
        /// </summary>
        /// <param name="Message">Ҫ��ʾ����ʾ��Ϣ</param>
        /// ���ߣ����ַ�
        /// �������ڣ�2006-12-29
        public static void Alert(object Message)
        {
            string Js = @"<Script language='JavaScript'>
						alert('{0}');
					</script>";
            HttpContext.Current.Response.Write(string.Format(Js, Message.ToString()));
        }
        /// <summary>
        /// ������Է�ֹ����ҳ��ʽ��
        /// </summary>
        /// <param name="page">����ҳ</param>
        /// <param name="values">��Ϣ</param>
        /// ���ߣ� ���ַ�
        /// �������ڣ� 2010��4��25��
        public static void Alert(System.Web.UI.Page page, string values)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "", "<script language=javascript>alert('" + values + "')</script>");
        }

        /// <summary>
        /// ȫ����һ���´��ڲ��ر�Դ����
        /// </summary>
        /// <param name="page">��ǰҳ</param>
        /// <param name="url">�򿪵�url</param>
        /// ���ߣ� ���ַ�
        /// �������ڣ� 2010��4��25��
        public static void OpenNewWinCloseOldWin(System.Web.UI.Page page,string url)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "start", "var win=window.open('" + url + "','','top=0,left=0,scrollbars=no,status=0,directory=0,location=0');var h = screen.availHeight;var w = screen.availWidth;win.resizeTo(w,h);window.opener=null;window.open('','_self');window.close();", true);   
        }
        /// <summary>
        /// ȫ����һ���´���
        /// </summary>
        /// <param name="page">��ǰҳ</param>
        /// <param name="url">�򿪵�url</param>
        /// ���ߣ� ���ַ�
        /// �������ڣ� 2010��4��25��
        public static void OpenNewWin(System.Web.UI.Page page, string url)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "start", "var win=window.open('" + url + "','','top=0,left=0,scrollbars=no,status=0,directory=0,location=0');var h = screen.availHeight;var w = screen.availWidth;win.resizeTo(w,h);window.opener=null;window.open('','_self');", true);
        }
        /// <summary>
        /// ��һ���´���
        /// </summary>
        /// <param name="page">��ǰҳ</param>
        /// <param name="url">url��ַ</param>
        /// <param name="top">top����</param>
        /// <param name="left">left����</param>
        /// <param name="width">���</param>
        /// <param name="height">�߶�</param>
        /// ���ߣ� ���ַ�
        /// �������ڣ� 2010��4��25��
        public static void OpenNewWin(System.Web.UI.Page page, string url,string top,string left,string width,string height)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "start", "var win=window.open('" + url + "','','top="+ top +",left="+left+",scrollbars=no,status=0,directory=0,location=0');win.resizeTo(width,height);window.opener=null;window.open('','_self');", true);
        }
        /// <summary>
        /// ��HTMLҳ����window.onload()���¼�����Alert������ʾ��ʾ��Ϣ
        /// </summary>
        /// <param name="Message">Ҫ��ʾ����ʾ��Ϣ</param>
        /// ���ߣ����ַ�
        /// �������ڣ�2006-12-29
        public static void AlertInWinLoad(object Message)
        {
            string Js = @"<Script language='JavaScript'>function window.onload(){alert('" + Message.ToString() + "');}</script>";
            HttpContext.Current.Response.Write(Js);
        }
        /// <summary>
        /// ��HTMLҳ����window.onload()���¼�����Alert������ʾ��ʾ��Ϣ,���رմ���
        /// </summary>
        /// <param name="Message">Ҫ��ʾ����ʾ��Ϣ</param>
        /// ���ߣ����ַ�
        /// �������ڣ�2006-12-29
        public static void AlertInWinLoadAndClose(object Message)
        {
            string Js = @"<Script language='JavaScript'>function window.onload(){alert('" + Message.ToString() + "');window.close();}</script>";
            HttpContext.Current.Response.Write(Js);
        }
        /// <summary>
        /// ��HTMLҳ����window.onload()���¼�����Alert������ʾ��ʾ��Ϣ,���رմ���
        /// </summary>
        /// <param name="Message">Ҫ��ʾ����ʾ��Ϣ</param>
        /// ���ߣ����ַ�
        /// �������ڣ�2006-12-29
        public static void AlertAndClose(object Message)
        {
            string Js = @"<Script language='JavaScript'>alert('" + Message.ToString() + "');window.close();</script>";
            HttpContext.Current.Response.Write(Js);
        }
        /// <summary>
        /// ��HTMLҳ����window.onload()���¼�����Alert������ʾ��ʾ��Ϣ,����ת��ָ��ҳ��
        /// </summary>
        /// <param name="Message">Ҫ��ʾ����ʾ��Ϣ</param>
        /// <param name="Url">Ҫ��ת��ҳ��</param>
        /// ���ߣ����ַ�
        /// �������ڣ�2006-12-29
        public static void AlertInWinLoadAndClose(object Message, string Url)
        {
            string Js = @"<Script language='JavaScript'>function window.onload(){alert('" + Message.ToString() + "');window.close();window.location.replace('" + Url + "');}</script>";
            HttpContext.Current.Response.Write(Js);
        }
        /// <summary>
        /// ��ʾ��ʾ��Ϣ,���ڵ�ǰ�����ڴ�ָ����ҳ��
        /// </summary>
        /// <param name="Message">Ҫ��ʾ����Ϣ</param>
        /// <param name="URL">Ҫ�ڵ�ǰ���ڵ�ҳ���URL</param>
        /// ���ߣ����ַ�
        /// �������ڣ�2006-12-29
        public static void AlertInWinLoadAndRedirect(object Message, string URL)
        {
            string Js = @"<Script language='JavaScript'>function window.onload(){alert('" + Message.ToString() + "');window.location.replace('" + URL + "');}</Script>";
            HttpContext.Current.Response.Write(Js);

        }
        /// <summary>
        /// ��ʾ��ʾ��Ϣ,���ڵ�ǰ�����ڴ�ָ����ҳ��
        /// </summary>
        /// <param name="Message">Ҫ��ʾ����Ϣ</param>
        /// <param name="URL">Ҫ�ڵ�ǰ���ڵ�ҳ���URL</param>
        /// ���ߣ����ַ�
        /// �������ڣ�2006-12-29
        public static void AlertAndRedirect(object Message, string URL)
        {
            string Js = @"<Script language='JavaScript'>
							alert('{0}');
							window.location.replace('{1}');
						</Script>";
            HttpContext.Current.Response.Write(string.Format(Js, Message.ToString(), URL));

        }

        /// <summary>
        /// ��ʾ��ʾ��Ϣ,����ת������
        /// </summary>
        /// <param name="Message">Ҫ��ʾ����Ϣ</param>
        /// <param name="URL">Ҫ�ڵ�ǰ���ڵ�ҳ���URL</param>
        /// ���ߣ����ַ�
        /// �������ڣ�2006-12-29
        public static void AlertAndRedirectParent(object Message, string URL)
        {
            string Js = @"<Script language='JavaScript'>
							alert('{0}');
							window.parent.location.replace('{1}');
						</Script>";
            HttpContext.Current.Response.Write(string.Format(Js, Message.ToString(), URL));

        }

        /// <summary>
        /// �رյ�ǰ����
        /// </summary>
        /// ���ߣ����ַ�
        /// �������ڣ�2006-12-29
        public static void CloseParent()
        {
            string Js = @"<Script language='JavaScript'>
							window.parent.close();
						</script>";
            HttpContext.Current.Response.Write(Js);
            HttpContext.Current.Response.End();
        }


        /// <summary>
        /// �رյ�ǰ����
        /// </summary>
        /// ���ߣ����ַ�
        /// �������ڣ�2006-12-29
        public static void CloseWindow()
        {
            string Js = @"<Script language='JavaScript'>
							window.close();
						</script>";
            HttpContext.Current.Response.Write(Js);
            HttpContext.Current.Response.End();
        }
        /// <summary>
        /// ��һ���´��ڣ������´�������ʾָ����URL
        /// </summary>
        /// <param name="URL">Ҫ���´�������ʾ��URL</param>
        /// ���ߣ����ַ�
        /// �������ڣ�2006-12-29
        public static void OpenWebForm(string URL)
        {
            string Js = @"<Script language='JavaScript'>
							window.open('" + URL + @"')
						</script>";
            HttpContext.Current.Response.Write(Js);
        }
        /// <summary>
        /// ���һ��JavaScript�����
        /// </summary>
        /// <param name="strJs">Ҫ�����JavaScript����</param>
        /// ���ߣ����ַ�
        /// �������ڣ�2006-12-29
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
        /// ����ǰҳת��ָ����URL
        /// </summary>
        /// <param name="URL">Ҫת���URL</param>
        /// ���ߣ����ַ�
        /// �������ڣ�2006-12-29
        public static void LocationRedirect(string URL)
        {
            string Js = @"<Script language='JavaScript'>
						window.location.replace('{0}');
					</Script>";
            Js = string.Format(Js, URL);
            HttpContext.Current.Response.Write(Js);
        }
        /// <summary>
        /// �ڴ��С�ȷ��������ȡ������ť�ĶԻ�������ʾ��ʾ��Ϣ���������û���ѡ��
        /// </summary>
        /// <param name="message">Ҫ�ڶԻ�������ʾ����Ϣ</param>
        /// <param name="sWinCtrl">�û���ѡ��TRUE��FALSE��</param>
        /// ���ߣ����ַ�
        /// �������ڣ�2006-12-29
        public static void ReturnMsgboxResult(object message, string sWinCtrl)
        {
            string Js = @"<Script language='JavaScript'>
							sWinCtrl=false;
							sWinCtrl=if(confrim('{0}')) return false;</Script>";
            HttpContext.Current.Response.Write(string.Format(Js, message.ToString()));
        }
        /// <summary>
        /// ��ʾ��ʷҳ
        /// </summary>
        /// <param name="val">-1/1��ǰһҳ���һҳ</param>
        /// ���ߣ����ַ�
        /// �������ڣ�2006-12-29
        public static void GotoHistory(int val)
        {
            string Js = @"<Script language='JavaScript'>
							history.go(" + val + @");
						</Script>";
            HttpContext.Current.Response.Write(Js);
        }

        /// <summary>
        /// ˢ�µ�ǰ���ڵĸ�����
        /// </summary>
        /// ���ߣ����ַ�
        /// �������ڣ�2006-12-29
        public static void RefreshParent()
        {
            string Js = @"<Script language='JavaScript'>
							parent.location.reload();
						</script>";
            HttpContext.Current.Response.Write(Js);
        }
        /// <summary>
        /// ˢ�´򿪵�ǰ���ڵĴ���
        /// </summary>
        /// ���ߣ����ַ�
        /// �������ڣ�2006-12-29
        public static void RefreshOpener()
        {
            string Js = @"<Script language='JavaScript'>
							opener.location.reload();
						</Script>";
            HttpContext.Current.Response.Write(Js);
        }

        

        /// <summary>
        /// ��ָ����FRAME�ڴ�ָ����ҳ�档
        /// </summary>
        /// <param name="FrameName">Frame���������</param>
        /// <param name="URL">Ҫ�򿪵�ҳ���URL</param>
        /// ���ߣ����ַ�
        /// �������ڣ�2006-12-29
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
        /// ˢ������б�����Ŀר��
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
        /// ˢ�´򿪵�ǰ����
        /// </summary>
        /// ���ߣ����ַ�
        /// �������ڣ�2010-5-19
        public static void Refresh()
        {
            string Js = @"<Script language='JavaScript'>
							location.reload();
						</Script>";
            HttpContext.Current.Response.Write(Js);
        }
    }
}
