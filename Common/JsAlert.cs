/**
*
* Author: raloon
* E-mail: raloon@tom.com
* 
* ��Ȩ������
* �˴����������ʹ�ã�������ҵӦ�ã������뱣���˶�����˵����
* ����ʹ�ô˳�������µ��κ�ϵͳȱ�ݼ���ʧ������һ�Ų������Ρ�
* ����㷢���˴����е�BUG�������޸��˴˴��룬ϣ������֪ͨ���ߡ�
*
* �������ࣺ��ҳ���ϵ�����ʾ�Ի���
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
	/// �������ࣺ��ҳ���ϵ�����ʾ�Ի���
	/// </summary>
	public sealed class JsAlert
	{
		private JsAlert() {}
		
    /// <summary>
    /// ��ʾ��ʾ��Ϣ��Ȼ��ִ��ĳ��������
    /// </summary>
    /// <param name="message">Ҫ��ʾ����Ϣ��</param>
    /// <param name="afterOption">��ʾ��Ҫ���еĲ�����</param>
    /// <param name="end">�Ƿ����Response.Endֹͣ���������</param>
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
          end = false;      // ��������ʾ��Ϣʱ����Ӧ��ֹͣ�����
          break;
        case "close":
          alertJs += "\nwindow.close();";
          end = true;       // ������ʾ��Ϣʱ���ر�ʱ��Ӧ��ֹͣ�����
          break;
        case "back":
          alertJs += "\nhistory.back();";
          end = true;       // ������ʾ��Ϣʱ���˻�ʱ��Ӧ��ֹͣ�����
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
    /// ����ʾ��ʾ��Ϣ��
    /// </summary>
    /// <param name="message">Ҫ��ʾ����Ϣ��</param>
    public static void Alert(string message)
    {
      AlertThenDo(message, "", false);
    }
    
    /// <summary>
    /// ��ʾ��ʾ��Ϣ�������Թر�ҳ�档
    /// </summary>
    /// <param name="message">Ҫ��ʾ����Ϣ��</param>
    /// <param name="isClosePage">�Ƿ�ر�ҳ�档</param>
    public static void Alert(string message, bool isClosePage)
    {
      if(isClosePage)
        AlertThenDo(message, "close", true);
      else
        Alert(message);
    }
    
    /// <summary>
    /// ��ʾ��ʾ��Ϣ������ת��ָ��URL�����Կ���֡��ת��
    /// </summary>
    /// <param name="message">Ҫ��ʾ����Ϣ��</param>
    /// <param name="url">��ת��ָ��URL��</param>
    /// <param name="targetName">֡���ơ�</param>
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
    /// ��ʾ��ʾ��Ϣ��������ҳ��ת��ָ��URL��
    /// </summary>
    /// <param name="message">Ҫ��ʾ����Ϣ��</param>
    /// <param name="url">��ת��ָ��URL��</param>
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
