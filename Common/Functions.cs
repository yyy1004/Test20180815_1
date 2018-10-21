using System;
using System.Configuration;
using System.Collections.Specialized;
using System.Web;
using System.Web.SessionState;
using System.IO;
using System.Web.Mail;

//using Model;

namespace JrscSoft.Common
{
	/// <summary>
	/// 常用函数，与特定的应用程序无关，方便代码重用。
	/// </summary>
	/// <remarks>gwd, 2004-04-12</remarks>
	public sealed class Functions
	{
	
		/// <summary>
		/// Since this class provides only static methods, make the default constructor private to prevent 
		/// instances from being created with "new Functions()"
		/// </summary>
		private Functions() {}

		public static void Debug(string msg)
		{
			System.Diagnostics.Debug.WriteLine(msg);
		}
    
		#region property
    
		public static string ApplicationDirectory
		{
			get 
			{
				return Functions.GetValidDirectoryName(HttpContext.Current.Request.ApplicationPath);
			}
		}
    
		#endregion propery

		#region 数据有效性检查和类型转换

		/// <summary>
		/// 检查一个字符串看其是否为null或者string.Empty.
		/// </summary>
		/// <remarks>gwd, 2004-04-12</remarks>
		/// <param name="str">要检查的字符串</param>
		/// <returns>
		/// true -- null or string.Empty
		/// false -- str.length > 0
		/// </returns>
		public static bool IsValidString(string str)
		{
			return !(str == null || str == string.Empty);
		}

		/// <summary>
		/// 检查字符串是否为空，返回有效值。
		/// </summary>
		/// <param name="toCheck">待检查字符串</param>
		/// <returns>返回字符串</returns>
		public static string ParseStr(object toCheck, string defaultValue)
		{
			if(toCheck == null)
				return defaultValue;
			else
				return toCheck.ToString();
		}
		public static string ParseStr(object toCheck)
		{
			return Functions.ParseStr(toCheck, "");
		}
    
		/// <summary>
		/// 将string转化为int，发生错误时，返回缺省值。
		/// </summary>
		/// <param name="toParse">要转化为数字的字符串</param>
		/// <param name="defaultValue">缺省数值</param>
		/// <returns>数字</returns>
		public static int ParseInt(string toParse, int defaultValue)
		{
			try
			{
				return int.Parse(toParse);
			}
			catch
			{
				return defaultValue;
			}
		}
		public static int ParseInt(string toParse)
		{
			return Functions.ParseInt(toParse, 0);
		}

		/// <summary>
		/// 将string转化为float，发生错误时，返回缺省值。
		/// </summary>
		/// <param name="toParse">要转化为数字的字符串</param>
		/// <param name="defaultValue">缺省数值</param>
		/// <returns>float</returns>
		public static float ParseFlt(string toParse, float defaultValue)
		{
			try
			{
				return float.Parse(toParse);
			}
			catch
			{
				return defaultValue;
			}
		}
		public static float ParseFlt(string toParse)
		{
			return Functions.ParseFlt(toParse, 0F);
		}

		/// <summary>
		/// 将string转化为double，发生错误时，返回缺省值。
		/// </summary>
		/// <param name="toParse">要转化为数字的字符串</param>
		/// <param name="defaultValue">缺省数值</param>
		/// <returns>double</returns>
		public static double ParseDbl(string toParse, double defaultValue)
		{
			try
			{
				return double.Parse(toParse);
			}
			catch
			{
				return defaultValue;
			}
		}
		public static double ParseDbl(string toParse)
		{
			return Functions.ParseDbl(toParse, 0D);
		}

		/// <summary>
		/// 将string转换为DateTime，发生错误时，返回缺省值。
		/// </summary>
		/// <param name="toParse"></param>
		/// <param name="defaultValue"></param>
		/// <returns></returns>
		public static DateTime ParseDt(string toParse, DateTime defaultValue)
		{
			try
			{
				return DateTime.Parse(toParse);
			}
			catch
			{
				return defaultValue;
			}
		}
		/// <summary>
		/// 将string转换为DateTime，发生错误时，返回DateTime("1900-1-1")。
		/// </summary>
		/// <param name="toParse"></param>
		/// <returns></returns>
		public static DateTime ParseDt(string toParse)
		{
			return Functions.ParseDt(toParse, DateTime.Parse("1900-1-1"));
		}

		/// <summary>
		/// 将bool.TrueString和bool.FalseString转换为bool，或者执行int.Parse(toParse)>0?true:false，发生错误时，返回defaultValue。
		/// </summary>
		/// <param name="toParse"></param>
		/// <param name="defaultValue"></param>
		/// <returns></returns>
		public static bool ParseBln(string toParse, bool defaultValue)
		{
			if(toParse.Trim().ToLower()==bool.TrueString.ToLower() || toParse.Trim().ToLower()==bool.FalseString.ToLower())
				return bool.Parse(toParse);
			else
			{
				try
				{
					return (int.Parse(toParse)>0?true:false);
				}
				catch
				{
					return defaultValue;
				}
			}
		}
		public static bool ParseBln(string toParse)
		{
			return Functions.ParseBln(toParse, true);
		}

		#endregion

		/// <summary>
		/// 得到有效的路径名
		/// </summary>
		/// <param name="directoryName">要检查的路径名</param>
		/// <returns>有效的路径名</returns>
		public static string GetValidDirectoryName(string directoryName)
		{
			if (directoryName.EndsWith("/"))
				return directoryName;
			else
				return directoryName + "/";
		}

		/// <summary>
		/// 将文本框的空值转换成零，以与数据列的值类型兼容
		/// </summary>
		/// <param name="controlText">文本框内容</param>
		/// <param name="flag">看是要转换成int类型还是decimal类型，如果任意输入的flag（如1）值是整数则转换成int</param>
		/// <returns></returns>
		public static int ConvertTextValue(string controlText,int flag)
		{
			try
			{
				if(controlText=="")
				{
					return 0;
				}
				else
				{
					return Convert.ToInt32(controlText);
				}
			}
			catch
			{
				return 0;
			}
		}

		/// <summary>
		/// 将文本框的空值转换成零，以与数据列的值类型兼容
		/// </summary>
		/// <param name="controlText">文本框内容</param>
		/// <param name="flag">看是要转换成int类型还是decimal类型，如果任意输入的flag值（如1.1）是decimal则转换成decimal</param>
		/// <returns></returns>
		public static decimal ConvertTextValue(string controlText,double flag)
		{
			try
			{
				if(controlText=="")
				{
					return 0;
				}
				else
				{
					return Convert.ToDecimal(controlText);
				}
			}
			catch
			{
				return 0;
			}
		}
    
    
		#region 读取“appSettings”配置项。
    
		/// <summary>
		/// 获取Web.config文件中“appSettings”的string值。
		/// </summary>
		/// <remarks>gwd, 2003-04-12</remarks>
		/// <param name="keyName">键名称</param>
		/// <param name="defaultValue">键的默认值</param>
		/// <returns>键值</returns>
		public static string GetAppConfigString(string keyName, string defaultValue)
		{
			NameValueCollection appSettings = ConfigurationSettings.AppSettings;
			if (appSettings != null)
			{
				string keyValue = appSettings[keyName];
				if (Functions.IsValidString(keyValue))
					return keyValue;
			}
		  
			return defaultValue;
		}

		/// <summary>
		/// 获取Web.config文件中“appSettings”的bool值。
		/// </summary>
		/// <remarks>gwd, 2003-04-12</remarks>
		/// <param name="keyName">键名称</param>
		/// <param name="defaultValue">键的默认值</param>
		/// <returns>键值</returns>		
		public static bool GetAppConfigBool(string keyName, bool defaultValue)
		{
			NameValueCollection appSettings = ConfigurationSettings.AppSettings;
			if (appSettings != null)
			{
				try
				{
					bool keyValue = bool.Parse(appSettings[keyName]);
					return keyValue;
				}
				catch {}
			}
		  
			return defaultValue;
		}
		
		/// <summary>
		/// 获取Web.config文件中“appSettings”的int值。
		/// </summary>
		/// <remarks>gwd, 2003-04-12</remarks>
		/// <param name="keyName">键名称</param>
		/// <param name="defaultValue">键的默认值</param>
		/// <returns>键值</returns>
		public static int GetAppConfigInt(string keyName, int defaultValue)
		{
			NameValueCollection appSettings = ConfigurationSettings.AppSettings;
			if (appSettings != null)
			{
				try
				{
					int keyValue = int.Parse(appSettings[keyName]);
					return keyValue;
				}
				catch {}
			}
		  
			return defaultValue;
		}
    
		#endregion 读取“appSettings”配置项。
    
		#region 读取自定义配置项。

		/// <summary>
		/// 获取Web.config文件中自定义配置的string值。
		/// </summary>
		/// <remarks>gwd, 2003-04-12</remarks>
		/// <param name="configName">配置项名称</param>
		/// <param name="keyName">键名称</param>
		/// <param name="defaultValue">键的默认值</param>
		/// <returns>键值</returns>    
		public static string GetConfigString(string configName, string keyName, string defaultValue)
		{
			NameValueCollection configSettings = ConfigurationSettings.GetConfig(configName) as NameValueCollection;
			if (configSettings != null)
			{
				string keyValue = configSettings[keyName];
				if (Functions.IsValidString(keyValue))
					return keyValue;
			}
      
			return defaultValue;
		}
    
		/// <summary>
		/// 获取Web.config文件中自定义配置的bool值。
		/// </summary>
		/// <remarks>gwd, 2003-04-12</remarks>
		/// <param name="configName">配置项名称</param>
		/// <param name="keyName">键名称</param>
		/// <param name="defaultValue">键的默认值</param>
		/// <returns>键值</returns> 
		public static bool GetConfigBool(string configName, string keyName, bool defaultValue)
		{
			NameValueCollection configSettings = ConfigurationSettings.GetConfig(configName) as NameValueCollection;
			if (configSettings != null)
			{
				try
				{
					bool keyValue = bool.Parse(configSettings[keyName]);
					return keyValue;
				}
				catch {}
			}
		  
			return defaultValue;
		}
		
		/// <summary>
		/// 获取Web.config文件中自定义配置的int值。
		/// </summary>
		/// <remarks>gwd, 2003-04-12</remarks>
		/// <param name="configName">配置项名称</param>
		/// <param name="keyName">键名称</param>
		/// <param name="defaultValue">键的默认值</param>
		/// <returns>键值</returns> 
		public static int GetConfigInt(string configName, string keyName, int defaultValue)
		{
			NameValueCollection configSettings = ConfigurationSettings.GetConfig(configName) as NameValueCollection;
			if (configSettings != null)
			{
				try
				{
					int keyValue = int.Parse(configSettings[keyName]);
					return keyValue;
				}
				catch {}
			}
		  
			return defaultValue;
		}
    
		#endregion 读取自定义配置项。

		#region 二进制数字的操作（用于权限操作）
		/// <summary>
		/// 看A中是否包含B
		/// 所谓“包含”：
		/// A中是1的位，B中对应的可以是0或1
		/// A中是0的位，B中对应的必须是0
		/// 注：用于判断权限的包含关系
		/// </summary>
		/// <param name="A">A</param>
		/// <param name="B">B</param>
		/// <returns>包含，true；不包含，false</returns>
		public static bool IsAContainB(int A,int B)
		{
			if ( A< B ) return false;
			for (int i=1;i<=A;i<<=1)
			{
				if( ((B&i)!=0) && ((A&i)==0) ) return false;
			}
			return true;
		}
		/// <summary>
		/// 将两个整数按位“或”得到新的整数
		/// 注：用于将合并权限组合角色
		/// </summary>
		/// <param name="A"></param>
		/// <param name="B"></param>
		/// <returns></returns>
		public static int GetTotalValue(int A, int B)
		{
			if( A < 0 || B < 0) return 0;
			return A|B;
		}
		/// <summary>
		/// 从A中按位将B除去
		/// 所谓“除去”：
		/// 如果A包含B的话，就从A中将B中的1全部除去
		/// </summary>
		/// <param name="A">A</param>
		/// <param name="B">B</param>
		/// <returns></returns>
		public static int SeperateBFromA(int A,int B)
		{
			if (!IsAContainB(A,B)) return A;
			return A-B;
		}
		#endregion 二进制数字的操作（用于权限操作）


		#region 时间日期

		#region 根据年和季度的编号返回对应季度的起止日期
		/// <summary>
		/// 根据年和季度的编号返回对应季度的起止日期
		/// </summary>
		/// <param name="year">年</param>
		/// <param name="id">季度</param>
		/// <param name="Date_s">起始时间</param>
		/// <param name="Date_e">结束时间</param>
		public static void GetJiduRangByID(int year,int id,out string Date_s,out string Date_e)
		{
			if (year==0)
			{
				Date_s="1900-1-1";
				Date_e="1900-1-1";
				return;
			}

			switch (id)
			{
				case 1:
				{
					Date_s=Convert.ToString(year)+"-1-1";
					Date_e=Convert.ToString(year)+"-4-1";
					break;
				}
				case 2:
				{
					Date_s=Convert.ToString(year)+"-4-1";
					Date_e=Convert.ToString(year)+"-7-1";
					break;
				}
				case 3:
				{
					Date_s=Convert.ToString(year)+"-7-1";
					Date_e=Convert.ToString(year)+"-10-1";
					break;
				}
				case 4:
				{
					Date_s=Convert.ToString(year)+"-10-1";
					Date_e=Convert.ToString(year+1)+"-1-1";
					break;					
				}
				default:
				{
					Date_s=Convert.ToString(year)+"-1-1";
					Date_e=Convert.ToString(year)+"-4-1";
					break;				
				}				
			}//switch			
			
			
		}
		
		/// <summary>
		/// 根据年和季度的编号返回对应季度的起止日期
		/// </summary>
		/// <param name="year">年</param>
		/// <param name="id">季度</param>
		/// <param name="Date_s">起始时间</param>
		/// <param name="Date_e">结束时间</param>
		public static void GetJiduRangByID(int year,int id,out DateTime Date_s,out DateTime Date_e)
		{
			string sDate_s,sDate_e;
			GetJiduRangByID(year,id,out sDate_s,out sDate_e);
			Date_s=Convert.ToDateTime(sDate_s);
			Date_e=Convert.ToDateTime(sDate_e);
		}
		#endregion 根据年和季度的编号返回对应季度的起止日期

		public static int CompareTwoDate(DateTime d1, DateTime d2)
		{
			string s1 = d1.ToString("yyyy-MM-dd HH:mm:ss");
			string s2 = d2.ToString("yyyy-MM-dd HH:mm:ss");

			if(s1 == s2) return 0;

			s1 = s1.Replace("00:00:00", "23:59:59");

			return DateTime.Parse(s1).CompareTo(DateTime.Parse(s2));
		}



		#endregion 时间日期

		#region 货币相关
		
		/// <summary>
		/// 中国货币转标准数字形式字符串
		/// </summary>
		/// <param name="s">中国货币值字符串</param>
		/// <returns></returns>
		public static string ConvertToStandIntTypedString(string s)
		{
			if(s.Equals("")) return "0";
			string[] ss = s.TrimStart('￥').Split(',');
			System.Collections.IEnumerator ie = ss.GetEnumerator();
			string res = "";
			while (ie.MoveNext())
			{
				res +=ie.Current.ToString();
			}
			return res;			
		}

		internal static string FormatRmb(double rmb, string format)
		{
			return rmb.ToString(format);
		}

		internal static string FormatRmb(float rmb, string format)
		{
			return rmb.ToString(format);
		}

		#endregion 货币相关


		#region 数据库空类型的转换
		public static int CleanDBInt(object drvalue)
		{
			return drvalue==DBNull.Value?0:(Int32)drvalue;
		}
		public static string CleanDBString(object drvalue)
		{
			return drvalue==DBNull.Value?"":(String)drvalue;
		}
		public static double CleanDBDouble(object drvalue)
		{
			return drvalue==DBNull.Value?0:(Double)drvalue;
		}
		public static DateTime CleanDBDateTime(object drvalue)
		{
			return drvalue==DBNull.Value?Convert.ToDateTime("1900-1-1"):(DateTime)drvalue;
		}
		#endregion 


		public static string GetValidSQLString(string sql)
		{
			return sql.Replace("'","''");
		}





		//产生随机密码
		public static string MakePassword( int pwdlen)
		{
			string randomchars = "abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
			string pwdchars = randomchars;
			string tmpstr = "";
			int iRandNum;
			Random rnd = new Random();
			for(int i=0;i<pwdlen;i++)
			{
				iRandNum = rnd.Next(pwdchars.Length);
				tmpstr += pwdchars[iRandNum];
			}
			return tmpstr;

		}


		#region URL参数检验 Zlk @2008-3-30
		/// <summary>
		/// 得到请求参数的字符串值
		/// </summary>
		/// <param name="keyword">key</param>
		/// <returns></returns>
		public static string  GetQueryString(string keyword)
		{
			string value = HttpContext.Current.Request.QueryString[keyword];
			if (! IsValidString(value))
				return null;

			return value;
            
		}
		/// <summary>
		/// 得到请求参数的数字类型值
		/// </summary>
		/// <param name="keyword">key</param>
		/// <returns></returns>
		public static int GetIntQueryString(string keyword)
		{
			string  value = GetQueryString(keyword);
			try
			{
				return int.Parse(value);
			}
			catch (Exception)
			{
				throw new Exception("参数没有值或者不是数值类型");
			}
		}
		/// <summary>
		/// 结束没有参数值的请求
		/// </summary>
		/// <param name="keys">参数key数组</param>
		public static void EndWithEmptyURLParms(params string[] keys)
		{
			foreach (string key in keys)
			{
				EndWithAEmptyURLParm(key);
			}
		}
		/// <summary>
		/// 结束没有参数值的请求
		/// </summary>
		/// <param name="key">参数key</param>
		private static void EndWithAEmptyURLParm(string key)
		{
			string parmValue = HttpContext.Current.Request[key];
			if(! IsValidString(parmValue))
				HttpContext.Current.Response.End();
		}

		#endregion

		#region 文本文件操作

		public static void GenerateTxtFile(string content, string filename)
		{
			FileInfo  myfile = new  FileInfo(filename);
			if(myfile.Exists)//如果存在就删除
			{
				if(myfile.Attributes.ToString().IndexOf("ReadOnly")!= -1  )
				{
					myfile.Attributes = FileAttributes.Normal;
				}
				File.Delete(filename) ;
				FileInfo mf = new  FileInfo(filename);
			}
			StreamWriter sw = new StreamWriter(filename );
			sw.WriteLine(content);  
				
			sw.Flush();
			sw.Close();				

		}

		#endregion

		#region 格式化函数
		public static string FormatRmb(double rmb)
		{
			return Functions.FormatRmb(rmb, "F");
		}

		public static string FormatRmb(float rmb)
		{
			return Functions.FormatRmb(rmb, "F");
		}

		public static string FormatDate(DateTime date)
		{
			return date.ToString("yyyy-MM-dd");
		}

		public static string FormatDate(string date)
		{
			return Convert.ToDateTime(date).ToString("yyyy-MM-dd");
		}

		public static string FormatDateTime(DateTime date)
		{
			return date.ToString("yyyy-MM-dd HH:mm");
		}

		public static string FormatDateTime(string date)
		{
			return Convert.ToDateTime(date).ToString("yyyy-MM-dd HH:mm");
		}

		public static bool CheckTwoDate(System.Web.UI.WebControls.TextBox d1, System.Web.UI.WebControls.TextBox d2, string title1, string title2, PageBase page)
		{
			DateTime date1, date2;

			date1 = DateTime.Parse("1900-1-1");
			if(d1.Text.Trim().Length > 0)
			{
				date1 = Functions.ParseDt(d1.Text);
				if(date1.Year <= 1990)
				{
					page.GetAlertMsg(string.Format("请正确填写{0}，建议的格式:{1}！", title1, FormatDate(DateTime.Now)));
					page.SetFocus("", d1.ClientID);
					return false;
				}
			}
      
			if(d2.Text.Trim().Length > 0)
			{
				date2 = Functions.ParseDt(d2.Text);
				if(date2.Year <= 1990)
				{
					page.GetAlertMsg(string.Format("请正确填写{0}，建议的格式: {1}！", title2, FormatDate(DateTime.Now)));
					page.SetFocus("", d2.ClientID);
					return false;
				}
				else if(date2.CompareTo(date1) < 0)
				{
					page.GetAlertMsg(string.Format("{0}应该在{1}之后！", title2, title1));
					page.SetFocus("", d2.ClientID);
					return false;
				}
			}

			return true;
		}
		#endregion

        /// <summary>
        /// 发送邮件函数
        /// </summary>
        /// <param name="SmtpServer">smtp服务器 比如："smtp.163.com"</param>
        /// <param name="PortNo">smtp服务器端口号： 默认 25</param>
        /// <param name="UserName">smtp服务器用户名 比如：zhaoxianfa</param>
        /// <param name="PassWord">smtp服务器用户密码： 比如： xxxxx</param>
        /// <param name="Sender">发件人邮箱地址 比如：zhaoxianfa@163.com</param>
        /// <param name="Receiver">收件人邮箱地址 比如： xxx@163.com</param>
        /// <param name="Subject">邮件标题 比如： 本月工资条 </param>
        /// <param name="Content">邮件内容 比如： 实发工资20000</param>
        /// <param name="FileList">发送文件列表 内容为文件的服务器物理绝对路径</param>
        /// <returns>成功返回true，否则返回false</returns>
        public static bool SendMail(string SmtpServer, int PortNo, string UserName, string PassWord, string Sender, string Receiver, string Subject, string Content, System.Collections.Generic.List<string> FileList)
        {
            try
            {
                System.Net.Mail.MailMessage myMail = new System.Net.Mail.MailMessage();
                myMail = new System.Net.Mail.MailMessage(Sender, Receiver, Subject, Content);
                if (FileList != null)
                {
                    for (int i = 0; i < FileList.Count; i++)
                    {
                        string file = FileList[i].ToString();
                        System.Net.Mail.Attachment myAttachment = new System.Net.Mail.Attachment(file, System.Net.Mime.MediaTypeNames.Application.Octet);
                        System.Net.Mime.ContentDisposition disposition = myAttachment.ContentDisposition;
                        disposition.CreationDate = System.IO.File.GetCreationTime(file);
                        disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
                        disposition.ReadDate = System.IO.File.GetLastAccessTime(file);
                        myMail.Attachments.Add(myAttachment);
                    }
                }
                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(SmtpServer, PortNo);
                client.Credentials = new System.Net.NetworkCredential(UserName, PassWord);

                client.Send(myMail);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

