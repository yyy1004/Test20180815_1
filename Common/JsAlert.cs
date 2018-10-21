/**
*
* Author: raloon
* E-mail: raloon@tom.com
* 
* 版权声明：
* 此代码可以自由使用，包括商业应用，但必须保留此段文字说明。
* 由于使用此程序而导致的任何系统缺陷及损失，作者一概不负责任。
* 如果你发现了代码中的BUG，或者修改了此代码，希望你能通知作者。
*
* 帮助器类：在页面上弹出提示对话框。
*
*
* ----------------------------------------------------------------------------
* Version 1.0 : 2004-04-20 to 2004-04-21
* 
* 
**/
using System;
using System.Web;

namespace JrscSoft.Common
{

	/// <summary>
	/// 帮助器类：在页面上弹出提示对话框。
	/// </summary>
	public sealed class JsAlert
	{
		private JsAlert() {}
		
    /// <summary>
    /// 显示提示信息，然后执行某个操作。
    /// </summary>
    /// <param name="message">要提示的信息。</param>
    /// <param name="afterOption">提示后，要进行的操作。</param>
    /// <param name="end">是否调用Response.End停止后续输出。</param>
    public static void AlertThenDo(string message, string afterOption, bool end)
    {
      string alertJs = @"
<script language='javascript'>
try{
";

      if(message.Trim().Length > 0)
        alertJs += @"alert('"+message.Replace("'", "\'").Replace("\n", "\\n")+@"');";
      
      switch(afterOption.ToLower())
      {
        case "":
          end = false;      // 仅发出提示信息时，不应该停止输出。
          break;
        case "close":
          alertJs += "\nwindow.close();";
          end = true;       // 发出提示信息时并关闭时，应该停止输出。
          break;
        case "back":
          alertJs += "\nhistory.back();";
          end = true;       // 发出提示信息时并退回时，应该停止输出。
          break;
        case "opener_reload":
          alertJs += "\nopener.location.reload();\n";
          alertJs += "window.close();";
          break;
        default:
          alertJs += "\n"+afterOption;
          break;
      }
      
      alertJs += @"
}
catch(e){
// Do Nothing
}
</script>";
    
      HttpContext.Current.Response.Write(alertJs);
      if(end) HttpContext.Current.Response.End();
    }
    
    /// <summary>
    /// 仅显示提示信息。
    /// </summary>
    /// <param name="message">要提示的信息。</param>
    public static void Alert(string message)
    {
      AlertThenDo(message, "", false);
    }
    
    /// <summary>
    /// 显示提示信息，并可以关闭页面。
    /// </summary>
    /// <param name="message">要提示的信息。</param>
    /// <param name="isClosePage">是否关闭页面。</param>
    public static void Alert(string message, bool isClosePage)
    {
      if(isClosePage)
        AlertThenDo(message, "close", true);
      else
        Alert(message);
    }
    
    /// <summary>
    /// 显示提示信息，并跳转到指定URL，可以控制帧跳转。
    /// </summary>
    /// <param name="message">要提示的信息。</param>
    /// <param name="url">跳转到指定URL。</param>
    /// <param name="targetName">帧名称。</param>
    public static void Alert(string message, string url, string targetName)
    {
      if(url.Length == 0)
        Alert(message);
      else
      {
        targetName = targetName.Trim().ToLower();
        if(targetName.Length == 0) targetName = "window";
        string gotoUrl = "try{" + targetName + ".navigate('" + url + "');}\n"
          + "catch(e){window.navigate('" + url + "');}\n";
        AlertThenDo(message, gotoUrl, true);
      }
    }
    
    /// <summary>
    /// 显示提示信息，并将本页跳转到指定URL。
    /// </summary>
    /// <param name="message">要提示的信息。</param>
    /// <param name="url">跳转到指定URL。</param>
    public static void Alert(string message, string url)
    {
      Alert(message, url, "");
    }

    public static void AlertBack(string message)
    {
      AlertThenDo(message, "back", true);
    }
    
	}
}
